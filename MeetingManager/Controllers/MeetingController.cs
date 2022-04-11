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
        public MeetingController()
        {
            _meetingsList = new List<Meeting>();
        }
        public void AddMeeting(Meeting meeting)
        {
            _meetingsList.Add(meeting);
        }

        public void AddPersonToMeeting(Guid meetingId, string personName)
        {
            throw new NotImplementedException();
        }

        public void DeleteMeeting(Guid id, string username)
        {
            throw new NotImplementedException();
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

        public void SaveAppData()
        {
            throw new NotImplementedException();
        }
    }
}
