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
            elements_Designator = new List<string>();
            _name = name;
            designatorsCount = 0;
        }

        //public void Add(string designatorName)
        //{
        //    string temp = designatorName;
        //    //if (!designatorName.Contains(_letter))
        //    //    return;


            

        //    List<string> obj = new List<string>();
        //    while (designatorName.Contains(",") || designatorName.Contains("...") || designatorName != "")
        //    {
        //        if (designatorName.Contains(",") && (designatorName.Contains("...")))
        //        {
        //            if (designatorName.IndexOf(",") < designatorName.IndexOf("..."))
        //            {
        //                int firstposition = designatorName.IndexOf(",");
        //                string tempObj = designatorName.Substring(0, designatorName.IndexOf(","));
        //                obj.Add(tempObj);
        //                ++designatorsCount;
        //                if (_isArrayOfElements)
        //                {
        //                    designatorsCount += Convert.ToInt32(obj[obj.Count - 1].Replace(_letter, null)) -
        //                                        Convert.ToInt32(obj[obj.Count - 2].Replace(_letter, null)) - 1;
        //                    _isArrayOfElements = false;
        //                }
        //                designatorName.Replace(tempObj + ",", null);
        //            }
        //            else if (designatorName.IndexOf(",") > designatorName.IndexOf("..."))
        //            {
        //                if (designatorName.IndexOf("...") == 0)
        //                {
        //                    if (designatorName.Contains(","))
        //                    {
        //                        string tempObj = designatorName.Replace("...", null);
        //                        tempObj = tempObj.Replace(",", null);
        //                        obj.Add(tempObj);
        //                        ++designatorsCount;
        //                        designatorsCount += Convert.ToInt32(obj[obj.Count - 1].Replace(_letter, null)) -
        //                                            Convert.ToInt32(obj[obj.Count - 2].Replace(_letter, null)) - 1;
        //                        _isArrayOfElements = false;
        //                    }
        //                }
        //                else
        //                {
        //                    designatorName.Replace("...", null);
        //                    obj.Add(designatorName);
        //                    ++designatorsCount;
        //                    _isArrayOfElements = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            obj.Add(designatorName);
        //            ++designatorsCount;
        //        }

        //    }
        //    designatorName.Replace(_letter, null);
        //    int digit;

        //    if (designatorName.Contains("..."))
        //    {

        //    }
        //    else
        //    {
        //        designatorName.Replace(",", null);
        //    }

        //    digit = Convert.ToInt32(designatorName);
        //    elements_Designator.Add(new ElementDesignationObject(_letter + Convert.ToString(digit)));
        //}

        //public bool DesignatorService()
        //{
        //    for (int i = 0; i < elements_Designator.Length; ++i)
        //    {
        //        elements_Designator.Substring(0, elements_Designator.IndexOf("|") + 1);
        //        if (elements_Designator.IndexOf(_letter, 0) == 0)
        //        {
        //            if (elements_Designator.Contains("..."))
        //            {
        //                int position = elements_Designator.IndexOf("...");
        //            }
        //            else if (elements_Designator.Contains(","))
        //            {}

                    
        //        }
        //    }
        //    return true;
        //}

        /// <summary>
        /// Getter/Setter of name of element
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int DesignatorsCount
        {
            get { return designatorsCount; }
            set { designatorsCount = value; }
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
        public List<string> elements_Designator;

        private string[] elements_DesignatorArray;

        private int[] elementsNumbers;

        private int designatorsCount;

        private bool _isArrayOfElements;
        
    }

}
