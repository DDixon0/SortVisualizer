using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SortVisualizer
{
    class HeapSortEngine : ISortEngine
    {

        private bool _sorted = false;
        private int[] TheArray;
        private Graphics g;
        private int MaxVal;
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);


        public HeapSortEngine(int[] TheArray_In, Graphics g_In, int MaxVal_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            MaxVal = MaxVal_In;
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
            int l = TheArray.Count();

            // Build max heap
            for (int i = (l / 2) - 1; i >= 0; i--)
            {
                heap(TheArray, l, i);
            }

            // Heap sort
            for (int i = l - 1; i >= 0; i--)
            {
                Swap(0, i);
                heap(TheArray, i, 0);
            }
        }

        private void heap(int[] arr, int n, int i)
        {
            // Find largest among root, left child and right child
            int large = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;

            if (l < n && arr[l] > arr[large])
                large = l;

            if (r < n && arr[r] > arr[large])
                large = r;

            // Swap and continue heapifying if root is not largest
            if (large != i)
            {
                Swap(i, large);
                heap(arr, n, large);
            }
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
            g.FillRectangle(BlackBrush, position, 0, 1, MaxVal);
            g.FillRectangle(WhiteBrush, position, MaxVal - TheArray[position], 1, MaxVal);
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
