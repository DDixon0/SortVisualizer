using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SortVisualizer
{
    class SortEngineMerge : ISortEngine
    {
        private int[] TheArray;
        private Graphics g;
        private int MaxVal;
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        //Constructor for Merge Sort
        public SortEngineMerge(int[] TheArray_In, Graphics g_In, int MaxVal_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            MaxVal = MaxVal_In;

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

        public void NextStep()
        {
            mergeSort(TheArray, 0, TheArray.Count()-1);
        }

        private void mergeSort(int[] arr, int start, int end)
        {

            if (start < end)
            {
                //mid point
                int mid = start + (end - start) / 2;
                //Spliting array
                mergeSort(arr, start, mid);
                mergeSort(arr, mid + 1, end);
                //Sorting and merging
                merge(arr, start, mid, end);
            }

        }

        private void merge(int[] arr, int start, int mid, int end)
        {

            //Create Two Sub-arrays
            int a1 = mid - start + 1;
            int a2 = end - mid;
            int[] arr1 = new int[a1];
            int[] arr2 = new int[a2];
            //int arr1[a1], arr2[a2];

            //Copy arry
            for (int p = 0; p < a1; p++) {
                arr1[p] = arr[start + p];
            }

            for (int p = 0; p < a2; p++) {
                arr2[p] = arr[mid + 1 + p];
            }

            int i, j, k;
            i = 0;
            j = 0;
            k = start;

            //Sort the values
            while (i < a1 && j < a2)
            {
                if (arr1[i] <= arr2[j])
                {
                    arr[k] = arr1[i];
                    i++;
                }
                else
                {
                    arr[k] = arr2[j];
                    j++;
                }
                DrawBar(k, TheArray[k]);
                k++;
            }

            //Add rest of values
            while (i < a1)
            {
                arr[k] = arr1[i];
                i++;
                DrawBar(k, TheArray[k]);
                k++;
            }

            while (j < a2)
            {
                arr[k] = arr2[j];
                j++;
                DrawBar(k, TheArray[k]);
                k++;
            }
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
