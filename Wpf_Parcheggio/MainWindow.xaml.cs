using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Parcheggio
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Parcheggio parcheggio = new Parcheggio();
        public MainWindow()
        {
            InitializeComponent();
            cbxTipoVeicolo.Items.Add("");
            cbxTipoPosto.Items.Add("");
            foreach (var tipo in Enum.GetValues(typeof(Tipo))) { cbxTipoVeicolo.Items.Add(tipo); cbxTipoPosto.Items.Add(tipo); }
            cbxTipoVeicolo.SelectedIndex = 0;

            ckbMostraLiberi.IsChecked = true;
            ckbMostraOccupati.IsChecked = true;
            ckbNonParcheggiati.IsChecked = true;
            ckbParcheggiati.IsChecked = true;
            tabVeicoliParcheggiati.ItemsSource = parcheggio.VisualizzaVeicoliParcheggiati();
            CaricaTabVeicoli();
            tabPosti.ItemsSource = parcheggio.VisualizzaPosti();
        }

        private void btnAggiungiVeicolo_Click(object sender, RoutedEventArgs e)
        {
            if (txtTarga.Text != "" && txtProprietario.Text != "" && cbxTipoVeicolo.Text != "")
            {
                try
                {
                    if (cbxTipoVeicolo.Text == Tipo.Auto.ToString())
                        if (parcheggio.AggiungiVeicolo(new Automobile(txtTarga.Text, txtProprietario.Text))) MessageBox.Show("Nuova auto aggiunta", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        else MessageBox.Show("L'auto che si vuole aggiungere esite già", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    else if (cbxTipoVeicolo.Text == Tipo.Moto.ToString())
                        if (parcheggio.AggiungiVeicolo(new Moto(txtTarga.Text, txtProprietario.Text))) MessageBox.Show("Nuova moto aggiunta", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        else MessageBox.Show("La moto che si vuole aggiungere esite già", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); }
                finally { PulisciFormVeicolo(); }
            }
            else MessageBox.Show("Compilare tutti i campi", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        public void PulisciFormVeicolo()
        {
            txtTarga.Clear();
            txtProprietario.Clear();
            cbxTipoVeicolo.Text = "";
            CaricaTabVeicoli();
        }
        //Form Veicoli
        private void FormVeicoli_Changed(object sender, EventArgs e)
        {
            tabVeicoli.ItemsSource = parcheggio.VisualizzaVeicoli(txtTarga.Text, txtProprietario.Text, cbxTipoVeicolo.Text, ckbParcheggiati.IsChecked.Value, ckbNonParcheggiati.IsChecked.Value);
        }
        private void CaricaTabVeicoli()
        {
            tabVeicoli.ItemsSource = parcheggio.VisualizzaVeicoli(txtTarga.Text, txtProprietario.Text, cbxTipoVeicolo.Text, ckbParcheggiati.IsChecked.Value, ckbNonParcheggiati.IsChecked.Value);
        }

        private void tabVeicoli_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && tabVeicoli.SelectedIndex != -1)
            {
                parcheggio.RimuoviVeicolo(((Veicolo)tabVeicoli.SelectedItem).Targa);
                CaricaTabVeicoli();
            }
        }

        private void btnAggiungiPosto_Click(object sender, RoutedEventArgs e)
        {
            if (txtNumeroPosti.Text != "" && cbxTipoPosto.Text != "")
            {
                try
                {
                    if (cbxTipoPosto.Text == Tipo.Auto.ToString())
                    {
                        parcheggio.AggiungiPosti<PostoAuto>(int.Parse(txtNumeroPosti.Text));
                        MessageBox.Show("Nuovi posti auto aggiunti", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (cbxTipoPosto.Text == Tipo.Moto.ToString())
                    {
                        parcheggio.AggiungiPosti<PostoMoto>(int.Parse(txtNumeroPosti.Text));
                        MessageBox.Show("Nuovi posti moto aggiunti", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error); }
                finally { PulisciFormPosti(); }
            }
            else MessageBox.Show("Compilare tutti i campi", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public void PulisciFormPosti()
        {
            txtNumeroPosti.Clear();
            cbxTipoPosto.Text = "";
            CaricaTabPosti();
        }

        private void CaricaTabPosti()
        {
            tabPosti.ItemsSource = parcheggio.VisualizzaPosti(cbxTipoPosto.Text, ckbMostraOccupati.IsChecked.Value, ckbMostraLiberi.IsChecked.Value);
        }

        private void tabVeicoli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabVeicoli.SelectedIndex != -1) txtTarga2.Text = ((Veicolo)tabVeicoli.SelectedItem).Targa;
        }

        private void tabPosti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabPosti.SelectedIndex != -1) txtID.Text = ((Posto)tabPosti.SelectedItem).ID.ToString();
        }
        //Form Posti
        private void cbxTipoPosto_SelectionChanged(object sender, SelectionChangedEventArgs e) { CaricaTabPosti(); }
        private void CheckboxPosti_Changed(object sender, RoutedEventArgs e) { CaricaTabPosti(); }

        private void btnRegistra_Click(object sender, RoutedEventArgs e)
        {
            if(tabPosti.SelectedIndex!=-1 && tabVeicoli.SelectedIndex!=-1) parcheggio.MezzoInEntrata((Posto)tabPosti.SelectedItem, (Veicolo)tabVeicoli.SelectedItem);
            tabVeicoliParcheggiati.ItemsSource = parcheggio.VisualizzaVeicoliParcheggiati();
        }

        private void tabVeicoliParcheggiati_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            tabVeicoliParcheggiati.ItemsSource = parcheggio.VisualizzaVeicoliParcheggiati();
        }
    }
    public class VeicoliParcheggiatiValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            Parcheggia p = (value as BindingGroup).Items[0] as Parcheggia;
            return p.DataUscita <= p.DataIngresso ? new ValidationResult(false, "") : ValidationResult.ValidResult;
        }
    }
}
