using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Scple
{
    public class ProductRepository
    {
        #region Constructor
        // реализация Singleton
        static readonly ProductRepository _instance = new ProductRepository();
        public static ProductRepository Instance
        {
            get { return _instance; }
        }
        public ProductRepository()
        {
            Products = new List<Product>();
        }
        #endregion

        #region Variables
        /// <summary>
        /// Коллекция 
        /// </summary>
        public List<Product> Products;
        #endregion

        public void SortByDesignationName()
        {
            for (int i = 0; i < Products.Count; ++i)
                QuickSort(Products[i].ElementsName, 0, Products[i].ElementsName.Count - 1);
        }
        private void QuickSort(List<ElementNameObject> arrayList, int left, int right)
        {
            if (left >= right)
                return;
            int pivot = Partition(arrayList, left, right);
            QuickSort(arrayList, left, pivot - 1);
            QuickSort(arrayList, pivot + 1, right);
        }
        private int Partition(List<ElementNameObject> arrayList, int left, int right)
        {
            ElementNameObject temp;
            int marker = left;
            for (int i = left; i <= right; i++)
            {
                if (Comparison(arrayList[i].Name, arrayList[right].Name) == -1 ||
                    Comparison(arrayList[i].Name, arrayList[right].Name) == 0)
                {
                    temp = arrayList[marker];
                    arrayList[marker] = arrayList[i];
                    arrayList[i] = temp;
                    marker += 1;
                }
            }
            return marker - 1;
        }
        private int Comparison(string Obj1, string Obj2)
        {
            for (int i = 0; i < (Obj1.Length > Obj2.Length ? Obj2.Length : Obj1.Length); i++)
            {
                if (Obj1.ToCharArray()[i] < Obj2.ToCharArray()[i]) return -1;
                if (Obj1.ToCharArray()[i] > Obj2.ToCharArray()[i]) return 1;
            }
            return 0;
        }
    }
}
