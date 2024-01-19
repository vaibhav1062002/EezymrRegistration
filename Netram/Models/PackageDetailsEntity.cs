namespace Netram.Models
{
	public class PackageDetailsEntity
	{
		private string _fertility;
		private string _package;
		private string _price;
		

		public int? Id { get; set; }
		public  string Fertility
		{
			get { return _fertility; }
			set { _fertility = value; }
		}

		public string Package
		{
			get { return _package; }
			set {_package = value; }			
	    }

		public string Price
		{
			get { return _price; }
			set { _price = value; }
		}

		}


	public class FertilityEntity
	{
		private string _fertility;

		public int? Id { get; set; }

		public string Fertility
		{
			get { return _fertility; }
			set { _fertility = value; }
		}
	}
}
