using System;
using System.Windows.Controls;

namespace Business_Layer
{
    public enum Focus { Got, Lost }
    public abstract partial class TextManager
    {
        
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
    }
}
