using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.Models.Editors;
using TMT_Code_Migration1.DataLogics.Utility;
namespace TMT_Code_Migration1.DataLogics
{
    public class EditorDL
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;

        public EditorDL([FromServices] IConfiguration config, CommonDl commonDl) 
        {
            _commonDl = commonDl; 
            _config = config;
        }
        public StatusResponse AddStatus(List<StatusRequest> request)
        {
            StatusResponse statusResponse = new StatusResponse();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    foreach (StatusRequest statusrequest in request)
                    {
                        SqlCommand cmd = new SqlCommand("[spCardsComment]", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CardsID", statusrequest.CardsID);
                        cmd.Parameters.AddWithValue("@Comment", statusrequest.Comment);
                        cmd.Parameters.AddWithValue("@updatedBy", statusrequest.updatedBy);
                        cmd.Parameters.AddWithValue("@CardStatus", statusrequest.CardStatus);
                        cmd.Parameters.AddWithValue("@QCId", statusrequest.QCId);


                        int k = cmd.ExecuteNonQuery();
                        if (k != 0)
                        {
                            statusResponse.Message = "Record Inserted Succesfully into the StatusTable";
                            statusResponse.Code = k;
                        }


                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error in AddStatusDl. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return statusResponse;
        }
        public StatusResponse EditorSubmit(EditorssubmitRequest request)
        {
            StatusResponse statusResponse = new StatusResponse();
            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spCardsfinalsubmit", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@updatedBy", request.updatedBy);
                    cmd.Parameters.AddWithValue("@CardStatusCurrent", request.CardStatusCurrent);
                    cmd.Parameters.AddWithValue("@CardStatusTobe", request.CardStatusTobe);

                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        statusResponse.Message = "Record Inserted Succesfully into the comment table";
                        statusResponse.Code = k;
                    }
                    else
                    {
                        statusResponse.Message = "Record not inserted";
                        statusResponse.Code = k;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in EditorSubmit. ", ex);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return statusResponse;
        }
    }
}
