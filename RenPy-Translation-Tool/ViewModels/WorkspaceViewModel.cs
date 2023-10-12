using Core.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RenPy_Translation_Tool.ViewModels
{
    internal class WorkspaceViewModel
    {
        private Workspace _workspace;

        public string Name
        {
            get => _workspace.Name;
            set
            {
                _workspace.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _workspace.Description;
            set
            {
                _workspace.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string OriginalDirectory
        {
            get => _workspace.OriginalDirectory;
            set
            {
                _workspace.OriginalDirectory = value;
                OnPropertyChanged(nameof(OriginalDirectory));
            }
        }

        public List<ParsedFileViewModel> Files
        {
            get => _workspace.Files.Select(file => new ParsedFileViewModel(file)).ToList();
            set
            {
                _workspace.Files = (List<ParsedFile>)value.Select(file => file.GetParsedFile());
                OnPropertyChanged(nameof(Files));
            }
        }

        public WorkspaceViewModel(Workspace workspace)
        {
            _workspace = workspace;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Workspace GetWorkspace()
        {
            return _workspace;
        }
    }
}
