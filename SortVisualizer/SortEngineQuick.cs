using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    class SortEngineQuick : ISortEngine
    {
        //private bool _sorted = false;
        private int[] TheArray;
        private Graphics g;
        private int MaxVal;
        private int thickness;
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        //Constructor for Merge Sort
        public SortEngineQuick(int[] TheArray_In, Graphics g_In, int MaxVal_In, int thickness_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            MaxVal = MaxVal_In;
            thickness = thickness_In;

        }

        public bool IsSorted()
        {
            for (int i = 0; i < TheArray.Count() - 1; i++)
            {
                if (TheArray[i] > TheArray[i + 1]) return false;
            }

            return true;
        }

        public void NextStep()
        {
            quickSort(TheArray, 0, TheArray.Count() - 1);
        }

        //Recursive
        void quickSort(int [] array, int start, int end)
        {
            if (start < end)
            {

                // find the pivot point
                // elements smaller than pivot are on left of pivot
                // elements greater than pivot are on righ of pivot
                int pi = partition(array, start, end);

                // recursive call on the left of pivot
                quickSort(array, start, pi - 1);

                // recursive call on the right of pivot
                quickSort(array, pi + 1, end);
            }
        }

        // function to rearrange array and find pivot point
        private int partition(int [] array, int start, int end)
        {

            // select the rightmost element as pivot
            int pivot = array[end];

            // pointer for greater element
            int i = (start - 1);

            // compare them with the pivot
            for (int j = start; j < end; j++)
            {
                if (array[j] <= pivot)
                {

                    // if element smaller than pivot is found
                    // swap it with the greater element pointed by i
                    i++;

                    // swap element at i with element at j
                    Swap(i, j);
                }
            }

            // swap pivot with the greater element at i
            Swap(i + 1, end);

            // return the pivot point
            return (i + 1);
        }


        private void Swap(int i, int p)
        {
            //swap here
            int temp = TheArray[i];
            TheArray[i] = TheArray[p];
            TheArray[p] = temp;

            DrawBar(i, TheArray[i]);
            DrawBar(p, TheArray[p]);
        }

        //Draws NewBars
        private void DrawBar(int position, int height)
        {
            g.FillRectangle(BlackBrush, position* thickness, 0, thickness, MaxVal);
            g.FillRectangle(WhiteBrush, position* thickness, MaxVal - TheArray[position], thickness, MaxVal);
        }

        //ReDraw the Screen
        public void ReDraw()
        {
            for (int i = 0; i < TheArray[i] - 1; i++)
            {

                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, MaxVal - TheArray[i], 1, MaxVal);
            }
        }
    }
}
