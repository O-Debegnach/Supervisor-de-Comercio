using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Business_Layer
{
    public enum Focus { Got, Lost }
    public abstract class TextManager
    {
        public enum TextMode { Alphbetic, Numeric, Alphanumeric, Money }
        public static void ToggleTextBox(TextBox textBox, string initText, Focus focus)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text) && focus == Focus.Lost)
            {
                textBox.Text = initText;
            }
            else if (string.Equals(textBox.Text, initText, StringComparison.CurrentCultureIgnoreCase) && focus == Focus.Got)
            {
                textBox.Text = string.Empty;
            }
        }

        public class EnableToggleText
        {

            #region Variables

            private readonly bool workingWithList;
            private readonly string initText = string.Empty;
            private readonly List<string> initTexts = new List<string>();
            private readonly TextBox textBox;
            private readonly TextMode textMode;
            private string Text = string.Empty;

            #endregion

            #region Constructores
            public EnableToggleText(TextBox textBox, string initText, TextMode textMode = TextMode.Alphanumeric)
            {
                this.initText = initText;
                workingWithList = false;
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


            public EnableToggleText(TextBox textBox, List<string> initTexts, TextMode textMode = TextMode.Alphanumeric)
            {
                this.initTexts = initTexts;
                workingWithList = true;
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


            public EnableToggleText(TextBox textBox, string[] initTexts, TextMode textMode = TextMode.Alphanumeric)
            {
                this.initTexts = new List<string>(initTexts);
                workingWithList = true;
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
                    textBox.PreviewTextInput += Money; ;
                    textBox.PreviewKeyDown += Borrar;
                }
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
                    //Money(textBox, /*a*/new TextCompositionEventArgs(e.KeyboardDevice, new TextComposition(InputManager.Current, Keyboard.FocusedElement, "")));
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

                textBox.Text = s.Insert(s.Length - 2, ",");
            }

            private void Alphbetic(object sender, TextCompositionEventArgs e)
            {
                Regex regex = new Regex("[0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }

            private void Numeric(object sender, TextCompositionEventArgs e)
            {
                Regex regex = new Regex("[^0-9,]+");
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
}
