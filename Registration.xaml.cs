using System.Windows;
using System.Windows.Controls;

namespace HW17_file_editor
{
    public partial class Registration : Window
    {
        public string accessMode = "";

        public Registration()
        {
            InitializeComponent();
        }
        private void accessModeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (accessModeTextBox.Text.Length >= 3)
            {
                registerButton.IsEnabled = true;
            }
            else
            {
                registerButton.IsEnabled = false;
            }
        }
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            if (accessModeTextBox.Text == "pro" || accessModeTextBox.Text == "trial")
            {
                accessMode = accessModeTextBox.Text;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Wrong key!");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
