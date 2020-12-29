using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using parking2.cs;




namespace parking2
{
    public partial class parking2 : Form
    {

        string path_date;
        String[] path_entry;
        String[] path_exit;
        
        
        public List<Akuv> maakav;
        Akuv Asal =new Akuv("53224101","Asal");
        Akuv King= new Akuv("52551901", "King");
        Akuv Eli = new Akuv("53224101", "Eli");
        List<Akuv> maakav_vip = new List<Akuv>();
        int counter_min=1000000000;//240
        int counter_max = 1000000000;//960



        struct Gniva
        {
            public int exit;
            public TimeSpan time;

            public Gniva(int ex,TimeSpan tim)
            {
                exit = ex;
                time = tim;
            }
        }

        public class Akuv
        {
            string plate;
            string name;
            public Akuv()
            {
                plate = null;
                name = null;
            }
            public Akuv(string number,string str)
            {
                this.plate=number;
                this.name=str;
            }

            public string getPlate()
            {
                return plate;
            }

            public string getName()
            {
                return name;
            }
        }

        List<Gniva> gnivot;
        int delay_41, delay_42, delay_43, delay_44 = 0;


        TimeSpan time_of_parking;
        List<string> allnames = new List<string>();
        
        int counter41, counter42, counter43, counter44 = 0;
        string last_short41, last_short42, last_short43, last_short44;
        string last_long41, last_long42, last_long43, last_long44;

        bool found = false;
        bool isColored = true;
        DateTime today_date;
        DateTime date1 = new DateTime(2020, 6, 14);
        DateTime date2 = new DateTime(2020, 6, 15);
        DateTime date3 = new DateTime(2020, 6, 17);
        TimeSpan show1 = new TimeSpan(17, 15, 0);
        TimeSpan show2 = new TimeSpan(17, 40, 0);
        TimeSpan show3 = new TimeSpan(18, 10, 0);
        TimeSpan show4 = new TimeSpan(18, 45, 0);
        TimeSpan show5 = new TimeSpan(19, 10, 0);
        TimeSpan show6 = new TimeSpan(19, 50, 0);
        TimeSpan show7 = new TimeSpan(20, 30, 0);
        TimeSpan show8 = new TimeSpan(20, 55, 0);
        TimeSpan show9 = new TimeSpan(21, 30, 0);
        TimeSpan show10 = new TimeSpan(22, 2, 0);
        TimeSpan show11 = new TimeSpan(22, 20, 0);
        TimeSpan show12 = new TimeSpan(22, 37, 0);
        TimeSpan show13 = new TimeSpan(22, 50, 0);
        TimeSpan dontShow1 = new TimeSpan(17, 30, 0);
        TimeSpan dontShow2 = new TimeSpan(17, 50, 0);
        TimeSpan dontShow3 = new TimeSpan(18, 20, 0);
        TimeSpan dontShow4 = new TimeSpan(18, 55, 0);
        TimeSpan dontShow5 = new TimeSpan(19, 20, 0);
        TimeSpan dontShow6 = new TimeSpan(20, 2, 0);
        TimeSpan dontShow7 = new TimeSpan(20, 45, 0);
        TimeSpan dontShow8 = new TimeSpan(21, 7, 0);
        TimeSpan dontShow9 = new TimeSpan(21, 45, 0);
        TimeSpan dontShow10 = new TimeSpan(22, 11, 0);
        TimeSpan dontShow11 = new TimeSpan(22, 28, 0);
        TimeSpan dontShow12 = new TimeSpan(22, 48, 0);
        TimeSpan dontShow13 = new TimeSpan(22, 55, 0);

        


        public parking2()
        {
            InitializeComponent();
            label_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            String today = DateTime.Now.ToString("yyyy-MM-dd");
            path_date = "C://IPI_LPR//Local//Files//CarImages//" + today;
            today_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            path_entry = new String[6];
            path_exit = new String[6];
            last_short41 = last_short42 = last_short43 = last_short44 = "0";
            last_long41 = last_long42 = last_long43 = last_long44 = "0";
            delay_41 = 15;
            delay_42 = 60;
            delay_43 = 60;
            delay_44 = 20;

            int num = 21;
            int num1 = 41;
            for (int i = 0; i < 6; i++)
            {
                path_entry[i] = path_date + "//" + "EN" + num;
                ++num;
            }

            for (int i = 0; i < 4; i++)
            {
                path_exit[i] = path_date + "//" + "EX" + num1;
                ++num1;
            }

            gnivot = new List<Gniva>();

            
            hand_platetextBox.KeyDown += new KeyEventHandler(enter_pressed_plate);
            maakavComboBox1.KeyDown += new KeyEventHandler(maakavAdd);
            maakavComboBox2.KeyDown += new KeyEventHandler(maakavAdd);
            maakavComboBox3.KeyDown += new KeyEventHandler(maakavAdd);
            maakavComboBox4.KeyDown += new KeyEventHandler(maakavAdd);
            maakavComboBox5.KeyDown += new KeyEventHandler(maakavAdd);
            maakav = new List<Akuv>(new Akuv[6]);
                
                
            maakav_vip = new List<Akuv>();
            allnames.Add("New");
            allnames.Add("Remove");
            setDelayComboBox.Text = "3-5";
            update_vip();
            timer1.Start();
            timer2.Start();
        }

        public void update()
        {

            ++counter41;
            ++counter42;
            ++counter43;
            ++counter44;

            for (int j = 0; j < 4; j++)
            {
                if (!(Directory.Exists(path_exit[j])))
                    continue;

                string[] folders_ex = Directory.GetDirectories(path_exit[j]);
                string last = folders_ex[folders_ex.Length - 1];
                
                TimeSpan time_entry = new TimeSpan();


                if (j == 0)
                {
                    if (last_long41.Equals(last))
                    {
                        if (counter41 > counter_min && counter41 < counter_max)
                        {
                            platetextBox41.BackColor = Color.Gold;
                            entrytextBox41.BackColor = Color.Gold;
                            totaltimetextBox41.BackColor = Color.Gold;
                            pricetextBox41.BackColor = Color.Gold;
                        }
                        if (counter41 > counter_max)
                        {
                            platetextBox41.BackColor = Color.OrangeRed;
                            entrytextBox41.BackColor = Color.OrangeRed;
                            totaltimetextBox41.BackColor = Color.OrangeRed;
                            pricetextBox41.BackColor = Color.OrangeRed;
                        }

                        continue;
                    }
                    else
                    {
                        last_long41 = last;
                        platetextBox41.BackColor = Color.White;
                        entrytextBox41.BackColor = Color.White;
                        totaltimetextBox41.BackColor = Color.White;
                        pricetextBox41.BackColor = Color.White;
                        counter41 = 0;
                    }
                }
                else
                    if (j == 1)
                    {
                        if (last_long42.Equals(last))
                        {
                            if (counter42 > counter_min && counter42 < counter_min)
                            {
                                platetextBox42.BackColor = Color.Gold;
                                entrytextBox42.BackColor = Color.Gold;
                                totaltimetextBox42.BackColor = Color.Gold;
                                pricetextBox42.BackColor = Color.Gold;
                            }
                            if (counter42 > counter_max)
                            {
                                platetextBox42.BackColor = Color.OrangeRed;
                                entrytextBox42.BackColor = Color.OrangeRed;
                                totaltimetextBox42.BackColor = Color.OrangeRed;
                                pricetextBox42.BackColor = Color.OrangeRed;
                            }

                            continue;
                        }
                        else
                        {
                            last_long42 = last;
                            platetextBox42.BackColor = Color.White;
                            entrytextBox42.BackColor = Color.White;
                            totaltimetextBox42.BackColor = Color.White;
                            pricetextBox42.BackColor = Color.White;
                            counter42 = 0;
                        }
                    }
                    else
                        if (j == 2)
                        {
                            if (last_long43.Equals(last))
                            {
                                if (counter43 >counter_min && counter43 < counter_max)
                                {
                                    platetextBox43.BackColor = Color.Gold;
                                    entrytextBox43.BackColor = Color.Gold;
                                    totaltimetextBox43.BackColor = Color.Gold;
                                    pricetextBox43.BackColor = Color.Gold;
                                }
                                if (counter43 > counter_max)
                                {
                                    platetextBox43.BackColor = Color.OrangeRed;
                                    entrytextBox43.BackColor = Color.OrangeRed;
                                    totaltimetextBox43.BackColor = Color.OrangeRed;
                                    pricetextBox43.BackColor = Color.OrangeRed;
                                }

                                continue;
                            }
                            else
                            {
                                last_long43 = last;
                                platetextBox43.BackColor = Color.White;
                                entrytextBox43.BackColor = Color.White;
                                totaltimetextBox43.BackColor = Color.White;
                                pricetextBox43.BackColor = Color.White;
                                counter43 = 0;
                            }
                        }
                        else
                            if (j == 3)
                            {
                                if (last_long44.Equals(last))
                                {
                                    if (counter44 > counter_min && counter44 < counter_max)
                                    {
                                        platetextBox44.BackColor = Color.Gold;
                                        entrytextBox44.BackColor = Color.Gold;
                                        totaltimetextBox44.BackColor = Color.Gold;
                                        pricetextBox44.BackColor = Color.Gold;
                                    }
                                    if (counter44 > counter_max)
                                    {
                                        platetextBox44.BackColor = Color.OrangeRed;
                                        entrytextBox44.BackColor = Color.OrangeRed;
                                        totaltimetextBox44.BackColor = Color.OrangeRed;
                                        pricetextBox44.BackColor = Color.OrangeRed;
                                    }

                                    continue;
                                }
                                else
                                {
                                    last_long44 = last;
                                    platetextBox44.BackColor = Color.White;
                                    entrytextBox44.BackColor = Color.White;
                                    totaltimetextBox44.BackColor = Color.White;
                                    pricetextBox44.BackColor = Color.White;
                                    counter44 = 0;
                                }
                            }
                


                if (last.ElementAt(64) == '0')
                {
                    switch (j)
                    {
                        case 0:
                            platetextBox41.Text = "לא זוהה מספר!";
                            entrytextBox41.Text = "";
                            totaltimetextBox41.Text = "";
                            pricetextBox41.Text = "";
                            //gnivot.Add(new Gniva(41 + j, DateTime.Now.TimeOfDay.Add(new TimeSpan(0,0,15))));
                            break;
                        case 1:
                            platetextBox42.Text = "לא זוהה מספר!";
                            entrytextBox42.Text = "";
                            totaltimetextBox42.Text = "";
                            pricetextBox42.Text = "";
                            //gnivot.Add(new Gniva(41 + j, DateTime.Now.TimeOfDay.Add(new TimeSpan(0,1,0))));
                            break;
                        case 2:
                            platetextBox43.Text = "לא זוהה מספר!";
                            entrytextBox43.Text = "";
                            totaltimetextBox43.Text = "";
                            pricetextBox43.Text = "";
                            //gnivot.Add(new Gniva(41 + j, DateTime.Now.TimeOfDay.Add(new TimeSpan(0,1,0))));
                            break;
                        case 3:
                            platetextBox44.Text = "לא זוהה מספר!";
                            entrytextBox44.Text = "";
                            totaltimetextBox44.Text = "";
                            pricetextBox44.Text = "";
                            //gnivot.Add(new Gniva(41 + j, DateTime.Now.TimeOfDay.Add(new TimeSpan(0,0,20))));
                            break;
                    }                  
                    continue;
                }

                if (last.ElementAt(73) == '_')
                {
                    last = last.Substring(67, 6);
                    switch (j)
                    {
                        case 0:
                            platetextBox41.Text = last;
                            entrytextBox41.Text = "";
                            totaltimetextBox41.Text = "";
                            pricetextBox41.Text = "";
                            break;
                        case 1:
                            platetextBox42.Text = last;
                            entrytextBox42.Text = "";
                            totaltimetextBox42.Text = "";
                            pricetextBox42.Text = "";
                            break;
                        case 2:
                            platetextBox43.Text = last;
                            entrytextBox43.Text = "";
                            totaltimetextBox43.Text = "";
                            pricetextBox43.Text = "";
                            break;
                        case 3:
                            platetextBox44.Text = last;
                            entrytextBox44.Text = "";
                            totaltimetextBox44.Text = "";
                            pricetextBox44.Text = "";
                            break;
                    }
                }
                else
                    if (last.ElementAt(74) == '_')
                    {
                        last = last.Substring(67, 7);
                        switch (j)
                        {
                            case 0:
                                platetextBox41.Text = last;
                                entrytextBox41.Text = "";
                                totaltimetextBox41.Text = "";
                                pricetextBox41.Text = "";
                                break;
                            case 1:
                                platetextBox42.Text = last;
                                entrytextBox42.Text = "";
                                totaltimetextBox42.Text = "";
                                pricetextBox42.Text = "";
                                break;
                            case 2:
                                platetextBox43.Text = last;
                                entrytextBox43.Text = "";
                                totaltimetextBox43.Text = "";
                                pricetextBox43.Text = "";
                                break;
                            case 3:
                                platetextBox44.Text = last;
                                entrytextBox44.Text = "";
                                totaltimetextBox44.Text = "";
                                pricetextBox44.Text = "";
                                break;
                        }
                    }
                    else
                        if (last.ElementAt(75) == '_')
                        {
                            last = last.Substring(67, 8);
                            switch (j)
                            {
                                case 0:
                                    platetextBox41.Text = last;
                                    entrytextBox41.Text = "";
                                    totaltimetextBox41.Text = "";
                                    pricetextBox41.Text = "";
                                    break;
                                case 1:
                                    platetextBox42.Text = last;
                                    entrytextBox42.Text = "";
                                    totaltimetextBox42.Text = "";
                                    pricetextBox42.Text = "";
                                    break;
                                case 2:
                                    platetextBox43.Text = last;
                                    entrytextBox43.Text = "";
                                    totaltimetextBox43.Text = "";
                                    pricetextBox43.Text = "";
                                    break;
                                case 3:
                                    platetextBox44.Text = last;
                                    entrytextBox44.Text = "";
                                    totaltimetextBox44.Text = "";
                                    pricetextBox44.Text = "";
                                    break;
                            }
                        }

                for (int i = 0; i < 6; i++)
                {
                    string[] folders_en = Directory.GetDirectories(path_entry[i]);
                    foreach (string folder in folders_en)
                    {
                        if (folder.Contains(last))
                        {

                            string entry_time = folder.Substring(55, 8);
                            TimeSpan temp_time = new TimeSpan(Int32.Parse(entry_time.Substring(0, 2)), Int32.Parse(entry_time.Substring(3, 2)), Int32.Parse(entry_time.Substring(6, 2)));
                            if (temp_time <= time_entry)
                                break;
                            else
                                time_entry = temp_time;
                            int price = -1;

                            switch (j)
                            {
                                case 0:
                                    //platetextBox41.Text = last;
                                    entrytextBox41.Text = time_entry.ToString();
                                    price = calcPrice(CalcTime(entry_time));

                                    if (time_of_parking.Hours > 9 && time_of_parking.Minutes > 9)
                                        totaltimetextBox41.Text = time_of_parking.Hours.ToString() + ":" + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours <= 9 && time_of_parking.Minutes <= 9)
                                        totaltimetextBox41.Text = "0" + time_of_parking.Hours.ToString() + " : " + "0" + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours <= 9 && time_of_parking.Minutes > 9)
                                        totaltimetextBox41.Text = "0" + time_of_parking.Hours.ToString() + " : " + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours > 9 && time_of_parking.Minutes <= 9)
                                        totaltimetextBox41.Text = time_of_parking.Hours.ToString() + " : " + "0" + time_of_parking.Minutes.ToString();
                                    pricetextBox41.Text = price.ToString();

                                    break;
                                case 1:
                                    //platetextBox42.Text = last;
                                    entrytextBox42.Text = time_entry.ToString();
                                    price = calcPrice(CalcTime(entry_time));

                                    if (time_of_parking.Hours > 9 && time_of_parking.Minutes > 9)
                                        totaltimetextBox42.Text = time_of_parking.Hours.ToString() + ":" + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours <= 9 && time_of_parking.Minutes <= 9)
                                        totaltimetextBox42.Text = "0" + time_of_parking.Hours.ToString() + " : " + "0" + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours <= 9 && time_of_parking.Minutes > 9)
                                        totaltimetextBox42.Text = "0" + time_of_parking.Hours.ToString() + " : " + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours > 9 && time_of_parking.Minutes <= 9)
                                        totaltimetextBox42.Text = time_of_parking.Hours.ToString() + " : " + "0" + time_of_parking.Minutes.ToString();
                                    pricetextBox42.Text = price.ToString();

                                    break;
                                case 2:
                                    //platetextBox43.Text = last;
                                    entrytextBox43.Text = time_entry.ToString();
                                    price = calcPrice(CalcTime(entry_time));

                                    if (time_of_parking.Hours > 9 && time_of_parking.Minutes > 9)
                                        totaltimetextBox43.Text = time_of_parking.Hours.ToString() + ":" + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours <= 9 && time_of_parking.Minutes <= 9)
                                        totaltimetextBox43.Text = "0" + time_of_parking.Hours.ToString() + " : " + "0" + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours <= 9 && time_of_parking.Minutes > 9)
                                        totaltimetextBox43.Text = "0" + time_of_parking.Hours.ToString() + " : " + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours > 9 && time_of_parking.Minutes <= 9)
                                        totaltimetextBox43.Text = time_of_parking.Hours.ToString() + " : " + "0" + time_of_parking.Minutes.ToString();
                                    pricetextBox43.Text = price.ToString();
                                    break;
                                case 3:
                                    // platetextBox44.Text = last;
                                    entrytextBox44.Text = time_entry.ToString();
                                    price = calcPrice(CalcTime(entry_time));

                                    if (time_of_parking.Hours > 9 && time_of_parking.Minutes > 9)
                                        totaltimetextBox44.Text = time_of_parking.Hours.ToString() + ":" + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours <= 9 && time_of_parking.Minutes <= 9)
                                        totaltimetextBox44.Text = "0" + time_of_parking.Hours.ToString() + " : " + "0" + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours <= 9 && time_of_parking.Minutes > 9)
                                        totaltimetextBox44.Text = "0" + time_of_parking.Hours.ToString() + " : " + time_of_parking.Minutes.ToString();
                                    if (time_of_parking.Hours > 9 && time_of_parking.Minutes <= 9)
                                        totaltimetextBox44.Text = time_of_parking.Hours.ToString() + " : " + "0" + time_of_parking.Minutes.ToString();
                                    pricetextBox44.Text = price.ToString();
                                    break;
                            }//end of switch

                            found = true;

                        }//end of if contains

                    }//end for each string in entry folder

                }//end of for of all entrys

                if (found == false)
                {

                    time_entry = new TimeSpan();
                    switch (j)
                    {
                        case 0:
                            entrytextBox41.Text = "לא נמצאה כניסה";
                            break;
                        case 1:
                            entrytextBox42.Text = "לא נמצאה כניסה";
                            break;
                        case 2:
                            entrytextBox43.Text = "לא נמצאה כניסה";
                            break;
                        case 3:
                            entrytextBox44.Text = "לא נמצאה כניסה";
                            break;
                    }

                }
                else
                {
                    found = false;
                    time_entry = new TimeSpan();

                }

            }//enf of for of all exits

            /* MAAKAV CHECK */

            int f;
            for (f = 1; f < 6;f++ )
            {
                if (maakav[f] == null)
                    continue;
                else
                {
                    if(check_if_in(maakav[f].getPlate()))
                    {
                        switch (f)
                        {
                            case 1:
                                maakavButton1.BackColor=Color.Green;
                                break;
                            case 2:
                                maakavButton2.BackColor=Color.Green;
                                break;
                            case 3:
                                maakavButton3.BackColor=Color.Green;
                                break;
                            case 4:
                                maakavButton4.BackColor=Color.Green;
                                break;
                            case 5:
                                maakavButton5.BackColor=Color.Green;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (f)
                        {
                            case 1:
                                maakavButton1.BackColor = Color.Red;
                                break;
                            case 2:
                                maakavButton2.BackColor = Color.Red;
                                break;
                            case 3:
                                maakavButton3.BackColor = Color.Red;
                                break;
                            case 4:
                                maakavButton4.BackColor = Color.Red;
                                break;
                            case 5:
                                maakavButton5.BackColor = Color.Red;
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
        }


        private DateTime CalcTime(string time)
        {

            DateTime entry_time = today_date.AddMinutes(Convert.ToInt32(time.Substring(0, 2)) * 60 + Convert.ToInt32(time.Substring(3, 2)));
            return entry_time;
        }

        private int calcPrice(DateTime entry_time)
        {
            TimeSpan parking_time = DateTime.Now - entry_time;
            time_of_parking = parking_time;

            if (parking_time.TotalMinutes < 121)
            {
                return 0;
            }
            else
                if (parking_time.TotalMinutes >= 121 && parking_time.TotalMinutes < 181)
                {
                    return 8;
                }
                else
                {
                    TimeSpan temp = parking_time - new TimeSpan(3, 0, 0);
                    int minutes = (int)temp.TotalMinutes;
                    int intervals_of_15 = minutes / 15 + 1;
                    if ((8 + intervals_of_15 * 3) < 50)
                        return (8 + intervals_of_15 * 3);
                    else
                        return 50;
                }

        }

        public void enter_pressed_plate(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string plate_number = hand_platetextBox.Text;
                bool isNumeric = true;
                foreach (char ch in plate_number)
                    if (Char.IsDigit(ch))
                        continue;
                    else
                    {
                        isNumeric = false;
                        break;
                    }
                if (isNumeric == false)
                {
                    hand_entrytextBox.Text = "מספר לא תקין";
                    hand_totaltimetextBox.Clear();
                    hand_pricetextBox.Clear();
                    hand_statustextbox.Clear();
                    return;
                }

                if (plate_number.Length < 7)
                {
                    hand_entrytextBox.Text = "חסרות ספרות";
                    hand_totaltimetextBox.Clear();
                    hand_pricetextBox.Clear();
                    hand_statustextbox.Clear();
                    return;
                }
                if (plate_number.Length > 8)
                {
                    hand_entrytextBox.Text = "יותר מדי ספרות";
                    hand_totaltimetextBox.Clear();
                    hand_pricetextBox.Clear();
                    hand_statustextbox.Clear();
                    return;
                }

                hand_entrytextBox.Clear();
                hand_totaltimetextBox.Clear();
                hand_pricetextBox.Clear();
                hand_statustextbox.Clear();

                TimeSpan time_entry = new TimeSpan();
                TimeSpan time_exit = new TimeSpan();
                bool exits=false;
                bool entrys = false;

                for (int i = 0; i < 6; i++)
                {
                    string[] folders = Directory.GetDirectories(path_entry[i]);
                    foreach (string folder in folders)
                    {

                        if (folder.Contains(plate_number))
                        {
                            string entry_time = folder.Substring(55, 8);
                            TimeSpan temp_time = new TimeSpan(Int32.Parse(entry_time.Substring(0, 2)), Int32.Parse(entry_time.Substring(3, 2)), Int32.Parse(entry_time.Substring(6, 2)));
                            if (temp_time <= time_entry)
                                break;
                            else
                                time_entry = temp_time;

                            hand_entrytextBox.Text = entry_time.Substring(0, 5);

                            int price = calcPrice(CalcTime(entry_time));
                            if (time_of_parking.Hours > 9 && time_of_parking.Minutes > 9)
                                hand_totaltimetextBox.Text = time_of_parking.Hours.ToString() + ":" + time_of_parking.Minutes.ToString();
                            if (time_of_parking.Hours <= 9 && time_of_parking.Minutes <= 9)
                                hand_totaltimetextBox.Text = "0" + time_of_parking.Hours.ToString() + " : " + "0" + time_of_parking.Minutes.ToString();
                            if (time_of_parking.Hours <= 9 && time_of_parking.Minutes > 9)
                                hand_totaltimetextBox.Text = "0" + time_of_parking.Hours.ToString() + " : " + time_of_parking.Minutes.ToString();
                            if (time_of_parking.Hours > 9 && time_of_parking.Minutes <= 9)
                                hand_totaltimetextBox.Text = time_of_parking.Hours.ToString() + " : " + "0" + time_of_parking.Minutes.ToString();

                            hand_pricetextBox.Text = price.ToString();
                            found = true;
                            entrys = true;
                        }
                    }
                }

                    for (int i = 0; i < 4; i++)
                    {
                        string[] folders = Directory.GetDirectories(path_exit[i]);
                        foreach (string folder in folders)
                        {
                            if (folder.Contains(plate_number))
                            {
                                exits = true;
                                string exit_time = folder.Substring(55, 8);
                                TimeSpan temp_time = new TimeSpan(Int32.Parse(exit_time.Substring(0, 2)), Int32.Parse(exit_time.Substring(3, 2)), Int32.Parse(exit_time.Substring(6, 2)));
                                if (temp_time <= time_exit)
                                    break;
                                else
                                    time_exit = temp_time;
                            }

                        }
                    }
                    if (exits == true)
                    {
                        if (time_exit > time_entry)
                            hand_statustextbox.Text = "בחוץ";
                        else
                            hand_statustextbox.Text = "בפנים";
                    }
                    else
                    {
                        if(entrys)
                            hand_statustextbox.Text = "בפנים";
                        else
                            hand_statustextbox.Text = "בחוץ";

                    }
                

                if (found == false)
                {
                    hand_entrytextBox.Text = "אין כניסה";
                }
                else
                {
                    found = false;
                }
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            update();
        }

        public void timer2_Tick(object sender, EventArgs e)
        {
            String today = DateTime.Now.ToString("yyyy-MM-dd");
            path_date = "C://IPI_LPR//Local//Files//CarImages//" + today;
            today_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            label_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Start");
            foreach (Gniva gniva in gnivot)
            {           
                Console.WriteLine("exit {0} on {1}",gniva.exit,gniva.time.ToString());
            }
            Console.WriteLine("End");
        }

        public void maakavAdd(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cbox= ((System.Windows.Forms.ComboBox)sender).Name;
                string text = ((System.Windows.Forms.ComboBox)sender).Text;
                switch(cbox)
                {
                    case "maakavComboBox1":
                        actionMaakav(1,text);
                        break;
                    case "maakavComboBox2":
                        actionMaakav(2, text);
                        break;
                    case "maakavComboBox3":
                        actionMaakav(3, text);
                        break;
                    case "maakavComboBox4":
                        actionMaakav(4, text);
                        break;
                    case "maakavComboBox5":
                        actionMaakav(5, text);
                        break;
                    default:
                        break;
                }
            }
            else
                return;
        }

        private void actionMaakav(int num,string text)
        {
            switch (num)
            {
                case 1:
                    switch(text)
                    {
                        case "New":
                            Akuv temp=add_new_maakav();
                            if (temp == null)
                                break;
                            if ((temp.getName()).Equals(""))
                                temp = new Akuv(temp.getPlate(), temp.getPlate());
                            maakav[1] = temp;
                            maakavText1.Enabled = true;
                            maakavText1.Text = temp.getName();
                            maakavButton1.Visible=true;
                            //if (check_if_in(temp.getPlate()))
                            //    maakavButton1.BackColor = Color.Green;
                            //else
                            //    maakavButton1.BackColor = Color.Red;
                            break;

                        case "Remove":
                            maakav[1] = null;
                            maakavText1.Clear();
                            maakavText1.Enabled = false;
                            maakavButton1.Visible = false;
                            break;
                        default: //for vip 
                            maakavText1.Text =text;
                            maakavText1.Enabled = true;
                            foreach(Akuv akuv in maakav_vip)
                            {
                                if (akuv.getName() == text)
                                {
                                    maakav[1] = akuv;
                                    break;
                                }
                            }
                            maakavButton1.Visible=true;
                            break;
                    }                      
                    break;
                case 2:
                    switch(text)
                    {
                        case "New":
                            Akuv temp=add_new_maakav();
                            if (temp == null)
                                break;
                            if ((temp.getName()).Equals(""))
                                temp = new Akuv(temp.getPlate(), temp.getPlate());
                            maakav[2] = temp;
                            maakavText2.Enabled = true;
                            maakavText2.Text = temp.getName();
                            maakavButton2.Visible=true;
                            //if (check_if_in(temp.getPlate()))
                            //    maakavButton1.BackColor = Color.Green;
                            //else
                            //    maakavButton1.BackColor = Color.Red;
                            break;

                        case "Remove":
                            maakav[2] = null;
                            maakavText2.Clear();
                            maakavText2.Enabled = false;
                            maakavButton2.Visible = false;
                            break;
                        default: //for vip 
                            maakavText2.Text =text;
                            maakavText2.Enabled = true;
                            foreach (Akuv akuv in maakav_vip)
                            {
                                if (akuv.getName() == text)
                                {
                                    maakav[2] = akuv;
                                    break;
                                }
                            }
                            maakavButton2.Visible=true;
                            break;
                    }                      
                    break;
                case 3:
                    switch(text)
                    {
                        case "New":
                            Akuv temp=add_new_maakav();
                            if (temp == null)
                                break;
                            if ((temp.getName()).Equals(""))
                                temp = new Akuv(temp.getPlate(), temp.getPlate());
                            maakav[3] = temp;
                            maakavText3.Enabled = true;
                            maakavText3.Text = temp.getName();
                            maakavButton3.Visible=true;
                            //if (check_if_in(temp.getPlate()))
                            //    maakavButton3.BackColor = Color.Green;
                            //else
                            //    maakavButton3.BackColor = Color.Red;
                            break;

                        case "Remove":
                            maakav[3] = null;
                            maakavText3.Clear();
                            maakavText3.Enabled = false;
                            maakavButton3.Visible = false;
                            break;
                        default: //for vip 
                            maakavText3.Text =text;
                            maakavText3.Enabled = true;
                            foreach (Akuv akuv in maakav_vip)
                            {
                                if (akuv.getName() == text)
                                {
                                    maakav[3] = akuv;
                                    break;
                                }
                            }
                            maakavButton3.Visible=true;
                            break;
                    }                      
                    break;
                case 4:
                    switch(text)
                    {
                        case "New":
                            Akuv temp=add_new_maakav();
                            if (temp == null)
                                break;
                            if ((temp.getName()).Equals(""))
                                temp = new Akuv(temp.getPlate(), temp.getPlate());
                            maakav[4] = temp;
                            maakavText4.Enabled = true;
                            maakavText4.Text = temp.getName();
                            maakavButton4.Visible=true;
                            //if (check_if_in(temp.getPlate()))
                            //    maakavButton4.BackColor = Color.Green;
                            //else
                            //    maakavButton4.BackColor = Color.Red;
                            break;

                        case "Remove":
                            maakav[4] = null;
                            maakavText4.Clear();
                            maakavText4.Enabled = false;
                            maakavButton4.Visible = false;
                            break;
                        default: //for vip 
                            maakavText4.Text =text;
                            maakavText4.Enabled = true;
                            foreach (Akuv akuv in maakav_vip)
                            {
                                if (akuv.getName() == text)
                                {
                                    maakav[4] = akuv;
                                    break;
                                }
                            }
                            maakavButton4.Visible=true;
                            break;
                    }                      
                    break;
                case 5:
                    switch(text)
                    {
                        case "New":
                            Akuv temp=add_new_maakav();
                            if (temp == null)
                                break;
                            if ((temp.getName()).Equals(""))
                                temp = new Akuv(temp.getPlate(), temp.getPlate());
                            maakav[5] = temp;
                            maakavText5.Enabled = true;
                            maakavText5.Text = temp.getName();
                            maakavButton5.Visible=true;
                            if (check_if_in(temp.getPlate()))
                                maakavButton5.BackColor = Color.Green;
                            else
                                maakavButton5.BackColor = Color.Red;
                            break;

                        case "Remove":
                            maakav[5] = null;
                            maakavText5.Clear();
                            maakavText5.Enabled = false;
                            maakavButton5.Visible = false;
                            break;

                        default: //for vip 
                            maakavText5.Text =text;
                            maakavText5.Enabled = true;
                            foreach (Akuv akuv in maakav_vip)
                            {
                                if (akuv.getName() == text)
                                {
                                    maakav[5] = akuv;
                                    break;
                                }
                            }
                            maakavButton5.Visible=true;
                            break;
                    }                      
                    break;
                default:
                    break;
            }
        }

        private Akuv add_new_maakav()
        {
            MaakavForm mform = new MaakavForm();
            mform.StartPosition = FormStartPosition.Manual;
            mform.Left = 500;
            mform.Top = 430;
            mform.ShowDialog();
            while (mform.exit == false)
            {
                if (mform.Visible == false)
                    return null;
            }
            return mform.data;
        }

        public bool check_if_in(string plate)
        {
                TimeSpan time_entry = new TimeSpan();
                TimeSpan time_exit = new TimeSpan();
                bool exits=false;
                bool entrys = false;

                for (int i = 0; i < 6; i++)
                {
                    string[] folders = Directory.GetDirectories(path_entry[i]);
                    foreach (string folder in folders)
                    {
                        if (folder.Contains(plate))
                        {
                            string entry_time = folder.Substring(55, 8);
                            TimeSpan temp_time = new TimeSpan(Int32.Parse(entry_time.Substring(0, 2)), Int32.Parse(entry_time.Substring(3, 2)), Int32.Parse(entry_time.Substring(6, 2)));
                            if (temp_time <= time_entry)
                                break;
                            else
                                time_entry = temp_time;
                            entrys = true;

                        }
                    }
                }

                    for (int i = 0; i < 4; i++)
                    {
                        string[] folders = Directory.GetDirectories(path_exit[i]);
                        foreach (string folder in folders)
                        {
                            if (folder.Contains(plate))
                            {
                                exits = true;
                                string exit_time = folder.Substring(55, 8);
                                TimeSpan temp_time = new TimeSpan(Int32.Parse(exit_time.Substring(0, 2)), Int32.Parse(exit_time.Substring(3, 2)), Int32.Parse(exit_time.Substring(6, 2)));
                                if (temp_time <= time_exit)
                                    break;
                                else
                                    time_exit = temp_time;
                            }

                        }
                    }

                    if (exits == true)
                    {
                        if (time_exit > time_entry)
                            return false;
                        else
                            return true;
                    }
                    else
                    {
                        if (entrys)
                            return true;
                        else
                            return false;
                    }            
            }

        void update_vip()
        {
            string path = "C:\\Users\\Administrator\\Desktop\\קבצים לתוכנות\\"+"vip.txt";
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach(string line in lines)
            {
                string temp_number=line.Split(',')[0];
                string temp_name=line.Split(',')[1];           
                Akuv temp = new Akuv(temp_number, temp_name);
                maakav_vip.Add(temp);
                allnames.Add(temp_name);
 
            }
            maakavComboBox1.DataSource = new List<string>(allnames);
            maakavComboBox2.DataSource = new List<string>(allnames);
            maakavComboBox3.DataSource = new List<string>(allnames);
            maakavComboBox4.DataSource = new List<string>(allnames);
            maakavComboBox5.DataSource = new List<string>(allnames);
                       
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setDelayComboBox_Click(object sender, EventArgs e)
        {
            
            string text = ((System.Windows.Forms.ToolStripComboBox)sender).Text;
            if (text.Equals("") || bitulDelayComboBox.Checked==false)
                return;
            string from = text.Split('-')[0];
            string to = text.Split('-')[1];
            counter_min = Int32.Parse(from)*120;
            counter_max = Int32.Parse(to)*120;
            labelLow.Text = "0-" + from + " min";
            labelMedium.Text = from + "-" + to + " min";
            label1High.Text = to + "+"+" min";
            editToolStripMenuItem.HideDropDown();

            
        }

        private void bitulDelayComboBox_Click(object sender, EventArgs e)
        {
            if (bitulDelayComboBox.Checked == false)
            {
                string text = setDelayComboBox.Text;
                if (text.Equals(""))
                {
                    counter_min = 240;
                    counter_max = 960;
                    labelLow.Text = "0-2" + " min";
                    labelMedium.Text = "2-5" + " min";
                    label1High.Text = "5+";
                    bitulDelayComboBox.Checked = true;
                    return;
                }

                bitulDelayComboBox.Checked = true;
                string from = text.Split('-')[0];
                string to = text.Split('-')[1];
                counter_min = Int32.Parse(from) * 120;
                counter_max = Int32.Parse(to) * 120;
                labelLow.Text = "0-" + from+" min";
                labelMedium.Text = from + "-" + to + " min";
                label1High.Text = to + "+";
            }
            else
            {
                bitulDelayComboBox.Checked = false;
                counter_min = 1000000;
                counter_max = 1000000;
                labelLow.Text = "";
                labelMedium.Text = "";
                label1High.Text = "";
            }
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            return;
            DateTime date_now = new DateTime();
            TimeSpan time_now=new TimeSpan();
            date_now = DateTime.Now;
            time_now=DateTime.Now.TimeOfDay;

            if (date_now.Date == date2.Date)
            {
                Console.WriteLine("date2");
                if (time_now > show1 && time_now < dontShow1)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "..רות לא לשכוח לשתות קפה";
                    return;
                }
                if (time_now > show2 && time_now < dontShow2)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "?אולי עוד קפה";
                    return;
                }
                if (time_now > show3 && time_now < dontShow3)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "תרגעי ותפסיקי לצעוק על אנשים";
                    return;
                }
                if (time_now > show4 && time_now < dontShow4)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "קצת לחץ לא יזיק,ואל תגידי שהתעייפת";
                    return;
                }
                if (time_now > show5 && time_now < dontShow5)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "...הרבה זמן לא היו תקלות";
                    return;
                }
                if (time_now > show6 && time_now < dontShow6)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = " ..מה משעמם להם בבית?לי מה זה כיף";
                    return;
                }
                if (time_now > show7 && time_now < dontShow7)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "?קפה או סרט";
                    return;
                }
                if (time_now > show8 && time_now < dontShow8)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "!!!אלוו לא להרדם";
                    return;
                }
                if (time_now > show9 && time_now < dontShow9)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "?מה זה העשן הזה מעל הקניון";
                    return;
                }
                if (time_now > show10 && time_now < dontShow10)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "וואלה 200 שקל עשית כבר";
                    return;
                }
                if (time_now > show11 && time_now < dontShow11)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "לא לשכוח לאכול משהן לפני השינה";
                    return;
                }
                if (time_now > show12 && time_now < dontShow12)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "עייפות זה לא סיבה לא לקום מוקדם";
                    return;
                }
                if (time_now > show13 && time_now < dontShow13)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "!יאללה לילה טוב ,היה כיף,לנעול ";
                    return;
                }

                MessageLabel.Visible = false;
                    
            }

            if (date_now.Date == date2.Date)
            {
                if (time_now > show1 && time_now < dontShow1)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "...עוד פעם את";
                    return;
                }
                if (time_now > show2 && time_now < dontShow2)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "coffee timee";
                    return;
                }
                if (time_now > show3 && time_now < dontShow3)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "עד מתייייי";
                    return;
                }
                if (time_now > show4 && time_now < dontShow4)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "...הנה הם באים";
                    return;
                }
                if (time_now > show5 && time_now < dontShow5)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "אין כמו תקלה באמצע משמרת";
                    return;
                }
                if (time_now > show6 && time_now < dontShow6)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "תמיד אפשר לסגור וללכת הביתה";
                    return;
                }
                if (time_now > show7 && time_now < dontShow7)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "נראה לי תינוקות בוכים פחות ממך";
                    return;
                }
                if (time_now > show8 && time_now < dontShow8)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "עוגת נקניק נשמה מעניין";
                    return;
                }
                if (time_now > show9 && time_now < dontShow9)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "dont worry, eat something";
                    return;
                }
                if (time_now > show10 && time_now < dontShow10)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "יהיה טוב ,אולי";
                    return;
                }
                if (time_now > show11 && time_now < dontShow11)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "אני אומר אפשר לסגור";
                    return;
                }
                if (time_now > show12 && time_now < dontShow12)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "?עוד פעם התעייפת";
                    return;
                }
                if (time_now > show13 && time_now < dontShow13)
                {
                    MessageLabel.Visible = true;
                    MessageLabel.Text = "ליל'ט לא לשכוח לנעול";
                    return;
                }

                MessageLabel.Visible = false;

            }


                
        }



    }
}
