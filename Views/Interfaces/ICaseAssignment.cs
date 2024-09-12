using TMT_Code_Migration1.Models.caseAssignment;

namespace TMT_Code_Migration1.Views.Interfaces
{
    public interface ICaseAssignment
    {
        List<ExcelUploadedDataResponse> GetExcelUploadedData(ExcelUploadedDataRequest request);
        List<TaskTeamUserResponse> GetAllTaskTeamUsers();
        string ReassignUser(ReassignUserRequest request);
    }
}
