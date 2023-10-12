using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.Models;
using Core.Parsing.Serialization;
using RenPy_Translation_Tool.ViewModels;

namespace RenPy_Translation_Tool.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkspaceViewModel currentWorkspace = new WorkspaceViewModel(new Workspace("", ""));
        private ParsedFileViewModel currentFile = new ParsedFileViewModel(new ParsedFile(null, null, new List<ParsedLine>()));
        private ParsedLineViewModel currentLine = new ParsedLineViewModel(new ParsedLine(0, ""));

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ReloadWorkspace()
        {
            if (currentWorkspace == null || currentWorkspace.Files == null)
            {
                return;
            }
            if (currentWorkspace.Files.Count == 0)
            {
                filesListView.ItemsSource = new ObservableCollection<ParsedFile>();
                ReloadLines();
                System.Windows.MessageBox.Show("This project don't have any files.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            filesListView.ItemsSource = currentWorkspace.Files.Select(file => file.FileName);

            currentFile = currentWorkspace.Files.First();
            ReloadLines();
        }

        public void ReloadLines()
        {
            ObservableCollection<ParsedLineViewModel> lines = new ObservableCollection<ParsedLineViewModel>();

            foreach(ParsedLineViewModel line in currentFile.Lines)
            {
                lines.Add(line);
            }

            linesDataGrid.ItemsSource = lines;
        }

        public void ReloadInputs()
        {
            originalLineTextBox.Text = currentLine.OriginalLine;
            translatedLineTextBox.Text = currentLine.TranslatedLine;
            commentLineTextBox.Text = currentLine.CommentLine;
        }

        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            NewProjectWindow newProjectWindow = new NewProjectWindow();
            newProjectWindow.Owner = this;
            bool? result =  newProjectWindow.ShowDialog();
            if (result == false)
            {
                return;
            }
            this.currentWorkspace = newProjectWindow.workspace;
            ReloadWorkspace();
        }

        private void filesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentWorkspace == null || currentWorkspace.Files == null || currentWorkspace.Files.Count == 0)
            {
                return;
            }
            currentFile = currentWorkspace.Files[filesListView.SelectedIndex];
            ReloadLines();
        }

        private void linesDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ParsedLineViewModel? line = linesDataGrid.SelectedItem as ParsedLineViewModel;
            if (line != null)
            {
                currentLine = line;
                ReloadInputs();
            }
        }

        private void copyOriginal_Click(object sender, RoutedEventArgs e)
        {
            translatedLineTextBox.Text = originalLineTextBox.Text;
            ReloadInputs();
        }

        private void translatedLineTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentLine.TranslatedLine = translatedLineTextBox.Text;
        }

        private void commentLineTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentLine.CommentLine = commentLineTextBox.Text;
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            ExportProjectWindow ExportProjectWindow = new ExportProjectWindow(currentWorkspace.GetWorkspace());
            ExportProjectWindow.Owner = this;
            ExportProjectWindow.ShowDialog();
        }

        private void saveProject_Click(object sender, RoutedEventArgs e)
        {
            using (SaveFileDialog fileDialog = new SaveFileDialog())
            {
                System.Windows.Forms.DialogResult? result = fileDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    XmlSerializer<Workspace> serializer = new XmlSerializer<Workspace>(fileDialog.FileName);
                    serializer.Serialize(currentWorkspace.GetWorkspace());
                }
            }
        }

        private void openProject_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                System.Windows.Forms.DialogResult? result = fileDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    currentWorkspace = new WorkspaceViewModel(new XmlSerializer<Workspace>(fileDialog.FileName).Deserialize());
                }
            }
            ReloadWorkspace();
        }
    }
}
