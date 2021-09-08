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
                        Console.WriteLine(" Name \t\t Address\tCity \t\tState\t\tZipCode\tPhoneNumber\t EmailId  \t AdrBookName");
                        Console.WriteLine();
                        while (dr.Read())
                        {
                            count++;
                            per.FirstName = dr.GetString(1);
                            per.LastName = dr.GetString(2);
                            per.Address = dr.GetString(3);
                            per.City = dr.GetString(4);
                            per.State = dr.GetString(5);
                            per.ZipCode = dr.GetInt32(6);
                            per.PhoneNumber = dr.GetString(7);
                            per.EmailId = dr.GetString(8);
                            per.AdrBookName = dr.GetString(9);

                            Console.Write(" {0} {1} \t {2} \t {3} \t {4}\t{5}\t{6}\t{7}\t{8}\n", per.FirstName, per.LastName, per.Address, per.City, per.State, per.ZipCode, per.PhoneNumber, per.EmailId, per.AdrBookName);
                        }
                    }
                    dr.Close();
                }
                Console.WriteLine(" Total contacts : "+count);
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

    }
}
