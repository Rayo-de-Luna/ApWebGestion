using System.ComponentModel.DataAnnotations;

namespace AWGestion.Models
{
    public class CatArticulo
    {
        [Key]
        public int ArtId { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del artículo.")]
        [MaxLength(30, ErrorMessage = "El nombre del artículo no puede exceder de 30 caracteres.")]
        public string ArtNombre { get; set; }

        [Required(ErrorMessage = "Debe seleccionar alguna marca.")]
        public int MarcaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el tipo de artículo.")]
        public int TipoArtId { get; set; }
    }
}