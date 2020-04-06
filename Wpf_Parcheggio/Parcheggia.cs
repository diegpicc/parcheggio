using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Parcheggio
{
    [Serializable]
    class Parcheggia
    {
        public DateTime DataIngresso { get; private set; }
        public DateTime? DataUscita { get; set; }
        public int IdPosto { get; private set; }
        public string Targa { get; private set; }
        public decimal Costo { get; private set; }
        public Parcheggia(Posto p, Veicolo v) { DataIngresso = DateTime.Now; DataUscita = null; IdPosto = p.ID; Targa = v.Targa; }
    }
}
