using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using System.Text.RegularExpressions;

namespace ItAintBoring.RegexCustomAction
{
    public class RegexActionPlugin: IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            string result = "";
            if (context.InputParameters.Contains("Pattern") &&
                context.InputParameters.Contains("Data"))
            {
                string pattern = (string)context.InputParameters["Pattern"];
                string data = (string)context.InputParameters["Data"];

                MatchCollection foundMatches = Regex.Matches(data, pattern, RegexOptions.IgnoreCase);
                foreach (Match m in foundMatches)
                {
                    if (result != "") result += ";";
                    result += m.Value;
                }
                
            }
            context.OutputParameters["Result"] = result;
        }
    }

}
