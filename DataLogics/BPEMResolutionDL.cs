using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.BPEMResolution;
using TMT_Code_Migration1.Views.Interfaces;

namespace TMT_Code_Migration1.DataLogics
{
    public class BPEMResolutionDL : IBPEMResolution
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationDL _authenticationDL;
        public BPEMResolutionDL(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _configuration = configuration;
            _authenticationDL = new AuthenticationDL(commonDl, configuration);
        }
        public List<BpemResolutionData> GetAllBpemResolutionData(int? taskId)
        {
            List<BpemResolutionData> bpemResolutionDataList = new List<BpemResolutionData>();

            try
            {
                using (SqlConnection conn = _commonDl.Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GET_ALL_BPEM_RESOLUTION_DATA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@taskId", taskId.HasValue ? (object)taskId.Value : DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BpemResolutionData data = new BpemResolutionData
                                {
                                    WorkTypeValue = reader.GetInt32(reader.GetOrdinal("worktypevalue")),
                                    WorkTypeName = reader.GetString(reader.GetOrdinal("worktypename")),
                                    LinksForDocs = reader.IsDBNull(reader.GetOrdinal("LinkForDocs"))
                                        ? null
                                        : reader.GetString(reader.GetOrdinal("LinkForDocs")).Split(',').ToList()
                                };

                                bpemResolutionDataList.Add(data);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in fetching BPEM resolution data.", ex);
            }

            return bpemResolutionDataList;
        }
        public List<AvailableWorkDocument> GetAvailableWorkDocuments(BpemResolutionGetRequest rqst)
        {
            List<AvailableWorkDocument> availableWorkDocuments = new List<AvailableWorkDocument>();

            try
            {
                using (SqlConnection conn = _commonDl.Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GET_AVAILABLE_WORK_DOCUMENT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@taskId", rqst.taskId.HasValue ? (object)rqst.taskId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@workId", rqst.workId.HasValue ? (object)rqst.workId.Value : DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AvailableWorkDocument doc = new AvailableWorkDocument
                                {
                                    DocsId = reader.GetInt32(reader.GetOrdinal("docsId")),
                                    LinkForDocs = reader.GetString(reader.GetOrdinal("linkForDocs"))
                                };

                                availableWorkDocuments.Add(doc);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in fetching available work documents.", ex);
            }

            return availableWorkDocuments;
        }
        public string InsertBpemResolutionData(BpemResolutionDocument request)
        {
            string successMessage = string.Empty;

            try
            {
                using (SqlConnection conn = _commonDl.Connect())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_INSERT_BPEM_RESOLUTION_DATA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@fileLink", request.FilePath);
                        cmd.Parameters.AddWithValue("@createdBy", request.CreatedBy);
                        cmd.Parameters.AddWithValue("@taskId", request.TaskId);
                        cmd.Parameters.AddWithValue("@workId", request.WorkId);

                        SqlParameter outputParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 255)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        successMessage = outputParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in inserting BPEM resolution data.", ex);
            }

            return successMessage;
        }
        public string DeleteBpemResolutionData(List<BpemResolutionDeleteRequest> rqst)
        {
            string firstSuccessMessage = null;

            try
            {
                using (SqlConnection conn = _commonDl.Connect())
                {
                    foreach (var item in rqst)
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_DELETE_BPEM_RESOLUTION_DATA", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@docsId", item.docsId);
                            cmd.Parameters.AddWithValue("@dltUser", item.userId);

                            SqlParameter outputParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 255)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            string successMessage = outputParam.Value.ToString();

                            // Store the first success message
                            if (firstSuccessMessage == null)
                            {
                                firstSuccessMessage = successMessage;
                            }
                            else
                            {
                                // If the current success message is different from the first one, return null
                                if (successMessage != firstSuccessMessage)
                                {
                                    firstSuccessMessage = "";
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in deleting BPEM resolution data.", ex);
            }

            return firstSuccessMessage;
        }
    }
}
