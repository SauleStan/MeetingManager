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
    Console.WriteLine("5 - List meetings [-arg]\n" +
        "\t-a List all\n" +
        "\t-d Filter by description\n" +
        "\t-rp Filter by responsible person\n" +
        "\t-c Filter by category\n" +
        "\t-t Filter by type\n" +
        "\t-date Filter by date\n" +
        "\t-att Filter by attendees");
}

static void DisplayList(List<Meeting> meetings)
{
    foreach (var meeting in meetings)
    {
        Console.WriteLine(meeting);
    }
}

ListCommands();

IMeetingController meetingController = new MeetingController();

while (true)
{
    int choice = 0;
    string filterVar = "";
    try
    {
        var line = Console.ReadLine();
        var input = line.Split(' ');
        choice = int.Parse(input[0]);
        if (input.Length > 1)
        {
            filterVar = input[1];
        }
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
            Console.WriteLine("Enter start date (format: yyyy-mm-dd hh:mm):");
            DateTime.TryParse(Console.ReadLine(), out DateTime startDate);
            Console.WriteLine("Enter end date (format: yyyy-mm-dd hh:mm):");
            DateTime.TryParse(Console.ReadLine(), out DateTime endDate);

            Meeting newMeeting = new(meetingName, responsiblePerson, description, category, type, startDate, endDate);

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
        case 5 when filterVar.Equals("-a"):
            var meetingList = meetingController.GetAllMeetings();
            if(meetingList.Count == 0)
            {
                Console.WriteLine("There are no meetings.");
            }
            else
            {
                DisplayList(meetingList);                
            }
            
            break;
        case 5 when filterVar.Equals("-d"):
            Console.WriteLine("Enter word or sentence to filter descriptions by:");
            string descFilter = Console.ReadLine();
            DisplayList(meetingController.FilterByDescription(descFilter));
            break;
        case 5 when filterVar.Equals("-rp"):
            Console.WriteLine("Enter responsible person name:");
            rp = Console.ReadLine();
            DisplayList(meetingController.FilterByResponsiblePerson(rp));
            break;
        default:
            Console.WriteLine("Invalid command.");
            break;
    }

    // Store app data in json file
    meetingController.SaveAppData();
}
