using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Forms;

namespace Happy_Tree
{
    public static class Data
    {
        static string record = "0";
        static string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.txt");
        public static void read()
        {
            if (File.Exists(_fileName))
            {
                record = File.ReadAllText(_fileName);
            }
        }
        public static int getRecord()
        {
            int i = 0;
            try
            {
                i = int.Parse(record);
            }
            catch
            { }
            return i;
        }
        public static string getRecordString()
        {
            return record;
        }
        public static void setRecord(int n)
        {
            record = n.ToString();
        }
        public static void save()
        {
            int s = GamePage.score;
            if (s > getRecord()) record = s.ToString();
            File.WriteAllText(_fileName, record);
        }
    }
}
