namespace TMT_Code_Migration1.Models.BPEMResolution
{
    public class BpemResolutionData
    {
        public int? WorkTypeValue { get; set; }
        public string? WorkTypeName { get; set; }
        public List<string>? LinksForDocs { get; set; }
    }
    public class AvailableWorkDocument
    {
        public int? DocsId { get; set; }
        public string? LinkForDocs { get; set; }
    }
    public class BpemResolutionDocument
    {
        public int? WorkId { get; set; }
        public int? TaskId { get; set; }
        public string? FilePath { get; set; }
        public string? CreatedBy { get; set; }

    }
    public class BpemResolutionDeleteRequest
    {
        public int? docsId;
        public string? userId;
    }
    public class BpemResolutionGetRequest
    {
        public int? taskId;
        public int? workId;
    }
}
