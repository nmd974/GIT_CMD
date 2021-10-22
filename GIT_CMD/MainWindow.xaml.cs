using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
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
                string command = "git clone " + link_repo.Text;
                string folder = folder_link.Text;
                
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WorkingDirectory = folder,
                    FileName = "cmd.exe",
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                try
                {
                    Process cmd = Process.Start(startInfo);
                    cmd.StandardInput.WriteLine(command);
                    cmd.StandardInput.Close();
                    cmd.WaitForExit();
                    MessageBox.Show("Le repo a bien été cloné !");
                    link_repo.Clear();
                }
                catch (System.ComponentModel.Win32Exception win32Exception)
                {
                    Debug.WriteLine(win32Exception.Message);
                    MessageBox.Show(win32Exception.Message);
                }

            }
            
        }
        private void SelectDirection(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Title = "Selection de l'emplacement";
            dialog.Filter = "Directory|*.this.directory";
            dialog.FileName = "select";

            var result = dialog.ShowDialog();
            if (result == true)
            {
                string path = dialog.FileName;
                path = path.Replace("\\select.this.directory", "");
                path = path.Replace(".this.directory", "");

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                folder_link.Text = path;
            }
        }

        private void Push(object sender, RoutedEventArgs e)
        {
            string folder = folder_link.Text;
            string check_git_folder = folder + "/toto";
            var fileInfo = new FileInfo(check_git_folder);
            if (fileInfo.Exists)
            {
                Debug.WriteLine("NOK");
            }
            if (fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
            {
                Debug.WriteLine("OK");
            }
            
            if (MessagePush.Text == "")
            {
                alert_push.Visibility = 0;
            }
            else if (System.IO.Directory.Exists(check_git_folder))
            {
                MessageBox.Show("Pas de repo git existant dans ce dossier !");
            }
            else
            {

                string[] commands;
                commands = new string[3]{
                    "git add .",
                    "git commit -m " + MessagePush.Text,
                    "git push"
                };

                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WorkingDirectory = folder,
                    FileName = "cmd.exe",
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                try
                {
                    foreach (string c in commands)
                    {
                        Process cmd = Process.Start(startInfo);
                        cmd.StandardInput.WriteLine(c);
                        cmd.StandardInput.Close();
                        cmd.WaitForExit();
                    }
                    
                    MessageBox.Show("Le push s'est bien passé !");
                }
                catch (System.ComponentModel.Win32Exception win32Exception)
                {
                    Debug.WriteLine(win32Exception.Message);
                    MessageBox.Show(win32Exception.Message);
                }
            }
        }

        
    }

    public partial class ProcessCommand : Process
    {
        public ProcessCommand(string folder)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = folder,
                FileName = "cmd.exe",
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                RedirectStandardInput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            try
            {
                Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception win32Exception)
            {
                Debug.WriteLine(win32Exception.Message);
                MessageBox.Show(win32Exception.Message);
            }
                        
        }
        
    }
}

