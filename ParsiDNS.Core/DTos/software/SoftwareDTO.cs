using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsiDNS.Core.DTos.software
{
    public class SoftwareDTO
    {
        [Display(Name = "عنوان")]
        [Required]
        [MaxLength(150, ErrorMessage = "فیلد {0} نمی تواند بیش از {1} کاراکتر باشد.")]
        public required string Title { get; set; }
    }
}
