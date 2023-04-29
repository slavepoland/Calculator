using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ACalculator
{
    public partial class App : Application
    {
        public App(int widthInDp, int heightInDp)
        {
            InitializeComponent();

            MainPage = new MainPage(widthInDp, heightInDp);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
