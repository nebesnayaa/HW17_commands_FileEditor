using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace HW17_file_editor
{
    public partial class MainWindow : Window
    {
        string mode = "unregistered";

        public MainWindow()
        {
            InitializeComponent();

            CommandBinding binding;

            binding = new CommandBinding(ApplicationCommands.New);
            binding.Executed += NewCommand_Executed;
            binding.CanExecute += FileCommands_CanExecute;

            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Open);
            binding.Executed += OpenCommand_Executed;
            binding.CanExecute += FileCommands_CanExecute;

            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Save);
            binding.Executed += SaveCommand_Executed;
            binding.CanExecute += FileCommands_CanExecute;
            CommandBindings.Add(binding);


            binding = new CommandBinding(ApplicationCommands.Close);
            binding.Executed += CloseCommand_Executed;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Cut);
            binding.CanExecute += EditCommands_CanExecute;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Copy);
            binding.CanExecute += EditCommands_CanExecute;
            CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Paste);
            binding.CanExecute += EditCommands_CanExecute;
            CommandBindings.Add(binding);
        }

        /* File */
        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            textField.Text = "";
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                textField.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, textField.Text);
            }
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void FileCommands_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mode == "trial" || mode == "pro")
            {
                e.CanExecute = true;
            }
        }
        /* \File */

        private void EditCommands_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mode == "pro")
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void RegisterCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Registration registrationWindow = new Registration();
            if (DialogResult == registrationWindow.ShowDialog())
            {
                mode = registrationWindow.accessMode;
            }
            mode = registrationWindow.accessMode;
        }

        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if ((e.Command == ApplicationCommands.Copy ||
                 e.Command == ApplicationCommands.Cut ||
                 e.Command == ApplicationCommands.Paste) && mode != "pro")
            {
                e.Handled = true;
            }
        }
    }

    public class DataCommands
    {
        private static RoutedUICommand Register;
        static DataCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
            Register = new RoutedUICommand("Register", "Register", typeof(DataCommands), inputs);
        }
        public static RoutedUICommand _Register
        {
            get { return Register; }
        }
    }
}
