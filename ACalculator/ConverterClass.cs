using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACalculator
{
    public class ConverterClass
    {
        public List<string> ConverterArea = new List<string>()
        {
            "Akry (ac)", 
            "Ary (a)", 
            "Hektary (ha)", 
            "Centymetry kwadratowe (cm2)",
            "Stopy kwadratowe (ft2)", 
            "Cale kwadratowe (in2)", 
            "Metry kwadratowe (m2)"
        };
        public List<string> ConverterLength = new List<string>()
        {
            "Milimetry (mm)",
            "Centymetry (cm)", 
            "Metry (m)", 
            "Kilometry (km)",
            "Cale (in)", 
            "Stopy (ft)", 
            "Jardy (yd)", 
            "Mile (mi)", 
            "Mile morskie (NM)", 
            "Mil (mil)"
        };
        public List<string> ConverterTemperature = new List<string>()
        {
            "Stopnie Celsjusza (C)", 
            "Stopnie Fahrenheita (F)", 
            "Stopnie Kelwina (K)",
        };
        readonly Dictionary<string, double> keyValuePairs = new Dictionary<string, double>()
        {
            { "ac/ac", 1 },
            { "ac/a", 40.468564224 },
            { "ac/ha", 0.40468564224 },
            { "ac/cm2", 40468564.224 },
            { "ac/ft2", 43560.018755 },
            { "ac/in2", 6272886.96405 },
            { "ac/m2", 4046.8564224 },

            { "a/ac", 0.0247105381467 },
            { "a/a", 1 },
            { "a/ha", 0.01 },
            { "a/cm2", 1000000 },
            { "a/ft2", 1076.39150512 },
            { "a/in2", 155006.412615 },
            { "a/m2", 100 },

            { "ha/ac", 2.47105381467 },
            { "ha/a", 100 },
            { "ha/ha", 1 },
            { "ha/cm2", 100000000 },
            { "ha/ft2", 107639.150512 },
            { "ha/in2", 15500641.2615 },
            { "ha/m2", 10000 },

            { "cm2/ac", 2.47105381467e-08 },
            { "cm2/a", 1e-06 },
            { "cm2/ha", 1e-08 },
            { "cm2/cm2", 1 },
            { "cm2/ft2", 0.00107639150512 },
            { "cm2/in2", 0.155006412615 },
            { "cm2/m2", 0.0001 },

            { "ft2/ac", 2.29568312544e-05 },
            { "ft2/a", 0.00092903 },
            { "ft2/ha", 9.2903e-06 },
            { "ft2/cm2", 929.03 },
            { "ft2/ft2", 1 },
            { "ft2/in2", 144.005607512 },
            { "ft2/m2", 0.092903 },

            { "in2/ac", 1.59416231431e-07 },
            { "in2/a", 6.451346e-06 },
            { "in2/ha", 6.451346e-08 },
            { "in2/cm2", 6.451346 },
            { "in2/ft2", 0.00694417403098 },
            { "in2/in2", 1 },
            { "in2/m2", 0.0006451346 },

            { "m2/ac", 0.000247105381467 },
            { "m2/a", 0.01 },
            { "m2/ha", 0.0001 },
            { "m2/cm2", 10000 },
            { "m2/ft2", 10.7639150512 },
            { "m2/in2", 1550.06412615 },
            { "m2/m2", 1 },
        };

        public List<string> converterArea { get; }
        public List<string> converterLength { get; }
        public List<string> converterTemperature { get; }

        public ConverterClass() 
        {
            converterArea = ConverterArea;
            converterLength = ConverterLength;
            converterTemperature = ConverterTemperature;
        }

        public double ConverterCount(double entryupvalue, double entrydownvalue, string converter, 
            int tabIndex)
        {
            double result = 0;
            switch(tabIndex)
            {
                case 1:
                    {
                        result = entryupvalue * 
                            keyValuePairs.Where(x => x.Key == converter)
                            .Select(x => x.Value).FirstOrDefault();
                        return result;

                    }
                case 2:
                    {
                        result = entrydownvalue * 
                            keyValuePairs.Where(x => x.Key == converter)
                            .Select(x => x.Value).FirstOrDefault();
                        return result;
                    }
            }

            return 0;
        }
    }
}
