using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace EnvironmentVariableEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string strCmdText;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void apply_button_Click(object sender, RoutedEventArgs e)
        {
            string variable = var_textbox.Text;
            string value = value_textbox.Text;

            if (string.IsNullOrEmpty(variable) || string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Enter environment variable name and corresponding value.");
            }
            else 
            {
                if (variable.Equals("PATH"))
                {
                    strCmdText = "setx PATH \"%PATH%;" + value + "\" /M";
                    //Console.WriteLine(strCmdText);
                    ExecuteCommand(strCmdText);
                }
                else
                {
                    strCmdText = "setx -m " + variable + " \"" + value + "\"";
                    //Console.WriteLine(strCmdText);
                    ExecuteCommand(strCmdText);
                }
            }

        }

        private void ExecuteCommand(string cmdtxt)
        {

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(cmdtxt);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            MessageBox.Show("Environment variable successfully added.");


        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            var_textbox.Text = "";
            value_textbox.Text = "";
        }
    }
}
