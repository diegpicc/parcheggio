using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Parcheggio
{
    [Serializable]
    class PostoMoto : Posto
    {
        public PostoMoto():base()
        {
            TariffaOraria = 5;
            Tipo = Tipo.Moto;
        }
    }
}
