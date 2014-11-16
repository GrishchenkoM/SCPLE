using System.Globalization;
using Word = Microsoft.Office.Interop.Word;


namespace Scple.Models
{
    public class ModelPatternDetermination : IPatternDetermination
    {
        public bool IsList(Word.Table _table, int row)
        {
            if (!(_table.Cell(row, 1).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("поз") &&
                 _table.Cell(row, 2).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("наименование")))
                return true;
            else 
                return false;
        }

        public bool IsSpecification(Word.Table _table, int row)
        {
            if (!_table.Cell(1, 1).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("№") ||
                (!(_table.Cell(1, 1).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("№") &&
                  _table.Cell(1, 3).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("поз")) &&
                !_table.Cell(1, 4).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("обозн")))
                return true;
            else
                return true;
        }
    }
}
