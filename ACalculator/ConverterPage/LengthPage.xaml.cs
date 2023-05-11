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
	public partial class LengthPage : ContentPage
	{
		public LengthPage ()
		{
            InitializeComponent();
            Btn_addsubtract.IsEnabled = false;

            //ConverterClass cc = new ConverterClass();
            //picker.ItemsSource = cc.converterLength;
            //picker.SelectedIndex = 4;
            //picker1.ItemsSource = cc.converterLength;
            //picker1.SelectedIndex = 1;
        }
	}
}