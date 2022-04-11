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
            break;
        default:
            Console.WriteLine("Invalid command.");
            break;
    }
}