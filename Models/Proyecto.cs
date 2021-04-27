using System.ComponentModel.DataAnnotations;

namespace AWGestion.Models
{
    public class Proyecto
    {
        [Key]
        public int ProyectoId { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del proyecto.")]
        [MaxLength(100, ErrorMessage = "El nombre del proyecto no puede exceder de 100 caracteres.")]
        public string ProyectoName { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el RFC de la empresa")]
        [MaxLength(100, ErrorMessage = "El RFC de la empresa no puede exceder de 100 caracteres.")]
        public string EmpresaRfc { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el responsable del proyecto.")]
        [MaxLength(100, ErrorMessage = "El nombre del responsable no puede exceder de 100 caracteres.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor de autorización de salida")]
        [MaxLength(2, ErrorMessage = "El valor de autorización de salida no puede exceder de 2 caracteres.")]
        public string AuthSalida { get; set; }

        [Required(ErrorMessage = "Debe ingresar el valor de autorización de entrada")]
        [MaxLength(2, ErrorMessage = "El valor de autorización de entrada no puede exceder de 2 caracteres.")]
        public string AuthEntrada { get; set; }
    }
}