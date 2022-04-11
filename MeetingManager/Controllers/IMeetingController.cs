using MeetingManager.Models;
using MeetingManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Controllers
{
    public interface IMeetingController
    {
        void AddMeeting(Meeting meeting);
        void AddPersonToMeeting(Guid meetingId, string personName);
        void DeleteMeeting(Guid id, string user);
        List<Meeting> FilterByCategory(Categories category);
        List<Meeting> FilterByDate(DateTime startDate, DateTime? endDate = null);
        List<Meeting> FilterByDescription(string filter);
        List<Meeting> FilterByNumberOfAttendees(int numberOfAttendees);
        List<Meeting> FilterByResponsiblePerson(string responsiblePerson);
        List<Meeting> FilterByType(Types type);
        Meeting GetMeeting(Guid id);
        List<Meeting> GetAllMeetings();
        void RemovePersonFromMeeting(Guid meetingId, string personName);
        void SaveAppData();
    }

}