using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWGestion.Models
{
    public class Parametro
    {
        public int ParametroId { get; set; }
        public string VcParamName { get; set; }
        public string VcParamValue { get; set; }
        public string VcParamStatus { get; set; }
    }
}
