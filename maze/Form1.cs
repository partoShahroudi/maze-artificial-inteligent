using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using maze;

namespace maze
{
    public partial class form1 : Form
    {
        private Label[,] arr;
        Maze m;
        int row, column;
        int i;
        public form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m = new Maze();
        }

        private void fillArr()
        {
            int[,] Mat= m.mat;
            for(int i=0;i<row;i++)
                for(int j=0;j<column;j++)
                {
                    arr[i, j].BackColor = getColor(Mat[i, j]);
                }
        }
        private void run(string str)
        {
            m.firstInitial(str);
            row = m.row;
            column = m.column;
            fillPanel();
            arr[0, 0].BackColor = Color.Black;
            m.findPath();
            fillArr();
            i= 0;
            timer1.Enabled = true;
        }
       
        private Color getColor(int val)
        {
            if (val == -1)
                return Color.Black;
            int x = 2 * val;
            Color c = Color.FromArgb(255, 255-x, 255-x);
            return c;
        }
        private void fillPanel()
        {
            arr = new Label[row, column];
            int w = panel1.Width / row;
            int h = panel1.Height / column;
           
            for(int i=0;i<row;i++)
                for(int j=0;j<column;j++)
                {
                       Label l= new Label();
                       l.Location = new Point(w * i , h * j );
                         l.Size = new Size(w, h);
                     arr[i, j] = l;
                       panel1.Controls.Add(arr[i, j]);
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m.findPath();
            fillArr();
            i++;
            if (i > 1000)
                timer1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            run(textBox1.Text);
        }
    }
}
