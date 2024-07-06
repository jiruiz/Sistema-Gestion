using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    internal class EntidadDeposito
    {
        public int Id { get; set; }
        public DateTime FechaDeposito { get; set; }
        public float ImporteDeposito { get; set; }
        public string BancoDeposito { get; set; }
        public string ObservacionesDepositos { get; set; }

        public int idAgencia { get; set; }
        public virtual Agencia Agencia { get; set; }
    }
}
