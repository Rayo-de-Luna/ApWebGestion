using System.ComponentModel.DataAnnotations;

namespace AWGestion.Models
{
    public class Empresa
    {
        [Key]
        [Required(ErrorMessage = "Debe ingresar un código RFC válido.")]
        [StringLength(12, ErrorMessage = "El código RFC debe contener 12 caracteres exactamente.", MinimumLength = 12)]
        public string EmpresaRfc { get; set; }

        [Required(ErrorMessage = "Debe ingresar la razón social de la empresa.")]
        [MaxLength(150, ErrorMessage = "La razón social de la empresa no debe exceder de 150 caracteres.")]
        public string EmpresaRazSoc { get; set; }

        [Required(ErrorMessage = "Debe ingresar la calle de la empresa.")]
        [MaxLength(150, ErrorMessage = "La calle de la empresa no debe exceder de 150 caracteres.")]
        public string EmpresaCalle { get; set; }

        [Required(ErrorMessage = "Debe ingresar el número de interior de la empresa.")]
        [MaxLength(10, ErrorMessage = "El número de interior no puede exceder de 10 caracteres")]
        public string EmpresaNumInt { get; set; }

        [Required(ErrorMessage = "Debe ingresar el número de calle de la empresa.")]
        [MaxLength(10, ErrorMessage = "El número de interior no puede exceder de 10 caracteres")]
        public string EmpresaNumExt { get; set; }

        [Required(ErrorMessage = "Debe ingresar el código postal de la empresa.")]
        [Range(1, 99999, ErrorMessage = "Debe ingresar un número válido.")]
        [MaxLength(5)]
        public string EmpresaCp { get; set; }

        [Required(ErrorMessage = "Debe ingresar la ciudad de la empresa.")]
        [MaxLength(30, ErrorMessage = "La ciudad de la empresa no debe exceder de 30 caracteres.")]
        public string EmpresaCd { get; set; }

        [Required(ErrorMessage = "Debe ingresar el estado de la empresa.")]
        [MaxLength(30, ErrorMessage = "El estado de la empresa no debe exceder de 30 cacracteres.")]
        public string EmpresaEdo { get; set; }
    }
}