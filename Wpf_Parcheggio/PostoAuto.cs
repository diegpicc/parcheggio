using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Parcheggio
{
    [Serializable]
    class PostoAuto : Posto
    {
        public PostoAuto()
        {
            TariffaOraria = 10;
            Tipo = Tipo.Auto;
        }
    }
}
