using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.App.Moment.SDK.Requests.Moments
{
    public class Moment
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string publish_date { get; set; }
        public string cover { get; set; }
    }
}
