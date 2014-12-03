using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scple;

namespace Scple
{
    class DesignatorNameRepository
    {
        #region Constructor
        // реализация Singleton
        static readonly DesignatorNameRepository _instance = new DesignatorNameRepository();
        public static DesignatorNameRepository Instance
        {
            get { return _instance; }
        }
        /// <summary>
        /// Конструткор
        /// </summary>
        public DesignatorNameRepository()
        {
            _designatorNames = new List<DesignatorName>();

            _designatorNames.Add(new DesignatorName("BQ", "резонатор"));
            _designatorNames.Add(new DesignatorName("C", "конденсатор", "ионистор"));
            _designatorNames.Add(new DesignatorName("DR", "сборка резисторная", "резисторная сборка"));
            _designatorNames.Add(new DesignatorName("VD", "диод", "супрессор", "диодный мост"));
            _designatorNames.Add(new DesignatorName("D", "микросхема"));
            _designatorNames.Add(new DesignatorName("EL", "лампа осветительная", "осветительная лампа"));
            _designatorNames.Add(new DesignatorName("F", "предохранитель", "вставка"));
            _designatorNames.Add(new DesignatorName("HG", "индикатор"));
            _designatorNames.Add(new DesignatorName("HL", "индикатор"));
            _designatorNames.Add(new DesignatorName("K", "реле"));
            _designatorNames.Add(new DesignatorName("L", "дроссель"));
            _designatorNames.Add(new DesignatorName("RU", "варистор"));
            _designatorNames.Add(new DesignatorName("R", "резистор"));
            
            _designatorNames.Add(new DesignatorName("VT", "транзистор"));
            _designatorNames.Add(new DesignatorName("XP", "вилка", "держатель", "колодка"));
            _designatorNames.Add(new DesignatorName("XT", "колодка", "клемма"));
            _designatorNames.Add(new DesignatorName("X", "соединитель", "джампер", "розетка"));
            _designatorNames.Add(new DesignatorName("S", "кнопка", "микрокнопка", "микропереключатель"));
            _designatorNames.Add(new DesignatorName("ZQ", "резонатор"));
            _designatorNames.Add(new DesignatorName("T", "трансформатор"));
        }
        #endregion

        public List<string> ReturnDesignatorsName(string designator)
        {
            if (designator == null) return null;
            foreach (DesignatorName designatorName in _designatorNames)
            {
                if (designator.Contains(designatorName.Designator))
                    return designatorName.VariableNames;
            }
            return null;
        }
        
        #region Variables
        private List<DesignatorName> _designatorNames;
        #endregion

    }
}
