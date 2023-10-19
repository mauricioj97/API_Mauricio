using System.ComponentModel.DataAnnotations;

namespace LibraryAPI_Mauricio.DAL.Entities
{
    public class AuditBase
    {
        [Key] // PK
        [Required] // CAMPO OBLIGATORIO
        public virtual Guid Id { get; set; } // PK TODAS LAS TABLAS

        public virtual DateTime? CreatedDate { get; set; } // PARA GUARDAR TODO REGISTRO CON SU FECHA

        public virtual DateTime? ModifiedDate { get; set; } // PARA GUARDAR TODO REGISTRO CON SU FECHA DE MODIFICACION

    }
}
