using System;
using testgitlab.Model;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

namespace testgitlab.Business
{
    public class ShelterInfoBusiness
    {

        private static String searchSql = "select Top 20 placename,address,latitude,longitude,"
            + "abs(latitude-@ido) as A ,abs(longitude-@keido) as B ,"
            + " abs(latitude-@ido) + abs(longitude-@keido) as C from ShelterInfo"
            + " order by C asc";
        
        public ShelterInfoBusiness()
        {
            
        }

        public List<ShelterInfoValue> getShelterInfo(decimal ido,decimal keido){

            List<ShelterInfoValue> result = new List<ShelterInfoValue>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "teamefstadb.database.windows.net";
                builder.UserID = "teamefsta";
                builder.Password = "Yuuka0707";
                builder.InitialCatalog = "teamefstaDB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");
                    Console.WriteLine("Ido:"+ ido.ToString());
                    Console.WriteLine("Keido:" + keido.ToString());

                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append(searchSql);
                    sb.Replace("@ido",ido.ToString());
                    sb.Replace("@keido", keido.ToString());

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        ShelterInfoValue value = null;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                value = new ShelterInfoValue();
                                value.Name = reader.GetString(0); 
                                value.Address = reader.GetString(1); 
                                value.Ido = reader.GetDecimal(2); 
                                value.Keido = reader.GetDecimal(3); 

                                result.Add(value);
                                Console.WriteLine("場所：" + value.Name + " 住所："+ value.Address);

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
