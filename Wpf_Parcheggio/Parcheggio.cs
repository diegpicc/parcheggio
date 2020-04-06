using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Parcheggio
{
    class Parcheggio
    {
        List<Posto> _posti = new List<Posto>();
        List<Veicolo> _veicoli = new List<Veicolo>();
        List<Parcheggia> _parcheggia = new List<Parcheggia>();

        public Parcheggio() { Carica(); }

        public Posto PostoLibero<T>() where T : Posto
        {
            List<Posto> _postiLiberi = new List<Posto>();
            foreach (Posto p in _posti) if (_parcheggia.Find(x => x.IdPosto == p.ID && x.DataUscita == null) == null) _postiLiberi.Add(p);
            return _postiLiberi.Count > 0 ? _postiLiberi[0] : null;
        }

        public List<Posto> VisualizzaPosti(string tipo = "", bool occupati = true, bool liberi = true)
        {
            return _posti.FindAll(x=> x.Tipo.ToString().ToUpper().Contains(tipo.ToUpper()));
        }
        public List<Veicolo> VisualizzaVeicoli(string targa = "", string proprietario = "", string tipo = "", bool parcheggiati = true, bool nonParcheggiati = true)
        {
            return _veicoli.FindAll(x => x.Targa.ToUpper().Contains(targa.ToUpper()) && x.Proprietario.ToUpper().Contains(proprietario.ToUpper()) && x.Tipo.ToString().ToUpper().Contains(tipo.ToUpper()));
        }
        public List<Parcheggia> VisualizzaVeicoliParcheggiati()
        {
            return _parcheggia.FindAll(x=>true);
        }
        public void MezzoInEntrata(Posto p, Veicolo v)
        {
            _parcheggia.Add(new Parcheggia(p, v));
            Salva();
        }

        public double MezzoInUscita(Veicolo v)
        {
            Parcheggia p = (_parcheggia.Find(x => x.DataUscita == null && x.Targa == v.Targa));
            p.DataUscita = DateTime.Now;
            return (p.DataUscita.Value - p.DataIngresso).Hours * GetPosto(p.IdPosto).TariffaOraria;
        }

        public bool AggiungiVeicolo(Veicolo v)
        {
            bool ok = true;
            if (_veicoli.Find(x => x.Targa.ToUpper() == v.Targa.ToUpper()) == null) { _veicoli.Add(v); Salva(); }
            else ok = false;
            return ok;
        }

        public Veicolo TrovaVeicoloTarga(string targa)
        {
            return _veicoli.Find(x => x.Targa == targa);
        }

        public List<Veicolo> TrovaVeicoliProprietario(string proprietario)
        {
            return _veicoli.FindAll(x => x.Proprietario.ToUpper().Contains(proprietario.ToUpper()));
        }

        public void RimuoviVeicolo(string targa)
        {
            if (_parcheggia.Find(x => x.Targa == targa) == null && _veicoli.Find(x => x.Targa == targa) != null)
            {
                _veicoli.Remove(_veicoli.Find(x => x.Targa == targa));
                Salva();
            }
        }

        public Posto GetPosto(int id) { return _posti.Find(x => x.ID == id); }

        public void AggiungiPosti<T>(int n) where T:Posto , new()
        {
            for (int i = 0; i < n; i++) _posti.Add(new T());
            Salva();
        }

        private void Salva()
        {
            IFormatter f = new BinaryFormatter();
            Stream file = new FileStream("posti.dat", FileMode.Create);
            f.Serialize(file, _posti);
            file.Close();

            file = new FileStream("parcheggia.dat", FileMode.Create);
            f.Serialize(file, _parcheggia);
            file.Close();

            file = new FileStream("veicoli.dat", FileMode.Create);
            f.Serialize(file, _veicoli);
            file.Close();
        }
        /// <summary>
        /// Carica la lista da un file
        /// </summary>
        private void Carica()
        {
            IFormatter f = new BinaryFormatter();
            if (File.Exists("posti.dat"))
            {
                Stream file = new FileStream("posti.dat", FileMode.Open);
                _posti = (List<Posto>)f.Deserialize(file);
                if (_posti.Count > 0)
                {
                    int max = _posti[0].ID;
                    for (int i = 0; i < _posti.Count; i++) if (max < _posti[i].ID) max = _posti[i].ID;
                    Posto.AUTO_INCREMENT = max + 1;
                }
                file.Close();
            }

            if (File.Exists("parcheggia.dat"))
            {
                Stream file = new FileStream("parcheggia.dat", FileMode.Open);
                _parcheggia = (List<Parcheggia>)f.Deserialize(file);
                file.Close();
            }

            if (File.Exists("veicoli.dat"))
            {
                Stream file = new FileStream("veicoli.dat", FileMode.Open);
                _veicoli = (List<Veicolo>)f.Deserialize(file);
                file.Close();
            }
        }


    }
}
