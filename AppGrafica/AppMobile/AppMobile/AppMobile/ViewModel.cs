using Newtonsoft.Json.Linq;
using proj;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppMobile
{
    public class ViewModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public IList<string> Emozioni
        {
            get
            {
                return new List<string> { "Arrabbiato", "Felice", "Triste", "Disgustato", "Spaventato", "Sorpreso", "Neutro"  };
            }
        }

        enum Emozione
        {
            Arrabbiato = 1,
            Felice = 2,
            Triste = 3,
            Disgustato = 4,
            Spaventato = 5,
            Sorpreso = 6,
            Neutro = 7,
        }

        public ObservableCollection<Model> DataPie { get; set; }

        private ObservableCollection<Model> _DataLine;
        public ObservableCollection<Model> DataLine 
        {   get { return _DataLine; }
            set { _DataLine = value; NotifyPropertyChanged("DataLine"); }
        }

        public ViewModel()
        {
            DataPie = new ObservableCollection<Model>();
            DataLine = new ObservableCollection<Model>();

            FillDataPie();
            FillDataLine("1");
        }

        private void FillDataPie()
        {
            List<JObject> list = (List<JObject>)Utente.GetEmozioni();

            list.Sort(delegate (JObject x, JObject y)
            {
                string id1 = (string)x["IdEmozione"];
                string id2 = (string)y["IdEmozione"];
                return id1.CompareTo(id2);
            });

            int count = 0;
            for(int i = 0; i < list.Count - 1; i++)
            {
                if (int.Parse((string)list[i]["IdEmozione"]) != int.Parse((string)list[i + 1]["IdEmozione"]))
                {
                    count++;
                    Model model = new Model()
                    {
                        xValue = Enum.GetName(typeof(Emozione), int.Parse((string)list[i]["IdEmozione"])), //prendo il valore dell'enum
                        yValue = count
                    };
                    DataPie.Add(model);
                    count = 0;
                }
                else
                    count++; 
            }

            count++;
            Model model2 = new Model()
            {
                xValue = Enum.GetName(typeof(Emozione), int.Parse((string)list[list.Count - 1]["IdEmozione"])), //prendo il valore dell'enum
                yValue = count
            };
            DataPie.Add(model2);
        }

        public void FillDataLine(string emozione)
        {
            DataLine.Clear();
            List<JObject> list = (List<JObject>)Utente.GetEmozioni(DateTime.Now.Subtract(TimeSpan.FromDays(30)).ToString("yyyy-MM-ddT"), int.Parse(emozione) + 1);

            list.Sort(delegate (JObject x, JObject y)
            {
                string id1 = x["DataRilevazione"].ToString().Substring(0, 10);
                string id2 = y["DataRilevazione"].ToString().Substring(0, 10);
                return id2.CompareTo(id1);
            });

            int count = 0;
            for (int i = 0; i < list.Count - 1; i++)
            {
                if ((string)list[i]["DataRilevazione"].ToString().Substring(0, 10) != (string)list[i + 1]["DataRilevazione"].ToString().Substring(0, 10))
                {
                    count++;
                    Model model = new Model()
                    {
                        xValue = list[i]["DataRilevazione"].ToString().Substring(0, 10), //prendo il valore dell'enum
                        yValue = count
                    };
                    DataLine.Add(model);
                    count = 0;
                }
                else
                    count++;
            }

            count++;
            Model model2 = new Model()
            {
                xValue = list[list.Count - 1]["DataRilevazione"].ToString().Substring(0, 10), //prendo il valore dell'enum
                yValue = count
            };
            DataLine.Add(model2);


        }
    }
}