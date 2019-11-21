using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ItAintBoring.AzureFunction
{

    public class RegExPostData
    {
        public string pattern { get; set; }
        public string value { get; set; }
    }

    public static class RegExFunction
    {
        [FunctionName("RegEx")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            RegExPostData data = await req.Content.ReadAsAsync<RegExPostData>();

            //log.Info("C# HTTP trigger function processed a request.");

            string result = "";
           
            MatchCollection foundMatches = Regex.Matches(data.value, data.pattern, RegexOptions.IgnoreCase);
            foreach (Match m in foundMatches)
            {
                if (result != "") result += ";";
                result += m.Value;
            }
            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
