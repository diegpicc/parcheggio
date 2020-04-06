using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wpf_Parcheggio
{
    public enum Tipo { Auto, Moto }
    [Serializable]
    class Automobile : Veicolo
    {
        public Automobile(string targa, string proprietario)
        {
            if (!ControllaTarga(targa)) throw new Exception("Targa non corretta");
            Targa = targa.ToUpper();
            Proprietario = proprietario;
            Tipo = Tipo.Auto;
        }
        public static bool ControllaTarga(string targa)
        {
            return targa.Length == 7 && char.IsLetter(targa[0]) && char.IsLetter(targa[1]) && char.IsNumber(targa[2]) && char.IsNumber(targa[3]) && char.IsNumber(targa[4]) && char.IsLetter(targa[5]) && char.IsLetter(targa[6]);
        }
    }
}
