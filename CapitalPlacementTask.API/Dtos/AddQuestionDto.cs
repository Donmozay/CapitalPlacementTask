using CapitalPlacementTask.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CapitalPlacementTask.API.Dtos
{

    public class QuestionDto
    {
        [Required(ErrorMessage = "Label is required")]
        public string Label { get; set; }
        [Required(ErrorMessage = "IsRequired is required")]
        public bool IsRequired { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
        public List<string>? Options { get; set; }
    }

    public class ParagraphQuestionDto : QuestionDto
    {
        public ParagraphQuestionDto()
        {
            Type = "Paragraph";
        }
    }

    public class YesNoQuestionDto : QuestionDto
    {
        public YesNoQuestionDto()
        {
            Type = "YesNo";
        }
    }

    public class DropdownQuestionDto : QuestionDto
    {
        public DropdownQuestionDto()
        {
            Type = "Dropdown";
        }
        public List<string> Options { get; set; }
    }

    public class MultipleChoiceQuestionDto : QuestionDto
    {
        [JsonPropertyName("options")]
        public List<string> Options { get; set; }
        public MultipleChoiceQuestionDto()
        {
            Type = "MultipleChoice";
        }
    }

    public class DateQuestionDto : QuestionDto
    {
        public DateQuestionDto()
        {
            Type = "Date";
        }
    }

    public class NumberQuestionDto : QuestionDto
    {
        public NumberQuestionDto()
        {
            Type = "Number";
        }
    }

}
