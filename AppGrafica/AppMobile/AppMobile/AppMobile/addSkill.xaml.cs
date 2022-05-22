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
            TabbedPage1 tabbedPage1 = new TabbedPage1();
            tabbedPage1.CurrentPage = tabbedPage1.Children[1];
            (App.Current as App).MainPage = tabbedPage1;
        }

        public void Aggiungi(object sender, EventArgs args)
        {
            string nome = Nome.Text;
            string descrizione = Descrizione.Text.ToString();
            string azione = Azione.Text.ToString();
            int idEmozione = int.Parse(picker.SelectedIndex.ToString()) + 1;

            if (nome != "" && descrizione != "" && azione != "" && picker.SelectedIndex != -1)
            {
                utente.AddSkill(nome, descrizione, azione, idEmozione.ToString());
                TabbedPage1 tabbedPage1 = new TabbedPage1();
                tabbedPage1.CurrentPage = tabbedPage1.Children[1];
                (App.Current as App).MainPage = tabbedPage1;
            }
            else
                error.Text = "compilare tutti i campi";
            
        }
    }
}