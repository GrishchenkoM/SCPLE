using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCPLE
{
    public class Product
    {
        public Product(string name)
        {
            _name = name;
        }
        
        public string Name
        {
            get { return _name; }
        }

        private string _name;
        private string _lettering;
        
        public string manufacturers;
        public List<ElementNameObject> elements_Name;
    }
}
