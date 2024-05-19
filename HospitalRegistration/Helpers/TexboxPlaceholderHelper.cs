using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace HospitalRegistration.Helpers
{
    public class TexboxPlaceholderHelper
    {
        private string defaultText = string.Empty;
        public void CreatePlaceHolder(TextBox textbox, string text)
        {
            defaultText = text;

            textbox.GotFocus += RemoveText;
            textbox.LostFocus += AddText;

            if (string.IsNullOrWhiteSpace(textbox.Text))
            {
                textbox.Text = defaultText;
                textbox.Foreground = new SolidColorBrush(Color.FromArgb(64, 0, 0, 0));
            }
        }

        private void RemoveText(object sender, EventArgs e)
        {
            TextBox textbox = (sender as TextBox);
            textbox.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            if (textbox.Text == defaultText)
            {
                textbox.Text = string.Empty;
            }
        }

        private void AddText(object sender, EventArgs e)
        {
            TextBox textbox = (sender as TextBox);

            if (string.IsNullOrWhiteSpace(textbox.Text))
            {
                textbox.Foreground = new SolidColorBrush(Color.FromArgb(64, 0, 0, 0));
                textbox.Text = defaultText;
            }
        }
    }
}
