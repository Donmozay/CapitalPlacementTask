using CapitalPlacementTask.API.Models;

namespace CapitalPlacementTask.API.Repository.Interfaces
{
    public interface ISetUpRepository
    {
        Task<List<Question>?> GetQuestions(string type);
        Task AddQuestion(Question question);
        Task UpdateQuestionAsync(Question question);
        Task<List<Question>?> GetQuestionByName(string question);
        Task<Question?> GetQuestionById(string id);
        Task SaveApplication(ApplicationModel model);
    }
}
