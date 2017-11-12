using System;
using testgitlab.Model;
using testgitlab.Common;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

namespace testgitlab.Business
{
    public class HumanDetectInfoBusiness
    {

        private static String searchSql = "select Top 20 s.detect,s.detecttime,s.datatype"
            + " from SensorData as s"
            + " where CONVERT(VARCHAR, s.detecttime, 112) = @today"
            + " order by s.detecttime asc";

        public HumanDetectInfoBusiness()
        {
        }

        public List<HumanDetectInfoValue> getHumanDetectInfo(String today)
        {

            List<HumanDetectInfoValue> result = new List<HumanDetectInfoValue>();

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
                    Console.WriteLine("Today:" + today);

                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append(searchSql);
                    sb.Replace("@today", today);

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        HumanDetectInfoValue value = null;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                value = new HumanDetectInfoValue();
                                value.Detect = reader.GetString(0);
                                value.Detecttime = reader.GetDateTime(1).AddHours(9).ToString("yyyy/MM/dd hh:mm:ss");
                                value.Datatype = reader.GetString(2);
                                //value.Imageurl = reader.GetString(3);
                                result.Add(value);
                                Console.WriteLine("検出：" + value.Detect + " 時間：" + value.Detecttime + " 種別：" + value.Datatype);

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
