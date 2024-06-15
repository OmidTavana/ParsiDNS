using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsiDNS.Core.DTos.software
{
    public class DnsSoftwareDTO
    {
        public string DnsSoftware { get; set; }
        public int TotalLikeCount { get; set; }
        public int LastMonthLikeCount { get; set; }
        public int LastWeekLikeCount { get; set; }
    }
}
