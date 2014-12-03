using System.Globalization;
using Word = Microsoft.Office.Interop.Word;


namespace Scple.Models
{
    /// <summary>
    /// Определение документа по шапке в таблице
    /// </summary>
    public class ModelPatternDetermination : IPatternDetermination
    {
        // <summary>
        /// Проверка документа на соответствие
        /// документу "Перечень элементов"
        /// </summary>
        /// <param name="_table">Таблица</param>
        /// <param name="row">Номер строки</param>
        /// <returns>Документ является "Перечнем элементов"</returns>
        public bool IsList(Word.Table _table, int row)
        {
            if (_table == null)
                return false;
            if (_table.Cell(row, 1).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("поз") &&
                 _table.Cell(row, 2).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("наименование"))
                return true;
            return false;
        }
        /// <summary>
        /// Проверка документа на соответствие
        /// документу "Спецификация"
        /// </summary>
        /// <param name="_table">Таблица</param>
        /// <param name="row">Номер строки</param>
        /// <returns>Документ является "Спецификацией"</returns>
        public bool IsSpecification(Word.Table _table, int row)
        {
            if (_table == null)
                return false;
            if ((_table.Cell(1, 1).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("№") &&
                  _table.Cell(1, 3).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("поз")) &&
                _table.Cell(1, 4).Range.Text.ToLower(CultureInfo.CurrentCulture).Contains("обозн"))
                return true;
            return false;
        }
    }
}
