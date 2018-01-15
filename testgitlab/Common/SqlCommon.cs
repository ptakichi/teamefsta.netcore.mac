using System;

using System.Data.SqlClient;
using System.Text;

namespace testgitlab.Common
{
    public static class SqlCommon
    {

        public static String getSqlConnectionString(){
            
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "teamefstadb.database.windows.net";
            builder.UserID = "teamefsta";
            builder.Password = "Yuuka0707";
            builder.InitialCatalog = "teamefstaDB";

            return builder.ConnectionString;

        }
    }
}
