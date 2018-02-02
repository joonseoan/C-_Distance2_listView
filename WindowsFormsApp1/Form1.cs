using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // To draw lines
        Graphics graphics;

        // To draw small circles
        Graphics circle;

        // To draw black a background panel for the users to recognize the lines easily
        Graphics formGraphics;
        SolidBrush brush;

        
        Pen pen;
        Pen shape1;
        Pen shape2;

        // To store distance data and sort elements in a way of ascending
        ArrayList arr = new ArrayList();

        // Values of warehouse and customer locations
        double wX, wY, cX, cY;

        // Distance variable
        double dist;

        // Closest distance value out of ArrayList above
        double min;

        // Black background display control
        bool bg = false;

        // There are 5 warehouse locations and 100 customer locations.
        // Therefore, the user can make events 500 times. It is an initial value.
        int count = 0;

        // When a new closest line is made out, the previous closest line is deactiviated.
        double[] lastLine = new double[4];

        // To store and show the previous warehouse information 
        // when the user enters an incorrect number which is out of range.
        string lastWH;

        // To sort elements in ascending and pull out the first element which is minimum value
        public double minDistance(ArrayList arr)
        {
            min = (double)arr[0];

            return min;
        }


        public Form1()
        {
            InitializeComponent();
        }

        // To clear textBoxes
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control c in (Controls))
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Main event method
        private void button2_Click(object sender, EventArgs e)
        {

            // To find out and send the warehouse information the user is currently working on 
            bool[] veri = { false, false, false, false, false };

            try
            {
                // Values from customer text fields
                cX = Convert.ToDouble(textBox11.Text);
                cY = Convert.ToDouble(textBox12.Text);

                cX = Math.Abs(cX);
                cY = Math.Abs(cY);

                if (radioButton1.Checked)
                {
                    wX = Convert.ToDouble(textBox1.Text);
                    wY = Convert.ToDouble(textBox2.Text);

                    wX = Math.Abs(wX);
                    wY = Math.Abs(wY);

                    label20.Text = "1";
                    veri[0] = true;

                    // In the first try, the distance value is not able to be compared to the existing previous value
                    // Therefore, the value generated at first is a closest value and warehouse info about the value must be delivered.
                    if (count == 0)
                    {
                        label23.Text = "1";
                    }

                }
                else if (radioButton2.Checked)
                {
                    wX = Convert.ToDouble(textBox3.Text);
                    wY = Convert.ToDouble(textBox4.Text);

                    wX = Math.Abs(wX);
                    wY = Math.Abs(wY);

                    label20.Text = "2";
                    veri[1] = true;
                    if (count == 0)
                    {
                        label23.Text = "2";
                    }
                }
                else if (radioButton3.Checked)
                {
                    wX = Convert.ToDouble(textBox5.Text);
                    wY = Convert.ToDouble(textBox6.Text);

                    wX = Math.Abs(wX);
                    wY = Math.Abs(wY);

                    label20.Text = "3";
                    veri[2] = true;
                    if (count == 0)
                    {
                        label23.Text = "3";
                    }
                }
                else if (radioButton4.Checked)
                {
                    wX = Convert.ToDouble(textBox7.Text);
                    wY = Convert.ToDouble(textBox8.Text);

                    wX = Math.Abs(wX);
                    wY = Math.Abs(wY);

                    label20.Text = "4";
                    veri[3] = true;
                    if (count == 0)
                    {
                        label23.Text = "4";
                    }
                }
                else if (radioButton5.Checked)
                {
                    wX = Convert.ToDouble(textBox9.Text);
                    wY = Convert.ToDouble(textBox10.Text);

                    wX = Math.Abs(wX);
                    wY = Math.Abs(wY);

                    label20.Text = "5";
                    veri[4] = true;
                    if (count == 0)
                    {
                        label23.Text = "5";
                    }
                }

                // Defined range of values only because black background panel size. In short, it is because of the design.
                // Without the panel, the line display does not look good.
                if (wX > 350 || wY > 350 || cX > 350 || cY > 350)
                {

                    string message = "You entered the numbers out of the range defined.";
                    string caption = "Warining!";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // When the alert or error occurs, the previous warehouse information must not change.
                    // If it shifts, the user will be confused. Actually, it is still changing in the codes above
                    // however, it gets back to the previous value here. I still needs to fix it.
                    label20.Text = lastWH;

                    // When the alert occurs, it must not write warehouse information.
                    if (count == 0)
                    {
                        label23.Text = "";
                    }


                }
                else
                {
                    // Distance calculation
                    dist = Math.Round(Math.Sqrt(Math.Pow((wX - cX), 2) + Math.Pow((wY - cY), 2)), 2);
                    label11.Text = Convert.ToString(dist);

                    // Console.WriteLine(count);

                    // The location info in the first try must be recorded because it the first closest distance value.
                    // When the next line is a closer distance, it gives previouis location information and then the line to be deactivated.
                    if (count == 0)
                    {
                        lastLine[0] = wX;
                        lastLine[1] = wY;
                        lastLine[2] = cX;
                        lastLine[3] = cY;

                        // The color of the closest distance line is Blue
                        pen = new Pen(Color.Blue);
                    }

                    // min: the existing closest distance value
                    // dist: new distance value
                    else if (dist <= min)
                    {

                        // The new value is more closer or lower than the existing value,
                        // it deactivates the exsiting closest line by changing the color, blue to gray.
                        graphics = CreateGraphics();
                        pen = new Pen(Color.LightGray);
                        graphics.DrawLine(pen, (int)lastLine[0] + 175, (int)lastLine[1] + 20, (int)lastLine[2] + 175, (int)lastLine[3] + 20);
                        graphics.Dispose();

                        lastLine[0] = wX;
                        lastLine[1] = wY;
                        lastLine[2] = cX;
                        lastLine[3] = cY;


                        // It receive and display warehouse info at the moment that the new distance value is closer or lower.
                        for (int i = 0; i < 5; i++)
                        {
                            if (veri[i] == true)
                            {
                                label23.Text = Convert.ToString(i + 1);
                            }
                        }

                        pen = new Pen(Color.Blue);
                    }
                    else if (dist > min)
                    {
                        pen = new Pen(Color.LightGray);
                    }

                    // To draw black background panel for the better look and feel.
                    graphics = CreateGraphics();
                    brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                    formGraphics = this.CreateGraphics();
                    formGraphics = this.CreateGraphics();

                    // To draw the small circle and notifify which one is customer or warehouse
                    circle = CreateGraphics();

                    shape1 = new Pen(Color.Yellow, 5);
                    shape2 = new Pen(Color.Red, 5);

                    // ArrayList sorting elements to find out the minimum number which is a distance.
                    arr.Add(dist);
                    arr.Sort();
                    double minDist = minDistance(arr);
                    label13.Text = Convert.ToString(minDist);

                    // It renders the black background panel once.
                    if (bg == false)
                        formGraphics.FillRectangle(brush, new Rectangle(170, 20, 400, 360));
                    brush.Dispose();
                    formGraphics.Dispose();
                    bg = true;

                    // Line rendering
                    graphics.DrawLine(pen, (int)wX + (int)175, (int)wY + 20, (int)cX + 175, (int)cY + 20);
                    graphics.Dispose();

                    label16.Text = "Good. Please, try another location";

                    circle = CreateGraphics();

                    shape1 = new Pen(Color.Orange, 3);
                    shape2 = new Pen(Color.Red, 3);

                    circle.DrawEllipse(shape1, (int)wX + 175, (int)wY + 20, 3, 3);

                    circle.DrawEllipse(shape2, (int)cX + 175, (int)cY + 20, 3, 3);
                    circle.Dispose();

                    count++;

                    // When the user tries 100 times, it populates the warning message and set "count" to "0" again.
                    if (count == 500)
                    {
                        string message = "You tries 500 times. The count number will set to 0.";
                        string caption = "Warining!";
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        count = 0;
                    }
                }

                // The existing warehouse information
                lastWH = label20.Text;


            }
            catch (FormatException fe)
            {
                label16.Text = "Please, click a correct radio button and enter numeric symbols.";
                Console.WriteLine(fe);
            }

            catch (Exception ee)
            {
                Console.WriteLine(ee);
                label16.Text = "Maybe, This message should not pop up.";
            }


        }

    }
}
