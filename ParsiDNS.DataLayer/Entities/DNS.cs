using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ParsiDNS.DataLayer.Entities
{
    public class DNS
    {
        [Key]
        public int DnsId { get; set; }

        [Display(Name = "عنوان")]
        [Required]
        [MaxLength(150, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کاراکتر باشد.")]
        public string Title { get; set; }

        [Display(Name = "دی ان اس 1")]
        [Required]
        [MaxLength(150, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کاراکتر باشد.")]
        public string Dns1 { get; set; }

        [Display(Name = "دی ان اس 2")]
        [Required]
        [MaxLength(150, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کاراکتر باشد.")]
        public string Dns2 { get; set; }


        //navigation properties
        public ICollection<DnsSoftwares> DnsSoftwares { get; set; }
    }
}
