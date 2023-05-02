using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			InitializeComponent ();
            Btn_addsubtract.IsEnabled = false;

            pickerup.ItemsSource = cc.converterArea;
			pickerup.SelectedIndex = 0;
			pickerdown.ItemsSource = cc.converterArea;
            pickerdown.SelectedIndex = 1;
            EntryUpBool = true; EntryDownBool = false; ConvertCount = false;
            _ = entryup.Focus();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            entryup.Focus();
        }

        private void Pickerup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker pk = (Picker)sender;
            labelup.Text = pk.SelectedItem.ToString().Substring(pk.SelectedItem.ToString()
                .LastIndexOf('(')).Replace("(", "").Replace(")", "");        
        }

        private void Pickerdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker pk = (Picker)sender;
            labeldown.Text = pk.SelectedItem.ToString().Substring(pk.SelectedItem.ToString()
                .LastIndexOf('(')).Replace("(", "").Replace(")", "");
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

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
                        _ = entryup.Focus();
                        Btn_up.IsEnabled = false;
                        Btn_down.IsEnabled = true;
                        break;
                    }
                case 2: //arrow down button click
                    {
                        _ = entrydown.Focus();
                        Btn_up.IsEnabled = true;
                        Btn_down.IsEnabled = false;
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
                                entrydown.Text = cc.ConverterCount(Convert.ToDouble(entryup.Text),
                                    Convert.ToDouble(entrydown.Text), (labelup.Text + "/" + labeldown.Text), noKeyboardEntry.TabIndex).ToString();
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
                                entryup.Text = cc.ConverterCount(Convert.ToDouble(entryup.Text),
                                    Convert.ToDouble(entrydown.Text), (labeldown.Text + "/" + labelup.Text), noKeyboardEntry.TabIndex).ToString();
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