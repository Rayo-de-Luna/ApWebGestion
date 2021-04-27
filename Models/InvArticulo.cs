using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWGestion.Models
{
    public class InvArticulo
    {
        [Key]
        [Required(ErrorMessage = "Debe ingresar el modelo del artículo")]
        [MaxLength(100, ErrorMessage = "El modelo del artículo no debe exceder de 100 caracteres.")]
        public string ArtModelo  { get; set; }

        //Se supone que no es necesario que el artículo tenga una descripción, pero sí el nombre.
        [Required(ErrorMessage = "Debe ingresar el modelo del artículo")]
        [MaxLength(120, ErrorMessage = "La descripción del artículo no debe exceder de 120 caracteres.")]
        public string ArtDesc  { get; set; }

        [Required(ErrorMessage = "Debe seleccionarse un artículo del almacén.")]
        public int ArtId  { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad neta de artículos.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int CantidadNeta  { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad de artículos prestados.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int CantidadPrestada  { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad de artículos en el almacén.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int CantidadEnAlmacen  { get; set; }
    }
}