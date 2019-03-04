using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Lib.Entity
{
    public class Sorteio
    {
        public int CartaID { get; set; }
        public Carta Carta { get; set; }
        public Guid IdentificadorSorteio { get; set; }
        public int ID { get; set; }
        public bool NaoRepetir { get; set; }
    }
}
