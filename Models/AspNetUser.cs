using System.ComponentModel.DataAnnotations;

namespace AWGestion.Models
{
    public class AspNetUser
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Nombres { get; set; }
        public string ApPat { get; set; }
        public string ApMat { get; set; }
        public string RFC { get; set; }
    }
}