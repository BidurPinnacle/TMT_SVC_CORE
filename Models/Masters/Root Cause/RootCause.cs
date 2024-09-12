namespace TMT_Code_Migration1.Models.Masters.Root_Cause
{
    public class RootCauseForMasterData
    {
        public int? causeId { get; set; }
        public string? RootCauseText { get; set; }
        public string? status { get; set; }
        public string? createdBy { get; set; }
        public string? causeDescription { get; set; }
        public string? companyName { get; set; }
    }
    public class RootCauseForMasterDataById
    {
        public int? causeId { get; set; }
        public string? RootCauseText { get; set; }
        public string? status { get; set; }
        public string? createdBy { get; set; }
        public string? causeDescription { get; set; }
        public int? companyId { get; set; }
    }
    public class InsertUpdateRootCauseForMasterData
    {
        public int? returnCode { get; set; }
        public string? returnText { get; set; }
    }
    public class InsUpdateRootCauseForMasterRequest
    {
        public string? operationType { get; set; }
        public int? causeId { get; set; }
        public string? RootCauseText { get; set; }
        public string? status { get; set; }
        public string? createdBy { get; set; }
        public string? causeDesc { get; set; }
        public int? companyid { get; set; }
    }
}
