using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.Models.New_Dashboard;

namespace TMT_Code_Migration1.Views.Interfaces
{
    public interface INewDashBoard
    {
        List<Assignement_Response> GetAssignements(Assignement_Request request);
        List<Projects_Response> GetProjects(Projects_Request request);
        List<WeekCount_Response> GetWeek();
        List<Status_Response> GetStatus(Status_Request request);
        Task<JsonResult> GetNewDashboard_Data(NewDashboard_request request);
        JsonResult GetNewDashboard_Data_Status(NewDashboard_Status_Request request);
        JsonResult GetNewDashboard_Data_User(NewDashboard_User_Request request);
        JsonResult GET_Dashboard_all_tbl_data(DS_REQUEST request, string Dynamic_sp);
        JsonResult GET_Dashboard_GRAPH(DS_REQUEST request, string Dynamic_sp);
        AccessForComment GetAccessCodeForComment(string userId);
        DashboardCommentResponse GetDashboardComment(DashboardCommentRequest request);
        DashboardCommentInsUpdateResponse InsertUpdateDashboardComment(DashboardCommentInsUpdateRequest request);
        List<DashboardCommentList> GetAllDashboardComments();
        string DeleteDashboardComment(DashboardCommentRequest request);
    }
}
