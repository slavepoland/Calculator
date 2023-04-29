using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ACalculator
{
    public class Result
    {
        readonly Funkcje funkcje = new Funkcje();
        public Result() 
        { }

        public string LiczProcent(string PodajZnakDzialania) //obliczanie %(np.6%z50 to 3) z podanej liczby, aby go wyświetlić na ekranie(PasekWyniku)
        {
            try
            {
                switch (PodajZnakDzialania)
                {
                    case "+": return (Global.DrugaLiczba = Global.PierwszaLiczba * Global.DrugaLiczba / 100).ToString();
                    case "-": return (Global.DrugaLiczba = Global.PierwszaLiczba * Global.DrugaLiczba / 100).ToString();
                    case "/": return (Global.DrugaLiczba /= 100).ToString();
                    case "x": return (Global.DrugaLiczba /= 100).ToString();
                    case "spec": return (Global.PierwszaLiczba /= 100).ToString();
                    default: return "0";
                }
            }catch (Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":LiczProcent(PodaZnakDziałania)"); return "0";
            }
        }

        public double LiczProcent()
        {
            try
            {
                if (Global.GlobalDzialanieProcent == "%")
                {
                    switch (Global.GlobalDzialanie)
                    {
                        case "+": return Global.PierwszaLiczba += Global.PierwszaLiczba * Global.DrugaLiczba / 100;
                        case "-": return Global.PierwszaLiczba -= Global.PierwszaLiczba * Global.DrugaLiczba / 100;
                        case "/": return Global.PierwszaLiczba /= Global.DrugaLiczba;
                        case "x": return Global.PierwszaLiczba *= Global.DrugaLiczba;
                        default:  return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":LiczProcent()"); return 0;
            }
            return 0;
        }

        public double LiczDrugaLiczbaProc() //obliczanie Global.DrugaLiczba, po zdarzeniu =
        {
            try
            {
                switch (Global.GlobalDzialanie)
                {
                    case "+": return Global.PierwszaLiczba *= Global.DrugaLiczba / 100;
                    case "-": return Global.PierwszaLiczba *= Global.DrugaLiczba / 100;
                    case "/": return Global.DrugaLiczba / 100;
                    case "x": return Global.DrugaLiczba / 100;
                    default: return 0;
                }
            }
            catch (Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":LiczDrugaLiczbaProcent()"); return 0;
            }   
        }

        public double Licz()
        {
            try
            {
                if (Global.GlobalDzialanieSpec == null)
                {
                    switch (Global.GlobalDzialanie)
                    {
                        case "+": return Global.PierwszaLiczba += Global.DrugaLiczba;
                        case "-": return Global.PierwszaLiczba -= Global.DrugaLiczba;
                        case "/":
                            {
                                if(Global.DrugaLiczba == 0)
                                {
                                    funkcje.NewPopup("Nie można dzielić przez zero!!!"); return 0;
                                }
                                else
                                {
                                    return Global.PierwszaLiczba /= Global.DrugaLiczba;
                                }  
                            }
                        case "x": return Global.PierwszaLiczba *= Global.DrugaLiczba;
                        case "^": return Global.PierwszaLiczba = Math.Pow(Global.PierwszaLiczba, Global.DrugaLiczba);
                        //znaku ^ nie można zastosować między dwiema liczbami typu double ; 
                        case "x2": return Math.Pow((double)Global.PierwszaLiczba, 2);
                        case "Sqrt": return Math.Sqrt(Global.PierwszaLiczba);
                        case "!":
                            {
                                long s = 1;
                                for (int i = 1; i <= Global.PierwszaLiczba; ++i)
                                    s *= i;
                                return s;
                            }
                    }
                }
            }
            catch(Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":Licz()"); return 0;
            }
            try
            {
                if (Global.GlobalDzialanieSpec == "1/x")
                {
                    if (Global.PierwszaLiczba == 0)
                    {
                        funkcje.NewPopup("Nie można dzielić przez zero!!!");
                    }
                    else
                    {
                        return Global.PierwszaLiczba = 1 / Global.PierwszaLiczba;
                    }
                }
            }
            catch(Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":Licz() 1/x"); return 0;
            }            
            return 0;
        }

        public double LiczSpecjalne(double liczba)
        {
            try
            {
                if (Global.GlobalDzialanieSpec == "1/x")
                    return 1 / liczba;
                if (Global.GlobalDzialanieSpec == "x2")
                    return Math.Pow((double)liczba, 2);
                if (Global.GlobalDzialanieSpec == "Sqrt")
                    return Math.Sqrt((double)liczba);
                else
                    return 0;
            }
            catch(Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":LiczSpecjane()"); return 0;
            }
            
        }

        public double Licz(double lp, double dl, string znak) //not used
        {
            switch(znak)
            {
                case "+": return lp + dl;
                case "-": return lp - dl;
                case "*": return lp * dl;
                case "/": return lp / dl;
                default:return 0;
            }
        }
    }
}