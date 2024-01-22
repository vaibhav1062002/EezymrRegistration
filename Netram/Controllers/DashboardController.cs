using Microsoft.AspNetCore.Mvc;
using Netram.Models;
using Netram.ViewMiodels;
using System.Net.Mail;
using System.Net;

namespace Netram.Controllers
{
    public class DashboardController : Controller
    {





        public IActionResult Dashboard()
        {
            return View();
        }

		public IActionResult SetPackage()
		{
			EazymrReposetory eazymrReposetory = new EazymrReposetory();
			List<PackageDetailsEntity> objselecpack = eazymrReposetory.ShowSetPackage();

			EazymrReposetory _DBEazymr = new EazymrReposetory();
			List<FertilityEntity> result = _DBEazymr.GetFertilityData();

			SetPackageViewModel viewModel = new SetPackageViewModel
			{
				PackageDetails = objselecpack,
				FertilityData = result
			};

			return View(viewModel);
		}



		public IActionResult EazymrAllData()
		{
			EazymrReposetory repository = new EazymrReposetory();
			List<(EazymrEntity, SelectPackageEntity)> eazymrData = repository.getAllEazymrData();

			return View("EazymrAllData", eazymrData); 
		}




		[HttpPost]
		public async Task<IActionResult>AddPackageDetails(PackageDetailsEntity packagedetails)
        {
            try {
            if(ModelState.IsValid)
                {
                    ModelState.Clear();
                    EazymrReposetory _DBEazymr = new EazymrReposetory();
                    if (_DBEazymr.addpackagedetails(packagedetails))
                    {
						TempData["PackageAddedMessage"] = "New Package Added successful!";

						EazymrReposetory eazymrReposetory = new EazymrReposetory();
						List<PackageDetailsEntity> objselecpack = eazymrReposetory.ShowSetPackage();

						List<FertilityEntity> result = _DBEazymr.GetFertilityData();

						SetPackageViewModel viewModel = new SetPackageViewModel
						{
							PackageDetails = objselecpack,
							FertilityData = result
						};

						return View("SetPackage", viewModel);
					}
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return View();
        }
	




		//edit package view
	   public IActionResult EditPackage(int Id)
	 {
	   List<PackageDetailsEntity> packageDetailsList = new List<PackageDetailsEntity>();
	   EazymrReposetory eazymrReposetory = new EazymrReposetory();
	   packageDetailsList = eazymrReposetory.GetPackagById(Id);

	 	return View("PackageEdit", packageDetailsList);

	}



		//update package
		public async Task<IActionResult> UpdatePackageById(PackageDetailsEntity packageDetails)
		{
			try
			{
				if (ModelState.IsValid)
				{
					ModelState.Clear();
					EazymrReposetory _DbEazymr = new EazymrReposetory();
					if (_DbEazymr.updatePackage(packageDetails))
					{
						TempData["updatedPackage"] = "Your Package Has Been Updated Successfully....!";

						EazymrReposetory eazymrReposetory = new EazymrReposetory();
						List<PackageDetailsEntity> objselecpack = eazymrReposetory.ShowSetPackage();

						EazymrReposetory _DBEazymr = new EazymrReposetory();
						List<FertilityEntity> result = _DBEazymr.GetFertilityData();

						SetPackageViewModel viewModel = new SetPackageViewModel
						{
							PackageDetails = objselecpack,
							FertilityData = result
						};

						return View("SetPackage", viewModel);
					}

				}
			}
			catch (Exception ex)
			{
				return View("PackageEdit");
			}

			return View("PackageEdit");
		}


		// DELETE VIEW
		public IActionResult DeletePackageView(int Id)
		{
			List<PackageDetailsEntity> packageDetailsList = new List<PackageDetailsEntity>();
			EazymrReposetory eazymrReposetory = new EazymrReposetory();
			packageDetailsList = eazymrReposetory.GetPackagById(Id);

			return View("DeletePackageView", packageDetailsList);

		}



		// delete Package 

		public ActionResult DeletePackage(int Id)
		{
			PackageDetailsEntity packageDetails = new PackageDetailsEntity();
			EazymrReposetory _dbEazymr=new EazymrReposetory();
			if(_dbEazymr.DaletePackageById(Id))
			{
				TempData["deletedPackage"] = " Package Has Been Deleted ...!";

				EazymrReposetory eazymrReposetory = new EazymrReposetory();
				List<PackageDetailsEntity> objselecpack = eazymrReposetory.ShowSetPackage();

				EazymrReposetory _DBEazymr = new EazymrReposetory();
				List<FertilityEntity> result = _DBEazymr.GetFertilityData();

				SetPackageViewModel viewModel = new SetPackageViewModel
				{
					PackageDetails = objselecpack,
					FertilityData = result
				};

				return View(viewModel);
			}
			return RedirectToAction();

		}

		// Fertility  section start


		public ActionResult FertilityMaster()  //  show all fertility
		{
			List<FertilityEntity> result = new List<FertilityEntity>();
			EazymrReposetory _DBEazymr = new EazymrReposetory();
			result = _DBEazymr.GetFertilityData();
			return View("FertilityMaster", result);
		}


		public IActionResult StoreFertility(FertilityEntity fertilityEntity) // Add fertility
		{

			try
			{
               if(ModelState.IsValid)
				{
				  EazymrReposetory _DBEazymr=new EazymrReposetory();
					if (_DBEazymr.StoreFertility(fertilityEntity))
					{
						List<FertilityEntity> result = new List<FertilityEntity>();
						result = _DBEazymr.GetFertilityData();
						return View("FertilityMaster", result);
					}
				}
			}
			catch (Exception ex) { 
			}

			return View("");

		}

		public ActionResult ShowFertilityEdit(int Id) //edit show fertility
		{
			List<FertilityEntity> result = new List<FertilityEntity>();
			EazymrReposetory  eazymrReposetory = new EazymrReposetory();
			result = eazymrReposetory.EditFertilityById(Id);
			return View("EditFertility",result);
		}







		//Fertility section end
		public async Task<IActionResult> UpdateFertility(FertilityEntity fertilityEntity)
		{
			try
			{
				if (ModelState.IsValid)
				{
					ModelState.Clear();
					EazymrReposetory _DbEazymr = new EazymrReposetory();
					if(_DbEazymr.UpdateFertilityById(fertilityEntity))
					{
						List<FertilityEntity> PackageDetails = new List<FertilityEntity>();
						EazymrReposetory eazymrReposetory = new EazymrReposetory();
						PackageDetails = eazymrReposetory.GetFertilityData();
						return View("FertilityMaster", PackageDetails);

					}

				}
			}
			catch (Exception ex)
			{
				return View("PackageEdit");
			}

			return View("PackageEdit");
		}

		// delete Fertility 
		public ActionResult DeleteFertility(int Id)
		{
			EazymrReposetory _dbEazymr = new EazymrReposetory();
			if (_dbEazymr.DaleteFertilityById(Id))
			{
				List<FertilityEntity> result = new List<FertilityEntity>();
				EazymrReposetory eazymrReposetory = new EazymrReposetory();
				result = eazymrReposetory.GetFertilityData();
				return View("FertilityMaster", result);
			}
			return RedirectToAction();

		}



		public IActionResult AllDataEditPage(string JoinTable)
		{
			EazymrReposetory repository = new EazymrReposetory();
			List<(EazymrEntity, SelectPackageEntity)> result = repository.allDataEditView(JoinTable);

			AllData allData = new AllData
			{
				slectedPackage = result.Select(x => x.Item2).ToList(),
				eazymrPackage = result.Select(x => x.Item1).ToList()
			};

			return View("demo", allData);

		}


		// update status 

		public IActionResult UpdateStatus(string JoinTable, string PaymentStatus, string DeploymentStatus,string Email)
		{
			EazymrReposetory repository = new EazymrReposetory();
			    if(repository.UpdateStatus(JoinTable, PaymentStatus, DeploymentStatus)){
				List<(EazymrEntity, SelectPackageEntity)> eazymrData2 = repository.getAllEazymrData();

				if(PaymentStatus == "Completed")
				{

					string UserEmail = Email;

					string fromMail = "easyemr3010@gmail.com";
					string fromPassword = "lwybpqfunvlzkifh";

					MailMessage message = new MailMessage();
					message.From = new MailAddress(fromMail);
					message.Subject = "Confirmation of Successful Payment for Your EazyMr Package";
					message.To.Add(new MailAddress(UserEmail));
					message.Body = "<html><body>" +
		   "<p>We are pleased to inform you that the payment for your selected package has been successfully processed. Thank you for choosing EezyMr </p>" +
		   "<p>If you have any questions or require further assistance, please do not hesitate to contact our support team at support@Eezymr.com</p>" +
		   "<p>Best regards,<br/>The EezyMr Team</p></body></html>";

					message.IsBodyHtml = true;

					var smtpClient = new SmtpClient("smtp.gmail.com")
					{
						Port = 587,
						Credentials = new NetworkCredential(fromMail, fromPassword),
						EnableSsl = true,
					};

					smtpClient.Send(message);

				}
			

				if(DeploymentStatus == "Completed")
				{
					string UserEmail = Email;

					string fromMail = "easyemr3010@gmail.com";
					string fromPassword = "lwybpqfunvlzkifh";

					MailMessage message = new MailMessage();
					message.From = new MailAddress(fromMail);
					message.Subject = "Confirmation: Successful Completion of Deployment";
					message.To.Add(new MailAddress(UserEmail));
					message.Body = "<html><body>" +
		   "<p>We are delighted to announce that the deployment for your selected package has been successfully completed. Thank you for choosing EezyMr.</p>" +
		   "<p>If you have any questions or require further assistance, please do not hesitate to contact our support team at support@Eezymr.com</p>" +
		   "<p>Best regards,<br/>The EezyMr Team</p></body></html>";

					message.IsBodyHtml = true;

					var smtpClient = new SmtpClient("smtp.gmail.com")
					{
						Port = 587,
						Credentials = new NetworkCredential(fromMail, fromPassword),
						EnableSsl = true,
					};

					smtpClient.Send(message);

				}

				return View("EazymrAllData", eazymrData2);
			}

          List<(EazymrEntity, SelectPackageEntity)> eazymrData3 = repository.getAllEazymrData();
			return View("EazymrAllData", eazymrData3);
		}

	}
}
