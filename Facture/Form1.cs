using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Facture
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            try
            {
                System.Drawing.Text.PrivateFontCollection f = new PrivateFontCollection();
                f.AddFontFile("f\\1.ttf");
                textBox10.Font = new Font(f.Families[0], 18, FontStyle.Regular);
            }
            catch
            {
            }
            textBox8.Text = DateTime.Now.ToShortDateString();
            dataGridView1.Columns[3].DefaultCellStyle.ForeColor = Color.Green;
            StreamReader sr = new StreamReader("data.txt");
            string line;
            List<string> data = new List<string>();
            do
            {
                line = sr.ReadLine();
                if (line != null)
                {
                    data.Add(line);
                }
            } while (line != null);
            comboBox1.DataSource = new BindingSource(data,null);
            sr.Close();
            //textBox4.Select();
            //textBox4.SelectAll();
            //textBox4.Focus();
            textBox9.Select();
            textBox9.SelectAll();
            textBox9.Focus();
        }
        int n = 0;
        private void button1_Click(object sender, EventArgs e)
        { 
            if (n % 2 == 0)
            {
                textBox1.ReadOnly = false;
                button1.Text = "ok";
            }
            else
            {
                textBox1.ReadOnly = true;
                button1.Text = "modifier";
            }
            n++;   
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int n = 0;
            foreach (string l in textBox2.Lines)
            {
                n++;
                if (n >= 8)
                {
                    MessageBox.Show("le contenu et très grand");
                }
            }
        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Down))
            {
                //textBox5.Text = "1";
                textBox5.Select();
                textBox5.Focus();
                label7.Text = "";
            }
        }
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) /*|| (e.KeyCode == Keys.Down)*/)
            {
                //textBox5.Text = "1";
                
                label7.Text = "";
                StreamWriter sw = new StreamWriter("data.txt", true);
                int r = 0;
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    if (comboBox1.Text == comboBox1.Items[i].ToString())
                    {
                        r = 1;    
                    } 
                }
                if (r == 0)
                {
                    sw.WriteLine(comboBox1.Text);
                }
                sw.Close();
                textBox5.Select();
                textBox5.SelectAll();
                textBox5.Focus();
            }
        }
        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Down))
            {
                textBox6.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                //textBox4.Focus();
                comboBox1.Focus();
            }
        }
        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.PerformClick();
                //textBox4.Focus();
                comboBox1.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                textBox5.Focus();
            }
        }     
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 16)
            {
                MessageBox.Show("il y a beaucoup de lignes ,il faut créer deux factures pour régler le problème","message",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            int nbr = 1;
            int sum = 0;
            if ((comboBox1.Text != "") && (textBox5.Text != "") && (textBox6.Text != ""))
            {
                //string item = textBox4.Text;
                string item = comboBox1.Text;
                int qty = Convert.ToInt32(textBox5.Text);
                int mon = Convert.ToInt32(textBox6.Text);
                object[] row = { nbr, item, qty, mon };
                dataGridView1.Rows.Add(row);

                //textBox4.Clear();
                
                textBox5.Clear();
                textBox6.Clear();
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    r.Cells[0].Value = nbr.ToString();
                    nbr++;
                    sum += Convert.ToInt32(r.Cells[3].Value);
                    textBox3.Text = sum.ToString();
                }   
            }
            else
            {
                if (MessageBox.Show("tu a laissé quelque champs vide") == DialogResult.OK);
                {
                    //textBox4.Focus();
                    comboBox1.Focus();
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //if ((textBox4.Text != "") && (textBox5.Text != "") && (textBox6.Text != ""))
            //{
            if ((comboBox1.Text != "") && (textBox5.Text != "") && (textBox6.Text != ""))
            {
                int sum = 0;
                if (dataGridView1.CurrentRow != null)
                {
                    //dataGridView1.CurrentRow.Cells[1].Value = textBox4.Text;
                    dataGridView1.CurrentRow.Cells[1].Value = comboBox1.Text;
                    dataGridView1.CurrentRow.Cells[2].Value = textBox5.Text;
                    dataGridView1.CurrentRow.Cells[3].Value = textBox6.Text;
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        sum += Convert.ToInt32(r.Cells[3].Value);
                        textBox3.Text = sum.ToString();
                    }
                }
                //textBox4.Focus();
                comboBox1.Focus();
            }
            else
            {
                if (MessageBox.Show("tu a laissé quelque champs vide") == DialogResult.OK) ;
                {
                    textBox4.Focus();
                }
            }
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && ((!char.IsControl(e.KeyChar))))
            {
                e.Handled = true;
            }
            if (cal < 1)
            {
                if ((e.KeyChar == (char)Keys.D0) || (e.KeyChar == (char)Keys.NumPad0))
                {
                    e.Handled = true;
                }
            }
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && ((!char.IsControl(e.KeyChar))))
            {
                e.Handled = true;
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                //textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            //textBox4.Focus();
            comboBox1.Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //pageSetupDialog1.PageSettings.PaperSize.Height = 1169;
            //pageSetupDialog1.PageSettings.PaperSize.Width = 826;
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("le tableau est vide");
                return;
            }
            printPreviewDialog1.Document.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("a4", 826, 1169);
            printDocument1.PrinterSettings.Copies = (short)numericUpDown1.Value;
            ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        int cal;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            cal = textBox5.Text.Length;
        }
        float p;
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Font f = new Font("Calibri", 12, FontStyle.Regular);
            this.CreateGraphics().DrawString(textBox4.Text,f,Brushes.White,0,380);
            SizeF s = textBox4.CreateGraphics().MeasureString((textBox4.Text),f);
            p = s.Width * 100 / 392;
            label7.Text = p.ToString() + " %";
            if (p <= 45 )
            {
                label7.ForeColor = Color.Green;
            }
            else if ((p >= 45) && (p <= 70))
            {
                label7.ForeColor = Color.Orange;
            }
            else
            {
                label7.ForeColor = Color.Red;
            }
            //textBox7.Text = numbertoword(Convert.ToInt32(textBox4.Text));
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            Font f = new Font("Calibri", 12, FontStyle.Regular);
            this.CreateGraphics().DrawString(comboBox1.Text, f, Brushes.White, 0, 380);
            SizeF s = comboBox1.CreateGraphics().MeasureString((comboBox1.Text), f);
            p = s.Width * 100 / 392;
            label7.Text = p.ToString() + " %";
            if (p <= 45)
            {
                label7.ForeColor = Color.Green;
            }
            else if ((p >= 45) && (p <= 70))
            {
                label7.ForeColor = Color.Orange;
            }
            else
            {
                label7.ForeColor = Color.Red;
            }
            //textBox7.Text = numbertoword(Convert.ToInt32(textBox4.Text));
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((p >= 97) && ((!char.IsControl(e.KeyChar))))
            {
                e.Handled = true;
            }
        }
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((p >= 97) && ((!char.IsControl(e.KeyChar))))
            {
                e.Handled = true;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int nbr = 1;
            int sum = 0;
            if (dataGridView1.Rows != null)
            {
                try
                {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                }
                catch
                {
                    textBox4.Focus();
                }  
            }
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.Cells[0].Value = nbr.ToString();
                nbr++;
                sum += Convert.ToInt32(r.Cells[3].Value);
                textBox3.Text = sum.ToString();
            }
            if (dataGridView1.Rows.Count == 0)
            {
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
            }
        }
        public static string numbertoword(int number)
        {
            string word = "";
            string[] unitmap = { "", " et un", " deux", " trois", " quatre", " cinq", " six", " sept", " huit", " neuf", " dix", " et onze", " douze", " treize", " quatorze", " quinze", " seize", " dix-sept", " dix-huit", " dix-neuf", " vingt" };
            string[] tensmap = { "", " dix", " vingt", " trente", " quarante", " cinquante", " soixante", " soixante-dix", " quatre-vingt", " quatre-vingt-dix" };
            if ((number / 1000000) > 0)
            {
                word += numbertoword(number / 1000000) + " million";
                number %= 1000000;
            }
            if((float)(number / 1000) > 1)
            {
                word += numbertoword(number / 1000) + " mille";
                number %= 1000;
            }
            if ((number / 1000) == 1)
            {
                word += /*numbertoword(number / 1000) + */" mille";
                number %= 1000;
            }
            if ((float)(number / 100) > 1)
            {
                word += numbertoword(number / 100) + " cent";
                number %= 100; 
            }
            if ((number / 100) == 1)
            {
                word += /*numbertoword(number / 100) + */" cent";
                number %= 100;
            }
            if ((number <= 20))
            {
                if ((number <= 10) && (number == 1))
                {
                    word += " un";
                }
                else if (number == 11)
                {
                    word += " onze";
                }
                else
                {
                    word += unitmap[number];
                }
            }
            else
            {
                if (((number / 10) == 7) && ((number % 10) != 0))
                {
                    word += tensmap[6] + unitmap[(number % 10) + 10 ];
                }
                else if (((number / 10) == 9) && ((number % 10) != 0))
                {
                    word += tensmap[8] + unitmap[(number % 10) + 10];
                }
                else if (((number % 10) > 0) && (number != 0))
                {
                    word += tensmap[number / 10] + unitmap[number % 10];
                }
                else { word += tensmap[number / 10]; }
            }
            return word;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((textBox3.Text == null) || (textBox3.Text == ""))
                {
                    textBox7.Clear();
                }
                else
                {
                    textBox7.Text = numbertoword(Convert.ToInt32(textBox3.Text)) + " Dinars";
                }
            }
            catch { }
        }
        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Down))
            {
                textBox10.Focus();
            }
        }
        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox1.Focus();
            }
            if (e.KeyCode == Keys.Up)
            {
                textBox9.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                comboBox1.Focus();
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            double marginl = 75.6;
            double marginr = 56.7;
            double margint = 52.9;
            Font f1 = new Font("Calibri", 12,FontStyle.Bold);
            Font f2 = new Font("Calibri", 12, FontStyle.Regular);
            string m = textBox1.Text;
            string c = textBox2.Text;
            string strno = "Facture N° " + textBox9.Text+DateTime.Now.ToString("/yyyy");
            SizeF csize = textBox2.Size;
            SizeF nosize = e.Graphics.MeasureString(strno, f2);
            if (k % 2 == 0)
            {
                if (ph == 1)
                {
                    e.Graphics.DrawImage(Properties.Resources.image1, 0, 0, 826, 1169);
                }
                if (ph == 2)
                {
                    e.Graphics.DrawImage(Properties.Resources.image2, 0, 0, 826, 1169);
                }
            }
            e.Graphics.DrawString(m, f2, Brushes.Black, (float)marginl, (float)margint);
            e.Graphics.DrawString(c, f1, Brushes.Black, (float)(e.PageBounds.Width - csize.Width - marginr), (float)margint + 20);
            if ((textBox9.Text != "") && (textBox9.Text != " "))
            {
                e.Graphics.DrawString(strno, f1, Brushes.Black, (float)((e.PageBounds.Width / 2) - (nosize.Width / 2)), (float)290.15);
            }

            double preheight = 332.51;

            double colheight = 24.56;
            double vch = preheight + colheight;
            double col0wight = marginl;
            double col1wigth = col0wight + 56.69; 
            double col2wigth = col1wigth + 385.51;
            double col3wigth = col2wigth + marginl;
            double col4wigth = e.PageBounds.Width - marginr; 

            e.Graphics.DrawLine(Pens.Black, (float)marginl, (float)preheight, (float)col4wigth, (float)preheight);
            e.Graphics.DrawLine(Pens.Black, (float)marginl, (float)vch, (float)col4wigth, (float)vch);

            e.Graphics.DrawLine(Pens.Black, (float)col0wight, (float)preheight, (float)col0wight, (float)((preheight + colheight * 2) + (dataGridView1.Rows.Count * colheight)));
            e.Graphics.DrawLine(Pens.Black, (float)col1wigth, (float)preheight, (float)col1wigth, (float)((preheight + colheight) + (dataGridView1.Rows.Count * colheight)));
            e.Graphics.DrawLine(Pens.Black, (float)col2wigth, (float)preheight, (float)col2wigth, (float)((preheight + colheight) + (dataGridView1.Rows.Count * colheight)));
            e.Graphics.DrawLine(Pens.Black, (float)col3wigth, (float)preheight, (float)col3wigth, (float)((preheight + colheight * 2) + (dataGridView1.Rows.Count * colheight)));
            e.Graphics.DrawLine(Pens.Black, (float)col4wigth, (float)preheight, (float)col4wigth, (float)((preheight + colheight * 2) + (dataGridView1.Rows.Count * colheight)));

            double hcol0wight = marginl + 5;
            double hcol1wight = col0wight + 56.69 +5;
            double hcol2wight = col1wigth + 385.51 +5;
            double hcol3wight = col2wigth + marginl +5 ;
            double hcol4wight = e.PageBounds.Width - marginr;

            string nbr = "NBR";
            string des = "Désignation";
            string un = "Unité";
            string mon = "Montant";

            SizeF nbrsize = e.Graphics.MeasureString(nbr, f1);
            SizeF dessize = e.Graphics.MeasureString(des, f1);
            SizeF unsize = e.Graphics.MeasureString(un, f1);
            SizeF monsize = e.Graphics.MeasureString(mon, f1);

            e.Graphics.DrawString("NBR", f1, Brushes.Black, (float)(hcol0wight), (float)((preheight + (colheight / 2)) - (dessize.Height / 2)));
            e.Graphics.DrawString(des, f1, Brushes.Black, (float)((col1wigth + (385.51 / 2)) - (dessize.Width / 2)), (float)((preheight + (colheight / 2)) - (dessize.Height / 2)));
            e.Graphics.DrawString(un, f1, Brushes.Black, (float)((col2wigth + (marginl / 2)) - (unsize.Width /2 )), (float)((preheight + (colheight / 2)) - (dessize.Height / 2)));
            e.Graphics.DrawString(mon, f1, Brushes.Black, (float)(((col4wigth - col3wigth )/2) - (monsize.Width / 2) + col3wigth), (float)((preheight + (colheight / 2)) - (dessize.Height / 2)));

            // invoice contents

            double rowheight = 24.56;
            double rs = (preheight + (colheight / 2)) - (dessize.Height / 2);
            for (int i = 0;i < dataGridView1.Rows.Count; i++)
            {
                vch += colheight;
                rs += rowheight;
                string uns = dataGridView1.Rows[i].Cells[2].Value.ToString();
                string mons = dataGridView1.Rows[i].Cells[3].Value.ToString();
                SizeF unssize = e.Graphics.MeasureString(uns, f1);
                SizeF monssize = e.Graphics.MeasureString(mons, f1);
                
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[0].Value.ToString(), f2, Brushes.Black, (float)hcol0wight, (float)rs);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[1].Value.ToString(), f2, Brushes.Black, (float)hcol1wight, (float)rs);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[2].Value.ToString(), f2, Brushes.Black, (float)((col2wigth + (marginl / 2)) - (unssize.Width / 2)), (float)rs);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[3].Value.ToString(), f2, Brushes.Black, (float)(((col4wigth - col3wigth) / 2) - (monssize.Width / 2) + col3wigth), (float)rs);

                e.Graphics.DrawLine(Pens.Black, (float)marginl, (float)vch, (float)col4wigth, (float)vch);
            }
            SizeF totsize = e.Graphics.MeasureString("Total", f1);
            SizeF tsize = e.Graphics.MeasureString(textBox3.Text, f1);
            e.Graphics.DrawLine(Pens.Black, (float)marginl, (float)(vch += colheight), (float)col4wigth, (float)(vch));
            e.Graphics.DrawString("Total", f1, Brushes.Black, (float)((col3wigth / 2) + (totsize.Width / 2)), (float)(rs += rowheight));
            e.Graphics.DrawString(textBox3.Text, f1, Brushes.Black, (float)(((col4wigth - col3wigth) / 2) - (tsize.Width / 2) + col3wigth), (float)(rs));
            //véhicule Imm
            e.Graphics.DrawString("Le Gérant", f1, Brushes.Black, (float)col3wigth, (float)((e.PageBounds.Height) - 302.36));
            e.Graphics.DrawString("BENFELLAH REDOUANE", f1, Brushes.Black, (float)(col2wigth + (marginl / 2)), (float)((e.PageBounds.Height) - 270.36));
            e.Graphics.DrawString("Arrêté la présente facture en TTC à la somme de :", f2, Brushes.Black, (float)marginl, (float)(rs += 56.69));
            e.Graphics.DrawString(textBox7.Text, f1, Brushes.Black, (float)marginl, (float)(rs += 30.69));
            if ((textBox10.Text != "") && (textBox10.Text != " "))
            {
                e.Graphics.DrawString("véhicule Imm : ", f1, Brushes.Black, (float)marginl + 20, (float)(rs += 30.69));
                SizeF vsize = e.Graphics.MeasureString("véhicule Imm : ", f1);

                Font f4 = new Font("Digital-7", 18, FontStyle.Regular);
                e.Graphics.DrawString(textBox10.Text, f4, Brushes.Black, (float)marginl + vsize.Width + 20, (float)(rs));
            }
            Font f3 = new Font("Calibri", 8, FontStyle.Regular);
            SizeF dsize = e.Graphics.MeasureString(textBox8.Text, f3);
            e.Graphics.DrawString("Alger le : " +textBox8.Text, f3, Brushes.Black, (float)(e.PageBounds.Width - marginr -dsize.Width -20 ), (float)(e.PageBounds.Height - 60)); 
        }
        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && ((!char.IsControl(e.KeyChar))) && ((!char.IsSeparator(e.KeyChar))))
            {
                e.Handled = true;
            }
        }
        int k = 1;
        int ph;
        private void button6_Click(object sender, EventArgs e)
        {
            k++;
            if (k % 2 == 0)
            {
                this.button6.BackgroundImage = global::Facture.Properties.Resources._1;
            }
            else
            {
                this.button6.BackgroundImage = null;
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ph = 1;
            button6.Visible = true;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ph = 2;
            button6.Visible = true;
        }
    }
}
