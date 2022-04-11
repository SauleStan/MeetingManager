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

static string[] ParseInput()
{
    var line = Console.ReadLine();
    return line.Split(' ');
}

ListCommands();

IMeetingController meetingController = new MeetingController();

while (true)
{
    int choice = 0;
    string filterVar = "";
    try
    {
        var input = ParseInput();
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
            Console.WriteLine("Enter meeting id:");
            Guid.TryParse(Console.ReadLine(), out meetingId);
            Console.WriteLine("Enter the name of the person to remove from the meeting:");
            name = Console.ReadLine();
            meetingController.RemovePersonFromMeeting(meetingId, name);
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

        case 5 when filterVar.Equals("-c"):
            Console.WriteLine("Enter category to filter by (CodeMonkey / Hub / Short / TeamBuilding):");
            try
            {
                Categories categoryFilter = Enum.Parse<Categories>(Console.ReadLine());
                DisplayList(meetingController.FilterByCategory(categoryFilter));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid category, please select one from the brackets.");
            }
            break;

        case 5 when filterVar.Equals("-t"):
            Console.WriteLine("Enter category to filter by (Live / InPerson):");
            try
            {
                Types typeFilter = Enum.Parse<Types>(Console.ReadLine());
                DisplayList(meetingController.FilterByType(typeFilter));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid type, please select one from the brackets.");
            }
            break;

        case 5 when filterVar.Equals("-date"):
            DateTime dateFilter1 = new();
            DateTime? dateFilter2 = null;

            Console.WriteLine("Enter date to filter by (format: yyyy-mm-dd or yyyy-mm-dd yyyy-mm-dd for date range)");
            var input = ParseInput();
            try
            {
                if (input.Length > 1)
                {
                    dateFilter1 = DateTime.Parse(input[0]);
                    dateFilter2 = DateTime.Parse(input[1]);
                }
                else
                {
                    DateTime.TryParse(input[0], out dateFilter1);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dateFilter1 != DateTime.MinValue)
                {
                    var filteredList = meetingController.FilterByDate(dateFilter1, dateFilter2);
                    if (filteredList.Count > 0)
                    {
                        DisplayList(filteredList);
                    }
                    else
                    {
                        Console.WriteLine("No meetings found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            break;

        case 5 when filterVar.Equals("-att"):
            Console.WriteLine("Enter the number of attendees to filter by (will return meetings with that amount or more people):");
            try
            {
                int peopleFilter = Int32.Parse(Console.ReadLine());
                DisplayList(meetingController.FilterByNumberOfAttendees(peopleFilter));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            break;

        default:
            Console.WriteLine("Invalid command.");
            break;
    }

    // Store app data in json file
    meetingController.SaveAppData();
}
