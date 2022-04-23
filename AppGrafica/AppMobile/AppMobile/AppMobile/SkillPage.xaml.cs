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
    public partial class SkillPage : ContentPage
    {
        Utente utente;
        public SkillPage()
        {
            utente = new Utente();
            InitializeComponent();
        }

        public void Aggiungi(object sender, EventArgs e)
        {
            App.Current.MainPage = new addSkill();
        }

        public void Rimuovi(object sender, EventArgs e)
        {
            /*for(int i = 0; i < list.Count; i++)
           {
               if(list[i]["Id"].ToString() == picker.SelectedItem.ToString())
               {
                   id = list[i]["Id"].ToString();
                   utente.DeleteSkill(id);
               }
           }*/
        }

        public void EmChange(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            utente.GetSkills();
        }
    }
}