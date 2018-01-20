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

        public MemoInfoBusiness()
        {
        }

        //tablet.html表示用、終了フラグが0のものを取得
        private static String searchSql = "select id, naiyou, endflg, CONVERT(NVARCHAR, kigendate, 111), CONVERT(NVARCHAR, entrydate, 111),CONVERT(NVARCHAR, updatedate, 111)"
            + " from MemoData where kigendate <= GETDATE()";

        public List<MemoInfoValue> getMemoInfo(){

            List<MemoInfoValue> result = new List<MemoInfoValue>();

            try
            {

                using (SqlConnection connection = new SqlConnection(SqlCommon.getSqlConnectionString()))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append(searchSql);

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                result.Add(createMemoInfoValue(reader));

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


        private MemoInfoValue createMemoInfoValue(SqlDataReader reader){
            
            MemoInfoValue value = new MemoInfoValue();
            value.id = reader.GetInt32(0).ToString();
            value.Naiyou = reader.GetString(1);
            value.Endflg = reader.GetString(2);
            value.Kigendate = reader.GetString(3);
            value.Entrydate = reader.GetString(4);
            value.Updatedate = reader.GetString(5);
            Console.WriteLine("内容：" + value.Naiyou + " 完了フラグ：" + value.Endflg);

            return value;

        }

        private static String deleteSql = "delete from MemoData where id = @id";

        public bool deleteMemoInfo(int id)
        {
            try{
                using (SqlConnection connection = new SqlConnection(SqlCommon.getSqlConnectionString()))
                {
                    connection.Open();
                    int result = 0;

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandText = deleteSql;

                        command.Parameters.Add(new SqlParameter("@id", id));

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
                Console.WriteLine(e.ToString());
                return false;
            }

            return true;
        
        }

        private static String searchSqlAll = "select id, naiyou, endflg, CONVERT(NVARCHAR, kigendate, 111), CONVERT(NVARCHAR, entrydate, 111),CONVERT(NVARCHAR, updatedate, 111)"
            + " from MemoData";
        
        //設定用全検索
        public List<MemoInfoValue> getMemoInfoAll()
        {

            List<MemoInfoValue> result = new List<MemoInfoValue>();

            try
            {

                using (SqlConnection connection = new SqlConnection(SqlCommon.getSqlConnectionString()))
                {
                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    sb.Append(searchSqlAll);

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                result.Add(createMemoInfoValue(reader));

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



        private static String insertSql = "insert into MemoData (naiyou, endflg, kigendate, entrydate, updatedate)"
            + " VALUES (@naiyou, '0', @kigendate, GETDATE(), GETDATE())";

        public Boolean insertMemoInfo(MemoInfoValue info)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(SqlCommon.getSqlConnectionString()))
                {
                    connection.Open();
                    int result = 0;

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandText = insertSql;

                        command.Parameters.Add(new SqlParameter("@naiyou", info.Naiyou));
                        //日付型
                        var dateparam = new SqlParameter();
                        dateparam.ParameterName = "@kigendate";
                        dateparam.SqlDbType = System.Data.SqlDbType.DateTime2;
                        dateparam.Value = info.Kigendate.Replace("/", "-") + " 00:00:00";
                        command.Parameters.Add(dateparam);

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
                Console.WriteLine(e.ToString());
                return false;
            }

            return true;
        }

        private static String updateSql = "update MemoData set"
            + " naiyou = @naiyou, endflg = @endflg, kigendate = @kigendate, updatedate = GETDATE()"
            + " where id = @id";

        public Boolean updateMemoInfo(MemoInfoValue info)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(SqlCommon.getSqlConnectionString()))
                {
                    connection.Open();
                    int result = 0;

                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandText = updateSql;

                        command.Parameters.Add(new SqlParameter("@id", info.id));
                        command.Parameters.Add(new SqlParameter("@naiyou", info.Naiyou));
                        command.Parameters.Add(new SqlParameter("@endflg", info.Endflg));
                        //日付型
                        var dateparam = new SqlParameter();
                        dateparam.ParameterName = "@kigendate";
                        dateparam.SqlDbType =System.Data.SqlDbType.DateTime2;
                        dateparam.Value = info.Kigendate.Replace("/","-")+" 00:00:00";
                        command.Parameters.Add(dateparam);

                        //SqlParameter parameter = new SqlParameter();
                        //parameter.ParameterName = "@Date";
                        //parameter.SqlDbType = SqlDbType.Date;
                        //parameter.Value = "2007/12/1";

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
                Console.WriteLine(e.ToString());
                return false;
            }

            return true;
        }
    }
}
