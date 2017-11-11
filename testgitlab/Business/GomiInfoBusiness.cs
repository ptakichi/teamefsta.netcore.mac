using System;
using testgitlab.Model;
using testgitlab.Common;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

namespace testgitlab.Business
{
    public class GomiInfoBusiness
    {

        private static String searchSql = "select * from GomiInfo where gomidate = '@today';";

        public GomiInfoBusiness()
        {
            
        }

        public GomiInfoValue getGomiInfo(String today){

            GomiInfoValue result = null;

            try
            {

                //TODO:共通化したい
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "teamefstadb.database.windows.net";
                builder.UserID = "teamefsta";
                builder.Password = "Yuuka0707";
                builder.InitialCatalog = "teamefstaDB";


                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");
                    Console.WriteLine("today:"+ today);

                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append(searchSql);
                    sb.Replace("@today",today);

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                result = new GomiInfoValue();
                                result.date = reader.GetString(1); 
                                result.youbi = reader.GetString(2); 
                                result.naiyou = reader.GetString(3); 
                                result.code = reader.GetString(4); 

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {   
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

    }
}
