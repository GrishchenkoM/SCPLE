using Word = Microsoft.Office.Interop.Word;

namespace Scple.Models
{
    /// <summary>
    /// Интерфейс определения документа по его шапке в таблице
    /// </summary>
    public interface IPatternDetermination
    {
        /// <summary>
        /// Проверка документа на соответствие
        /// документу "Перечень элементов"
        /// </summary>
        /// <param name="_table">Таблица</param>
        /// <param name="row">Номер строки</param>
        /// <returns>Документ является "Перечнем элементов"</returns>
        bool IsList(Word.Table _table, int row);
        /// <summary>
        /// Проверка документа на соответствие
        /// документу "Спецификация"
        /// </summary>
        /// <param name="_table">Таблица</param>
        /// <param name="row">Номер строки</param>
        /// <returns>Документ является "Спецификацией"</returns>
        bool IsSpecification(Word.Table _table, int row);
    }
}
