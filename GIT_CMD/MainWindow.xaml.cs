using System.Windows;
using System.Diagnostics;
using System.Windows.Forms;

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

        private void Select_Direction(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }
        private void Cloner(object sender, RoutedEventArgs e)
        {
            string link_folder = "cd " + folder_link.Text;
            string command = "git clone " +  link_repo.Text;
            
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
}
