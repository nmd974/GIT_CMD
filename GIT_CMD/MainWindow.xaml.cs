using System.Windows;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GIT_CMD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Cloner(object sender, RoutedEventArgs e)
        {
            if(folder_link.Text == "")
            {
                MessageBox.Show("Veuillez renseigner le dossier où cloner ce repo !");
            }
            else if(link_repo.Text == "")
            {
                MessageBox.Show("Veuillez renseigner le repo à cloner !");
            }
            else
            {
                string link_folder = "cd " + folder_link.Text;
                string command = "git clone " + link_repo.Text;

                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    // WorkingDirectory = @"J:\Projets\C#\APPS",
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                };

                try
                {
                    Process cmd = Process.Start(startInfo);
                    cmd.StandardInput.WriteLine(link_folder);
                    cmd.StandardInput.WriteLine(command);
                    cmd.StandardInput.Close();
                    cmd.WaitForExit();
                    MessageBox.Show("Le repo a bien été cloné !");
                    folder_link.Clear();
                    link_repo.Clear();
                    System.Environment.Exit(0);
                }
                catch (System.ComponentModel.Win32Exception win32Exception)
                {
                    //The system cannot find the file specified...
                    Debug.WriteLine(win32Exception.Message);
                    MessageBox.Show(win32Exception.Message);
                }
            }
            
        }
        private void SelectDirection(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You selected: " + dialog.FileName);
            }
        }


    }
}
