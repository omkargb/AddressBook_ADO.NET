using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookSystem
{
    public class ContactOperations
    {
        public static string connectionString = "Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=AddressBookServiceDB";
        Person per = new Person();

        public int GetContactDetails()
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            int count = 0;
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    SqlCommand com = new SqlCommand("spGetAllContactsDetails", sqlConnect); //query, sqlconnection
                    com.CommandType = CommandType.StoredProcedure;

                    Console.WriteLine(" Database connected.");
                    Console.WriteLine(" Database name : " + sqlConnect.Database);

                    SqlDataReader dr = com.ExecuteReader();

                    if (dr.HasRows)
                    {
                        Console.WriteLine("\n Person contacts Contents : \n");
                        Console.WriteLine(" Id\t Name \t\t Address\tCity \t\tState\t\tZipCode\tPhoneNumber\t EmailId  \t AdrBookName");
                        Console.WriteLine();
                        while (dr.Read())
                        {
                            count++;
                            per.Id = dr.GetInt32(0);
                            per.FirstName = dr.GetString(1);
                            per.LastName = dr.GetString(2);
                            per.Address = dr.GetString(3);
                            per.City = dr.GetString(4);
                            per.State = dr.GetString(5);
                            per.ZipCode = dr.GetInt32(6);
                            per.PhoneNumber = dr.GetString(7);
                            per.EmailId = dr.GetString(8);
                            per.AdrBookName = dr.GetString(9);

                            Console.Write(" {0} \t {1} \t {2} \t {3} \t {4}\t{5}\t{6}\t{7}\t{8}\t{9}\n",per.Id, per.FirstName, per.LastName, per.Address, per.City, per.State, per.ZipCode, per.PhoneNumber, per.EmailId, per.AdrBookName);
                        }
                    }
                    dr.Close();
                }
                Console.WriteLine(" --> Total contacts : " + count);
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(" Database not connected. Error details : \n" + e.Message);
                return count;
            }
            finally
            {
                sqlConnect.Close();
            }
        }



        public void EditContact()
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    Console.WriteLine(" SQL Database connection open..");
                    SqlCommand cmd1 = new SqlCommand("spEditContactDetails", sqlConnect);
                    SqlCommand cmd2 = new SqlCommand("spEditAddressDetails", sqlConnect);

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandType = CommandType.StoredProcedure;

                    Console.Write(" Enter Person Id to Edit data : ");
                    int PersonId = int.Parse(Console.ReadLine());


                    //check id exists or not
                    SqlCommand com = new SqlCommand("spGetAllContactsDetails", sqlConnect); //query, sqlconnection
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (dr.GetInt32(0) == PersonId)     // (0) = 1st column - PersonId
                            {
                                Console.WriteLine(" Contact Id present.");
                                EditThisContact();
                                break;
                            }
                        }
                        sqlConnect.Close();
                        Console.WriteLine(" SQL Database connection closed..");

                        void EditThisContact()
                        {
                            int affRows = 0;
                            Console.WriteLine(" Select a field --> ");
                            Console.WriteLine(" 1.Name and contact \t 2.Address \t 3.Exit");
                            Console.Write(" Enter your choice : ");
                            int choice = int.Parse(Console.ReadLine());

                            switch (choice)
                            {
                                case 1:
                                    cmd1.Parameters.AddWithValue("@ContactId", PersonId);
                                    Console.Write(" First Name | Original: {0} \t|\t Modify : ", dr.GetString(1));
                                    per.FirstName = Console.ReadLine();
                                    cmd1.Parameters.AddWithValue("@FirstName", per.FirstName);
                                    Console.Write(" Last Name | Original: {0} \t|\t Modify : ", dr.GetString(2)); 
                                    per.LastName = Console.ReadLine();
                                    cmd1.Parameters.AddWithValue("@LastName", per.LastName);
                                    Console.Write(" Phone number | Original: {0} \t|\t Modify : ", dr.GetString(7)); 
                                    per.PhoneNumber = Console.ReadLine();
                                    cmd1.Parameters.AddWithValue("@PhoneNumber", per.PhoneNumber);
                                    Console.Write(" EmailId | Original : {0} \t|\t Modify : ", dr.GetString(8)); 
                                    per.EmailId = Console.ReadLine();
                                    cmd1.Parameters.AddWithValue("@EmailId", per.EmailId);
                                    dr.Close();
                                    //returns num of affected rows after query execution
                                    affRows = cmd1.ExecuteNonQuery();
                                    break;
                                case 2:
                                    cmd2.Parameters.AddWithValue("@ContactId", PersonId);
                                    Console.Write(" AddressLine | Original: {0} \t|\t Modify  : ", dr.GetString(3)); 
                                    per.Address = Console.ReadLine();
                                    cmd2.Parameters.AddWithValue("@Address", per.Address);
                                    Console.Write(" City | Original: {0} \t|\t Modify : ", dr.GetString(4)); 
                                    per.City = Console.ReadLine();
                                    cmd2.Parameters.AddWithValue("@City", per.City);
                                    Console.Write(" State | Original: {0} \t|\t Modify : ", dr.GetString(5)); 
                                    per.State = Console.ReadLine();
                                    cmd2.Parameters.AddWithValue("@State", per.State);
                                    Console.Write(" ZipCode | Original: {0} \t|\t Modify : ", dr.GetInt32(6)); 
                                    per.ZipCode = int.Parse(Console.ReadLine());
                                    cmd2.Parameters.AddWithValue("@ZipCode", per.ZipCode);
                                    dr.Close();
                                    //returns num of affected rows after query execution
                                    affRows = cmd2.ExecuteNonQuery();
                                    break;
                                default: break;
                            }
                            if (affRows >= 1)
                            { Console.WriteLine(" --> Contact details Updated.."); }
                            else
                            { Console.WriteLine(" --> Contact details not Updated..."); }
                        }

                    }
                }
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }

    }
}
