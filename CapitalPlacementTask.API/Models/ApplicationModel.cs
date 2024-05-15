using CapitalPlacementTask.API.Dtos;

namespace CapitalPlacementTask.API.Models
{
    public class ApplicationModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IdNumber { get; set; }
        public string? Gender { get; set; }
        public string? Others { get; set; }
        public YesNoAnswer? YesNoAnswer { get; set; }
        public ParagraphAnswer? ParagraphAnswer { get; set; }
        public Dropdownanswer? Dropdownanswer { get; set; }
        public MultipleChoiceAnswer? MultipleChoiceAnswer { get; set; }
        public DateAnswer? DateAnswer { get; set; }
        public NumberAnswer? NumberAnswer { get; set; }
    }
}
