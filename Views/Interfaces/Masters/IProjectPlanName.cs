using TMT_Code_Migration1.Models.Masters.Project_Plan_Name;

namespace TMT_Code_Migration1.Views.Interfaces.Masters
{
    public interface IProjectPlanName
    {
        List<ProjectPlanData> GetProjectPlanData();
        List<ProjectPlanData> GetProjectPlanDataById(int projectId);
        InsertUpdateProjectPlanData insertUpdateProject(InsUpdateProjectRequest rqst);
    }
}
