using MeetingManager.Controllers;
using MeetingManager.Models;
using MeetingManager.Utils;

static void ListCommands()
{
    Console.WriteLine("Select command:");
    Console.WriteLine("1 - Create meeting");
    Console.WriteLine("2 - Delete meeting");
    Console.WriteLine("3 - Add person to a meeting");
    Console.WriteLine("4 - Remove person from a meeting");
    Console.WriteLine("5 - List all meetings");
}

ListCommands();

IMeetingController meetingController = new MeetingController();

while (true)
{
    int choice = 0;
    try
    {
        choice = Int32.Parse(Console.ReadLine());
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    switch (choice)
    {
        case 1:
            Console.WriteLine("Meeting name: ");
            string meetingName = Console.ReadLine();
            Console.WriteLine("Responsible person: ");
            string responsiblePerson = Console.ReadLine();
            Console.WriteLine("Description: ");
            string description = Console.ReadLine();
            Console.WriteLine("Category(CodeMonkey / Hub / Short / TeamBuilding): ");
            Enum.TryParse(Console.ReadLine(), out Categories category);
            Console.WriteLine("Type(Live / InPerson): ");
            Enum.TryParse(Console.ReadLine(), out Types type);

            Meeting newMeeting = new(meetingName, responsiblePerson, description, category, type, DateTime.UtcNow, DateTime.UtcNow);

            meetingController.AddMeeting(newMeeting);
            break;
        case 2:
            Console.WriteLine("Enter your name:");
            string rp = Console.ReadLine();

            Console.WriteLine("Enter meeting id: ");
            Guid meetingId;
            Guid.TryParse(Console.ReadLine(), out meetingId);

            meetingController.DeleteMeeting(meetingId, rp);
            break;
        case 3:
            Console.WriteLine("Enter meeting id:");
            Guid.TryParse(Console.ReadLine(), out meetingId);
            Console.WriteLine("Enter the name of the person to add to the meeting:");
            var name = Console.ReadLine();

            meetingController.AddPersonToMeeting(meetingId, name);
            break;
        case 4:
            break;
        case 5:
            var meetingList = meetingController.GetAllMeetings();
            if(meetingList.Count == 0)
            {
                Console.WriteLine("There are no meetings.");
            }
            else
            {
                foreach (var meeting in meetingList)
                {
                    Console.WriteLine(meeting);
                }
            }
            
            break;
        default:
            Console.WriteLine("Invalid command.");
            break;
    }

    // Store app data in json file
    meetingController.SaveAppData();
}
