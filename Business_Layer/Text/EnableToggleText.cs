using Business_Layer.Text;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Business_Layer
{
    public class EnableToggleText
    {

        #region Variables

        private readonly bool workingWithList = false;
        private readonly string initText = string.Empty;
        private readonly List<string> initTexts = new List<string>();
        private readonly TextBox textBox;
        private readonly TextMode textMode;
        private string Text = string.Empty;

        #endregion

        #region Constructores
        public EnableToggleText(TextBox textBox, TextMode textMode = TextMode.Alphanumeric)
        {
            this.textBox = textBox;
            this.textMode = textMode;
            textBox.GotFocus += Toggle;
            textBox.LostFocus += Toggle;
            if (textMode == TextMode.Numeric)
            {
                textBox.PreviewTextInput += Numeric;
            }
            else if (textMode == TextMode.Alphbetic)
            {
                textBox.PreviewTextInput += Alphbetic;
            }
            else if (textMode == TextMode.Money)
            {
                textBox.PreviewTextInput += Money;
                textBox.PreviewKeyDown += Borrar;
            }
        }
        public EnableToggleText(TextBox textBox, string initText, TextMode textMode = TextMode.Alphanumeric) : this(textBox, textMode)
        {
            this.initText = initText;
            workingWithList = false;
        }

        public EnableToggleText(TextBox textBox, string[] initTexts, TextMode textMode = TextMode.Alphanumeric) : this(textBox, textMode)
        {
            this.initTexts = new List<string>(initTexts);
            workingWithList = true;
        }

        #endregion

        #region Funciones

        public void InicializarTexto()
        {
            textBox.Foreground = new SolidColorBrush(Colors.Black);
            textBox.Text = workingWithList ? initTexts[0] : initText;
            Text = string.Empty;
        }

        private void Borrar(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                e.Handled = true;
                if (Text.Length > 0)
                {
                    Text = Text.Remove(Text.Length - 1);
                }

                Actualizar();
            }
        }

        private void IPv4(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (textBox.Text.Length - 1 % 3 == 0)
            {
                int caretIndex = textBox.CaretIndex;
                for (int i = 0; i < textBox.Text.Length; i += 3)
                {
                    if (textBox.Text[i] != '.')
                    {
                        textBox.Text = textBox.Text.Insert(i, ".");
                    }
                }
                textBox.CaretIndex = caretIndex;
            }
        }

        private void Money(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;

            Regex regex = new Regex("[0-9]+");
            if (regex.IsMatch(e.Text))
            {
                if (e.Text == "0" && string.IsNullOrEmpty(Text))
                {
                    return;
                }

                Text += e.Text;
            }

            Actualizar(e.Text);
        }

        private void Actualizar(string text = "")
        {
            string s = "0000";


            if (Text.Length < 4)
            {
                s = s.Substring(0, s.Length - Text.Length) + Text;
            }
            else
            {
                s = Text;
            }

            textBox.Text = s.Insert(s.Length - 2, ".");
        }

        private void Alphbetic(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool minus = false;
        private bool plus = true;
        private void Numeric(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");

            if (!textBox.Text.Contains("-"))
            {
                minus = false;
                plus = true;
            }
            if (!minus && plus && e.Text.Equals("-"))
            {
                int i = textBox.CaretIndex;
                textBox.Text = "-" + textBox.Text;
                textBox.CaretIndex = i + 1;
                minus = true;
                plus = false;
            }
            else if (!plus && minus && e.Text.Equals("+"))
            {
                int i = textBox.CaretIndex;
                textBox.Text = textBox.Text.Remove(0, 1);
                textBox.CaretIndex = i - 1;
                minus = false;
                plus = true;
            }
            e.Handled = regex.IsMatch(e.Text);

        }

        private void Toggle(object sender, RoutedEventArgs e)
        {
            if (textMode != TextMode.Money)
            {
                textBox.Foreground = new SolidColorBrush(Colors.Black);
                if (workingWithList)
                {
                    if (e.RoutedEvent.Name.Equals("gotfocus", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (string s in initTexts)
                        {
                            if (textBox.Text.Equals(s, StringComparison.OrdinalIgnoreCase))
                            {
                                textBox.Text = string.Empty;
                                Text = string.Empty;
                                return;
                            }
                        }
                    }


                    else if (e.RoutedEvent.Name.Equals("lostfocus", StringComparison.OrdinalIgnoreCase)
                        && textBox.Text == string.Empty)
                    {
                        textBox.Text = initTexts[0];
                    }
                }
                else
                {
                    if (e.RoutedEvent.Name.Equals("gotfocus", StringComparison.OrdinalIgnoreCase)
                        && textBox.Text == initText)
                    {
                        textBox.Text = string.Empty;
                    }
                    else if (e.RoutedEvent.Name.Equals("lostfocus", StringComparison.OrdinalIgnoreCase)
                        && textBox.Text == string.Empty)
                    {
                        textBox.Text = initText;
                    }
                }
            }
        }
        #endregion
    }

}
