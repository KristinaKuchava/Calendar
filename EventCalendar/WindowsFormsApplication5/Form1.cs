using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        Control controll = new Control();
        public Form1()
        {
            InitializeComponent();
        }

        //public void Check(DateTime timeS, DateTime timeE, EventCal ev2) //проверка пересечения
        //{
        //    if (timeS > ev2.TimeStart && timeS < ev2.TimeEnd)
        //    {
        //        MessageBox.Show("События не могут пересекаться!");
        //    }
        //    else if (timeE > ev2.TimeStart && timeE < ev2.TimeEnd)
        //    {
        //        MessageBox.Show("События не могут пересекаться!");
        //    }
        //    else if (timeS > ev2.TimeStart && timeE < ev2.TimeEnd)
        //    {
        //        MessageBox.Show("События не могут пересекаться!");
        //    }
        //    else if (timeS < ev2.TimeStart && timeE > ev2.TimeEnd)
        //    {
        //        MessageBox.Show("События не могут пересекаться!");
        //    }
        //}

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.SetSelectionRange(monthCalendar1.SelectionEnd, monthCalendar1.SelectionEnd);
            dateTimePicker1.Value = monthCalendar1.SelectionEnd;
            dateTimePicker2.Value = monthCalendar1.SelectionEnd;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.InitializeLifetimeService();
        }
        
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker2.InitializeLifetimeService();
        }

        private void button4_Click(object sender, EventArgs e) //установка события
        {
            DateTime TimeStart = dateTimePicker1.Value;
            DateTime TimeEnd = dateTimePicker2.Value;
            int result = DateTime.Compare(TimeEnd, TimeStart);
            if (textBox4.Text == "" || textBox5.Text == "") MessageBox.Show("Заполните все поля!");
            else if (monthCalendar1.TodayDate > monthCalendar1.SelectionEnd)
            {
                MessageBox.Show("Нельзя устанавливать событие на прошедшее время!");
            }
            else if (result == -1)
            {
                MessageBox.Show("Время окончания не может быть раньше времени начала!");
            }
            else
            {
                int count = 0;
                controll.Load("CalendarEvent.data");
                foreach(EventCal ev in controll.MyList)//проверка на пересечение
                {
                    if (ev.Day.ToLongDateString() == monthCalendar1.SelectionEnd.ToLongDateString())
                    {
                        if (dateTimePicker1.Value >= ev.TimeStart && dateTimePicker1.Value <= ev.TimeEnd)
                        {
                            MessageBox.Show("События не могут пересекаться!");
                            count++;
                        }
                        else if (dateTimePicker2.Value >= ev.TimeStart && dateTimePicker2.Value <= ev.TimeEnd)
                        {
                            MessageBox.Show("События не могут пересекаться!");
                            count++;
                        }
                        else if (dateTimePicker1.Value >= ev.TimeStart && dateTimePicker2.Value <= ev.TimeEnd)
                        {
                            MessageBox.Show("События не могут пересекаться!");
                            count++;
                        }
                        else if (dateTimePicker1.Value <= ev.TimeStart && dateTimePicker2.Value >= ev.TimeEnd)
                        {
                            MessageBox.Show("События не могут пересекаться!");
                            count++;
                        }
                    }
                }
                if (count == 0)
                {
                    EventCal @new = new EventCal(textBox4.Text, dateTimePicker1.Value, dateTimePicker2.Value, monthCalendar1.SelectionEnd, textBox5.Text);///////
                    controll.MyList.Add(@new);
                    controll.Save("CalendarEvent.data");
                    MessageBox.Show("Событие установлено!");
                    textBox4.Clear();
                    textBox5.Clear();
                }
                    
            }

            controll.Load("CalendarEvent.data"); 
            try
            {
                foreach (EventCal ev in controll.MyList)
                {
                    monthCalendar2.AddBoldedDate(ev.Day); //выделение жирным шрифтом
                    monthCalendar2.UpdateBoldedDates();
                    monthCalendar1.AddBoldedDate(ev.Day);
                    monthCalendar1.UpdateBoldedDates();
                    monthCalendar3.AddBoldedDate(ev.Day);
                    monthCalendar3.UpdateBoldedDates();
                }
            }
            catch (System.NullReferenceException) { return; }


        }

        private void Check()
        {
            throw new NotImplementedException();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void button5_Click(object sender, EventArgs e) //Вернуться в начало
        {
            listBox1.Items.Clear();
            tabControl1.SelectedIndex = 1;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e) //вывод событий на выбранный день
        {
            monthCalendar2.SetSelectionRange(monthCalendar2.SelectionEnd, monthCalendar2.SelectionEnd);
            listBox1.Items.Clear();
            try
            {
                controll.Load("CalendarEvent.data");
                foreach (EventCal ev in controll.MyList)
                {
                    if (monthCalendar2.SelectionEnd == ev.Day)
                    {
                        listBox1.Items.Add("Событие:" + ev.Name + " " + "Место:" + ev.Place);
                        listBox1.Items.Add("Время начала:" + ev.TimeStart + " " + "Время конца:" + ev.TimeEnd);
                        listBox1.Items.Add("");
                    }
                }
            }
            catch (System.NullReferenceException) { return; }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            int count = 0;

            controll.Load("CalendarEvent.data");
                foreach (EventCal ev in controll.MyList)  //выделение жирным шрифтом
                {
                    monthCalendar1.AddBoldedDate(ev.Day);   
                    monthCalendar1.UpdateBoldedDates();
                    monthCalendar2.AddBoldedDate(ev.Day); 
                    monthCalendar2.UpdateBoldedDates();
                    monthCalendar3.AddBoldedDate(ev.Day);
                    monthCalendar3.UpdateBoldedDates();
                }

                foreach (EventCal ev in controll.MyList)  //напоминание
            {
                    if (ev.Day.ToLongDateString() == monthCalendar1.TodayDate.ToLongDateString()) //проверка событий на сегодня
                    {
                        MessageBox.Show("На сегодня у вас запланированы дела!");
                        count++;
                    }
                    if (count > 0)
                    {
                        break;
                    }
                }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            tabControl1.SelectedIndex = 3;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // ввод для удаления
        {

        }

        private void button7_Click(object sender, EventArgs e) // кнопла удаления
        {
            string NameOfEvent = textBox1.Text;
            if (textBox1.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                controll.Load("CalendarEvent.data");
                foreach (EventCal ev in controll.MyList)
                {
                    if (NameOfEvent == ev.Name)
                    {
                        monthCalendar1.RemoveAllBoldedDates(); ///удаление жирного шрифта
                        monthCalendar2.RemoveAllBoldedDates();
                        monthCalendar3.RemoveAllBoldedDates();
                        MessageBox.Show("Событие удалено");
                        ev.Name = "";
                        ev.Day = new DateTime(0001, 01, 01);
                        controll.Save("CalendarEvent.data");
                    }
                }
                foreach (EventCal ev in controll.MyList)  //выделение жирным шрифтом
                {
                    monthCalendar1.AddBoldedDate(ev.Day);
                    monthCalendar1.UpdateBoldedDates();
                    monthCalendar2.AddBoldedDate(ev.Day);
                    monthCalendar2.UpdateBoldedDates();
                    monthCalendar3.AddBoldedDate(ev.Day);
                    monthCalendar3.UpdateBoldedDates();
                }
            }
            textBox1.Clear();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)//форма изменения
        {

        }

        private void monthCalendar3_DateChanged(object sender, DateRangeEventArgs e)
        {
            monthCalendar3.SetSelectionRange(monthCalendar3.SelectionEnd, monthCalendar3.SelectionEnd);
            dateTimePicker3.Value = monthCalendar3.SelectionEnd;
            dateTimePicker4.Value = monthCalendar3.SelectionEnd;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.ShowUpDown = true;
            dateTimePicker3.InitializeLifetimeService();
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.ShowUpDown = true;
            dateTimePicker4.InitializeLifetimeService();
        }

        private void button10_Click(object sender, EventArgs e)//кнопка изменить
        {
            controll.Load("CalendarEvent.data");
            string NameOfEvent = textBox2.Text;
            DateTime TimeOfStart = dateTimePicker3.Value;
            DateTime TimeOfEnd = dateTimePicker4.Value;
            int result = DateTime.Compare(TimeOfEnd, TimeOfStart);
            if (textBox2.Text == "") MessageBox.Show("Заполните все поля!");
            else if (monthCalendar3.TodayDate > monthCalendar3.SelectionEnd)
            {
                MessageBox.Show("Нельзя устанавливать событие на прошедшее время!");
            }
            else if (result == -1)
            {
                MessageBox.Show("Время окончания не может быть раньше времени начала!");
            }
            else
            {
                int count = 0;
                foreach (EventCal ev in controll.MyList)
                {
                    if (ev.Name == NameOfEvent)
                    {
                        int count1 = 0;
                        //controll.Load("CalendarEvent.data");
                        foreach (EventCal ev2 in controll.MyList)//проверка на пересечение
                        {
                            if (ev2.Day.ToLongDateString() == monthCalendar3.SelectionEnd.ToLongDateString())
                            {
                                if (dateTimePicker3.Value >= ev2.TimeStart && dateTimePicker3.Value <= ev2.TimeEnd)
                                {
                                    MessageBox.Show("События не могут пересекаться!");
                                    count1++;
                                }
                                else if (dateTimePicker4.Value >= ev2.TimeStart && dateTimePicker4.Value <= ev2.TimeEnd)
                                {
                                    MessageBox.Show("События не могут пересекаться!");
                                    count1++; 
                                }
                                else if (dateTimePicker3.Value >= ev2.TimeStart && dateTimePicker4.Value <= ev2.TimeEnd)
                                {
                                    MessageBox.Show("События не могут пересекаться!");
                                    count1++;
                                }
                                else if (dateTimePicker3.Value <= ev2.TimeStart && dateTimePicker4.Value >= ev2.TimeEnd)
                                {
                                    MessageBox.Show("События не могут пересекаться!");
                                    count1++;
                                }
                            }
                        }
                        if (count1 == 0)
                        {
                            monthCalendar1.RemoveAllBoldedDates(); ///удаление жирного шрифта
                            monthCalendar2.RemoveAllBoldedDates();
                            monthCalendar3.RemoveAllBoldedDates();
                            ev.Day = monthCalendar3.SelectionEnd;
                            ev.TimeStart = dateTimePicker3.Value;
                            ev.TimeEnd = dateTimePicker4.Value;
                            controll.Save("CalendarEvent.data");
                            MessageBox.Show("Событие перенесено!");
                            count++;
                        }
                        foreach (EventCal ev3 in controll.MyList)  //выделение жирным шрифтом
                        {
                            monthCalendar1.AddBoldedDate(ev3.Day);
                            monthCalendar1.UpdateBoldedDates();
                            monthCalendar2.AddBoldedDate(ev3.Day);
                            monthCalendar2.UpdateBoldedDates();
                            monthCalendar3.AddBoldedDate(ev3.Day);
                            monthCalendar3.UpdateBoldedDates();
                        }
                    }
                }
                if (count == 0)
                {
                    MessageBox.Show("Такого события не найдено!");
                }
            }
            textBox2.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }
    }
}




