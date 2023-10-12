using Core.Models;
using Core.Parsing.Parsers;
using RenPy_Translation_Tool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms; // just for open folder dialog

namespace RenPy_Translation_Tool.Views
{
    public partial class NewProjectWindow : Window
    {
        internal WorkspaceViewModel workspace = new WorkspaceViewModel(new Workspace("", ""));
        public NewProjectWindow()
        {
            InitializeComponent();
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            string workspaceName = workspaceNameTextBox.Text;
            string workspaceDescription = workspaceDescriptionTextBox.Text;
            string originalDirectory = importDirectoryTextBox.Text;
            string extention = ((ComboBoxItem)extentionComboBox.SelectedItem).Content.ToString();

            List<Regex> regexes = new List<Regex>();

            foreach (string line in regexTextBox.Text.Split("\n"))
            {
                try
                {
                    regexes.Add(new Regex(line));
                }
                catch (ArgumentException)
                {
                    System.Windows.MessageBox.Show($"Regex is not valid\n{line}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            List<ParsedFile> parsedFiles = AllFilesParser.ParseAllFiles(originalDirectory, regexes, extention).ToList();

            this.workspace = new WorkspaceViewModel(new Workspace(workspaceName, originalDirectory, workspaceDescription, parsedFiles));
            DialogResult = true;
            this.Close();
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult? result = folderDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    importDirectoryTextBox.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}
