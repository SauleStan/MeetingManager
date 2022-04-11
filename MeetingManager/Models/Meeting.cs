using MeetingManager.Utils;

namespace MeetingManager.Models
{
    public class Meeting
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public Categories Category { get; set; }
        public Types Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> People { get; set; }

        public Meeting(string Name, string ResponsiblePerson, string Description, Categories Category, Types Type, DateTime StartDate, DateTime EndDate)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
            this.ResponsiblePerson = ResponsiblePerson;
            this.Description = Description;
            this.Category = Category;
            this.Type = Type;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            People = new List<string>();
        }
        public override string ToString()
        {
            string peopleString = String.Join(", ", People);

            return
                $"ID: {Id}\n" +
                $"Name: {Name}\n" +
                $"ResponsiblePerson: {ResponsiblePerson}\n" +
                $"Description: {Description}\n" +
                $"Category: {Category}\n" +
                $"Type: {Type}\n" +
                $"StartDate: {StartDate}\n" +
                $"EndDate: {EndDate}\n" +
                $"People: {peopleString}\n";
        }
    }
}
