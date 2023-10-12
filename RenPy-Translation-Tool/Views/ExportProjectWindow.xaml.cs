using Core.Models;
using Core.Services;
using System.Windows;
using System.Windows.Forms;

namespace RenPy_Translation_Tool.Views
{
    /// <summary>
    /// Interaction logic for ExportProjectWindow.xaml
    /// </summary>
    public partial class ExportProjectWindow : Window
    {
        Workspace workspace;
        public ExportProjectWindow(Workspace workspace)
        {
            InitializeComponent();
            this.workspace = workspace;
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            FileService.ExportWorkspace(this.workspace, exportDirectoryTextBox.Text);
        }

        private void openExportDirectory_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult? result = folderDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    exportDirectoryTextBox.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}
