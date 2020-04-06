using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Parcheggio
{
    [Serializable]
    abstract class Posto
    {
        public static int AUTO_INCREMENT = 1;
        public int ID { get; protected set; }
        public Tipo Tipo { get; protected set; }
        public double TariffaOraria { get; protected set; }

        public Posto() { ID = AUTO_INCREMENT++; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Posto)) return false;
            return ID == ((Posto)obj).ID;
        }
    }
}
