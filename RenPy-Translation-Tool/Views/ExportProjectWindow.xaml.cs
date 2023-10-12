using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Core.Models;
using Core.Services;

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
