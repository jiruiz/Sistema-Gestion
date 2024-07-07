using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class EntidadDeposito
    {
        public int Id { get; set; } // Debe ser una propiedad de identidad en la base de datos
        public DateTime FechaDeposito { get; set; }
        public double ImporteDeposito { get; set; }
        public string BancoDeposito { get; set; }
        public string ObservacionesDepositos { get; set; }
        public int? IdAgencia { get; set; } // Permitir valores nulos para claves externas
        public virtual Agencia Agencia { get; set; }
    }

}
