using Microsoft.AspNetCore.Mvc;
using TMT_Code_Migration1.DataLogics.Utility;
using TMT_Code_Migration1.DataLogics;
using Microsoft.AspNetCore.Authorization;
using TMT_Code_Migration1.Models.BPEMResolution;
using System.Net;
using System.Web.Http.Description;

namespace TMT_Code_Migration1.Controllers
{
    [Route("BpemResolution")]
    public class BPEMResolutionController : Controller
    {
        private readonly CommonDl _commonDl;
        private readonly BPEMResolutionDL _bPEMResolution;
        private readonly IConfiguration _configuration;

        public BPEMResolutionController(CommonDl commonDl, [FromServices] IConfiguration configuration)
        {
            _commonDl = commonDl;
            _bPEMResolution = new BPEMResolutionDL(commonDl, configuration);
            _configuration = configuration;
        }
        [Route("GetAllBpemResolution/{taskId}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetAllBpemResolutionData(int taskId)
        {
            List<BpemResolutionData> bpemResolutionDatas = _bPEMResolution.GetAllBpemResolutionData(taskId);
            return Ok(bpemResolutionDatas);
        }
        [Route("UploadBpemDocument")]
        [HttpPost]
        [Authorize]
        public IActionResult UploadBpemDocument([FromForm] int workId, [FromForm] int taskId, [FromForm] string createdBy,  IList<IFormFile> files)
        {
            try
            {
                if (files.Count == 0)
                {
                    return BadRequest("No files received.");
                }

                var successMessages = new List<string>();
                var errorMessages = new List<string>();
                string path = _configuration.GetSection("FileManagement")["documentPath"];

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var file in files)
                {
                    if (file.Length == 0)
                    {
                        continue; 
                    }

                    string originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileName = originalFileName + fileExtension;
                    int fileIndex = 1;
                    while (System.IO.File.Exists(Path.Combine(path, fileName)))
                    {
                        fileName = originalFileName + "_" + fileIndex + fileExtension;
                        fileIndex++;
                    }

                    try
                    {
                        var filePath = Path.Combine(path, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        var request = new BpemResolutionDocument
                        {
                            WorkId = workId,
                            TaskId = taskId,
                            FilePath = fileName,
                            CreatedBy = createdBy
                        };
                        string successMsg = _bPEMResolution.InsertBpemResolutionData(request);

                        if (!string.IsNullOrEmpty(successMsg))
                        {
                            successMessages.Add(successMsg);
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add($"Error processing file '{file.FileName}': {ex.Message}");
                    }
                }

                string responseMessage = successMessages.Any() ? string.Join("\n", successMessages) : string.Join("\n", errorMessages);

                return Ok(responseMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [Route("DeleteBpemResolutionData")]
        [HttpPost]
        [Authorize]
        public IActionResult DeleteBpemResolutionData(List<BpemResolutionDeleteRequest> rqst)
        {
            string msg = _bPEMResolution.DeleteBpemResolutionData(rqst);
            return Ok(msg);
        }
        [Route("GetLatestLinkForDocs")]
        [HttpPost]
        [Authorize]
        public IActionResult GetLatestLinkForDocs(BpemResolutionGetRequest rqst)
        {
            List<AvailableWorkDocument> availableWorkDocuments = _bPEMResolution.GetAvailableWorkDocuments(rqst);
            return Ok(availableWorkDocuments);
        }
    }
}
