using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAppTest
{
    public class UserModel
    {
        public int id { get; set; }
        public string name { get; set; }

        public string login { get; set; }

        public string company { get; set; }

        public int followers { get; set; }

        public int public_repos { get; set; }
    }
}
