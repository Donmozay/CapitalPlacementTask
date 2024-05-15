using CapitalPlacementTask.API.Models;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask.API.Dtos
{
    public class ApplicationDto
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = " LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required"), RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
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

    public class YesNoAnswer
    {
        public string? Id { get; set; }
        public string? Answer { get; set; }
    }
    public class ParagraphAnswer
    {
        public string? Id { get; set; }
        public string? Answer { get; set; }
    }
    public class MultipleChoiceAnswer
    {
        public string? Id { get; set; }
        public List<string>? Answer { get; set; }
    }
    public class Dropdownanswer
    {
        public string? Id { get; set; }
        public string? Answer { get; set; }
    }
    public class DateAnswer
    {
        public string? Id { get; set; }
        [RegularExpression(@"^(?:20\d{2}|19\d{2})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Invalid date format")]
        public string? Answer { get; set; }
    }
    public class NumberAnswer
    {
        public string? Id { get; set; }
        public string? Answer { get; set; }
    }
}
