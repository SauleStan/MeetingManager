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
        [TestMethod]
        public void AddPersonToMeetingTest()
        {
            // First meeting
            DateTime startDate = new DateTime(2022, 1, 1, 10, 0, 0);
            DateTime endDate = new DateTime(2022, 1, 1, 13, 0, 0);

            Meeting newMeeting1 = new Meeting("Meeting1", "Andy",
                "Andy's meeting", Categories.Hub, Types.Live, startDate, endDate);
            IMeetingController meetingController = new MeetingController();

            // Act
            meetingController.AddMeeting(newMeeting1);
            string personToAdd = "Charlie";
            meetingController.AddPersonToMeeting(newMeeting1.Id, personToAdd);

            // Assert
            Assert.IsTrue(newMeeting1.People.Contains(personToAdd));
        }
        [TestMethod]
        public void AddPersonToMeetingTwiceTest()
        {
            // First meeting
            DateTime startDate = new DateTime(2022, 1, 1, 10, 0, 0);
            DateTime endDate = new DateTime(2022, 1, 1, 13, 0, 0);

            Meeting newMeeting1 = new Meeting("Meeting1", "Andy", "Andy's meeting",
                Categories.Hub, Types.Live, startDate, endDate);
            
            IMeetingController meetingController = new MeetingController();

            // Act
            meetingController.AddMeeting(newMeeting1);
            string personToAdd = "Charlie";
            meetingController.AddPersonToMeeting(newMeeting1.Id, personToAdd);
            meetingController.AddPersonToMeeting(newMeeting1.Id, personToAdd);

            // Assert
            var result = newMeeting1.People.FindAll(x => x.Equals(personToAdd)).Count;
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void AddPersonToMeetingOverlapingTimeRangeTest()
        {
            // First meeting
            DateTime startDate = new DateTime(2022, 1, 1, 10, 0, 0);
            DateTime endDate = new DateTime(2022, 1, 1, 13, 0, 0);

            // Second meeting
            DateTime startDate2 = new DateTime(2022, 1, 1, 11, 0, 0);
            DateTime endDate2 = new DateTime(2022, 1, 1, 14, 0, 0);

            Meeting newMeeting1 = new Meeting("Meeting1", "Andy", "Andy's meeting",
                Categories.Hub, Types.Live, startDate, endDate);
            Meeting newMeeting2 = new Meeting("Meeting2", "Alex", "Alex's meeting",
                Categories.Hub, Types.InPerson, startDate2, endDate2);
            IMeetingController meetingController = new MeetingController();

            // Act
            meetingController.AddMeeting(newMeeting1);
            meetingController.AddMeeting(newMeeting2);
            string personToAdd = "Charlie";
            meetingController.AddPersonToMeeting(newMeeting1.Id, personToAdd);
            meetingController.AddPersonToMeeting(newMeeting2.Id, personToAdd);

            // Assert
            Assert.IsFalse(newMeeting2.People.Contains(personToAdd));
        }
        [TestMethod]
        public void AddPersonToMeetingSameTimeTest()
        {
            // First meeting
            DateTime startDate = new DateTime(2022, 1, 1, 10, 0, 0);
            DateTime endDate = new DateTime(2022, 1, 1, 13, 0, 0);

            // Second meeting
            DateTime startDate2 = new DateTime(2022, 1, 1, 10, 0, 0);
            DateTime endDate2 = new DateTime(2022, 1, 1, 13, 0, 0);

            Meeting newMeeting1 = new Meeting("Meeting1", "Andy", "Andy's meeting",
                Categories.Hub, Types.Live, startDate, endDate);
            Meeting newMeeting2 = new Meeting("Meeting2", "Alex", "Alex's meeting",
                Categories.Hub, Types.InPerson, startDate2, endDate2);
            IMeetingController meetingController = new MeetingController();

            // Act
            meetingController.AddMeeting(newMeeting1);
            meetingController.AddMeeting(newMeeting2);
            string personToAdd = "Charlie";
            meetingController.AddPersonToMeeting(newMeeting1.Id, personToAdd);
            meetingController.AddPersonToMeeting(newMeeting2.Id, personToAdd);

            // Assert
            Assert.IsFalse(newMeeting2.People.Contains(personToAdd));
        }
        [TestMethod]
        public void RemovePersonFromMeetingTest()
        {
            // First meeting
            DateTime startDate = new DateTime(2022, 1, 1, 10, 0, 0);
            DateTime endDate = new DateTime(2022, 1, 1, 13, 0, 0);

            Meeting newMeeting1 = new Meeting("Meeting1", "Andy",
                "Andy's meeting", Categories.Hub, Types.Live, startDate, endDate);
            IMeetingController meetingController = new MeetingController();

            // Act
            meetingController.AddMeeting(newMeeting1);
            string person = "Charlie";
            meetingController.AddPersonToMeeting(newMeeting1.Id, person);
            meetingController.RemovePersonFromMeeting(newMeeting1.Id, person);
            // Assert
            Assert.IsFalse(newMeeting1.People.Contains(person));
        }
        [TestMethod]
        public void RemovePersonFromMeetingWrongNameTest()
        {
            // First meeting
            DateTime startDate = new DateTime(2022, 1, 1, 10, 0, 0);
            DateTime endDate = new DateTime(2022, 1, 1, 13, 0, 0);

            Meeting newMeeting1 = new Meeting("Meeting1", "Andy",
                "Andy's meeting", Categories.Hub, Types.Live, startDate, endDate);
            IMeetingController meetingController = new MeetingController();

            // Act
            meetingController.AddMeeting(newMeeting1);
            string person = "Charlie";
            meetingController.AddPersonToMeeting(newMeeting1.Id, person);
            meetingController.RemovePersonFromMeeting(newMeeting1.Id, "jsdksghd");
            // Assert
            Assert.IsTrue(newMeeting1.People.Contains(person));
        }
    }
}