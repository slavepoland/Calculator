using ACalculator.ConverterPage;
using ACalculator.Function;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ACalculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AreaPage : ContentPage
    {
        private bool EntryUpBool { get; set; }
        private bool EntryDownBool { get; set; }
        private bool ConvertCount { get; set; }

        readonly Funkcje funkcje = new Funkcje();
        readonly ConverterClass cc = new ConverterClass();

        public AreaPage ()
		{
            InitializeComponent();
            HeadingPage.labelHeadText.Text = "Powierzchnia     >>";
            Btn_addsubtract.IsEnabled = false;

            pickerup.ItemsSource = cc.ConverterArea;
            pickerup.SelectedIndex = 0;
            pickerdown.ItemsSource = cc.ConverterArea;
            pickerdown.SelectedIndex = 1;
            EntryUpBool = true; EntryDownBool = false; ConvertCount = false;
            _ = entryup.Focus();

            FontAndPaddingConverterPage();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        /// <summary>
        /// change font and padding in AreaPage for diffrent Dp for all devices 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void FontAndPaddingConverterPage()
        {
            float fontSize = 45;
            try
            {
                if (Global.HeightInDp < 320)
                {
                    foreach (Button item in buttonGrid.Children.Cast<Button>())
                    {
                        item.FontSize = 0.2f * fontSize;
                        item.Padding = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":Portrait, <320");
            }
            try
            {
                if (Global.HeightInDp >= 320 && Global.HeightInDp < 550)
                {
                    foreach (Button item in buttonGrid.Children.Cast<Button>())
                    {
                        item.FontSize = 0.40f * fontSize;
                        item.Padding = 3;
                    }
                }
            }
            catch (Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":Portrait, 320-549");
            }
            try
            {
                if (Global.HeightInDp >= 550 && Global.HeightInDp < 700)
                {
                    foreach (Button item in buttonGrid.Children.Cast<Button>())
                    {
                        item.FontSize = 0.50f * fontSize;
                        item.Padding = 6;
                    }
                }
            }
            catch (Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":Portrait, 550-699");
            }
            try
            {
                if (Global.HeightInDp >= 700 && Global.HeightInDp < 800)
                {
                    foreach (Button item in buttonGrid.Children.Cast<Button>())
                    {
                        item.FontSize = 0.55f * fontSize;
                        item.Padding = 10;
                    }
                }
            }
            catch (Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":Portrait, 700-799");
            }
            try
            {
                if (Global.HeightInDp >= 800)
                {
                    foreach (Button item in buttonGrid.Children.Cast<Button>())
                    {
                        item.FontSize = 0.65f * fontSize;
                        item.Padding = 15;
                    }
                }
            }
            catch (Exception ex)
            {
                funkcje.NewPopup(ex.Message + ":Portrait, >=800");
            }
        }

        private void Pickerup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker pk = (Picker)sender;
            labelup.Text = pk.SelectedItem.ToString().Substring(pk.SelectedItem.ToString()
                .LastIndexOf('(')).Replace("(", "").Replace(")", "");
            ConvertCount = false;
            _ = entryup.Focus();
        }

        private void Pickerdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker pk = (Picker)sender;
            labeldown.Text = pk.SelectedItem.ToString().Substring(pk.SelectedItem.ToString()
                .LastIndexOf('(')).Replace("(", "").Replace(")", "");
            ConvertCount = false;
            _ = entrydown.Focus();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue)) return;

            if (!double.TryParse(e.NewTextValue, out double value))
            {
                ((Entry)sender).Text = value.ToString();//e.OldTextValue;
            }
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch(btn.TabIndex)
            {
                case 0: //numeric button click
                    {
                        if (EntryUpBool == true)
                        {
                            if ((entryup.Text.Length + btn.Text.Length) <= 15)
                            {
                                if (entryup.Text == "0" && btn.Text != ",")
                                {
                                    entryup.Text = btn.Text;
                                }
                                else
                                {
                                    if (entryup.Text.Contains(",") == false && btn.Text == ",")
                                    {
                                        entryup.Text += btn.Text;
                                    }
                                    else if (btn.Text.Contains(",") == false)
                                    {
                                        entryup.Text += btn.Text;
                                    }
                                }
                                ConvertCount = false;
                            }
                            else
                            {
                                funkcje.NewPopup("Nie można wprowadzić więcej niż 15 cyfr!!!");
                            }
                            
                            _ = entryup.Focus();
                        }
                        else if(EntryDownBool == true)
                        {
                            if ((entrydown.Text.Length + btn.Text.Length) <= 15)
                            {
                                if (entrydown.Text == "0" && btn.Text != ",")
                                {
                                    entrydown.Text = btn.Text;
                                }
                                else
                                {
                                    if (entrydown.Text.Contains(",") == false && btn.Text == ",")
                                    {
                                        entrydown.Text += btn.Text;
                                    }
                                    else if (btn.Text.Contains(",") == false)
                                    {
                                        entrydown.Text += btn.Text;
                                    }
                                }
                                ConvertCount = false;
                            }
                            else
                            {
                                funkcje.NewPopup("Nie można wprowadzić więcej niż 15 cyfr!!!");
                            }
                            _ = entrydown.Focus();
                        }
                        break;
                    }
                case 1: //arrow up button click
                    {
                        Btn_up.IsEnabled = false;
                        Btn_down.IsEnabled = true;
                        _ = entryup.Focus();
                        break;
                    }
                case 2: //arrow down button click
                    {
                        Btn_up.IsEnabled = true;
                        Btn_down.IsEnabled = false;
                        _ = entrydown.Focus();
                        break;
                }
                case 3: //delete button click
                    {
                        ConvertCount = false;
                        if (EntryUpBool == true)
                        {
                                if (entryup.Text.Length > 0)
                                entryup.Text = entryup.Text.Remove(entryup.Text.Length-1, 1);
                            _ = entryup.Focus();
                        }
                        else if (EntryDownBool == true)
                        {
                                if (entrydown.Text.Length > 0)
                                entrydown.Text = entrydown.Text.Remove(entrydown.Text.Length - 1, 1);
                            _ = entrydown.Focus();
                        }
                        break;
                }
                case 4: //clear button click
                    {
                        ConvertCount = false;
                        entryup.Text = "0";
                        entrydown.Text = "0";
                        _ = entryup.Focus();
                        break;
                    }

            }

        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            if(entryup.Text == "" || entrydown.Text == "")
            {
                Btn_Clicked(Btn_clear, null);
            }

            Entry noKeyboardEntry = sender as Entry;
            switch (noKeyboardEntry.TabIndex)
            {
                case 1:
                {
                    Btn_up.IsEnabled = false;
                    Btn_down.IsEnabled = true;
                    entryup.CursorPosition = entryup.Text.Length;
                    EntryUpBool = true;
                    EntryDownBool = false;
                        if (entryup.Text != "0" && entryup.Text != "0,")
                        {
                            if (ConvertCount == false)
                            {
                                entrydown.Text = Convert.ToString(cc.ConverterCount(Convert.ToDouble(entryup.Text.Replace(",", "."), CultureInfo.InvariantCulture),
                                        Convert.ToDouble(entrydown.Text.Replace(",", "."), CultureInfo.InvariantCulture), (labelup.Text + "/" + labeldown.Text),
                                        noKeyboardEntry.TabIndex, "Area")).Replace(".", ",");
                                ConvertCount = true;
                            }
                        }
                        break; 
                }
                case 2:
                {
                    Btn_up.IsEnabled = true;
                    Btn_down.IsEnabled = false;
                    entrydown.CursorPosition = entrydown.Text.Length;
                    EntryUpBool = false;
                    EntryDownBool = true;
                        if (entrydown.Text != "0" && entrydown.Text != "0,")
                        {
                            if (ConvertCount == false)
                            {
                                entryup.Text = Convert.ToString(cc.ConverterCount(Convert.ToDouble(entryup.Text.Replace(",", "."), CultureInfo.InvariantCulture),
                                        Convert.ToDouble(entrydown.Text.Replace(",", "."), CultureInfo.InvariantCulture), (labeldown.Text + "/" + labelup.Text),
                                        noKeyboardEntry.TabIndex, "Area")).Replace(".", ",");
                                ConvertCount = true;
                            }
                        }
                        break; 
                }
            }
        }
        
    }

    public class NoKeyboardEntry : Entry
    {
    }
}