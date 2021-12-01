using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SortVisualizer
{
    class SortEngineHeap : ISortEngine
    {

        private int[] TheArray;
        private Graphics g;
        private int MaxVal;
        private int thickness;
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);


        public SortEngineHeap(int[] TheArray_In, Graphics g_In, int MaxVal_In, int thickness_In)
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
            g.FillRectangle(BlackBrush, position* thickness, 0, thickness, MaxVal);
            g.FillRectangle(WhiteBrush, position* thickness, MaxVal - TheArray[position], thickness, MaxVal);
        }

        //ReDraw the Screen
        public void ReDraw()
        {
            for (int i = 0; i < TheArray[i] - 1; i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i* thickness, MaxVal - TheArray[i], thickness, MaxVal);
            }
        }
    }
}
