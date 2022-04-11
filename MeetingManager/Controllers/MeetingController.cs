using MeetingManager.Data;
using MeetingManager.Models;
using MeetingManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Controllers
{
    public class MeetingController : IMeetingController
    {
        private List<Meeting> _meetingsList;
        private DataAccess _dataAccess;
        public MeetingController()
        {
            _dataAccess = new DataAccess();
            _meetingsList = _dataAccess.GetData<Meeting>();
        }
        /// <summary>
        /// This method adds given meeting to the meeting list
        /// </summary>
        /// <param name="meeting">Meeting to be added to the meetings list</param>
        public void AddMeeting(Meeting meeting)
        {
            _meetingsList.Add(meeting);
        }

        public void AddPersonToMeeting(Guid meetingId, string personName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// This method deletes a meeting from meetings list
        /// </summary>
        /// <param name="id">Id of the method that you want to delete</param>
        /// <param name="user">Name of the person who is performing the command</param>
        public void DeleteMeeting(Guid id, string user)
        {
            Meeting meetingToRemove;
            try
            {
                meetingToRemove = _meetingsList.Single(x => x.Id == id);
                if (meetingToRemove.ResponsiblePerson.Equals(user))
                {
                    _meetingsList.Remove(meetingToRemove);
                    Console.WriteLine("Meeting has been deleted.");
                }
                else
                {
                    Console.WriteLine("{0} is not the responsible person for this meeting.", user);
                    Console.WriteLine("Only the responsible person for the meeting can remove the meeting.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Meeting with ID:{0} does not exist.", id);
            }
        }

        public List<Meeting> FilterByCategory(Categories category)
        {
            throw new NotImplementedException();
        }

        public List<Meeting> FilterByDate(DateTime startDate, DateTime? endDate = null)
        {
            throw new NotImplementedException();
        }

        public List<Meeting> FilterByDescription(string filter)
        {
            throw new NotImplementedException();
        }

        public List<Meeting> FilterByNumberOfAttendees(int numberOfAttendees)
        {
            throw new NotImplementedException();
        }

        public List<Meeting> FilterByResponsiblePerson(string responsiblePerson)
        {
            throw new NotImplementedException();
        }

        public List<Meeting> FilterByType(Types type)
        {
            throw new NotImplementedException();
        }

        public List<Meeting> GetAllMeetings()
        {
            return _meetingsList;
        }

        public Meeting GetMeeting(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemovePersonFromMeeting(Guid meetingId, string personName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// This method stores meetings list in a json file
        /// </summary>
        public void SaveAppData()
        {
            _dataAccess.SaveData(_meetingsList);
        }
    }
}
