using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using System.Diagnostics;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;

namespace ACalculator
{
    public class Funkcje
    {
        public void NewPopup(string message)
        {
            MyPopup popup = new MyPopup();
            popup.myPopup.FontSize = 15;
            popup.myPopup.Text = message;

            PopupNavigation.Instance.PushAsync(popup);
        }

        public void ChangeFontAndPadding(string orientation, int widthInDp, int heightInDp)
        {
            float fontSize = 45;
            //int padding = orientation.Contains("po") ? 15 : 0;
            try
            {
                if (orientation.Contains("land"))
                {
                    MainPage.FrameText.HeightRequest = heightInDp * 0.5;
                    MainPage.GridButton.HeightRequest = heightInDp * 0.5;
                    MainPage.PasekWyniku.FontSize = 0.7f * fontSize;
                    MainPage.PasekFormuly.FontSize = 0.4f * fontSize;
                    foreach (var items in MainPage.GridButton.Children)
                    {
                        Button item = items as Button;
                        item.FontSize = 0.28f * fontSize;
                        item.Padding = 0;
                    }
                    foreach (var items in MainPage.GridPages.Children)
                    {
                        if(items is Button)
                        {
                            Button item = items as Button;
                            item.FontSize = 0.28f * fontSize;
                            item.Padding = 0;
                        }
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                NewPopup(ex.Message + ":Landscape");
            }

            MainPage.FrameText.HeightRequest = heightInDp * 0.4;
            MainPage.GridButton.HeightRequest = heightInDp * 0.6;
            try
            {
                if (heightInDp <= 320)
                {
                    MainPage.PasekWyniku.FontSize = 0.4f * fontSize;
                    MainPage.PasekFormuly.FontSize = 0.2f * fontSize;
                    foreach (var items in MainPage.GridButton.Children)
                    {
                        Button item = items as Button;
                        item.FontSize = 0.3f * fontSize;
                        item.Padding = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                NewPopup(ex.Message + ":Portrait, 320");
            }
            try
            {
                if (heightInDp > 320 && heightInDp < 720)
                {
                    MainPage.PasekWyniku.FontSize = 0.8f * fontSize;
                    MainPage.PasekFormuly.FontSize = 0.5f * fontSize;
                    foreach (var items in MainPage.GridButton.Children)
                    {
                        Button item = items as Button;
                        item.FontSize = 0.45f * fontSize;
                        item.Padding = 2;
                    }
                }
            }
            catch(Exception ex) 
            {
                NewPopup(ex.Message + ":Portrait, 321-719");
            }
            try
            {
                if (heightInDp >= 720)
                {
                    MainPage.PasekWyniku.FontSize = 1.1f * fontSize;
                    MainPage.PasekFormuly.FontSize = 0.5f * fontSize;
                    foreach (var items in MainPage.GridButton.Children)
                    {
                        Button item = items as Button;
                        item.FontSize = 0.7f * fontSize;
                        item.Padding = 10;
                    }
                }
            }
            catch (Exception ex)
            {
                NewPopup(ex.Message + ":Portrait, >720");
            }


        }

        public string PasekFormuly(string pasekformuly, string InfoText)
        {
            string dzialanie;
            switch (Global.GlobalDzialanie)
            {
                case "/": dzialanie = "÷"; break;
                default: dzialanie = Global.GlobalDzialanie; break;
            }
            try
            {
                //pasek formuły dla znaku +,-,/,x
                if (InfoText == "+" || InfoText == "-" || InfoText == "/" || InfoText == "x")
                {
                    if (Global.BoolPierwszaLiczba == false && Global.BoolDrugaLiczba == true && Global.GlobalWynik == true)
                    {
                        return _ = Convert.ToString(Global.PierwszaLiczba).Replace(".", ",") + " " + dzialanie;
                    }
                    else
                    {
                        string drugaLiczba;
                        if (Global.GlobalDzialanieProcent == "%")
                        {
                            return _ = (Global.PierwszaLiczba * 100).ToString().Replace(".", ",") + "%" + " " + InfoText;
                        }
                        drugaLiczba = Global.DrugaLiczba == 0 ? "" : Global.DrugaLiczba.ToString().Replace(".", ",");
                        drugaLiczba = drugaLiczba == "" ? "" : drugaLiczba + " =";
                        return _ = Global.PierwszaLiczba.ToString().Replace(".", ",") + " " + dzialanie + " " + drugaLiczba;
                    }

                }
            }
            catch(Exception ex)
            {
                NewPopup(ex.Message + ":dla +,-,*,/");
            }

            try
            {
                //pasek formuły dla =
                if (InfoText == "=" && Global.BoolPierwszaLiczba == false && Global.BoolDrugaLiczba == true
                    && Global.GlobalWynik == false)
                {
                    if (Global.GlobalDzialanieProcent == null)
                    {
                        return _ = Global.PierwszaLiczba.ToString().Replace(".", ",") + " " + dzialanie + " " +
                            Global.DrugaLiczba.ToString().Replace(".", ",") + " " + InfoText;
                    }
                }
            }
            catch(Exception ex)
            {
                NewPopup(ex.Message + ":wynik");
            }

            try
            {
                if (InfoText == "=" && Global.GlobalWynik == true && Global.BoolDrugaLiczba == true)
                {
                    if (Global.GlobalDzialanieProcent != null)
                        return _ = pasekformuly + " =";
                    else
                        return _ = Global.PierwszaLiczba.ToString().Replace(".", ",") + " " + dzialanie + " "
                         + Global.DrugaLiczba.ToString().Replace(".", ",") + " =";
                }
            }catch(Exception ex)
            {
                NewPopup(ex.Message + ":wynik");
            }

            try
            {
                //pasek formuły dla oblicznia procentów
                if (InfoText == "%" || InfoText == "%%")
                {
                    string drugaLiczba = Global.DrugaLiczba == 0 ? "" : Global.DrugaLiczba.ToString().Replace(".", ",");
                    if (Global.GlobalWynik == false && InfoText == "%")
                    {
                        if (drugaLiczba == "")
                            return _ = Convert.ToString(Global.PierwszaLiczba).Replace(".", ",") + "%" + " " + dzialanie
                            + " " + drugaLiczba;
                        else
                            return _ = Convert.ToString(Global.PierwszaLiczba).Replace(".", ",") + " " + dzialanie
                            + " " + drugaLiczba + "%";
                    }
                    else
                    {
                        if (pasekformuly.Contains("="))
                            return _ = Global.PierwszaLiczba.ToString().Replace(".", ",") + " " + dzialanie + " " +
                                Global.DrugaLiczba.ToString().Replace(".", ",") + " =";
                        else if (pasekformuly.Contains("%") && Global.GlobalWynik == true)
                            return _ = Global.PierwszaLiczba.ToString().Replace(".", ",") + " " + dzialanie + " " +
                                Global.DrugaLiczba.ToString().Replace(".", ",") + " =";
                        else if (!pasekformuly.Contains("=") && Global.GlobalWynik == true)
                            return _ = Global.PierwszaLiczba.ToString().Replace(".", ",") + " " + dzialanie + " " +
                                Global.DrugaLiczba.ToString().Replace(".", ",") + "%" + " =";
                        else
                            return _ = pasekformuly.Replace("=", "") + " =";
                    }
                }
            }
            catch(Exception ex)
            {
                NewPopup(ex.Message + ":oblicznia procent");
            }
            
            return "";
        }

        public string PasekFormulySpec(string pasekformuly, string InfoText)
        {
            try
            {
                if (Global.GlobalDzialanie != null)
                {
                    if (InfoText == "1/x")
                    {
                        return "1/(" + Convert.ToString(Global.PierwszaLiczba).Replace(".", ",") + ")";
                    }
                    if (InfoText == "x2")
                    {
                        return "sqr(" + Convert.ToString(Global.PierwszaLiczba).Replace(".", ",") + ")";
                    }
                    if (InfoText == "Sqrt")
                    {
                        if (InfoText == "Sqrt") InfoText = "√";
                        return pasekformuly + InfoText + "(" + Convert.ToString(Global.DrugaLiczba).Replace(".", ",") + ")";
                    }
                }
                else
                {
                    if (InfoText == "1/x")
                    {
                        return "1/(" + Convert.ToString(Global.PierwszaLiczba).Replace(".", ",") + ")";
                    }
                    if (InfoText == "x2")
                    {
                        return "sqr(" + Convert.ToString(Global.PierwszaLiczba).Replace(".", ",") + ")";
                    }
                    if (InfoText == "Sqrt")
                    {
                        if (InfoText == "Sqrt") InfoText = "√";
                        return InfoText + "(" + Convert.ToString(Global.PierwszaLiczba).Replace(".", ",") + ")";
                    }
                }
            }
            catch(Exception ex)
            {
                NewPopup(ex.Message + ":pasek formuły spec");
            }

            return "";
        }

        public void Dzialanie(string stringdzialanie)
        {
            try
            {
                switch (stringdzialanie)
                {
                    case "+": Global.GlobalDzialanie = stringdzialanie; break;
                    case "-": Global.GlobalDzialanie = stringdzialanie; break;
                    case "x": Global.GlobalDzialanie = stringdzialanie; break;
                    case "/": Global.GlobalDzialanie = stringdzialanie; break;
                    case "1/x": Global.GlobalDzialanie = stringdzialanie; break;
                    case "x2": Global.GlobalDzialanie = stringdzialanie; break;
                    case "Sqrt": Global.GlobalDzialanie = stringdzialanie; break;
                }
            }
            catch(Exception ex)
            {
                NewPopup(ex.Message + ":działanie");
            }
            
        }

        public string UsunOstatniZnak(string tekst)
        {
            try
            {
                if (tekst.Length > 1)
                {
                    return tekst.Remove(tekst.Length - 1, 1);
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                NewPopup(ex.Message + ":usuń ostatni znak"); return "0";
            } 
        }

        public string ClickLiczba(string textPaskaWyniku)
        {
            if(textPaskaWyniku.Length > 15)
            {
                NewPopup("Nie można wprowadzić więcej niż 15 cyfr.");
                return textPaskaWyniku;
            }
            try
            {
                if (Global.BoolPierwszaLiczba == true && Global.GlobalDzialanie == null)
                {
                    if (Global.GlobalTekstPasekWyniku == "0")
                    { }
                    else
                    {
                        if (textPaskaWyniku == "+/-")
                        {
                            double number = double.Parse(Global.GlobalTekstPasekWyniku.Replace(",", "."), CultureInfo.InvariantCulture);
                            if (number > 0)
                            {
                                textPaskaWyniku = "-" + Global.GlobalTekstPasekWyniku;
                            }
                            else if (number < 0)
                            {
                                textPaskaWyniku = Global.GlobalTekstPasekWyniku.Substring(1, Global.GlobalTekstPasekWyniku.Length - 1);
                            }
                        }
                        else
                        {
                            textPaskaWyniku = Global.GlobalTekstPasekWyniku + textPaskaWyniku;
                        }
                    }
                    if (textPaskaWyniku != "+/-")
                    {
                        Global.PierwszaLiczba = double.Parse(textPaskaWyniku.Replace(",", "."), CultureInfo.InvariantCulture);
                    }
                    else { textPaskaWyniku = "0"; }
                    Global.GlobalTekstPasekWyniku = textPaskaWyniku;
                }
            }
            catch (Exception e) { NewPopup(e.Message + ":click pierwsza liczba"); }

            try
            {
                if (Global.BoolDrugaLiczba == true && Global.GlobalDzialanie != null)
                {
                    if (Global.GlobalTekstPasekWyniku2 == "0")
                    { }
                    else
                    {
                        if (textPaskaWyniku == "+/-")
                        {
                            double number = double.Parse(Global.GlobalTekstPasekWyniku2.Replace(",", "."), CultureInfo.InvariantCulture);
                            if (number > 0)
                            {
                                textPaskaWyniku = "-" + Global.GlobalTekstPasekWyniku2;
                            }
                            else if (number < 0)
                            {
                                textPaskaWyniku = Global.GlobalTekstPasekWyniku2.Substring(1, Global.GlobalTekstPasekWyniku2.Length - 1);
                            }
                        }
                        else
                        { //textPaskaWyniku = Global.GlobalTekstPasekWyniku2 + textPaskaWyniku;
                            if(MainPage.PasekWyniku.Text != Global.PierwszaLiczba.ToString().Replace(".", ","))
                            {
                                textPaskaWyniku = Global.GlobalTekstPasekWyniku2 + textPaskaWyniku;
                            }
                            //else
                            //{
                            //    textPaskaWyniku = Global.GlobalTekstPasekWyniku2 + textPaskaWyniku;  //dodane teraz 12:13
                            //}
                        }
                    }
                    if (textPaskaWyniku != "+/-")
                    {
                        Global.DrugaLiczba = double.Parse(textPaskaWyniku.Replace(",", "."), CultureInfo.InvariantCulture);
                    }
                    else { textPaskaWyniku = "0"; }
                    Global.GlobalTekstPasekWyniku2 = textPaskaWyniku;
                }
            } catch(Exception ex) { NewPopup(ex.Message + ":click druga liczba"); }

            return textPaskaWyniku;
        }
    }
}