using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static double[,] tablasimplex = new double[4, 7];
        static int pos = 0;
        static double[,] tablasimplex2;
        static int pos1 = 0;
        static Queue<string> ColaC1 = new Queue<string>();
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            //Generar matriz
          // static double[,] tablasimplex = new double[4,7];
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            //Agregar columnas
            dataGridView1.Columns.Add("sf", "Solución Factible");
            dataGridView1.Columns.Add("z", "z");
            dataGridView1.Columns.Add("x1", "x1");
            dataGridView1.Columns.Add("x2", "x2");
            dataGridView1.Columns.Add("s1", "s1");
            dataGridView1.Columns.Add("s2", "s2");
            dataGridView1.Columns.Add("s3", "s3");
            dataGridView1.Columns.Add("sl", "Solución");
                
        
            //Renglon z
            tablasimplex[0, 0] = 1;
            tablasimplex[0, 1] = double.Parse(txtZx1.Text)* -1;
            tablasimplex[0, 2] = double.Parse(txtZx2.Text)* -1;
            tablasimplex[0, 3] = 0;
            tablasimplex[0, 4] = 0;
            tablasimplex[0, 5] = 0;
            tablasimplex[0, 6] = 0;
            

            //Renglon s1
            tablasimplex[1, 0] = 0;
            tablasimplex[1, 1] = double.Parse(txtR1x1.Text);
            tablasimplex[1, 2] = double.Parse(txtR1x2.Text);
            tablasimplex[1, 3] = 1;
            tablasimplex[1, 4] = 0;
            tablasimplex[1, 5] = 0;
            tablasimplex[1, 6] = double.Parse(txtR1x3.Text);
            

            //Renglon s2
            tablasimplex[2, 0] = 0;
            tablasimplex[2, 1] = double.Parse(txtR2x1.Text);
            tablasimplex[2, 2] = double.Parse(txtR2x2.Text);
            tablasimplex[2, 3] = 0;
            tablasimplex[2, 4] = 1;
            tablasimplex[2, 5] = 0;
            tablasimplex[2, 6] = double.Parse(txtR2x3.Text);
            

            //Renglon s3
            tablasimplex[3, 0] = 0;
            tablasimplex[3, 1] = double.Parse(txtR3x1.Text);
            tablasimplex[3, 2] = double.Parse(txtR3x2.Text);
            tablasimplex[3, 3] = 0;
            tablasimplex[3, 4] = 0;
            tablasimplex[3, 5] = 1;
            tablasimplex[3, 6] = double.Parse(txtR3x3.Text);
            
            //Agregar a dataGridView
            for (int row = 0; row < tablasimplex.GetLength(0); row++)
            {
                DataGridViewRow dataGridViewRow = new DataGridViewRow();
                dataGridViewRow.CreateCells(dataGridView1);                 

                for (int col = 0; col < tablasimplex.GetLength(1); col++)
                {
                    dataGridViewRow.Cells[col+1].Value = tablasimplex[row, col].ToString();
                }

                dataGridView1.Rows.Add(dataGridViewRow);
            }


            //Agregar filas fijas de la primer columna
            dataGridView1.Rows[0].Cells[0].Value = "z";
            dataGridView1.Rows[1].Cells[0].Value = "s1";
            dataGridView1.Rows[2].Cells[0].Value = "s2";
            dataGridView1.Rows[3].Cells[0].Value = "s3";
            
            //Primer paso, encontrar columna pivote
            double min = 0;
            //int pos = 0;
            for (int i = 0; i < 3; i++)
            {
                if (tablasimplex[0,i] < min)
                {
                   min = tablasimplex[0,i];
                   pos = i;
                }
                
            }

            //Segundo paso, encontrar renglon pivote
            double min1 = 99999;
            //int pos1 = 0;
            double renglon1 = tablasimplex[1,6] / tablasimplex[1,1];
            double renglon2 = tablasimplex[2,6] / tablasimplex[2,1];
            double renglon3 = tablasimplex[3,6] / tablasimplex[3,1];

            double[] renglones = new double[3];
            renglones[0] = renglon1;
            renglones[1] = renglon2;
            renglones[2] = renglon3;

            for (int i = 0; i < 3; i++)
            {
                if (renglones[i] < min1)
                {
                    min1 = renglones[i];
                    pos1 = i+1;
                }
            }

            //La coordenada del coeficiente que se dividirá entre todo lo demas (elemento pivote)
            double rpivote = tablasimplex[pos1,pos]; 

            //Cambiar color de la celda de elemento pivote
            dataGridView1.Rows[pos1].Cells[pos+1].Style.BackColor = Color.Red;

            // Crear una segunda matriz con los nuevos valores
           // double[,] tablasimplex2;
            tablasimplex2 = tablasimplex;

            //Dividir el renglon pivote por elemento pivote
            for (int i = 0; i < 7; i++)
            {
                tablasimplex2[pos1, i] = tablasimplex2[pos1, i] / rpivote;
               
            }
            

            //Calcular el nuevo renglon s1 con el renglon pivote
            /* double E_Pivote1 = tablasimplex[0, pos] * -1;
             for (int i = 0; i < 7; i++)
             {
                 tablasimplex2[0, i] = ((E_Pivote1)*(tablasimplex2[pos1,i]))+tablasimplex[0,i];
             }*/

            //Agregar columnas a nuevo datagridview
            dataGridView2.Columns.Add("sf", "Solución Factible");
            dataGridView2.Columns.Add("z", "z");
            dataGridView2.Columns.Add("x1", "x1");
            dataGridView2.Columns.Add("x2", "x2");
            dataGridView2.Columns.Add("s1", "s1");
            dataGridView2.Columns.Add("s2", "s2");
            dataGridView2.Columns.Add("s3", "s3");
            dataGridView2.Columns.Add("sl", "Solución");

            //Mostrar en un nuevo datagridview
            for (int row = 0; row < tablasimplex.GetLength(0); row++)
            {
                DataGridViewRow dataGridViewRow2 = new DataGridViewRow();
                dataGridViewRow2.CreateCells(dataGridView2);

                for (int col = 0; col < tablasimplex2.GetLength(1); col++)
                {
                    dataGridViewRow2.Cells[col + 1].Value = tablasimplex2[row, col].ToString();
                }

                dataGridView2.Rows.Add(dataGridViewRow2);
            }

            //Agregar filas fijas de la primer columna
            dataGridView2.Rows[0].Cells[0].Value = "z";
            dataGridView2.Rows[1].Cells[0].Value = "x1";
            dataGridView2.Rows[2].Cells[0].Value = "s2";
            dataGridView2.Rows[3].Cells[0].Value = "s3";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            double E_Pivote1 = tablasimplex[0, pos] * -1;
            double Const = tablasimplex[0, pos];
            double E_Pivote2 = tablasimplex[1, pos] * -1;
            for (int i = 0; i < 7; i++)
            {
                tablasimplex2[0, i] = ((E_Pivote1) * (tablasimplex2[pos1, i])) + tablasimplex[0, i];
                ColaC1.Enqueue("-(" + Const + ") * " + (tablasimplex2[pos1, i]) + "\n " +" + "+ tablasimplex[0, i] + "\n " + " = " +
                    ((E_Pivote1) * (tablasimplex2[pos1, i])) + " + (" + tablasimplex[0, i]+ ")  = "+ tablasimplex2[0,i]+ "\n \n");
                listBox1.DataSource = null;
                listBox1 .DataSource = ColaC1.ToArray();
               
            }
           
        }
        
    }
}
