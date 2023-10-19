using System.ComponentModel.DataAnnotations;

namespace LibraryAPI_Mauricio.DAL.Entities
{
    public class Book : AuditBase
    {
        [Required] // CAMPO OBLIGATORIO
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres.")] // LONGITUD MAX
        [Display(Name = "Libro")] // PARA IDENTIFICAR EL NOMBRE
        public string Name { get; set; }

    }
}
