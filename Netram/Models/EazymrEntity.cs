using System.ComponentModel.DataAnnotations;

namespace Netram.Models
{
    public class EazymrEntity
    {

		
		
		[Required]
        public string DoctorName { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public string Specialization { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MobileNumber { get; set; }

     
        public string? HospitalName { get; set; }

       

        public string? State { get; set; }


        public string? City { get; set; }



        public string? Pincode { get; set; }

     
        public string? Address { get; set; }


  

         public string? Insurance { get; set; }


        public string? NABHAccredation { get; set; }

      
        public  IFormFile? HospitalLogo { get; set; }

        public string? Image {  get; set; }

      
        public string? JoinTable { get; set; }

    }
}
