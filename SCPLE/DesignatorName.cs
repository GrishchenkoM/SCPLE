using System;
using System.Collections.Generic;

namespace Scple
{
    public class DesignatorName
    {
        public DesignatorName(string designator, params string[] names)
        {
            _designator = designator;
            _name = names[0];
            VariableNamesList = new List<string>();
            for (int i = 0; i < names.Length; ++i)
                VariableNamesList.Add(names[i]);
        }
        public string Name
        {
            get { return _name; }
        }
        public string Designator
        {
            get { return _designator; }
        }
        public List<string> VariableNames
        {
            get { return VariableNamesList; }
        }
        
        private string _name;
        private string _designator;
        private List<string> VariableNamesList;
    }
}
