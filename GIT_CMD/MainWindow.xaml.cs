using System.Windows;
using System.Diagnostics;

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
                    WorkingDirectory = folder_link.Text,
                    FileName = "cmd.exe",
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                try
                {
                    Process cmd = Process.Start(startInfo);
                    //cmd.StandardInput.WriteLine(link_folder);
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
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Title = "Selection de l'emplacement";
            dialog.Filter = "Directory|*.this.directory"; // Prevents displaying files
            dialog.FileName = "select"; // Filename will then be "select.this.directory"

            // Show save file dialog box
            var result = dialog.ShowDialog();
            // Process save file dialog box results
            if (result == true)
            {
                string path = dialog.FileName;
                // Remove fake filename from resulting path
                path = path.Replace("\\select.this.directory", "");
                path = path.Replace(".this.directory", "");
                // If user has changed the filename, create the new directory
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                // Our final value is in path
                folder_link.Text = path;
            }
        }


    }
}
