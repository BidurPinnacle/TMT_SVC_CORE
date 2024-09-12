namespace TMT_Code_Migration1.Models.Masters.Root_Cause
{
    public class RootCauseMappingResponse
    {
        public int? causeId { get; set; }
        public string? rootCause { get; set; }
    }
    public class RootCauseMappingRequest
    {
        public int? taskId { get; set; }
        public int? workId { get; set; }
        public int? statusId { get; set; }
    }
    public class RootCauseMappingInsertRequest
    {
        public int? causeId { get; set; }
        public int? taskId { get; set; }
        public int? workId { get; set; }
        public int? statusId { get; set; }
    }
    public class RootCauseResponse
    {
        public string? outputResponse { get; set; }
    }
    public class RootCauseMappingDeleteRequest
    {
        public int? mapId { get; set; }
    }
}
