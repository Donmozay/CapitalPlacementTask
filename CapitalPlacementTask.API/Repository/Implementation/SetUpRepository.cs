using CapitalPlacementTask.API.Data;
using CapitalPlacementTask.API.Models;
using CapitalPlacementTask.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask.API.Repository.Implementation
{
    public class SetUpRepository : ISetUpRepository
    {
        private readonly DataContext _context;
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public SetUpRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Question>?> GetQuestions(string type)
        {
            try
            {
                return await _context.Questions.Where(x => x.Type.ToUpper() == type.ToUpper()).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return null;
            }
        }

        public async Task<List<Question>?> GetQuestionByName(string question)
        {
            try
            {
                return await _context.Questions.Where(x => x.Label.ToUpper() == question.ToUpper()).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return null;
            }
        }

        public async Task<Question?> GetQuestionById(string id)
        {
            try
            {
                return await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return null;
            }
        }

        public async Task AddQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            var existingQuestion = await _context.Questions.FirstOrDefaultAsync(x => x.Id == question.Id);
            if (existingQuestion == null) throw new KeyNotFoundException("Question not found");

            _context.Entry(existingQuestion).CurrentValues.SetValues(question);
            await _context.SaveChangesAsync();
        }

        public async Task SaveApplication(ApplicationModel model)
        {
            _context.Applications.Add(model);
            await _context.SaveChangesAsync();
        }
    }
}
