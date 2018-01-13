using System;
using testgitlab.Model;
using testgitlab.Common;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

namespace testgitlab.Business
{
    public class MemoInfoBusiness
    {

        //private static String searchSql = "select Top 20 naiyou, endflg, CONVERT(NVARCHAR, kigendate, 111), CONVERT(NVARCHAR, entrydate, 20),CONVERT(NVARCHAR, updatedate, 20)"
            //+ " from MemoData";
        private static String searchSql = "select id, naiyou, endflg, CONVERT(NVARCHAR, kigendate, 111), CONVERT(NVARCHAR, entrydate, 20),CONVERT(NVARCHAR, updatedate, 20)"
            + " from MemoData";
        
        private static String insertSql = "insert into MemoData (naiyou, endflg, kigendate, entrydate, updatedate)"
            + " VALUES (@naiyou, @endflg, @kigendate, @entrydate, @updatedate)";

        private static String updateSql = "update MemoData set"
            + " set naiyou = @naiyou, endflg = @endflg, kigendate = @kigendate, updatedate = @updatedate"
            + " where id = @id";
        
        public MemoInfoBusiness()
        {
        }

        public List<MemoInfoValue> getMemoInfo(){

            List<MemoInfoValue> result = new List<MemoInfoValue>();

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
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append(searchSql);

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        MemoInfoValue value = null;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                value = new MemoInfoValue();
                                value.id = reader.GetInt32(0).ToString();
                                value.Naiyou = reader.GetString(1); 
                                value.Endflg = reader.GetString(2);
                                value.Kigendate = reader.GetString(3);
                                result.Add(value);
                                Console.WriteLine("内容：" + value.Naiyou + " 完了フラグ：" + value.Endflg);

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

        public Boolean insertMemoInfo(MemoInfoValue info)
        {
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
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append(insertSql);
                    sb.Replace("@naiyou", info.Naiyou);
                    sb.Replace("@endflg", info.Endflg);
                    sb.Replace("@kigendate", info.Kigendate);

                    String sql = sb.ToString();
                    int result = 0;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandText = insertSql;
                        // SQLの実行
                        result = command.ExecuteNonQuery();
                        if (result == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

            return true;
        }

        public Boolean updateMemoInfo(int id, MemoInfoValue info)
        {
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
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append(updateSql);
                    sb.Replace("@id", id.ToString());
                    sb.Replace("@naiyou", info.Naiyou);
                    sb.Replace("@endflg", info.Endflg);
                    sb.Replace("@kigendate", info.Kigendate);

                    String sql = sb.ToString();
                    int result = 0;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandText = insertSql;
                        // SQLの実行
                        result = command.ExecuteNonQuery();
                        if(result == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

            return true;
        }
    }
}
