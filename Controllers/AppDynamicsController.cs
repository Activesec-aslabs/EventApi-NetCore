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
using Microsoft.Extensions.Configuration;

namespace AppDynamicsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppDynamicsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public string baseURL;
        public string analyticsURL;
        public string username;
        public string password;
        public string globalAccount;
        public string accessKey;
        public string apiKey;
        public string schemaName;

        public AppDynamicsController(IConfiguration configuration)
        {
            _configuration = configuration;
            baseURL = _configuration.GetValue<string>("baseURL");
            analyticsURL = _configuration.GetValue<string>("analyticsURL");
            username = _configuration.GetValue<string>("username");
            password = _configuration.GetValue<string>("password");
            globalAccount = _configuration.GetValue<string>("globalAccount");
            accessKey = _configuration.GetValue<string>("accessKey");
            apiKey = _configuration.GetValue<string>("apiKey");
            schemaName = _configuration.GetValue<string>("schemaName");
        }
        
        //public string baseURL = "https://directvpan.saas.appdynamics.com/controller/";
        
        //"https://activesec.saas.appdynamics.com/controller/";
        //public string baseURL = "https://directvpan-dev.saas.appdynamics.com/controller/";
         
        //public string username = "activesec@directvpan";
         
        //public string username = "activesec@directvpan-dev";
        //public string password = "Manda.loriAn";
        
        //public string password = "Wanda.Vision";
        
        //public string globalAccount = "directvpan_386c8f95-5078-4ff3-a307-333578bc8445";
         
        //public string globalAccount = "directvpan-dev_e8d55948-7686-486c-9911-c343e83cf4a8";
        
        //public string accessKey = "df0e661ccd4c";
         
        //public string accessKey = "8e712b315788";
        //public string apiKey = "0a29d349-e7e4-4a8c-94e3-7605bc8c794f";
         
        //public string apiKey = "3390a87e-99c8-433c-8e98-98a5c883f17c";   
        [HttpGet("GetHealthRulesApplication/{applicationId}")]
        public async Task<string> GetHealthRulesApplication(int applicationId)
        {
            string url = this.baseURL + "alerting/rest/v1/applications/"+ applicationId + "/health-rules?output=JSON";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                 foreach (var item in content)
                {
                    
                }
            }
            return response.Content;
        }

        [HttpGet("GetHealthRuleDetail/{applicationId}/{healthRuleId}")]
        public async Task<string> GetHealthRuleDetail(int applicationId, int healthRuleId)
        {
            string url = this.baseURL + "alerting/rest/v1/applications/"+ applicationId + "/health-rules/"+ healthRuleId + "?output=JSON";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                 foreach (var item in content)
                {
                    
                }
            }
            return response.Content;
        }

        [HttpGet("CreateHealthRule/{applicationId}")]
        public async Task<string> CreateHealthRule(int applicationId)
        {
            string url = this.baseURL + "alerting/rest/v1/applications/"+ applicationId + "/health-rules?output=JSON";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.POST);

            using (StreamReader file = System.IO.File.OpenText(@"jsonFiles\newHR.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JSchema schema1 = JSchema.Load(reader);

                // validate JSON
                string schemaJson = schema1.ToString();
                Console.WriteLine(schemaJson);

                request.AddJsonBody(schemaJson);
               
            }

            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                 foreach (var item in content)
                {
                    
                }
            }
            return response.Content;
        }

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

                foreach (var item in content)
                {
                    string appEnv = item.SelectToken("name").Value<string>();
                    await GetAPMBusinessTransactions(appEnv);
                    await GetAPMBackends(appEnv);
                    await GetServiceEndpoints(appEnv);

                }
            }
            return response.Content;
        }

        [HttpGet("GetSIMListOfMachines")]
        public async Task<string> GetSIMListOfMachines()
        {
            string url = this.baseURL + "sim/v2/user/machines?output=JSON";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                 foreach (var item in content)
                {
                    
                }
            }
            return response.Content;
        }

        [HttpGet("GetAPMNodes/{application}")]
        public async Task<string> GetAPMNodes(string application)
        {
            string url = this.baseURL + "rest/applications/" + application +"/nodes?output=JSON";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                 foreach (var item in content)
                {
                    
                }
            }
            return response.Content;
        }

        [HttpGet("GetAPMNodeProperties/{nodeID}")]
        public async Task<string> GetAPMNodeProperties(string nodeID)
        {
            string url = this.baseURL + "restui/nodeUiService/appAgentByNodeId/" + nodeID;
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                 foreach (var item in content)
                {
                    
                }
            }
            return response.Content;
        }

        [HttpGet("GetSIMListOfNodes")]
        public async Task<string> GetSIMListOfNodes()
        {
            string url = this.baseURL + "sim/v2/user/app/nodes";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
            }
            return response.Content;
        }

        [HttpGet("GetAPMConfigurationDetailsJSON/{application}")]
        public async Task<string> GetAPMConfigurationDetailsJSON(string application)
        {
            string url = this.baseURL + "restui/applicationManagerUiBean/applicationConfiguration/" + application;
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            //IRestResponse response = await client.ExecuteAsync(request);

            
            request.AddHeader("X-Events-API-AccountName",this.globalAccount);
            request.AddHeader("X-Events-API-Key",this.apiKey);
            request.AddHeader("Content-type", "application/vnd.appd.events+json;v=2");
            request.AddHeader("Accept","application/vnd.appd.events+json;v=2");
            
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                 foreach (var item in content)
                {
                    
                }
            }
            return response.Content;
        }

        [HttpGet("GetAPMBusinessTransactions/{appEnv}")]
        public async Task<string> GetAPMBusinessTransactions(string appEnv){
            string url = this.baseURL + "rest/applications/"+ appEnv  + "/business-transactions?output=JSON";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            JArray array = new JArray();  
            
            if (response.IsSuccessful)
            {
                JArray arrayTiers = new JArray();
                arrayTiers = await GetTiersAndNodes(appEnv);

                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                foreach (var item in content)
                {
                    int numberOfNodes = 0;

                    foreach (var tier in arrayTiers)
                    {
                        if(item.SelectToken("tierName").Value<string>() == tier.SelectToken("tierName").Value<string>()){
                            numberOfNodes = tier.SelectToken("numberOfNodes").Value<int>();
                        }
                    }

                   var jsonTest = new JObject(
                        new JProperty("appEnv", appEnv),
                        new JProperty("tierName", item.SelectToken("tierName").Value<string>()),
                        new JProperty("numberOfNodes",numberOfNodes),
                        new JProperty("metricType", item.SelectToken("entryPointType").Value<string>()),
                        new JProperty("metricName", item.SelectToken("name").Value<string>()),
                        new JProperty("metricDefinition", "BusinessTransaction")
                    );

                    array.Add(jsonTest);
                }
                
                PublishSchema(schemaName,array.ToString());
            }

            return array.ToString();
        }

        [HttpGet("GetAPMBackends/{appEnv}")]
        public async Task<string> GetAPMBackends(string appEnv) 
        {
            string url = this.baseURL + "rest/applications/"+ appEnv  + "/backends?output=JSON";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            JArray array = new JArray();
            
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                foreach (var item in content)
                {
                   var jsonTest = new JObject(
                        new JProperty("appEnv", appEnv),
                        new JProperty("tierName", item.SelectToken("tierId").Value<string>()),
                        new JProperty("metricType", item.SelectToken("exitPointType").Value<string>()),
                        new JProperty("metricName", item.SelectToken("name").Value<string>()),
                        new JProperty("metricDefinition", "Backend")
                    );

                    array.Add(jsonTest);
                }
                PublishSchema(schemaName,array.ToString());
            }

            return array.ToString();
        }

        [HttpGet("GetAPMTiers/{appEnv}")]
        public async Task<string> GetAPMTiers(string appEnv){
            string url = this.baseURL + "rest/applications/"+ appEnv  + "/tiers?output=JSON";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            JArray array = new JArray();
            
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

               /* foreach (var item in content)
                {
                    string metricPathValue = item.SelectToken("metricPath").Value<string>();
                    string[] textSplit = metricPathValue.Split("|");
                    
                    var jsonTest = new JObject(
                        new JProperty("appEnv", appEnv),
                        new JProperty("tierName", textSplit[1]),
                        new JProperty("metricType", ""),
                        new JProperty("metricName", textSplit[2]),
                        new JProperty("metricDefinition", "ServiceEndpoint")
                    );

                    array.Add(jsonTest);
                }
                PublishSchema(schemaName,array.ToString());*/
            }

            return response.Content;
        }

        public async Task<JArray> GetTiersAndNodes(string appEnv){
            string urlTiers = this.baseURL + "rest/applications/"+ appEnv  + "/tiers?output=JSON";
            var clientTiers = new RestClient(urlTiers);
            clientTiers.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var requestTiers = new RestRequest(Method.GET);
            IRestResponse responseTiers = await clientTiers.ExecuteAsync(requestTiers);
            JArray arrayTiers = new JArray();
                
            if (responseTiers.IsSuccessful)
            {
                var contentTiers = JsonConvert.DeserializeObject<JToken>(responseTiers.Content);

                foreach(var tier in contentTiers){
                    string tierName = tier.SelectToken("name").Value<string>();
                    string numberOfNodes = tier.SelectToken("numberOfNodes").Value<string>();

                    var jsonTier = new JObject(
                        new JProperty("tierName", tier.SelectToken("name").Value<string>()),
                        new JProperty("numberOfNodes", tier.SelectToken("numberOfNodes").Value<string>())
                    );

                arrayTiers.Add(jsonTier);
                }
            }
            return arrayTiers;
        }

        [HttpGet("GetServiceEndpoints/{appEnv}")]
        public async Task<string> GetServiceEndpoints(string appEnv){
            string url = this.baseURL + "rest/applications/"+ appEnv  + "/metric-data?metric-path=Service Endpoints|*|*|Calls per Minute&time-range-type=BEFORE_NOW&duration-in-mins=15&output=JSON";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            JArray array = new JArray();
            
            if (response.IsSuccessful)
            {
                JArray arrayTiers = new JArray();
                arrayTiers = await GetTiersAndNodes(appEnv);
                
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                
                foreach (var item in content)
                    {
                        string metricPathValue = item.SelectToken("metricPath").Value<string>();
                        string[] textSplit = metricPathValue.Split("|");
                        int numberOfNodes = 0;

                        foreach (var tier in arrayTiers)
                        {
                            if(textSplit[1] == tier.SelectToken("tierName").Value<string>()){
                                numberOfNodes = tier.SelectToken("numberOfNodes").Value<int>();
                            }
                        }
                        
                        var jsonTest = new JObject(
                            new JProperty("appEnv", appEnv),
                            new JProperty("tierName", textSplit[1]),
                            new JProperty("numberOfNodes",numberOfNodes),
                            new JProperty("metricType", ""),
                            new JProperty("metricName", textSplit[2]),
                            new JProperty("metricDefinition", "ServiceEndpoint")
                        );

                        array.Add(jsonTest);
                    }
                PublishSchema(schemaName,array.ToString());
            }
            return array.ToString();
        }

        [HttpGet("GetServiceEndpointsForDash/{appEnvId}")]
        public async Task<string> GetServiceEndpointsForDash(string appEnv)
        {
            string url = this.baseURL + "rest/applications/" + appEnv + "/metric-data?metric-path=Service Endpoints|*|*|Calls per Minute&time-range-type=BEFORE_NOW&duration-in-mins=15&output=JSON";
            
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            JObject main = new JObject();
            JArray array = new JArray();

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                int serieNumber = 1;
                
                foreach (var item in content)
                {
                    string metricPathValue = item.SelectToken("metricPath").Value<string>();

                    var jsonTest = new JObject(
                        new JProperty("seriesType", "LINE"),
                        new JProperty("metricType", "OTHER"),
                        new JProperty("showRawMetricName", false),
                        new JProperty("colorPalette", null),
                        new JProperty("name", "Series " + serieNumber),
                        new JProperty("metricMatchCriteriaTemplate",
                            new JObject(
                                new JProperty("entityMatchCriteria", null),
                                  new JProperty("metricExpressionTemplate",
                                    new JObject(
                                        new JProperty("metricExpressionType", "Absolute"),
                                        new JProperty("functionType", "VALUE"),
                                        new JProperty("displayName", "null"),
                                        new JProperty("inputMetricText", null),
                                        new JProperty("metricPath", metricPathValue),
                                        new JProperty("scopeEntity",
                                            new JObject(
                                                new JProperty("applicationName", "ESB_12C"),
                                                new JProperty("entityType", "APPLICATION"),
                                                new JProperty("entityName", "ESB_12C"),
                                                new JProperty("scopingEntityType", null),
                                                new JProperty("scopingEntityName", null),
                                                new JProperty("subtype", null)
                                            )
                                        )
                                    )
                                ),
                                new JProperty("rollupMetricData", false),
                                new JProperty("expressionString", ""),
                                new JProperty("useActiveBaseline", false),
                                new JProperty("sortResultsAscending", false),
                                new JProperty("maxResults", 300),
                                new JProperty("evaluationScopeType", null),
                                new JProperty("baselineName", null),
                                new JProperty("applicationName", "ESB_12C"),
                                new JProperty("metricDisplayNameStyle", "DISPLAY_STYLE_AUTO"),
                                new JProperty("metricDisplayNameCustomFormat", null)
                            )
                        ),
                        new JProperty("axisPosition", null)
                        );
                    array.Add(jsonTest);
                    serieNumber++;
                    //Console.WriteLine(jsonTest);    
                }
                
            }
            return array.ToString();
        }

        [HttpGet("GetServiceEndpointsByTier/{appEnv}/{tierName}")]
        public async Task<string> GetServiceEndpointsByTier(string appEnv, string tierName)
        {
            string url = this.baseURL + "rest/applications/" + appEnv + "/metric-data?metric-path=Service Endpoints|"+ tierName + "|*|Calls per Minute&time-range-type=BEFORE_NOW&duration-in-mins=15&output=JSON";

            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            JObject main = new JObject();
            JArray array = new JArray();

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                int serieNumber = 1;
                

                foreach (var item in content)
                {
                    string metricPathValue = item.SelectToken("metricPath").Value<string>();

                    var jsonTest = new JObject(
                        new JProperty("seriesType", "LINE"),
                        new JProperty("metricType", "OTHER"),
                        new JProperty("showRawMetricName", false),
                        new JProperty("colorPalette", null),
                        new JProperty("name", "Series " + serieNumber),
                        new JProperty("metricMatchCriteriaTemplate",
                            new JObject(
                                new JProperty("entityMatchCriteria", null),
                                  new JProperty("metricExpressionTemplate",
                                    new JObject(
                                        new JProperty("metricExpressionType", "Absolute"),
                                        new JProperty("functionType", "VALUE"),
                                        new JProperty("displayName", "null"),
                                        new JProperty("inputMetricText", null),
                                        new JProperty("metricPath", metricPathValue),
                                        new JProperty("scopeEntity",
                                            new JObject(
                                                new JProperty("applicationName", appEnv),
                                                new JProperty("entityType", "APPLICATION"),
                                                new JProperty("entityName", appEnv),
                                                new JProperty("scopingEntityType", null),
                                                new JProperty("scopingEntityName", null),
                                                new JProperty("subtype", null)
                                            )
                                        )
                                    )
                                ),
                                new JProperty("rollupMetricData", false),
                                new JProperty("expressionString", ""),
                                new JProperty("useActiveBaseline", false),
                                new JProperty("sortResultsAscending", false),
                                new JProperty("maxResults", 300),
                                new JProperty("evaluationScopeType", null),
                                new JProperty("baselineName", null),
                                new JProperty("applicationName", appEnv),
                                new JProperty("metricDisplayNameStyle", "DISPLAY_STYLE_AUTO"),
                                new JProperty("metricDisplayNameCustomFormat", null)
                            )
                        ),
                        new JProperty("axisPosition", null)
                        );
                    array.Add(jsonTest);
                    serieNumber++;
                    //Console.WriteLine(jsonTest);    
                }
                
            }
            return array.ToString();
        }

        [HttpGet("CreateSchema/{schema}")]
        public async Task<string> CreateSchema(string schema)
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
        public async Task<string> RetrieveSchema(string schema)
        {
           // var configTest = _customConfiguration; 
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

        [HttpGet("UpdateSchema/{schema}")]
        public async Task<string> UpdateSchema(string schema)
        {
            string url = this.analyticsURL + "events/schema/" + schema + "";
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("X-Events-API-AccountName",this.globalAccount);
            request.AddHeader("X-Events-API-Key",this.apiKey);
            request.AddHeader("Content-type", "application/vnd.appd.events+json;v=2");
            request.AddHeader("Accept","application/vnd.appd.events+json;v=2");

            /*using (StreamReader file = System.IO.File.OpenText(@"jsonFiles\updateSchema.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JSchema schema1 = JSchema.Load(reader);

                // validate JSON
                string schemaJson = schema1.ToString();
                Console.WriteLine(schemaJson);

                request.AddJsonBody(schemaJson);
               
            }*/
            string body = "[{ \"add\": { \"numberOfNodes\": \"integer\" } }]";
            request.AddJsonBody(body);

            IRestResponse response = client.Execute(request);

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

       // [HttpGet("PublishSchema/{schema}")]

       // public async Task<string> PublishSchema(string schema, string body)
        public async void PublishSchema(string schema, string body)
        {
            string url = this.analyticsURL + "events/publish/" + schema ;
            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(this.username, this.password);
            
            var request = new RestRequest(Method.POST);
            request.AddHeader("X-Events-API-AccountName",this.globalAccount);
            request.AddHeader("X-Events-API-Key",this.apiKey);
            request.AddHeader("Accept","application/vnd.appd.events+json;v=2");
            request.AddHeader("Content-type", "application/vnd.appd.events+json;v=2");
          
            body = body.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("[  {    \"","[{\"").Replace("\"  }]","\"}]").Replace(",    \"",",\"").Replace(",  {",",{");
            request.AddJsonBody(body);

            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine(response.Content);
            }else{
                Console.WriteLine(response.Content);
            }
            
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