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
    public partial class Converter : ContentPage
    {
        //public int WidthInDp { get; }
        //public int HeightInDp { get; }
        public Converter() //int widthInDp, int heightInDp
        {
            InitializeComponent();
            //WidthInDp = widthInDp;
            //HeightInDp = heightInDp;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new MainPage(1, 1));
            await Navigation.PopModalAsync(true);
        }
    }
}