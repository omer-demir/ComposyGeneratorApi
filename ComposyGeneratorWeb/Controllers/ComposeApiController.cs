using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace ComposyGeneratorWeb.Controllers
{
    [RoutePrefix("api/composeapi")]
    public class ComposeApiController : ApiController
    {
        private readonly string _mainExecutablePath;
        private readonly string _arguments;
        private readonly string _storagePath;

        public ComposeApiController()
        {
            var composeJarPath = ConfigurationManager.AppSettings["ComposeJarPath"]; ;
            var environment = ConfigurationManager.AppSettings["Environment"];
            _storagePath = ConfigurationManager.AppSettings["StoragePath"];

            if (environment == "test")
            {
                _mainExecutablePath = "C:\\composeTest.bat";
                composeJarPath = "";
                _arguments = "";
            }
            else
            {
                _mainExecutablePath = ConfigurationManager.AppSettings["JREPath"];
                _arguments = $"-jar {composeJarPath}";
            }
        }

        [HttpGet, Route("")]
        public string Hello()
        {
            return "Hello world";
        }

        [HttpGet, Route("compose")]
        public string ComposeMusic([FromUri]string genre, [FromUri]string speed, [FromUri]string instrument)
        {

            //"myMusic.wav" "slow" "Piano" "321"

            String absolutePath = HttpContext.Current.Server.MapPath("~/GeneratedMusic");
            var filePath = absolutePath + @"\" + Guid.NewGuid() + ".wav";
            var newPath = $" \"{speed}\" \"{instrument}\" \"{321}\"";

            var newArguments = $"{_arguments} \"{filePath}\" {newPath}";
            Console.WriteLine(newArguments);

            Process p = new Process
            {
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    FileName = _mainExecutablePath,
                    Arguments = newArguments
                }
            };
            
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit(5000);

            return filePath;
        }

        [HttpGet, Route("music/{fileName}")]
        public HttpResponseMessage GetFile([FromUri] string fileName)
        {
            String absolutePath = HttpContext.Current.Server.MapPath("~/GeneratedMusic");
            var path = absolutePath + @"\" + fileName;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }
}
