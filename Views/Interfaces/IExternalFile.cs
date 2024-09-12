using System.Data;
using TMT_Code_Migration1.Models.Common;
using TMT_Code_Migration1.Models.External_File;

namespace TMT_Code_Migration1.Views.Interfaces
{
    public interface IExternalFile
    {
        DataTable ReadExceloDataTable(External_File_request request);
        Common Upload_excel(External_File_request request);
    }
}
