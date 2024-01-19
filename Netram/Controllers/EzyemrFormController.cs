using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Netram.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;
using System.Net.Mail;
using static System.Net.WebRequestMethods;






namespace Netram.Controllers
{
    
    public class EzyemrFormController : ControllerBase
    {
		private  IWebHostEnvironment _hostingEnvironment;
	

		public EzyemrFormController(IWebHostEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
			
		}



		// Generate OTP and send OTP ******************************************************************
		public ActionResult  Index( string Number)
        {
           
            int otp = new Random().Next(100000, 1000000);

          
            // Set session
            HttpContext.Session.SetString("otp", otp.ToString());
           
            

			string strUrl="http://bulksms.webmediaindia.com/sendsms?uname=habitm1&pwd=habitm1&senderid=WMEDIA&to=" +Number+"&msg=Dear+Sir%2FMam%2C+Your+OTP+is"+otp+"Send+by+WEBMEDIA&route=T&peid=1701159196421355379&tempid=1707161527969328476";
			

			WebRequest request = HttpWebRequest.Create(strUrl);
            request.Method = "POST";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();

            response.Close();
            s.Close();
            readStream.Close();

         

            return Content(Number);
        }


      //verify OTP***********************************************************

        public ActionResult VerifyOtp(String userOtp) {

            // Retrieve session value as string
            string sessionOtpString = HttpContext.Session.GetString("otp");

            if (userOtp?.ToString() == sessionOtpString?.ToString() && userOtp?.ToString() != null)
            {
                var data = new { number = 1};
                return new JsonResult(data);
            } 
            else
            {
                var data = new { number = 0 };
                return new JsonResult(data);
            }

        }

        //Registration  form Data store ***************************************************************************

        public async Task<IActionResult> RegistrationStore (EazymrEntity eazymr)
        {
			string prefix = "Eazymr";
			Random random = new Random();
			string uniqueId = prefix + random.Next(100000000, 1000000000).ToString();

			HttpContext.Session.SetString("uniqueId", uniqueId.ToString());

			try
            {
				
                if (ModelState.IsValid)
                {
					eazymr.JoinTable = HttpContext.Session.GetString("uniqueId");
					HttpContext.Session.SetString("Email", eazymr.Email.ToString());
					if (eazymr.HospitalLogo != null)
                    {
                        string folder = "Hospital/";
                        folder += Guid.NewGuid().ToString() + "_" + eazymr.HospitalLogo.FileName;
                        string serverFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                        eazymr.Image = folder;
                        await eazymr.HospitalLogo.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    };

                    EazymrReposetory _DbLead = new EazymrReposetory();
                    if (_DbLead.StoreRegistration(eazymr))
                    {
						return new JsonResult(1);
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(0);
            }

            return new JsonResult(0);


        }

        //SelectPackage  form Data store **************************************************************************
        public async Task<IActionResult> SelectPackage(SelectPackageEntity SelectPackage) {

            try { 
                if (ModelState.IsValid)
                {
					SelectPackage.JoinTable = HttpContext.Session.GetString("uniqueId");
					ModelState.Clear();
                    EazymrReposetory _DbEazymr = new EazymrReposetory();
                    if (_DbEazymr.StoreSelectPackage(SelectPackage))
                    {
					 string UserEmail =	HttpContext.Session.GetString("Email");

						string fromMail = "easyemr3010@gmail.com";
						string fromPassword = "lwybpqfunvlzkifh";

						MailMessage message = new MailMessage();
						message.From = new MailAddress(fromMail);
						message.Subject = "EazyMr";
						message.To.Add(new MailAddress(UserEmail));
						//message.Body = "<html><body> your </body></html>";
						message.Body = "<html><body>" +
			   "<p>We are pleased to inform you that your package has been successfully added to your EazyMr account. Thank you for choosing EazyMr.</p>" +
			   "<p>If you have any questions or require further assistance, please do not hesitate to contact our support team at support@eazymr.com.</p>" +
			   "<p>Best regards,<br/>The EazyMr Team</p></body></html>";

						message.IsBodyHtml = true;

						var smtpClient = new SmtpClient("smtp.gmail.com")
						{
							Port = 587,
							Credentials = new NetworkCredential(fromMail, fromPassword),
							EnableSsl = true,
						};

						smtpClient.Send(message);

						return new JsonResult(1);
                    }

                }
            }
            catch (Exception ex)
            {
                return new JsonResult(0);
            }

            return new JsonResult(0);
        }



    }
}
