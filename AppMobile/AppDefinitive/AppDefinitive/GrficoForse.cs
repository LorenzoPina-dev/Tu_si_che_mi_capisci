using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppDefinitive
{
    public class GrficoForse
    {
        public string Month { get; set; }

        public double Target { get; set; }

        public GrficoForse(string xValue, double yValue)
        {
            Month = xValue;
            Target = yValue;
        }
    }

    public class ViewModel_1 {

        public ObservableCollection<GrficoForse> Data { get; set; }

        public ViewModel_1() {

            Data = new ObservableCollection<GrficoForse>()
        {
            new GrficoForse("Jan", 50),
            new GrficoForse("Feb", 70),
            new GrficoForse("Mar", 65),
            new GrficoForse("Apr", 57),
            new GrficoForse("May", 48),
        };
        }


    }
}
