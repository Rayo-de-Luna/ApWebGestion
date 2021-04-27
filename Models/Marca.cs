using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AWGestion.Models
{
    public class Marca
    {
        public int MarcaId { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de la marca.")]
        [MaxLength(35, ErrorMessage = "El nombre de la marca no puede exeder de 35 caracteres.")]
        public string VcMarcaName { get; set; }

        [Required(ErrorMessage = "Debe ingresar el estatus de la marca.")]
        public string VcMarcaStatus { get; set; }
    }
}