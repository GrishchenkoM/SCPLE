using Word = Microsoft.Office.Interop.Word;

namespace Scple.Models
{
    public interface IPatternDetermination
    {
        bool IsList(Word.Table _table, int row);
        bool IsSpecification(Word.Table _table, int row);
    }
}
