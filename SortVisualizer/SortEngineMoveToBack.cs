using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    class SortEngineMoveToBack : ISortEngine
    {
        private int[] TheArray;
        private Graphics g;
        private int MaxVal;
        private int thickness;
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        private int CurrentListPointer = 0;

        public SortEngineMoveToBack(int[] TheArray_In, Graphics g_In, int MaxVal_In, int thickness_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            MaxVal = MaxVal_In;
            thickness = thickness_In;
        }

        public void NextStep()
        {
            if (CurrentListPointer >= TheArray.Count() - 1) CurrentListPointer = 0;
            if (TheArray[CurrentListPointer] > TheArray[CurrentListPointer + 1])
            {
                Rotate(CurrentListPointer);
            }
            CurrentListPointer++;
        }

        private void Rotate(int currentListPointer)
        {
            int temp = TheArray[CurrentListPointer];
            int EndPoint = TheArray.Count() - 1;
            for (int i = CurrentListPointer; i < EndPoint; i++)
            {
                TheArray[i] = TheArray[i + 1];
                DrawBar(i, TheArray[i]);
            }
            TheArray[EndPoint] = temp;
            DrawBar(EndPoint, TheArray[EndPoint]);
        }

        //Draw's New Bars
        private void DrawBar(int position, int height)
        {
            g.FillRectangle(BlackBrush, position* thickness, 0, thickness, MaxVal);
            g.FillRectangle(WhiteBrush, position* thickness, MaxVal - TheArray[position], thickness, MaxVal);
        }

        public bool IsSorted()
        {
            for (int i = 0; i < TheArray.Count() - 1; i++)
            {
                if (TheArray[i] > TheArray[i + 1]) return false;
            }
            return true;
        }

        public void ReDraw()
        {
            for (int i = 0; i < (TheArray.Count() - 1); i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i* thickness, MaxVal - TheArray[i], thickness, MaxVal);
            }
        }
    }
}
