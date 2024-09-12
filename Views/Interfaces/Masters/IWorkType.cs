using TMT_Code_Migration1.Models.Masters.Work_Type;

namespace TMT_Code_Migration1.Views.Interfaces.Masters
{
    public interface IWorkType
    {
        List<WorkTypeData> GetWorkTypeData(int taskId);
        List<WorkTypeData> GetWorkTypeDataById(int workTypeValue);
        InsertUpdateWorkTypeData insertUpdateWork(InsUpdateWorkRequest rqst);
    }
}
