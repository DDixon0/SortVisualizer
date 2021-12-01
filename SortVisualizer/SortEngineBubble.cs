using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;

namespace SortVisualizer
{
    class SortEngineBubble : ISortEngine
    {
        private int[] TheArray;
        private Graphics g;
        private int MaxVal;
        private int thickness;
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        public SortEngineBubble(int[] TheArray_In, Graphics g_In, int MaxVal_In, int thickness_In)
        {
            TheArray = TheArray_In;
            g = g_In;
            MaxVal = MaxVal_In;
            thickness = thickness_In;
        }

        public void NextStep()
        {
            for (int i = 0; i < TheArray.Count() - 1; i++)
            {
                if (TheArray[i] > TheArray[i + 1])
                {
                    Swap(i, i + 1);
                }
            }
        }

        private void Swap(int i, int p)
        {
            int temp = TheArray[i];
            TheArray[i] = TheArray[i + 1];
            TheArray[i + 1] = temp;

            DrawBar(i, TheArray[i]);
            DrawBar(p, TheArray[p]);

            //SystemSounds.Beep.Play();
            //Sound is too laggy, will need to change for future and other sounds
        }

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
