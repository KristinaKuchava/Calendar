using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WindowsFormsApplication5
{
    [Serializable]
    public class Control
    {
        public List<EventCal> MyList = new List<EventCal>();
        public void Load(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    MyList = (List<EventCal>)bf.Deserialize(fs);
                }
            }
            catch (FileNotFoundException) { return; }
        }

        public void Save(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, MyList);
                }
            }
            catch (ArgumentException) { return; }
        }

        
    }
}
