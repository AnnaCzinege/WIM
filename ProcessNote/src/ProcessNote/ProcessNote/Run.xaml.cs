using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Run : Window
    {
        public Run()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            setRunWin();
            base.OnClosed(e);
        }

        private static void setRunWin()
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.RunWin = null;
        }

        private void clickBtn(object sender, RoutedEventArgs e)
        {
            NewTask();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                NewTask();
            }
        }

        private void NewTask()
        {
            if (!string.IsNullOrEmpty(textField.Text))
            {
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = textField.Text;
                    proc.Start();
                    setRunWin(); //Run win closed with X button and w/ running task too
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Wrong task", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
