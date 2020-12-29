using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace parking2.cs
{
    public partial class MaakavForm : Form
    {
        public MaakavForm()
        {
            InitializeComponent();
            plate_text.KeyDown += new KeyEventHandler(enter_pressed_plate);
        }

        public parking2.Akuv data;
        public bool exit = false;
        
        //plate_text.KeyDown += new KeyEventHandler(enter_pressed_plate);

        private void button1_Click(object sender, EventArgs e)
        {
            string plate = plate_text.Text;
            string name = name_text.Text;
            int n;
            bool isNumeric = true;
            foreach (char ch in plate)
                if (Char.IsDigit(ch))
                    continue;
                else
                {
                    isNumeric = false;
                    break;
                }
                    
            if (isNumeric==false)
            {
                //plate_text.Clear();
                status_label.Text = "מספר לא תקין";
                //name_text.Clear();
                return;
            }

            if (plate.Length < 7)
            {
                //plate_text.Clear();
                status_label.Text = "חסרות ספרות";
                //name_text.Clear();
                return;
            }
            if (plate.Length >8)
            {
                //plate_text.Clear();
                status_label.Text = "יותר מדי ספרות";
                //name_text.Clear();
                return;
            }

            parking2.Akuv temp = new parking2.Akuv(plate, name);
            data = temp;
            exit = true;
            this.Close();
        }

        public void enter_pressed_plate(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
        
    }
}
