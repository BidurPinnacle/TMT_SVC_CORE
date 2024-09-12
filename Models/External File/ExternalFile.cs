namespace TMT_Code_Migration1.Models.External_File
{
    public class External_File_request_data
    {
        public string? BPEM { get; set; }
        public string? WORKTYPE { get; set; }
        public string? Status { get; set; }
        public string? Installation { get; set; }
    }

    public class External_File_request
    {
        public List<External_File_request_data>? ExternalFileRequestData { get; set; }
        public string? LoginID { get; set; }

        public External_File_request()
        {
            ExternalFileRequestData = new List<External_File_request_data>();
        }
    }
}
