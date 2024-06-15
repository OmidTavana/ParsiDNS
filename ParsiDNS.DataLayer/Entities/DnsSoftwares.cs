using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsiDNS.DataLayer.Entities
{
    public class DnsSoftwares
    {
        [Key]
        //DS => DNS SOFTWARE
        public int DS_Id { get; set; }

        [Required]
        public int SoftwareId { get; set; }

        [Required]
        public int DnsId { get; set; }

        public int TotalLikeCount { get; set; } = 0;
        public int LastMonthLikeCount { get; set; } = 0;
        public int LastWeekLikeCount { get; set; } = 0;

        //navigation properties
        [ForeignKey("DnsId")]
        public DNS DNS { get; set; }

        [ForeignKey("SoftwareId")]
        public Software Software { get; set; }
    }
}
