using System;
using System.ComponentModel.DataAnnotations;

namespace AWGestion.Models
{
    public class Salida
    {

        [Required(ErrorMessage = "Debe seleccionar el número de modelo.")]
        public int SalidaId  { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el número de modelo.")]
        public string ArtModelo { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el proyecto.")]
        public int ProyectoId { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad de artículos a préstamo.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un número válido")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de Entrada.")]
        [DataType(DataType.DateTime, ErrorMessage = "Debe ingresar una fecha válida")]
        public DateTime Fecha { get; set; }
    }
}