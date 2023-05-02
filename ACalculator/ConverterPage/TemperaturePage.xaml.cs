using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ACalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemperaturePage : ContentPage
    {
        public TemperaturePage()
        {
            InitializeComponent();

            ConverterClass cc = new ConverterClass();
            picker.ItemsSource = cc.ConverterTemperature;
            picker.SelectedIndex = 1;
            picker1.ItemsSource = cc.ConverterTemperature;
            picker1.SelectedIndex = 0;
        }
    }
}