using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.Models.Masters.Root_Cause;
using TMT_Code_Migration1.Views.Interfaces.Masters;

namespace TMT_Code_Migration1.DataLogics.Masters
{
    public class MappingRootCauseDL: IMappingRootCause
    {
        private readonly CommonDl _commonDl;
        private readonly IConfiguration _config;
        public MappingRootCauseDL(CommonDl commonDl, [FromServices] IConfiguration config)
        {
            _commonDl = commonDl;
            _config = config;
        }
        public List<RootCauseMappingResponse> GetAvailableDataInRootCause(RootCauseMappingRequest rqst)
        {
            List<RootCauseMappingResponse> rootCauseMappingResponses = new List<RootCauseMappingResponse>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_AVAILABLE_ROOT_CAUSE", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@TASK_ID", rqst.taskId.HasValue ? rqst.taskId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@WORK_ID", rqst.workId.HasValue ? rqst.workId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@STATUS_ID", rqst.statusId.HasValue ? rqst.statusId : (object)DBNull.Value);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                RootCauseMappingResponse obj = new RootCauseMappingResponse();
                                obj.causeId = myReader["id"] != DBNull.Value ? Convert.ToInt32(myReader["id"]) : 0;
                                obj.rootCause = myReader["root_cause_text"] != DBNull.Value ? myReader["root_cause_text"].ToString() : "";
                                rootCauseMappingResponses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_AVAILABLE_ROOT_CAUSE. ", ex);
                }
            }
            return rootCauseMappingResponses;
        }
        public List<RootCauseMappingResponse> GetMappedDataInRootCause(RootCauseMappingRequest rqst)
        {
            List<RootCauseMappingResponse> rootCauseMappingResponses = new List<RootCauseMappingResponse>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    using (SqlCommand myCMD = new SqlCommand("SP_GET_MAPPED_ROOT_CAUSE", conn))
                    {
                        myCMD.CommandType = CommandType.StoredProcedure;
                        myCMD.Parameters.AddWithValue("@TASK_ID", rqst.taskId.HasValue ? rqst.taskId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@WORK_ID", rqst.workId.HasValue ? rqst.workId : (object)DBNull.Value);
                        myCMD.Parameters.AddWithValue("@STATUS_ID", rqst.statusId.HasValue ? rqst.statusId : (object)DBNull.Value);
                        using (SqlDataReader myReader = myCMD.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                RootCauseMappingResponse obj = new RootCauseMappingResponse();
                                obj.causeId = myReader["id"] != DBNull.Value ? Convert.ToInt32(myReader["id"]) : 0;
                                obj.rootCause = myReader["root_cause_text"] != DBNull.Value ? myReader["root_cause_text"].ToString() : "";
                                rootCauseMappingResponses.Add(obj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in SP_GET_MAPPED_ROOT_CAUSE. ", ex);
                }
            }
            return rootCauseMappingResponses;
        }
        public List<RootCauseResponse> InsertDataToCauseMapping(List<RootCauseMappingInsertRequest> requests)
        {
            List<RootCauseResponse> responses = new List<RootCauseResponse>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    foreach (var request in requests)
                    {
                        using (SqlCommand myCMD = new SqlCommand("SP_INSERT_DATA_TO_CAUSE_MAPPING", conn))
                        {
                            myCMD.CommandType = CommandType.StoredProcedure;
                            myCMD.Parameters.AddWithValue("@CAUSEID", request.causeId.HasValue ? request.causeId : (object)DBNull.Value);
                            myCMD.Parameters.AddWithValue("@TASK_ID", request.taskId.HasValue ? request.taskId : (object)DBNull.Value);
                            myCMD.Parameters.AddWithValue("@WORKTYPE_ID", request.workId.HasValue ? request.workId : (object)DBNull.Value);
                            myCMD.Parameters.AddWithValue("@STATUS_ID", request.statusId.HasValue ? request.statusId : (object)DBNull.Value);
                            SqlParameter outputParam = myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                            outputParam.Direction = ParameterDirection.Output;

                            myCMD.ExecuteNonQuery();

                            RootCauseResponse response = new RootCauseResponse();
                            response.outputResponse = outputParam.Value.ToString();
                            responses.Add(response);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in InsertDataToCauseMapping. ", ex);
                }
            }

            return responses;
        }
        public List<RootCauseResponse> DeleteDataFromCauseMapping(List<RootCauseMappingDeleteRequest> requests)
        {
            List<RootCauseResponse> responses = new List<RootCauseResponse>();

            using (SqlConnection conn = _commonDl.Connect())
            {
                try
                {
                    foreach (var request in requests)
                    {
                        using (SqlCommand myCMD = new SqlCommand("SP_DELETE_DATA_TO_CAUSE_MAPPING", conn))
                        {
                            myCMD.CommandType = CommandType.StoredProcedure;
                            myCMD.Parameters.AddWithValue("@MAP_ID", request.mapId.HasValue ? request.mapId : (object)DBNull.Value);
                            SqlParameter outputParam = myCMD.Parameters.Add("@VOUTPUT", SqlDbType.VarChar, 100);
                            outputParam.Direction = ParameterDirection.Output;

                            myCMD.ExecuteNonQuery();

                            RootCauseResponse response = new RootCauseResponse();
                            response.outputResponse = outputParam.Value.ToString();
                            responses.Add(response);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in DeleteDataFromCauseMapping. ", ex);
                }
            }

            return responses;
        }
    }
}
