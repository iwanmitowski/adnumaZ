using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.ViewModels
{
    public class BanInputModel
    {
        public string UserId { get; set; }

        [Required]
        public string BanReason { get; set; }
    }
}
