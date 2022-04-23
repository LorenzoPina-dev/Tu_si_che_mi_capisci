using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proj;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class addSkill : ContentPage
    {
        Utente utente;
        public addSkill()
        {
            utente = new Utente();
            InitializeComponent();
        }
        public void Indietro(object sender, EventArgs args)
        {
            App.Current.MainPage = new TabbedPage1();
        }

        public void Aggiungi(object sender, EventArgs args)
        {
            string nome = Nome.Text.ToString();
            string descrizione = Descrizione.Text.ToString();
            string azione = Azione.Text.ToString();
            int idEmozione = int.Parse(picker.SelectedIndex.ToString()) + 1;
            utente.AddSkill(nome, descrizione, azione, idEmozione.ToString());
        }
    }
}