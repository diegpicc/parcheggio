using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Parcheggio
{
    [Serializable]
    class Moto : Veicolo
    {
        public Moto(string targa, string proprietario)
        {
            if (!ControllaTarga(targa)) throw new Exception("Targa non corretta");
            Targa = targa.ToUpper();
            Proprietario = proprietario;
            Tipo = Tipo.Moto;
        }
        public static bool ControllaTarga(string targa)
        {
            return targa.Length == 7 && char.IsLetter(targa[0]) && char.IsLetter(targa[1]) && char.IsNumber(targa[2]) && char.IsNumber(targa[3]) && char.IsNumber(targa[4]) && char.IsNumber(targa[5]) && char.IsNumber(targa[6]);
        }
    }
}
