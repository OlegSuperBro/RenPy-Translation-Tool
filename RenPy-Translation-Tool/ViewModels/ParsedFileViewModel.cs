using Core.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RenPy_Translation_Tool.ViewModels
{
    internal class ParsedFileViewModel : INotifyPropertyChanged
    {
        private ParsedFile _parsedFile;
        public int? index;

        public string OriginalPath
        {
            get => _parsedFile.OriginalPath;
            set
            {
                _parsedFile.OriginalPath = value;
                OnPropertyChanged(nameof(OriginalPath));
            }
        }

        public string FileName
        {
            get => _parsedFile.FileName;
            set
            {
                _parsedFile.FileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public List<ParsedLineViewModel> Lines
        {
            get => _parsedFile.Lines.Select(file => new ParsedLineViewModel(file)).ToList();
            set
            {
                _parsedFile.Lines = (List<ParsedLine>)value.Select(line => line.GetParsedLine());
                OnPropertyChanged(nameof(Lines));
            }
        }

        public ParsedFileViewModel(ParsedFile parsedFile)
        {
            _parsedFile = parsedFile;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ParsedFile GetParsedFile()
        {
            return _parsedFile;
        }
    }
}
