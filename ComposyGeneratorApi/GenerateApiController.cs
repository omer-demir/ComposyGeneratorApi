﻿// ComposyGeneratorApi.ComposyGeneratorApi.GenerateApiController.cs
// 
// Generated By 
// Created At 29-06-2017

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;

namespace ComposyGeneratorApi
{
    [RoutePrefix("api/generateapi")]
    public class GenerateApiController : ApiController
    {
        private readonly string _mainExecutablePath;
        private readonly string _arguments;

        public GenerateApiController() {
            var composeJarPath = ConfigurationManager.AppSettings["ComposeJarPath"]; ;
            var environment = ConfigurationManager.AppSettings["Environment"];

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
        public string Hello() {
            return "Hello world";
        }

        [HttpGet, Route("compose")]
        public string ComposeMusic([FromUri]string genre, [FromUri]string speed, [FromUri]string instrument) {

            //"myMusic.wav" "slow" "Piano" "321"
            var filePath = Guid.NewGuid() + ".wav";
            var newPath = $" \"{speed}\" \"{instrument}\" \"{321}\"";

            var newArguments = $"{_arguments} \"{filePath}\" {newPath}";
            Console.WriteLine(newArguments);

            Process p = new Process
            {
                StartInfo = {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = _mainExecutablePath,
                    Arguments = newArguments
                }
            };
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            return output;
        }

        [HttpGet, Route("music/{fileName}")]
        public HttpResponseMessage GetFile([FromUri] string fileName)
        {
            var path = @"C:\Users\compose-vm-user\Desktop\batch\"+fileName;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }
}