
using System.Data;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.External_File;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TMT_Code_Migration1.Models.Common;
using TMT_Code_Migration1.Views.Interfaces;

namespace TMT_Code_Migration1.DataLogics
{
    public class ExternalFileDL: IExternalFile
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public ExternalFileDL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public DataTable ReadExceloDataTable(External_File_request request)
        {
            DataTable dataTable = new DataTable();

            // Define the columns in the DataTable
            dataTable.Columns.Add(new DataColumn("BPEM", typeof(string)));
            dataTable.Columns.Add(new DataColumn("INSTALLATION", typeof(string)));
            dataTable.Columns.Add(new DataColumn("WORK_TYPE", typeof(string)));
            dataTable.Columns.Add(new DataColumn("CARDSTATUS", typeof(string)));

            try
            {
                // Iterate through each External_File_request object in the list
                foreach (var req in request.ExternalFileRequestData)
                {
                    DataRow row = dataTable.NewRow();
                    // Assign values from External_File_request object to the DataRow
                    row["BPEM"] = req.BPEM;
                    row["INSTALLATION"] = req.Installation;
                    row["WORK_TYPE"] = req.WORKTYPE;
                    row["CARDSTATUS"] = req.Status;

                    // Add the DataRow to the DataTable
                    dataTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing request list: {ex.Message}");
                throw;
            }

            return dataTable;
        }


        public Common Upload_excel(External_File_request request)
        {
            DataTable dataTable = ReadExceloDataTable(request);
            Common response = new Common();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_CREATE_TEMP_TABLE_FROM_EXCEL", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TBLFILE", dataTable);
                    cmd.Parameters.AddWithValue("@LoginID", request.LoginID);
                    cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                    cmd.Parameters["@RETURN_VALUE"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@RETURN_TEXT", SqlDbType.Int);
                    cmd.Parameters["@RETURN_TEXT"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    int str = (int)cmd.Parameters["@RETURN_VALUE"].Value;
                    /// response.Message = str;
                    string returntextvalue = cmd.Parameters["@RETURN_TEXT"].Value.ToString();
                    response.Code = str;
                    response.Message = returntextvalue;


                }
                catch (Exception ex)
                {
                    throw new Exception("Error in Cards. ", ex);
                }
                return response;
            }
        }
    }
}
