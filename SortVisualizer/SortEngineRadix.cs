using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    class SortEngineRadix : ISortEngine
    {
        private int[] TheArray;
        private Graphics g;
        private int MaxVal;
        private int thickness;
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);


        //Constructor for this class
        public SortEngineRadix(int[] TheArray_In, Graphics g_In, int MaxVal_In, int thickness_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            MaxVal = MaxVal_In;
            thickness = thickness_In;
        }

        public void NextStep()
        {
            // Get max
            int max = getMax(TheArray, TheArray.Length);

            // Apply counting sort 
            for (int place = 1; max / place > 0; place *= 10)
                countingSort(TheArray, TheArray.Length, place);
        }

        // Using counting sort to sort the elements in the basis of significant places
        void countingSort(int [] array, int size, int place)
        {
            const int max = 10;
            int [] output = new int[size];
            int [] count = new int[max];

            for (int i = 0; i < max; ++i)
            {
                count[i] = 0;
            }
            // Calculate count 
            for (int i = 0; i < size; i++)
            {
                count[(array[i] / place) % 10]++;
            }
            // Calculate count
            for (int i = 1; i < max; i++)
            {
                count[i] += count[i - 1];
            }
            // Place sorted order
            for (int i = size - 1; i >= 0; i--)
            {
                output[count[(array[i] / place) % 10] - 1] = array[i];
                count[(array[i] / place) % 10]--;
            }

            for (int i = 0; i < size; i++)
            {
                array[i] = output[i];
            }
            ReDraw();
        }

        //Gets the Max number in a array
        int getMax(int [] arr, int l)
        {
            int max = 0;
            for (int i = 0; i < l; i++) if (arr[i] > max) max = arr[i];
            return max;
        }

        //Checks if array is sorted
        public bool IsSorted()
        {
            for (int i = 0; i < TheArray.Count() - 1; i++)
            {
                if (TheArray[i] > TheArray[i + 1]) return false;
            }

            return true;
        }

        //ReDraw the Screen
        public void ReDraw()
        {
            for (int i = 0; i < TheArray.Length; i++)
            {
                g.FillRectangle(BlackBrush, i* thickness, 0, thickness, MaxVal);
                g.FillRectangle(WhiteBrush, i* thickness, MaxVal - TheArray[i], thickness, MaxVal);
            }
        }

    }
}
