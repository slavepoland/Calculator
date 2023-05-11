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
        };
        public List<string> ConverterTemperature = new List<string>()
        {
            "Stopnie Celsjusza (C)", 
            "Stopnie Fahrenheita (F)", 
            "Stopnie Kelwina (K)",
        };

        readonly Dictionary<string, double> kvpArea = new Dictionary<string, double>()
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
        readonly Dictionary<string, double> kvpLength = new Dictionary<string, double>()
        {
            { "mm/mm", 1 },
            { "mm/cm", 0.1 },
            { "mm/m", 0.001 },
            { "mm/km", 1e-06 },
            { "mm/in", 0.0393700787402 },
            { "mm/ft", 0.00328083989501 },
            { "mm/yd", 0.00109361329834 },
            { "mm/mi", 6.21383431917e-07 },
            { "mm/NM", 5.39956803456e-07 },

            { "cm/mm", 10 },
            { "cm/cm", 1 },
            { "cm/m", 0.01 },
            { "cm/km", 1e-05 },
            { "cm/in", 0.393700787402 },
            { "cm/ft", 0.0328083989501 },
            { "cm/yd", 0.0109361329834 },
            { "cm/mi", 6.21383431917e-06 },
            { "cm/NM", 5.39956803456e-06 },

            { "m/mm", 1000 },
            { "m/cm", 100 },
            { "m/m", 1 },
            { "m/km", 0.001 },
            { "m/in", 39.3700787402 },
            { "m/ft", 3.28083989501 },
            { "m/yd", 1.09361329834 },
            { "m/mi", 0.000621383431917 },
            { "m/NM", 0.000539956803456 },

            { "km/mm", 1000000 },
            { "km/cm", 100000 },
            { "km/m", 1000 },
            { "km/km", 1 },
            { "km/in", 39370.0787402 },
            { "km/ft", 3280.83989501 },
            { "km/yd", 1093.61329834 },
            { "km/mi", 0.621383431917 },
            { "km/NM", 0.539956803456 },

            { "in/mm", 25.4 },
            { "in/cm", 2.54 },
            { "in/m", 0.0254 },
            { "in/km", 2.54e-05 },
            { "in/in", 1 },
            { "in/ft", 0.0833333333333 },
            { "in/yd", 0.0277777777778 },
            { "in/mi", 1.57831391707e-05 },
            { "in/NM", 1.37149028078e-05 },

            { "ft/mm", 0.3048 },
            { "ft/cm", 30.48 },
            { "ft/m", 0.3048 },
            { "ft/km", 0.0003048 },
            { "ft/in", 43560.018755 },
            { "ft/ft", 1 },
            { "ft/yd", 0.333333333333 },
            { "ft/mi", 0.000189397670048 },
            { "ft/NM", 0.000164578833693 },

            { "yd/mm", 914.4 },
            { "yd/cm", 91.44 },
            { "yd/m", 0.9144 },
            { "yd/km", 0.0009144 },
            { "yd/in", 36 },
            { "yd/ft", 3 },
            { "yd/yd", 1 },
            { "yd/mi", 0.000568193010145 },
            { "yd/NM", 0.00049373650108 },

            { "mi/mm", 1609312.3 },
            { "mi/cm", 160931.23 },
            { "mi/m", 1609.3123 },
            { "mi/km", 1.6093123 },
            { "mi/in", 63358.7519685 },
            { "mi/ft", 5279.89599738 },
            { "mi/yd", 1759.96533246 },
            { "mi/mi", 1 },
            { "mi/NM", 0.86895912527 },

            { "NM/mm", 1852000 },
            { "NM/cm", 185200 },
            { "NM/m", 1852 },
            { "NM/km", 1.852 },
            { "NM/in", 72913.3858268 },
            { "NM/ft", 6076.11548556 },
            { "NM/yd", 2025.37182852 },
            { "NM/mi", 1.15080211591 },
            { "NM/NM", 1 }

        };

        //public List<string> converterArea { get; }
        //public List<string> converterLength { get; }
        //public List<string> converterTemperature { get; }

        public ConverterClass() 
        {
            //converterArea = ConverterArea;
            //converterLength = ConverterLength;
            //converterTemperature = ConverterTemperature;
        }

        public List<string> AddPickerItems(string listname)
        {
            switch (listname)
            {
                case "Area": return ConverterArea; 
                case "Length": return ConverterLength;
                case "Temperature": return ConverterTemperature;
                default : return ConverterArea;
            }
        }

        public double ConverterCount(double entryupvalue, double entrydownvalue, string converter, 
            int tabIndex, string convertername)
        {
            Dictionary<string, double> kvp;
            double result = 0;
            switch (convertername)
            {
                case "Area": kvp = kvpArea; break;
                case "Length": kvp = kvpLength; break;
                case "Temperature":
                    {
                        return ConverterTemperatureCount(entryupvalue, entrydownvalue, converter,
                            tabIndex);
                    }
                default: kvp = kvpArea; break;
            }

            switch(tabIndex)
            {
                case 1:
                    {
                        result = entryupvalue * 
                            kvp.Where(x => x.Key == converter)
                            .Select(x => x.Value).FirstOrDefault();
                        break;

                    }
                case 2:
                    {
                        result = entrydownvalue * 
                            kvp.Where(x => x.Key == converter)
                            .Select(x => x.Value).FirstOrDefault();
                        break;
                    }
            }

            return result;
        }

        private double ConverterTemperatureCount(double entryupvalue, double entrydownvalue, string converter,
            int tabIndex)
        {
            double result = 0;

            switch (tabIndex)
            {
                case 1:
                    {
                        switch (converter)
                        {
                            case "C/C": result = entryupvalue; break;
                            case "C/F": result = (entryupvalue * 1.8) + 32; break; //°F = (°C × 1.8) + 32
                            case "C/K": result = entryupvalue + 273.15; break; //	K = °C + 273.15

                            case "F/C": result = (entryupvalue - 32) / 1.8; break; //°C = (°F − 32) / 1.8   
                            case "F/F": result = entryupvalue; break;
                            case "F/K": result = (entryupvalue + 459.67) * 5 / 9; break;   //K = (°F + 459.67) × 5/9

                            case "K/C": result = entryupvalue - 273.15; break; //°C = K − 273.15
                            case "K/F": result = (entryupvalue * 1.8) - 459.67; break; //°F = (K × 1.8) - 459.67
                            case "K/K": result = entryupvalue; break;
                        }
                        break;
                    }
                case 2:
                    {
                        switch (converter)
                        {
                            case "C/C": result = entrydownvalue; break;
                            case "C/F": result = (entrydownvalue * 1.8) + 32; break; //°F = (°C × 1.8) + 32
                            case "C/K": result = entrydownvalue + 273.15; break; //	K = °C + 273.15

                            case "F/C": result = (entrydownvalue - 32) / 1.8; break; //°C = (°F − 32) / 1.8   
                            case "F/F": result = entrydownvalue; break;
                            case "F/K": result = (entrydownvalue + 459.67) * 5 / 9; break;   //K = (°F + 459.67) × 5/9

                            case "K/C": result = entrydownvalue - 273.15; break; //°C = K − 273.15
                            case "K/F": result = (entrydownvalue * 1.8) - 459.67; break; //°F = (K × 1.8) - 459.67
                            case "K/K": result = entrydownvalue; break;
                        }
                        break;
                    }
            }
            return result;
        }
    }
}
