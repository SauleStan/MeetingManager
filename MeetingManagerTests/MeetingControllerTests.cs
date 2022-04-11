using MeetingManager.Controllers;
using MeetingManager.Models;
using MeetingManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MeetingManagerTests
{
    [TestClass]
    public class MeetingControllerTests
    {
        [TestMethod]
        public void AddMeetingTest()
        {
            // Arange
            DateTime startDate = new DateTime(2022, 1, 1, 11, 0, 0);
            DateTime endDate = new DateTime(2022, 1, 1, 13, 0, 0);
            Meeting meeting1 = new Meeting("Meeting1", "Andy", "Andy's meeting", Categories.Hub, Types.InPerson, startDate, endDate);

            IMeetingController meetingController = new MeetingController();

            // Act
            meetingController.AddMeeting(meeting1);

            // Assert
            var result = meetingController.GetAllMeetings();
            Assert.IsTrue(result.Contains(meeting1));
        }
        [TestMethod]
        public void GetAllMeetingsTest()
        {
            // Arange
            DateTime startDate = new DateTime(2022, 1, 1, 11, 0, 0);
            DateTime endDate = new DateTime(2022, 1, 1, 13, 0, 0);
            Meeting meeting1 = new Meeting("Meeting1", "Andy", "Andy's meeting", Categories.Hub, Types.InPerson, startDate, endDate);
            Meeting meeting2 = new Meeting("Meeting2", "Alex", "Alex's team building wub wub", Categories.TeamBuilding, Types.InPerson, startDate, endDate);
            
            IMeetingController meetingController = new MeetingController();
            
            // Act
            meetingController.AddMeeting(meeting1);
            meetingController.AddMeeting(meeting2);

            // Assert
            int result = meetingController.GetAllMeetings().Count;
            Assert.AreEqual(result, 2);
        }
        [TestMethod]
        public void DeleteMeetingTest()
        {
            // Arange
            DateTime startDate = new DateTime(2022, 1, 1, 11, 0, 0);
            DateTime endDate = new DateTime(2022, 1, 1, 13, 0, 0);
            string responsiblePerson = "Andy";
            Meeting meeting1 = new Meeting("Meeting1", responsiblePerson, "Andy's meeting", Categories.Hub, Types.InPerson, startDate, endDate);

            IMeetingController meetingController = new MeetingController();

            // Act
            meetingController.AddMeeting(meeting1);
            meetingController.DeleteMeeting(meeting1.Id, responsiblePerson);

            // Assert
            var result = meetingController.GetAllMeetings();
            Assert.IsFalse(result.Contains(meeting1));
        }
        [TestMethod]
        public void DeleteMeetingWrongResponsiblePersonTest()
        {
            // Arange
            DateTime startDate = new DateTime(2022, 1, 1, 11, 0, 0);
            DateTime endDate = new DateTime(2022, 1, 1, 13, 0, 0);
            string responsiblePerson = "Andy";
            Meeting meeting1 = new Meeting("Meeting1", responsiblePerson, "Andy's meeting", Categories.Hub, Types.InPerson, startDate, endDate);

            IMeetingController meetingController = new MeetingController();

            // Act
            meetingController.AddMeeting(meeting1);
            meetingController.DeleteMeeting(meeting1.Id, "Alex");
            
            // Assert
            var result = meetingController.GetAllMeetings();
            Assert.IsTrue(result.Contains(meeting1));
        }
    }
}