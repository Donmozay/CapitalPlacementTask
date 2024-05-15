namespace CapitalPlacementTask.API.Models
{
    public abstract class Question
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Type { get; set; }
        public string Label { get; set; }
        public bool IsRequired { get; set; } //indicates whether answering a particular question is mandatory or optional in the application form.
    }

    public class ParagraphQuestion : Question
    {
    }

    public class YesNoQuestion : Question
    {
    }

    public class DropdownQuestion : Question
    {
        public List<string> Options { get; set; }
    }

    public class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; set; }
    }

    public class DateQuestion : Question
    {
    }

    public class NumberQuestion : Question
    {
    }

}
