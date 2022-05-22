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
    public class ViewModel 
    {

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

        public ObservableCollection<Model> DataLineArr { get; set; }
        public ObservableCollection<Model> DataLineFel { get; set; }
        public ObservableCollection<Model> DataLineTriste { get; set; }
        public ObservableCollection<Model> DataLineDisg { get; set; }
        public ObservableCollection<Model> DataLineSpav { get; set; }
        public ObservableCollection<Model> DataLineSorp { get; set; }
        public ObservableCollection<Model> DataLineNeutro { get; set; }


        public ViewModel()
        {
            DataPie = new ObservableCollection<Model>();
            FillDataPie();
            DataLineArr = new ObservableCollection<Model>();
            FillDataLine("0", DataLineArr);
            DataLineFel = new ObservableCollection<Model>();
            FillDataLine("1", DataLineFel);
            DataLineTriste = new ObservableCollection<Model>();
            FillDataLine("2", DataLineTriste);
            DataLineDisg = new ObservableCollection<Model>();
            FillDataLine("3", DataLineDisg);
            DataLineSpav = new ObservableCollection<Model>();
            FillDataLine("4", DataLineSpav);
            DataLineSorp = new ObservableCollection<Model>();
            FillDataLine("5", DataLineSorp);
            DataLineNeutro = new ObservableCollection<Model>();
            FillDataLine("6", DataLineNeutro);
        }

        public void FillDataPie()
        {
            List<JObject> list = (List<JObject>)Utente.GetEmozioni();

            list.Sort(delegate (JObject x, JObject y)
            {
                string id1 = (string)x["IdEmozione"];
                string id2 = (string)y["IdEmozione"];
                return id1.CompareTo(id2);
            });

            int count = 0;
            Model model2 = new Model();
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count - 1; i++)
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
                model2 = new Model()
                {
                    xValue = Enum.GetName(typeof(Emozione), int.Parse((string)list[list.Count - 1]["IdEmozione"])), //prendo il valore dell'enum
                    yValue = count
                };
            }
            DataPie.Add(model2);
        }

        public void FillDataLine(string emozione, ObservableCollection<Model> Data)
        {
            List<JObject> list = (List<JObject>)Utente.GetEmozioni(DateTime.Now.Subtract(TimeSpan.FromDays(30)).ToString("yyyy-MM-ddT"), int.Parse(emozione) + 1);

            list.Sort(delegate (JObject x, JObject y)
            {
                string id1 = x["DataRilevazione"].ToString();
                string id2 = y["DataRilevazione"].ToString();
                return id1.CompareTo(id2);
            });

            int count = 0;

            Model model2 = new Model();
            if(list.Count > 0)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if ((string)list[i]["DataRilevazione"].ToString().Substring(0, 10) != (string)list[i + 1]["DataRilevazione"].ToString().Substring(0, 10))
                    {
                        count++;
                        Model model = new Model()
                        {
                            xValue = list[i]["DataRilevazione"].ToString().Substring(0, 2), 
                            yValue = count
                        };
                        Data.Add(model);
                        count = 0;
                    }
                    else
                        count++;
                }

                count++;
                model2 = new Model()
                {
                    xValue = list[list.Count - 1]["DataRilevazione"].ToString().Substring(0, 2),
                    yValue = count
                };
            }
            Data.Add(model2);
        }
    }
}