using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsiDNS.Core.DTos.dns
{
    public class DnsDTO
    {
        [Required]
        public int Id { get; set; }
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
    }
}
