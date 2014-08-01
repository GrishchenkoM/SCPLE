using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCPLE
{
    public class ElementDesignationObject
    {
        /// <summary>
        /// Delete all unnecessary symbols from name
        /// </summary>
        /// <param name="designatorName">Name of designator without transformation</param>
        public ElementDesignationObject(string designatorName)
        {
            
        }

        /// <summary>
        /// Name of designator
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Name of designator
        /// </summary>
        private string _name;
    }
}
