using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.IDP.Infrastructure.ViewModels
{
    public class PermissionAddModel
    {
        [Required]
        public string Function { get; set; }
        [Required]
        public string Command { get; set; }


    }
}
