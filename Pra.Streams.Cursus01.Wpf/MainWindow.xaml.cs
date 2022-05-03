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
using System.IO;

namespace Pra.Streams.Cursus01.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string errorMessage;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // pas eventueel hieronder pad en bestandsnaam aan
            txtContent.Text = ReadFile(@"c:\howest\bestand1.txt");
            txtFileName.Text = "bestand1.txt";
            ShowErrorMessage();
        }

        private void BtnReadTextFile_Click(object sender, RoutedEventArgs e)
        {
            string fileName;
            fileName = txtFileName.Text;
            // pas eventueel hieronder pad aan
            txtContent.Text = ReadFile(@"c:\howest\" + fileName);
            ShowErrorMessage();
        }

        private void BtnWriteTextFile_Click(object sender, RoutedEventArgs e)
        {
            string content = txtContent.Text;
            string fullFilePath = @"c:\howest\" + txtFileName.Text;
            WriteFile(fullFilePath,content);
            ShowErrorMessage();
        }

        string ReadFile(string fullFilePath)
        {
            string fileContent = "";
            errorMessage = "";

            try
            {
                using (StreamReader streamReader = new StreamReader(fullFilePath, Encoding.Default, true))
                {
                    fileContent = streamReader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                errorMessage = $"Het bestand {fullFilePath} is niet gevonden.";
            }
            catch (IOException)
            {
                errorMessage = $"Het bestand {fullFilePath} kan niet geopend worden.\nProbeer het te sluiten.";
            }
            catch (Exception e)
            {
                errorMessage = $"Er is een fout opgetreden. {e.Message}";
            }
            return fileContent;
        }

        bool WriteFile(string fullFilePath, string content)
        {
            bool isSuccessfullyWritten = false;
            errorMessage = "";
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(fullFilePath))
                {
                    streamWriter.Write(content);
                }
                isSuccessfullyWritten = true;
            }
            catch (DirectoryNotFoundException)
            {
                errorMessage = $"De map bestaat niet op deze computer";
            }
            catch (IOException)
            {
                errorMessage = $"Het bestand {fullFilePath} kan niet weggeschreven worden.\n" +
                                $"Probeer het geopende bestand op die locatie te sluiten.";
            }
            catch (Exception e)
            {
                errorMessage = $"Er is een fout opgetreden. {e.Message}";
            }
            return isSuccessfullyWritten;
        }

        void ShowErrorMessage()
        {
            tbkErrors.Text = errorMessage;
            if (errorMessage == "" || errorMessage == null)
            {
                tbkErrors.Visibility = Visibility.Hidden;
            }
            else
            {
                tbkErrors.Visibility = Visibility.Visible;
            }
        }

    }
}
