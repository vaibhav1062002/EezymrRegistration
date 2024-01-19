using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Netram.Models;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;





public class EazymrReposetory
{


    MySqlConnection _connection;

    public EazymrReposetory()
    {
        //string connStr = @"server=vaibhav\SQLEXPRESS01; database=Eazymr; Integrated Security=true; TrustServerCertificate=true";
        string connStr = "Data Source=localhost; Database=eazymr; USERID =root; PASSWORD =admin@1234";
        _connection = new MySqlConnection(connStr);
    }


    public bool StoreRegistration(EazymrEntity eazymr) // store Registration data 
    {
        string query = "INSERT INTO DoctorDetails (DoctorName,Degree, Specialization, RegistrationNumber, Email, MobileNumber, HospitalName, Address, State, Insurance, NABHAccredation, Pincode, City, Image, JoinTable) VALUES (@DoctorName, @Degree, @Specialization, @RegistrationNumber, @Email, @MobileNumber, @HospitalName, @Address, @State, @Insurance, @NABHAccredation, @Pincode, @City, @Image, @JoinTable)";

        using (MySqlCommand cmd = new MySqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@DoctorName", eazymr.DoctorName);
            cmd.Parameters.AddWithValue("@Specialization", eazymr.Specialization);
            cmd.Parameters.AddWithValue("@RegistrationNumber", eazymr.RegistrationNumber);
            cmd.Parameters.AddWithValue("@Email", eazymr.Email);
            cmd.Parameters.AddWithValue("@MobileNumber", eazymr.MobileNumber);
            cmd.Parameters.AddWithValue("@HospitalName", eazymr.HospitalName);
            cmd.Parameters.AddWithValue("@Address", eazymr.Address);
            cmd.Parameters.AddWithValue("@State", eazymr.State);
            cmd.Parameters.AddWithValue("@Insurance", eazymr.Insurance);
            cmd.Parameters.AddWithValue("@NABHAccredation", eazymr.NABHAccredation);
            cmd.Parameters.AddWithValue("@Pincode", eazymr.Pincode);
            cmd.Parameters.AddWithValue("@City", eazymr.City); 
            cmd.Parameters.AddWithValue("@Image", eazymr.Image);
            cmd.Parameters.AddWithValue("@JoinTable", eazymr.JoinTable);
            cmd.Parameters.AddWithValue("@Degree", eazymr.Degree);

            _connection.Open();

            int i = cmd.ExecuteNonQuery();

            _connection.Close();

            return i >= 1;
        }
    }






    //  to store Package details 

    public bool StoreSelectPackage(SelectPackageEntity selectPackage)
    {
        string query = "INSERT INTO selectedPackage(NumberOfDoctor, Fertility, Package, ApplyCoupon, CouponNumber, TAndC, PackagePrice, JoinTable)VALUES(@NumberOfDoctor, @Fertility, @Package, @ApplyCoupon, @CouponNumber, @TAndC, @PackagePrice, @JoinTable)";

        using (MySqlCommand cmd = new MySqlCommand(query, _connection))
        {

            cmd.Parameters.AddWithValue("@NumberOfDoctor", selectPackage.NumberOfDoctor);
            cmd.Parameters.AddWithValue("@Fertility", selectPackage.Fertility);
            cmd.Parameters.AddWithValue("@Package", selectPackage.Package);
            cmd.Parameters.AddWithValue("@ApplyCoupon", selectPackage.ApplyCoupon);
            cmd.Parameters.AddWithValue("@CouponNumber", selectPackage.CouponNumber);
            cmd.Parameters.AddWithValue("@TAndC", selectPackage.TAndC);
            cmd.Parameters.AddWithValue("@PackagePrice", selectPackage.PackagePrice);
            cmd.Parameters.AddWithValue("@JoinTable", selectPackage.JoinTable);


            _connection.Open();

            int i = cmd.ExecuteNonQuery();

            _connection.Close();

            if (i >= 1)
            {
                return true;
            }
            else { return false; }
        }
    }







    public bool addpackagedetails(PackageDetailsEntity package)
    {
        string query = "INSERT INTO setpackagedetails (Fertility, Package, Price)VALUES (@Fertility, @Package, @Price)";

        using (MySqlCommand cmd = new MySqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@Fertility", package.Fertility);
            cmd.Parameters.AddWithValue("@Package", package.Package);
            cmd.Parameters.AddWithValue("@Price", package.Price);


            _connection.Open();
            int i = cmd.ExecuteNonQuery();
            _connection.Close();
            if (i >= 1) { return true; }
            else { return false; }
        }


    }


    //to show Package Datails 
    public List<PackageDetailsEntity> ShowSetPackage()
    {
        List<PackageDetailsEntity> vaibhav = new List<PackageDetailsEntity>();

        _connection.Open();

        string query = "SELECT * FROM setpackagedetails ORDER BY Id DESC";

        using (MySqlCommand command = new MySqlCommand(query, _connection))
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    PackageDetailsEntity packageDetailsEntity = new PackageDetailsEntity
                    {
						 Id = Convert.ToInt32(reader["Id"]),
					    Fertility = reader["Fertility"].ToString(),
                        Package = reader["Package"].ToString(),
                        Price = reader["Price"].ToString()
                    };

                    vaibhav.Add(packageDetailsEntity);
                }
            }
        }

        _connection.Close();
        return vaibhav;
    }


	// retrive all form data 

	public List<(EazymrEntity, SelectPackageEntity)> getAllEazymrData()
	{
		List<(EazymrEntity, SelectPackageEntity)> result = new List<(EazymrEntity, SelectPackageEntity)>();

		_connection.Open();

		string query = "SELECT doctorDetails.DoctorName, doctorDetails.Degree, doctorDetails.Specialization, doctorDetails.RegistrationNumber, doctorDetails.Email, doctorDetails.MobileNumber, doctorDetails.HospitalName, doctorDetails.Address, doctorDetails.State, doctorDetails.Pincode, doctorDetails.City, doctorDetails.Image, doctorDetails.JoinTable, selectedPackage.NumberOfDoctor, selectedPackage.Fertility, selectedPackage.Package, selectedPackage.ApplyCoupon, selectedPackage.CouponNumber, selectedPackage.TAndC, selectedPackage.PackagePrice FROM doctorDetails INNER JOIN selectedPackage ON doctorDetails.JoinTable = selectedPackage.JoinTable ORDER BY doctorDetails.Id DESC";


		using (MySqlCommand command = new MySqlCommand(query, _connection))
		{
			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					EazymrEntity eazymr = new EazymrEntity()
					{
						DoctorName = reader["DoctorName"].ToString(),
						Specialization = reader["Specialization"].ToString(),
						RegistrationNumber = reader["RegistrationNumber"].ToString(),
						Email = reader["Email"].ToString(),
						MobileNumber = reader["MobileNumber"].ToString(),
						HospitalName = reader["HospitalName"].ToString(),
						Address = reader["Address"].ToString(),
						State = reader["State"].ToString(),
						Pincode = reader["Pincode"].ToString(),
						City = reader["City"].ToString(),
						Image = reader["Image"].ToString(),
						Degree = reader["Degree"].ToString(),
						JoinTable= reader["JoinTable"].ToString(),
					};

					SelectPackageEntity selectPackage = new SelectPackageEntity()
					{
						NumberOfDoctor = reader["NumberOfDoctor"].ToString(),
						Fertility = reader["Fertility"].ToString(),
						Package = reader["Package"].ToString(),
						ApplyCoupon = reader["ApplyCoupon"].ToString(),
						CouponNumber = reader["CouponNumber"].ToString(),
						TAndC = reader["TAndC"].ToString(),
						PackagePrice = reader["PackagePrice"].ToString()
					};

					result.Add((eazymr, selectPackage));
				}
			}
		}

		_connection.Close();

		return result;
	}


	// returning edit view with data using Id
	public List<PackageDetailsEntity> GetPackagById(int Id)
	{
		List<PackageDetailsEntity> result = new List<PackageDetailsEntity>();

		_connection.Open();

		string query = "SELECT * FROM setpackagedetails WHERE id = @ID";

		using(MySqlCommand command = new MySqlCommand(query, _connection))
		{
			command.Parameters.AddWithValue("@ID", Id);

			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					PackageDetailsEntity packageDetailsEntity = new PackageDetailsEntity
					{
						Id = Convert.ToInt32(reader["Id"]),
						Fertility = reader["Fertility"].ToString(),
						Package = reader["Package"].ToString(),
						Price = reader["Price"].ToString()
					};

					result.Add(packageDetailsEntity);
				}
			}
		}

		_connection.Close();
		return result;
	}


    //update package data
	public bool updatePackage(PackageDetailsEntity package) 
	{
		string query = "UPDATE  setpackagedetails SET Fertility=@Fertility, Package=@Package, Price=@Price WHERE Id=@Id";

		using (MySqlCommand cmd = new MySqlCommand(query, _connection))
		{
            cmd.Parameters.AddWithValue("@Id", package.Id);
			cmd.Parameters.AddWithValue("@Fertility", package.Fertility);
			cmd.Parameters.AddWithValue("@Package", package.Package);
			cmd.Parameters.AddWithValue("@Price", package.Price);


			_connection.Open();
			int i = cmd.ExecuteNonQuery();
			_connection.Close();
			if (i >= 1) { return true; }
			else { return false; }
		}
	}


    public bool DaletePackageById(int Id)
    {
        string query = "DELETE FROM setpackagedetails WHERE Id = @Id";
        using(MySqlCommand cmd = new MySqlCommand( query, _connection))
        {
            cmd.Parameters.AddWithValue("@Id",Id);

			_connection.Open();
			int i = cmd.ExecuteNonQuery();
			_connection.Close();
			if (i >= 1) { return true; }
			else { return false; }
		}
	}

	// fertility section stast


    // get fertility data 
    public List<FertilityEntity> GetFertilityData()
    {
        List<FertilityEntity> fertilityEntities = new List<FertilityEntity>();
         _connection.Open();

        string query = "SELECT * FROM fertility";
        
        using( MySqlCommand cmd = new MySqlCommand(query, _connection))
        {
            using(MySqlDataReader reader = cmd.ExecuteReader()) 
            { 
                while (reader.Read()) 
                {
                    FertilityEntity fertilityEntityData = new FertilityEntity
                    {
						Id = Convert.ToInt32(reader["Id"]),
						Fertility = reader["Fertility"].ToString()
					};

					fertilityEntities.Add(fertilityEntityData);


				}
            }
        }
        _connection.Close();

        return fertilityEntities;

    }

	public bool StoreFertility(FertilityEntity fertility)
    {

        string query = "INSERT INTO fertility(Fertility) VALUES(@Fertility)";

        using( MySqlCommand cmd = new MySqlCommand(query, _connection))
        {
            cmd.Parameters.AddWithValue("@Fertility", fertility.Fertility);

            _connection.Open();
			int i = cmd.ExecuteNonQuery();
			_connection.Close();

			if (i >= 1){ return true; }
			else { return false; }
		}
    }

	// fertility Edit 
	public List<FertilityEntity> EditFertilityById(int Id)  
	{
		List<FertilityEntity> result = new List<FertilityEntity>();

		_connection.Open();

		string query = "SELECT * FROM fertility WHERE Id=@Id ";

		using (MySqlCommand cmd = new MySqlCommand(query, _connection))
		{
			cmd.Parameters.AddWithValue("@Id", Id);

			using (MySqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					FertilityEntity fertilityEntity = new FertilityEntity()
					{
						Id = Convert.ToInt32(reader["Id"]),
						Fertility = reader["Fertility"].ToString()
					};
					result.Add(fertilityEntity);
				}
			}
		}

		_connection.Close();
		return result;
	}



		public bool UpdateFertilityById(FertilityEntity fertilityEntity) //update fertility 

	{
		string query = "UPDATE fertility SET Fertility=@Fertility WHERE Id=@Id";

		using (MySqlCommand cmd = new MySqlCommand(query, _connection))
		{
			cmd.Parameters.AddWithValue("@Fertility", fertilityEntity.Fertility);
			cmd.Parameters.AddWithValue("@Id", fertilityEntity.Id);

			_connection.Open();
			int i = cmd.ExecuteNonQuery();
			_connection.Close();
			if (i >= 1) { return true; }
			else { return false; }
		}
	}



	public bool DaleteFertilityById(int Id) //Delete Fertility
	{
		string query = "DELETE FROM fertility WHERE Id = @Id";
		using (MySqlCommand cmd = new MySqlCommand(query, _connection))
		{
			cmd.Parameters.AddWithValue("@Id", Id);

			_connection.Open();
			int i = cmd.ExecuteNonQuery();
			_connection.Close();
			if (i >= 1) { return true; }
			else { return false; }
		}
	}



	// all data edit page view

	public List<(EazymrEntity, SelectPackageEntity)> allDataEditView(string JoinTable)
	{
		List<(EazymrEntity, SelectPackageEntity)> result = new List<(EazymrEntity, SelectPackageEntity)>();

		_connection.Open();

		string query = "SELECT doctorDetails.DoctorName, doctorDetails.Degree, doctorDetails.Specialization, doctorDetails.RegistrationNumber, doctorDetails.Email, doctorDetails.MobileNumber, doctorDetails.HospitalName, doctorDetails.Address, doctorDetails.State, doctorDetails.Pincode, doctorDetails.City, doctorDetails.Image, doctorDetails.JoinTable, selectedPackage.NumberOfDoctor, selectedPackage.Fertility, selectedPackage.Package, selectedPackage.ApplyCoupon, selectedPackage.CouponNumber, selectedPackage.TAndC, selectedPackage.PackagePrice FROM doctorDetails INNER JOIN selectedPackage ON doctorDetails.JoinTable = selectedPackage.JoinTable WHERE doctorDetails.JoinTable = @JoinTable";


		using (MySqlCommand command = new MySqlCommand(query, _connection))
		{
			command.Parameters.AddWithValue("@JoinTable", JoinTable); 

			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					EazymrEntity eazymr = new EazymrEntity()
					{
						DoctorName = reader["DoctorName"].ToString(),
						Specialization = reader["Specialization"].ToString(),
						RegistrationNumber = reader["RegistrationNumber"].ToString(),
						Email = reader["Email"].ToString(),
						MobileNumber = reader["MobileNumber"].ToString(),
						HospitalName = reader["HospitalName"].ToString(),
						Address = reader["Address"].ToString(),
						State = reader["State"].ToString(),
						Pincode = reader["Pincode"].ToString(),
						City = reader["City"].ToString(),
						Image = reader["Image"].ToString(),
						Degree = reader["Degree"].ToString(),
						JoinTable = reader["JoinTable"].ToString(),
					};

					SelectPackageEntity selectPackage = new SelectPackageEntity()
					{
						NumberOfDoctor = reader["NumberOfDoctor"].ToString(),
						Fertility = reader["Fertility"].ToString(),
						Package = reader["Package"].ToString(),
						ApplyCoupon = reader["ApplyCoupon"].ToString(),
						CouponNumber = reader["CouponNumber"].ToString(),
						TAndC = reader["TAndC"].ToString(),
						PackagePrice = reader["PackagePrice"].ToString()
					};

					result.Add((eazymr, selectPackage));
				}
			}
		}

		_connection.Close();

		return result;
	}


}











