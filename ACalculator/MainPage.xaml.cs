using Rg.Plugins.Popup.Services;
using System;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;
using ACalculator;

namespace ACalculator
{
    public partial class MainPage : ContentPage
    {
        public int WidthInDp { get; }
        public int HeightInDp { get; }

        private const int ValueZero = 0;
        readonly Funkcje funkcje = new Funkcje();
        readonly Result result = new Result();

        public MainPage(int widthInDp, int heightInDp)
        {
            InitializeComponent();
            WidthInDp = widthInDp;
            HeightInDp = heightInDp;

            //AppTheme theme = AppInfo.RequestedTheme;
            //switch (theme)
            //{
            //    case AppTheme.Light:
            //        PasekWyniku.TextColor = Color.Black;
            //        break;
            //    case AppTheme.Dark:
            //        PasekWyniku.TextColor = Color.Black;
            //        break;
            //}

            if (HeightInDp <= 320)
            {
                funkcje.ChangeFontAndPadding("small", WidthInDp, HeightInDp);
            }
            else if(HeightInDp >= 320 && HeightInDp < 720)
            {
                funkcje.ChangeFontAndPadding("medium", WidthInDp, HeightInDp);
            }
            else
            {
                funkcje.ChangeFontAndPadding("large", WidthInDp, HeightInDp);
            }
        }

        protected void BtnClick(object sender, EventArgs e)
        {
            PasekWyniku.Text = funkcje.ClickLiczba((sender as Button).Text);
        }

        protected void BtnPrzecinek_Click(object sender, EventArgs e)
        {
            if (Global.BoolPierwszaLiczba == true && !PasekWyniku.Text.Contains(","))
            {
                PasekWyniku.Text = Global.GlobalTekstPasekWyniku += (sender as Button).Text.ToString();
            }
            if (Global.BoolDrugaLiczba == true && !Global.DrugaLiczba.ToString().Contains("."))
            {
                PasekWyniku.Text = Global.GlobalTekstPasekWyniku2 += (sender as Button).Text.ToString();
            }
            if (Global.GlobalWynik == false && Global.GlobalDzialanie != null && Global.BoolPierwszaLiczba == true)
            {
                //Global.BoolPierszaLiczba = false;
                PasekWyniku.Text = "0,";
            }
            //if (Global.GlobalWynik == true)
            //{
            //    (sender as Button).Text = "C";
            //    BtnDelete(sender, e);
            //    (sender as Button).Text = ",";
            //    PasekWyniku.Text = Global.GlobalTekstPasekWyniku = funkcje.ClickLiczba("0") + (sender as Button).Text;
            //}
        }

        protected void BtnDelete(object sender, EventArgs e)
        {
            try
            {
                if ((sender as Button).Text == "Del" && Global.BoolPierwszaLiczba == true && Global.GlobalDzialanie == null)
                {
                    PasekWyniku.Text = Global.GlobalTekstPasekWyniku = funkcje.UsunOstatniZnak(PasekWyniku.Text);
                    try
                    {
                        Global.PierwszaLiczba = Convert.ToDouble(PasekWyniku.Text);
                    }
                    catch { Global.PierwszaLiczba = ValueZero; }
                    return;
                }
            }
            catch(Exception expl)
            {
                funkcje.NewPopup(expl.Message);
            }

            try
            {
                if ((sender as Button).Text == "Del" && Global.BoolDrugaLiczba == true && Global.DrugaLiczba != ValueZero)
                {
                    if (Global.GlobalWynik == false)
                    {
                        PasekWyniku.Text = Global.GlobalTekstPasekWyniku2 = funkcje.UsunOstatniZnak(PasekWyniku.Text);
                        Global.DrugaLiczba = Convert.ToDouble(PasekWyniku.Text.Replace(",", "."));
                        return;
                    }
                    return;
                }
            }
            catch(Exception exdl)
            {
                funkcje.NewPopup(exdl.Message);
            }
            try
            {
                if ((sender as Button).Text == "C" || (sender as Button).Text == "CE") //&& Global.GlobalDzialanie != null)
                {
                    Global.GlobalDzialanie = null; Global.GlobalWynik = false;
                    Global.PierwszaLiczba = ValueZero; Global.BoolPierwszaLiczba = true;
                    Global.DrugaLiczba = ValueZero; Global.BoolDrugaLiczba = false;
                    Global.GlobalDzialanieProcent = null;
                    PasekWyniku.Text = Global.GlobalTekstPasekWyniku = Global.GlobalTekstPasekWyniku2 = "0";
                    PasekFormuly.Text = "";
                }
            }
            catch(Exception exC)
            {
                funkcje.NewPopup(exC.Message);
            }       
        }

        protected void BtnDzialanie(object sender, EventArgs e) //(add, substract, multuiply, Divide)
        {
            if (Global.BoolPierwszaLiczba)
            {
                Global.BoolPierwszaLiczba = false; Global.BoolDrugaLiczba = true;
            }
            funkcje.Dzialanie((sender as Button).Text); //przypisanie aktualnego znaku działania do Global.GlobalDzialanie
            PasekFormuly.Text = funkcje.PasekFormuly(PasekFormuly.Text, (sender as Button).Text);
        }

        protected void BtnWynik_Click(object sender, EventArgs e)
        {
            Global.GlobalWynik = true;
                Button button = sender as Button;
                if (button.Text == "1/x")
                {
                    Global.GlobalDzialanieSpec = "1/x";
                    PasekFormuly.Text = "1/(" + Global.PierwszaLiczba.ToString().Replace(".", ",") + ")";
                    PasekWyniku.Text = Global.GlobalTekstPasekWyniku = Convert.ToString(result.Licz(), CultureInfo.InvariantCulture).Replace(".", ",");
                    Global.GlobalDzialanieSpec = null;
                    return;
                }

            if (Global.GlobalDzialanie != null)
            {
                if (Global.GlobalDzialanieSpec == null)// != "1/x" && Global.GlobalDzialanieSpec != "x2" && Global.GlobalDzialanieSpec != "Sqrt")
                {
                    if (Global.GlobalDzialanieProcent != "%")
                    {
                        PasekFormuly.Text = funkcje.PasekFormuly(PasekFormuly.Text, (sender as Button).Text);
                        PasekWyniku.Text = Global.GlobalTekstPasekWyniku = Convert.ToString(result.Licz(), CultureInfo.InvariantCulture).Replace(".", ",");   
                    }
                    else
                    {
                        PasekFormuly.Text = funkcje.PasekFormuly(PasekFormuly.Text, "%%");
                        PasekWyniku.Text = Global.GlobalTekstPasekWyniku = 
                            Convert.ToString(result.Licz(), CultureInfo.InvariantCulture).Replace(".", ",");
                        //Global.DrugaLiczba = Convert.ToDouble(result.LiczDrugaLiczbaProc(), CultureInfo.InvariantCulture);
                        
                        Global.BoolPierwszaLiczba = false; Global.BoolDrugaLiczba = true;
                        //Global.PierwszaLiczba = Convert.ToDouble(PasekWyniku.Text.Replace(",", "."), CultureInfo.InvariantCulture);
                        Global.GlobalTekstPasekWyniku2 = "0";
                    }
                    //Global.GlobalDzialanieProcent = null;
                }
            }
            else { PasekFormuly.Text = PasekWyniku.Text + " ="; }
            
            if (Global.GlobalDzialanie == null && Global.GlobalTekstPasekWyniku.Contains(",") == true)
            {
                PasekWyniku.Text = Global.GlobalTekstPasekWyniku = funkcje.UsunOstatniZnak(PasekWyniku.Text);
            }
            Global.GlobalWynik = true;
        }

        protected void BtnFunkSpecjalne_Click(object sender, EventArgs e)
        {
            funkcje.Dzialanie((sender as Button).Text); //dodaj znak działania dla 1/x, x2, Sqrt

            PasekFormuly.Text = funkcje.PasekFormulySpec(PasekFormuly.Text, (sender as Button).Text);
            if (Global.GlobalDzialanie == null)
            {
                Global.PierwszaLiczba = result.Licz();
                if (!PasekWyniku.Text.Contains("Nie"))
                    PasekWyniku.Text = Global.GlobalTekstPasekWyniku = Convert.ToString(Global.PierwszaLiczba).Replace(".", ",");
            }
            else
            {
                Global.DrugaLiczba = Convert.ToDouble(result.LiczSpecjalne(Global.DrugaLiczba), CultureInfo.InvariantCulture);
                PasekWyniku.Text = Global.GlobalTekstPasekWyniku = Convert.ToString(Global.DrugaLiczba).Replace(".", ",");
            }

            //PasekWyniku.Text = Global.GlobalTekstPasekWyniku = Convert.ToString(Global.PierwszaLiczba);
            Global.GlobalWynik = true; Global.GlobalDzialanieSpec = null;
        }

        protected void BtnProcent_Click(object sender, EventArgs e)
        {
            Global.GlobalDzialanieProcent = "%";
            PasekFormuly.Text = funkcje.PasekFormuly(PasekFormuly.Text, (sender as Button).Text);
            if (Global.BoolPierwszaLiczba = true && Global.BoolDrugaLiczba == false)
            {
                PasekWyniku.Text = result.LiczProcent("spec").Replace(".", ",");
            }
            else
            {
                PasekWyniku.Text = result.LiczProcent(Global.GlobalDzialanie).Replace(".", ",");
            }
        }

        private async void Btn_converter_Clicked(object sender, EventArgs e)
        {//App.Current.MainPage = new Converter();
            await Navigation.PushModalAsync(new NavigationPage(new Converter()));  //Converter()
        }

        private async void Btn_History(object sender, EventArgs e)
        {//App.Current.MainPage = new Converter();
            await Navigation.PushModalAsync(new CarouselPage());  //Converter()
        }



        //public void Btn_Clicked(object sender, EventArgs e)
        //{
        //    Button button = (Button)sender;
        //    if (button.Text == "C")
        //    {
        //        PasekWyniku.Text = "0"; return;
        //    }

        //    if (PasekWyniku.Text == "0")
        //        if (PasekWyniku.Text == "0" && button.Text.Contains(","))
        //            PasekWyniku.Text = "0" + button.Text;
        //        else
        //            PasekWyniku.Text = button.Text;
        //    else
        //    {
        //        if (PasekWyniku.Text.Contains("-0") && Convert.ToInt16(button.Text) > 0
        //            && Convert.ToInt16(button.Text) < 9)
        //        {
        //            PasekWyniku.Text = "(" + PasekWyniku.Text.Remove(1, 1) + button.Text;
        //            return;
        //        }

        //        if (!PasekWyniku.Text.Contains(",") 
        //            || (PasekWyniku.Text.Contains(",") && !button.Text.Contains(",")))
        //            PasekWyniku.Text += button.Text;
        //    }
        //}

        //private void Btn_delete_Clicked(object sender, EventArgs e)
        //{
        //    string number = PasekWyniku.Text;
        //    if (number != "0")
        //    {
        //        number = number.Remove(number.Length - 1, 1);
        //        if(string.IsNullOrEmpty(number))
        //        {
        //            PasekWyniku.Text = "0";     
        //        }
        //        else
        //        {
        //            PasekWyniku.Text = number;
        //        }
        //    }
        //}

        //private void Btn_addsubtract_Clicked(object sender, EventArgs e)
        //{
        //    Button button = (Button)sender;
        //    if (button.Text == "+/-" && !PasekWyniku.Text.Contains("-"))
        //    {
        //        PasekWyniku.Text = "-" + PasekWyniku.Text; return;
        //    }
        //    else if(button.Text == "+/-" && PasekWyniku.Text.Contains("-"))
        //    {
        //        PasekWyniku.Text = PasekWyniku.Text.Remove(0, 1); return;
        //    }
        //}


    }
}
