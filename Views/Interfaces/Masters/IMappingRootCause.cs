using TMT_Code_Migration1.Models.Masters.Root_Cause;

namespace TMT_Code_Migration1.Views.Interfaces.Masters
{
    public interface IMappingRootCause
    {
        List<RootCauseMappingResponse> GetAvailableDataInRootCause(RootCauseMappingRequest rqst);
        List<RootCauseMappingResponse> GetMappedDataInRootCause(RootCauseMappingRequest rqst);
        List<RootCauseResponse> InsertDataToCauseMapping(List<RootCauseMappingInsertRequest> requests);
        List<RootCauseResponse> DeleteDataFromCauseMapping(List<RootCauseMappingDeleteRequest> requests);
    }
}
