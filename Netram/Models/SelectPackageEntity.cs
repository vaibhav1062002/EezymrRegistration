using System.ComponentModel.DataAnnotations;

namespace Netram.Models
{
    public class SelectPackageEntity
    {
        private string _numberOfDoctor;
        private string _fertility;
        private string _package;
        private string _applyCoupon;
        private string _couponNumber;
        private string _tAndC;
        private string _packagePrice;
        private string _joinTable;



	[Required(ErrorMessage = "Number of Doctor is required.")]
        public string NumberOfDoctor
        {
            get { return _numberOfDoctor; }
            set { _numberOfDoctor = value; }
        }

        [Required(ErrorMessage = "Fertility status is required.")]
        public string Fertility
        {
            get { return _fertility; }
            set { _fertility = value; }
        }

        [Required(ErrorMessage = "Package selection is required.")]
        public string Package
        {
            get { return _package; }
            set { _package = value; }
        }

        public string ApplyCoupon
        {
            get { return _applyCoupon; }
            set { _applyCoupon = value; }
        }

        public string? CouponNumber
        {
            get { return _couponNumber; }
            set { _couponNumber = value; }
        }

        [Required(ErrorMessage = "Please agree to the terms and conditions.")]
        public string TAndC
        {
            get { return _tAndC; }
            set { _tAndC = value; }
        }

        [Required] 
        public string PackagePrice
        {
           get {return _packagePrice;}
           set {_packagePrice = value;}
        }

        public string? JoinTable
        {
            get { return _joinTable; }
            set { _joinTable = value;}
		}


        public String? PaymentStatus { get; set; }

        public String? DeploymentStatus { get; set;}

    }


}
