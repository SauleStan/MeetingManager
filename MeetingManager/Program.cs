using MeetingManager.Controllers;

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
            break;
        case 2:
            break;
        case 3:
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
}