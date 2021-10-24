using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfWealthTest
{   
        public interface IHolidaysApiService
        {
            Task<List<HolidayModel>> GetHolidays(string countryCode);
        }    
}
