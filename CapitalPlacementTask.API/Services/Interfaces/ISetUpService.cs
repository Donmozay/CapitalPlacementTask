using CapitalPlacementTask.API.Dtos;
using CapitalPlacementTask.API.Models;
using CapitalPlacementTask.API.Responses;

namespace CapitalPlacementTask.API.Services.Interfaces
{
    public interface ISetUpService
    {
        Task<GenericResponse<dynamic>> AddQuestionAsync(QuestionDto dto);
        Task<GenericResponse<List<Question>>> GetQuestions(string type);
        Task<GenericResponse<dynamic>> UpdateQuestionAsync(string id, QuestionDto dto);
        Task<GenericResponse<dynamic>> SaveApplication(ApplicationDto dto);
    }
}
