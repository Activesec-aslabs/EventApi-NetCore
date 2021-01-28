using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.RepresentationModel;
using Newtonsoft.Json.Schema;

namespace AppDynamicsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppDynamicsController : ControllerBase
    {
        public string baseURL = "https://activesec.saas.appdynamics.com/controller/";
        public string analyticsURL = "http://analytics.api.appdynamics.com/";
        public string username = "matias.beltrame1@activesec";
        public string password = "welcome1";
        public string globalAccount = "Activesec_10b5e9f5-fee4-482a-a893-a32da826ad7d";
        public string accessKey = "4e91f2713d0c";
        public string apiKey = "3390a87e-99c8-433c-8e98-98a5c883f17c";

        [HttpGet("GetApplications")]
        public async Task<string> GetApplications()
        {
            string url = this.baseURL + "rest/applications?output=JSON";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
            }

            Console.WriteLine(response.Content);
            return response.Content;
        }

        [HttpGet("CreateSchema/{schema}")]
        public async Task<string> GetCreateSchema(string schema)
        {
            string url = this.analyticsURL + "events/schema/" + schema ;
            var client = new RestClient(url);
            client.AddDefaultHeader("X-Events-API-AccountName",this.globalAccount);
            client.AddDefaultHeader("X-Events-API-Key",this.apiKey);
            client.AddDefaultHeader("Accept","application/vnd.appd.events+json;v=2");
            
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Events-API-AccountName",this.globalAccount);
            request.AddHeader("X-Events-API-Key",this.apiKey);
            request.AddHeader("Content-type", "application/vnd.appd.events+json;v=2");
          
            using (StreamReader file = System.IO.File.OpenText(@"jsonFiles\test.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JSchema schema1 = JSchema.Load(reader);

                // validate JSON
                string schemaJson = schema1.ToString();
                Console.WriteLine(schemaJson);

                request.AddJsonBody(schemaJson);
               

            }

            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
            }else{
                Console.WriteLine(response.Content);
            }
            
            return response.Content;
        }

        [HttpGet("RetrieveSchema/{schema}")]
        public async Task<string> GetRetrieveSchema(string schema)
        {
            string url = this.analyticsURL + "events/schema/" + schema + "";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-Events-API-AccountName",this.globalAccount);
            request.AddHeader("X-Events-API-Key",this.apiKey);
            request.AddHeader("Content-type", "application/vnd.appd.events+json;v=2");
            request.AddHeader("Accept","application/vnd.appd.events+json;v=2");
            GetValuesFromYAML();
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
            }else{
                Console.WriteLine(response.Content);
            }
            
            return response.Content;
        }

         [HttpGet("DeleteSchema/{schema}")]
        public async Task<string> DeleteSchema(string schema)
        {
            string url = this.analyticsURL + "events/schema/" + schema;
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("X-Events-API-AccountName",this.globalAccount);
            request.AddHeader("X-Events-API-Key",this.apiKey);
            request.AddHeader("Accept","application/vnd.appd.events+json;v=2");
            
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
            }else{
                Console.WriteLine(response.Content);
            }
            
            return response.Content;
        }

        public void GetValuesFromYAML()
        {
           // string filepath = "";
            using (var reader = new StreamReader(@"jsonFiles\test.yaml")) {
                // Load the stream
                var yaml = new YamlStream();
                yaml.Load(reader);
                // the rest
            }
        }
    }
}