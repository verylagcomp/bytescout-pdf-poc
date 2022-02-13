using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bytescout.Pdf.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IConfiguration _config;

        public PdfController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("parse")]
        public async Task<IActionResult> ParsePdfFromUrl()
        {
            // TODO: move this method code to the separate service. Controllers should be small and clean without BLL.

            const string sourceFileUrl = "https://bytescout-com.s3.amazonaws.com/files/demo-files/cloud-api/document-parser/MultiPageTable.pdf";
            const string destinationFile = @".\result.json";
            const string url = "https://api.pdf.co/v1/pdf/documentparser";

            var apiKey = _config.GetSection("PdfConfig").GetSection("DefaultApiKey").Value;
            var templateText = await System.IO.File.ReadAllTextAsync(@".\Templates\MultiPageTable-template1.yml");

            using var webClient = new WebClient();
            webClient.Headers.Add("x-api-key", apiKey);

            try
            {
                // PARSE UPLOADED PDF DOCUMENT

                var requestBody = new Dictionary<string, string>
                {
                    {"template", templateText},
                    {"name", Path.GetFileName(destinationFile)},
                    {"url", sourceFileUrl}
                };

                // Convert dictionary of params to JSON
                var jsonPayload = JsonConvert.SerializeObject(requestBody);

                // Execute request
                var response = webClient.UploadString(url, "POST", jsonPayload);

                // Parse response
                var json = JObject.Parse(response);

                if (json["error"].ToObject<bool>() == false)
                {
                    // Get URL of generated JSON file
                    var resultFileUrl = json["url"].ToString();

                    // Download JSON file
                    webClient.DownloadFile(resultFileUrl, destinationFile);
                }
                else
                {
                    Console.WriteLine(json["message"].ToString());
                }
            }
            catch (WebException e)
            {
                // TODO: log exception
            }

            return null;
        }
    }
}
