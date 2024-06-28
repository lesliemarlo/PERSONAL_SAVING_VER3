using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Presentacion.Models
{
    public class EgresoModel
    {
        [DisplayName("Código")]
        public int codigo { get; set; }

        [DisplayName("Fecha de registro")]
        public DateTime fecha { get; set; }

        [DisplayName("Monto egresado")]
        public double monto { get; set; }

        [DisplayName("Descripción del egreso")]
        public string? descripcion { get; set; }
    }
}
