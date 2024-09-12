using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TMT_Code_Migration1.DataLogics.Utility
{
    public class CommonDl
    {
        private readonly IConfiguration _configuration;

        public CommonDl(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection Connect()
        {
            try
            {
                string strcon = _configuration.GetConnectionString("dominiondburl");

                SqlConnection conn = new SqlConnection(strcon);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                // You can log the exception here if needed
                throw new Exception("Error connecting to the database.", ex);
            }
        }
    }
}
