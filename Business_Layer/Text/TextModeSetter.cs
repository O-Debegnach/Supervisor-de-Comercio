using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Business_Layer.Text
{
    public enum TextMode { Alphbetic, Numeric, Alphanumeric, Money, TimeSpan }

    public static class TextModeSetter
    {
        #region Funciones

        private static void TimeSpanMode(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
            TextBox textBox = sender as TextBox;

            Regex regex = new Regex("[0-9]+");
            if (regex.IsMatch(e.Text))
            {
                var a = textBox.CaretIndex;
                if (a >= 0 && a < textBox.Text.Length)
                {
                    if (a != 2)
                    {
                        textBox.Text = textBox.Text.Remove(a, 1);
                        textBox.Text = textBox.Text.Insert(a, e.Text);
                        if (a + 1 == 2) a++;
                    }
                    string[] time = textBox.Text.Split(GetSpecialCharacter(textBox));

                    if (a == 2 && Convert.ToInt32(time[0]) > 23)
                    {
                        time[0] = "23";
                    }
                    else if (a == textBox.Text.Length - 1 && Convert.ToInt32(time[1]) > 59)
                    {
                        time[1] = "59";
                    }
                    textBox.Text = time[0] + GetSpecialCharacter(textBox) + time[1];
                    textBox.CaretIndex = a + 1;
                }
            }

        }

        private static void PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var text = sender as TextBox;
            var a = text.CaretIndex;
            if (text.CaretIndex < 3) text.CaretIndex = 0;
            else if (text.CaretIndex > 3) text.CaretIndex = 3;
        }

        private static void Borrar(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                e.Handled = true;
                TextBox textBox = sender as TextBox;
                if (textBox.CaretIndex > 0 && textBox.CaretIndex <= textBox.Text.Length)
                {
                    textBox.CaretIndex -= 1;
                    var a = textBox.CaretIndex;
                    if (textBox.CaretIndex != 2)
                    {
                        textBox.Text = textBox.Text.Remove(a, 1);
                        textBox.Text = textBox.Text.Insert(a, "0");
                    }
                    textBox.CaretIndex = a;
                }
            }
        }

        #endregion Funciones
        #region Properties
        #region InitTextProperty
        private static string GetInitText(DependencyObject obj)
        {
            return (string)obj.GetValue(InitTextProperty);
        }

        private static void SetInitText(DependencyObject obj, string value)
        {
            obj.SetValue(InitTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for InitText.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty InitTextProperty =
            DependencyProperty.RegisterAttached("InitText", typeof(string), typeof(TextModeSetter), new PropertyMetadata(string.Empty));
        #endregion InitTextProperty

        #region TextModeProperty

        public static TextMode GetTextMode(DependencyObject obj)
        {
            if (obj != null)
            {
                return (TextMode)obj.GetValue(TextModeProperty);
            }
            else
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }

        public static void SetTextMode(DependencyObject obj, TextMode value)
        {
            obj.SetValue(TextModeProperty, value);
            UIElement uIElement = obj as UIElement;
        }
        // Using a DependencyProperty as the backing store for TextMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextModeProperty =
            DependencyProperty.RegisterAttached("TextMode", typeof(TextMode), typeof(TextModeSetter), new PropertyMetadata(TextMode.Alphanumeric, OnLoaded));

        private static void OnLoaded(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TextBox textBox) || (e.NewValue is TextMode) == false)
            {
                return;
            }

            if ((TextMode)e.NewValue == TextMode.TimeSpan)
            {
                textBox.PreviewTextInput += TimeSpanMode;
                textBox.PreviewKeyDown += Borrar;
                textBox.PreviewMouseUp += PreviewMouseUp;
                SetInitText(d, "00:00");
                SetSpecialCharacter(d, ':');
                textBox.Text = GetInitText(d);
            }
        }




        #endregion TextModeProperty

        #region SpecialCharacterProperty


        public static char GetSpecialCharacter(DependencyObject obj)
        {
            return (char)obj.GetValue(SpecialCharacterProperty);
        }

        public static void SetSpecialCharacter(DependencyObject obj, char value)
        {
            obj.SetValue(SpecialCharacterProperty, value);
        }

        // Using a DependencyProperty as the backing store for SpecialCharacter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpecialCharacterProperty =
            DependencyProperty.RegisterAttached("SpecialCharacter", typeof(char), typeof(TextModeSetter), new PropertyMetadata('\0'));


        #endregion SpecialCharacterProperty

        #endregion Properties
    }
}
