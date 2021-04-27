using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AWGestion.Models
{
    public class AspNetRole
    {
        [Key]
        [Required(ErrorMessage = "Debe ingresar el nombre del rol")]
        public string Name { get; set; }
    }
}
