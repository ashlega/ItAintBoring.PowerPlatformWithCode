using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ItAintBoring.SimpleWebApi.Controllers
{
    public class DateTimeData
    {
        public DateTime date { get; set; }
        public int days { get; set; }
        public DateTime[] holidays { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiController]
    public class AddBusinessDaysController : ControllerBase
    {
        

        bool DateInArray(DateTime date, DateTime[] holidays)
        {
            bool result = false;
            if (holidays != null)
            {
                foreach(var h in holidays)
                {
                    if (h.DayOfYear == date.DayOfYear)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
        // POST api/addbusinessdays
        [HttpPost]
        public ActionResult<DateTime> Post([FromBody] DateTimeData data)
        {
            DateTime result = data.date;
            int days = data.days;
            DateTime[] holidays = data.holidays;
            
            if (days < 0)
            {
                throw new ArgumentException("Days cannot be negative", "days");
            }

            while (days > 0)
            {
                result = result.AddDays(1);
                if (result.DayOfWeek != DayOfWeek.Saturday 
                    && result.DayOfWeek != DayOfWeek.Sunday 
                    && !DateInArray(result, holidays))
                {
                    days -= 1;
                }
            }

            return result;
        }

        
    }
}
