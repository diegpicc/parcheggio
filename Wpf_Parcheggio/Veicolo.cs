using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Parcheggio
{
    [Serializable]
    abstract class Veicolo
    {
        public string Targa { get; protected set; }
        public string Proprietario { get; protected set; }
        public Tipo Tipo { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Veicolo) ) return false;
            return Targa == ((Veicolo)obj).Targa;
        }
    }
}
