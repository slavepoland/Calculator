namespace ACalculator
{
    public partial class Global
    {        
        public Global() 
        { 
        
        }

        private static bool boolPierwszaLiczba = true;
        private static bool boolDrugaLiczba = false;
        private static bool globalWynik = false;

        private static string globalDzialanie;
        private static string globalDzialanieSpec;
        private static string globalDzialanieProcent;
        private static string globalTekstPasekWyniku = "0";
        private static string globalTekstPasekWyniku2 = "0";
        private static string globalInfoText;

        private static double pierwszaLiczba;
        private static double drugaLiczba;

        public static bool BoolPierwszaLiczba { get => boolPierwszaLiczba; set => boolPierwszaLiczba = value; }
        public static bool BoolDrugaLiczba { get => boolDrugaLiczba; set => boolDrugaLiczba = value; }
        public static bool GlobalWynik { get => globalWynik; set => globalWynik = value; }
        public static string GlobalDzialanie { get => globalDzialanie; set => globalDzialanie = value; }
        public static string GlobalDzialanieSpec { get => globalDzialanieSpec; set => globalDzialanieSpec = value; }
        public static string GlobalDzialanieProcent { get => globalDzialanieProcent; set => globalDzialanieProcent = value; }
        public static string GlobalTekstPasekWyniku { get => globalTekstPasekWyniku; set => globalTekstPasekWyniku = value; }
        public static string GlobalTekstPasekWyniku2 { get => globalTekstPasekWyniku2; set => globalTekstPasekWyniku2 = value; }
        public static string GlobalInfoText { get => globalInfoText; set => globalInfoText = value; }
        public static double PierwszaLiczba { get => pierwszaLiczba; set => pierwszaLiczba = value; }
        public static double DrugaLiczba { get => drugaLiczba; set => drugaLiczba = value; }
    }


}
