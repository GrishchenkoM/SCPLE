using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCPLE
{
    public class ElementNameObject
    {
        public ElementNameObject(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Getter/Setter of name of element
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Name of element
        /// </summary>
        private string _name;

        /// <summary>
        /// For comparison names
        /// </summary>
        private string _nameWihoutSpaces;

        /// <summary>
        /// Element is SMD element
        /// </summary>
        private bool _isSMD;

        /// <summary>
        /// List of elements by current name
        /// </summary>
        //public List<ElementDesignationObject> elements_Designator; // заменить на строку
        public string elements_Designator;

        private int designatorsCount;

        public int DesignatorsCount
        {
            get { return designatorsCount; }
        }
    }

}
