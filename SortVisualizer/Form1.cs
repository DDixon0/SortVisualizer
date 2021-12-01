using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SortVisualizer
{
    public partial class Form1 : Form
    {
        int[] TheArray;
        Graphics g;
        BackgroundWorker bgw = null;
        bool Paused = false;
        int thickness = 5;

        public Form1()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            InitializeComponent();
            PopulateDropdown();
        }

        //Looks through internal structure to populate the the dropdown menu
        private void PopulateDropdown()
        {
            //Gets the names of class list
            List<string> ClassList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ISortEngine).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();
            ClassList.Sort();
            foreach (string entry in ClassList)
            {
                comboBox1.Items.Add(entry);
            }
            comboBox1.SelectedIndex = 0;

            numericUpDown1.Value = 5;
        }

        //File Close Butoon Menu
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Start Button
        private void btnStart_Click(object sender, EventArgs e)
        {
            //If the prigram is not reset
            if (TheArray == null) btnReset_Click(null, null);

            //If the program is paused
            if (Paused)
            {
                btnPause_Click(null, null);
                return;
            }

            bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerAsync(argument: comboBox1.SelectedItem);
        }
        //Pause/Resume Button
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!Paused)
            {
                if (bgw == null)
                {
                    System.Windows.Forms.MessageBox.Show("Press Start First!");
                    return;
                }

                bgw.CancelAsync();
                Paused = true;
            }
            else
            {
                if (bgw.IsBusy) return;
                int NumEntries = panel1.Width/thickness;
                int MaxVal = panel1.Height;
                Paused = false;
                for (int i = 0; i < NumEntries; i++)
                {
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), i* thickness, 0, thickness, MaxVal);
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i* thickness, MaxVal - TheArray[i], thickness, MaxVal);
                }
                bgw.RunWorkerAsync(argument: comboBox1.SelectedItem);
            }
        }
        //Reset Button
        private void btnReset_Click(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            thickness = Decimal.ToInt32(numericUpDown1.Value);
            int NumEntries = panel1.Width/thickness;
            int MaxVal = panel1.Height;
            TheArray = new int[NumEntries];

            //Draws background
            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 0, 0, panel1.Width, MaxVal);
            Random rand = new Random();

            //Creates array of rectangle heights
            for (int i = 0; i < NumEntries; i++)
            {
                TheArray[i] = rand.Next(0, MaxVal);
            }

            //Draws each rectangle based on heights
            for (int i = 0; i < NumEntries; i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i*thickness, MaxVal - TheArray[i], thickness, MaxVal);
            }
        }

        //Background worker, where sort is happening
        #region BackGroundStuff
        public void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            string SortEngineName = (string)e.Argument;
            Type type = Type.GetType("SortVisualizer." + SortEngineName);
            var ctors = type.GetConstructors();
            try
            {
                ISortEngine se = (ISortEngine)ctors[0].Invoke(new object[] { TheArray, g, panel1.Height, thickness});
                while (!se.IsSorted() && (!bgw.CancellationPending))
                {
                    se.NextStep();
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}
