using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenPy_Translation_Tool.ViewModels
{
    internal class ParsedLineViewModel : INotifyPropertyChanged
    {
        private ParsedLine _parsedLine;
        public int? index;

        public int LineNumber
        {
            get => _parsedLine.LineNumber;
            set
            {
                _parsedLine.LineNumber = value;
                OnPropertyChanged(nameof(LineNumber));
            }
        }

        public string OriginalLine
        {
            get => _parsedLine.OriginalLine;
            set
            {
                _parsedLine.OriginalLine = value;
                OnPropertyChanged(nameof(OriginalLine));
            }
        }

        public string? TranslatedLine
        {
            get => _parsedLine.TranslatedLine;
            set
            {
                _parsedLine.TranslatedLine = value;
                OnPropertyChanged(nameof(TranslatedLine));
            }
        }

        public string? CommentLine
        {
            get => _parsedLine.CommentLine;
            set
            {
                _parsedLine.CommentLine = value;
                OnPropertyChanged(nameof(CommentLine));
            }
        }

        public ParsedLineViewModel(ParsedLine parsedLine)
        {
            _parsedLine = parsedLine;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ParsedLine GetParsedLine()
        {
            return _parsedLine;
        }
    }
}
