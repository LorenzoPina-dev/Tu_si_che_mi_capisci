using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using proj;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkillPage : ContentPage
    {
        Utente utente;
        List<JObject> list, list2;
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
            string id = "";
            for(int i = 0; i < list.Count; i++)
            {
               if(list[i]["Nome"].ToString() == pickerSkill.SelectedItem.ToString())
               {
                   id = list[i]["Id"].ToString();
                   utente.DeleteSkill(id);
                   App.Current.MainPage = new TabbedPage1();
               }
            }
        }

        public void EmChange(object sender, EventArgs e)
        {
            Nome.Text = "";
            Descrizione.Text = "";
            Azione.Text = "";
            pickerSkill.Items.Clear();
            var picker = sender as Picker;
            list = (List<JObject>)utente.GetSkills((picker.SelectedIndex + 1).ToString());
            for(int i = 0; i < list.Count; i++)
            {
                pickerSkill.Items.Add((string)list[i]["Nome"]);
            }
        }

        public void SkillChange(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            for (int i = 0; i < list.Count; i++)
            {
                if(picker.Items.Count == list.Count)
                {
                    if (list[i]["Nome"].ToString() == picker.SelectedItem.ToString())
                    {
                        Nome.Text = list[i]["Nome"].ToString();
                        Descrizione.Text = list[i]["Descrizione"].ToString();
                        Azione.Text = list[i]["Azione"].ToString();
                    }
                }
                
            }

        }
    }
}