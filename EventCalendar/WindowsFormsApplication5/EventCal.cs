using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace WindowsFormsApplication5
{
    [Serializable]
    public class EventCal
    {
        public string Name;
        public DateTime TimeStart;
        public DateTime TimeEnd;
        public DateTime Day;///
        public string Place;
        
        public EventCal (string name, DateTime timeStart, DateTime timeEnd, DateTime day,  string place)///
        {
            if (name == null || timeStart == null || timeEnd == null || place == null || day == null)
            {
                throw new ArgumentNullException();
            }
            Name = name;
            TimeStart = timeStart;
            TimeEnd = timeEnd;
            Place = place;
            Day = day;
        }

        /*public static string checkMistake(DateTime TimeStart, DateTime TimeEnd) //Проверка на ошибки при установке времени
        {
            DateTime Now = DateTime.Now;
            int result = DateTime.Compare(TimeStart, TimeEnd);
            int resultOfNow = DateTime.Compare(Now, TimeStart);
            string resultOfMistake;
            if (resultOfNow == -1)
            {
                resultOfMistake = "Нельзя устанавливать событие на прошедшее время!";
            }
            if (result == -1)
            {
                resultOfMistake = "Время окончания не может быть раньше времени начала!";
            }
            else resultOfMistake = "Событие установлено!";
            return resultOfMistake;
        }*/

        /*public void ChangeTime(EventCal oldEv, DateTime newTimeStart, DateTime newTimeEnd) //Перенос события на потом
        {
            TimeStart = newTimeStart;
            TimeEnd = newTimeEnd;
        }*/

    }
}
