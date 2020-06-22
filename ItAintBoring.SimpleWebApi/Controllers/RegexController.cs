using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ItAintBoring.SimpleWebApi.Controllers
{
    public class RegexData
    {
        public string pattern { get; set; }
        public string value { get; set; }
    }

    public class RegexResult
    {
        public string data { get; set; }
        
    }

    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegexController : ControllerBase
    {
        
        // POST api/regex
        [HttpPost]
        public ActionResult<RegexResult> Post([FromBody] RegexData data)
        {
            RegexResult result = new RegexResult();
            MatchCollection foundMatches = Regex.Matches(data.value, data.pattern, RegexOptions.IgnoreCase);
            foreach (Match m in foundMatches)
            {
                
                if (result.data != "" && result.data != null) result.data += ";";
                //result.data += m.Value;
                result.data += m.Groups[1].Value.Trim();
            }
            return result;
        }

        [HttpGet]
        public string Test()
        {
            return "ok";
        }


    }
}
