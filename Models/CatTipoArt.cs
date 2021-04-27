using System.ComponentModel.DataAnnotations;

namespace AWGestion.Models
{
    public class CatTipoArt
    {
        [Key]
        public int TipoArtId { get; set; }

        [Required(ErrorMessage = "Debe escribir el nombre del tipo de artículo.")]
        [MaxLength(30, ErrorMessage = "El nombre de tipo de artículo no debe exceder de 30 caracteres.")]
        public string TipoArtDesc { get; set; }
    }
}