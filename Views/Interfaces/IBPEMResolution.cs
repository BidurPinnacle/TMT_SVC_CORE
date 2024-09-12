using TMT_Code_Migration1.Models.BPEMResolution;

namespace TMT_Code_Migration1.Views.Interfaces
{
    public interface IBPEMResolution
    {
        List<BpemResolutionData> GetAllBpemResolutionData(int? taskId);
        List<AvailableWorkDocument> GetAvailableWorkDocuments(BpemResolutionGetRequest rqst);
        string InsertBpemResolutionData(BpemResolutionDocument request);
        string DeleteBpemResolutionData(List<BpemResolutionDeleteRequest> rqst);
    }
}
