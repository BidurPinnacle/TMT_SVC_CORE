using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters;
using TMT_Code_Migration1.Views.Interfaces.Masters;

namespace TMT_Code_Migration1.DataLogics.Masters
{
    public class MastersDL : IMasters
    {
        private readonly CommonDl _commonDl;
        public MastersDL(CommonDl commonDl)
        {
            _commonDl = commonDl;
        }
        public List<TableNames> GetTableNames()
        {
            List<TableNames> tableNames = new List<TableNames>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_TABLE_NAME_FOR_MASTER", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                TableNames obj = new TableNames();
                                obj.TABLEID = myReader["TABLEID"].ToString();
                                obj.TABLENAME = myReader["TABLENAME"].ToString();
                                tableNames.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_TABLE_NAME_FOR_MASTER. ", ex);
                }
            }
            return tableNames;
        }
    }
}
