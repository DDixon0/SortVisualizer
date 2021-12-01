using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace SortVisualizer
{
    class SortEngineInsertion : ISortEngine
    {
        private bool _sorted = false;
        private int[] TheArray;
        private Graphics g;
        private int MaxVal;
        private int thickness;
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        //Constructor for this class
        public SortEngineInsertion(int[] TheArray_In, Graphics g_In, int MaxVal_In, int thickness_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            MaxVal = MaxVal_In;
            thickness = thickness_In;

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

            for (int i = 0; i < TheArray.Count() - 1; i++)
            {
                if (TheArray[i] > TheArray[i + 1])
                {
                    /*
                    Swap(i, i + 1);
                    i--;
                    */
                    
                    //Swap Move to Front
                    int j = i;
                    while(TheArray[j] > TheArray[j + 1])
                    {
                        Swap(j, j + 1);

                        //Edge case
                        if (j == 0) break;

                        j--;
                    }
                    return;
                }
            }

        }

        private void Swap(int i, int p)
        {
            //swap here
            int temp = TheArray[i];
            TheArray[i] = TheArray[i + 1];
            TheArray[i + 1] = temp;

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
