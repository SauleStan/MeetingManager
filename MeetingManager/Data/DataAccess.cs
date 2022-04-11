using MeetingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MeetingManager.Data
{
    public class DataAccess
    {
        private string _fileName = "MeetingData.json";
        public List<Meeting> GetData<Meeting>()
        {
            List<Meeting> meetingList = new List<Meeting>();
            try
            {
                if (File.Exists(_fileName))
                {
                    string jsonString = File.ReadAllText(_fileName);
                    meetingList = JsonSerializer.Deserialize<List<Meeting>>(jsonString);
                    return meetingList;
                }
                else
                {
                    Console.WriteLine($"{_fileName} does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return meetingList;
        }
        public void SaveData(List<Meeting> meetingList)
        {
            var json = JsonSerializer.Serialize(meetingList);
            File.WriteAllText(_fileName, json);
        }
    }
}
