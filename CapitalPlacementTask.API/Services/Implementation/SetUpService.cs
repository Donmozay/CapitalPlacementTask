using CapitalPlacementTask.API.Data;
using CapitalPlacementTask.API.Dtos;
using CapitalPlacementTask.API.Models;
using CapitalPlacementTask.API.Repository.Interfaces;
using CapitalPlacementTask.API.Responses;
using CapitalPlacementTask.API.Services.Interfaces;

namespace CapitalPlacementTask.API.Services.Implementation
{
    public class SetUpService: ISetUpService
    {
        private readonly DataContext _context;
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ISetUpRepository _setUpRepository;
        public SetUpService(DataContext context, ISetUpRepository setUpRepository)
        {
            _context = context;
            _setUpRepository = setUpRepository;
        }

        public async Task<GenericResponse<dynamic>> AddQuestionAsync(QuestionDto dto)
        {
            try
            {
                var question = MapToEntity(dto);
                var isquestionExist = _setUpRepository.GetQuestionByName(question.Label);
                if (isquestionExist == null)
                {
                    return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = "This question already exist" };
                }
              await _setUpRepository.AddQuestion(question);
               return  new GenericResponse<dynamic> {IsSuccessful = true, ResponseCode = "00", ResponseDescription ="Success" };
            }
            catch (ArgumentException ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"{ex.Message.ToLower()}" };
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"internal error occured" };
            }
        }

        public async Task<GenericResponse<dynamic>> UpdateQuestionAsync(string id, QuestionDto dto)
        {
            try
            {
                var question = MapToEntity(dto); question.Id = id;
                var isquestionExist = _setUpRepository.GetQuestionByName(question.Label);
                if (isquestionExist == null)
                {
                    return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = "This question already exist" };
                }
                await _setUpRepository.UpdateQuestionAsync(question);
                return new GenericResponse<dynamic> { IsSuccessful = true, ResponseCode = "00", ResponseDescription = "Success" };
            }
            catch (ArgumentException ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"{ex.Message.ToLower()}" };
            }
            catch (KeyNotFoundException ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"{ex.Message.ToLower()}" };
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"internal error occured" };
            }
           
        }

        public async Task<GenericResponse<List<Question>>>GetQuestions(string type)
        {
            try
            {
                var getQuestions = await _setUpRepository.GetQuestions(type);
                if (getQuestions.Any())
                {
                    return new GenericResponse<List<Question>> { Data = getQuestions, IsSuccessful = true, ResponseCode = "00", ResponseDescription = "Successs" };
                }
                return new GenericResponse<List<Question>> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = "No record found" };
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<Question>> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"internal error occured" };
            }
           
        }
        public async Task<GenericResponse<dynamic>>SaveApplication(ApplicationDto dto)
        {
            try
            {
                if (dto.YesNoAnswer != null && !string.IsNullOrWhiteSpace(dto.YesNoAnswer.Id))
                {
                    var getYesNoQuestion = await _setUpRepository.GetQuestionById(dto.YesNoAnswer.Id);
                    if (getYesNoQuestion == null)
                    {
                        return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"Unable to find id : {dto.YesNoAnswer.Id} for YesNo Question" };
                    }
                    else
                    {
                        if (getYesNoQuestion.IsRequired && string.IsNullOrWhiteSpace(dto.YesNoAnswer.Answer))
                        {
                            return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"This question : {getYesNoQuestion.Label} is compulsory." };
                        }
                    }
                }

                if (dto.ParagraphAnswer != null && !string.IsNullOrWhiteSpace(dto.ParagraphAnswer.Id))
                {
                    var getParagraphQuestion = await _setUpRepository.GetQuestionById(dto.ParagraphAnswer.Id);
                    if (getParagraphQuestion == null)
                    {
                        return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"Unable to find id : {dto.ParagraphAnswer.Id} for Paragraph Question" };
                    }
                    else
                    {
                        if (getParagraphQuestion.IsRequired && string.IsNullOrWhiteSpace(dto.ParagraphAnswer.Answer))
                        {
                            return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"This question : {getParagraphQuestion.Label} is compulsory." };
                        }
                    }
                }

                if (dto.DateAnswer != null && !string.IsNullOrWhiteSpace(dto.DateAnswer.Id))
                {
                    var getDateQuestion = await _setUpRepository.GetQuestionById(dto.DateAnswer.Id);
                    if (getDateQuestion == null)
                    {
                        return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"Unable to find id : {dto.DateAnswer.Id} for Date Question" };
                    }
                    else
                    {
                        if (getDateQuestion.IsRequired && string.IsNullOrWhiteSpace(dto.DateAnswer.Answer))
                        {
                            return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"This question : {getDateQuestion.Label} is compulsory." };
                        }
                    }
                }

                if (dto.NumberAnswer != null && !string.IsNullOrWhiteSpace(dto.NumberAnswer.Id))
                {
                    var getNumberQuestion = await _setUpRepository.GetQuestionById(dto.NumberAnswer.Id);
                    if (getNumberQuestion == null)
                    {
                        return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"Unable to find id : {dto.NumberAnswer.Id} for Number Question" };
                    }
                    else
                    {
                        if (getNumberQuestion.IsRequired && string.IsNullOrWhiteSpace(dto.NumberAnswer.Answer))
                        {
                            return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"This question : {getNumberQuestion.Label} is compulsory." };
                        }
                    }
                }

                if (dto.Dropdownanswer != null && !string.IsNullOrWhiteSpace(dto.Dropdownanswer.Id))
                {

                    var getDropdownQuestion = await _setUpRepository.GetQuestionById(dto.Dropdownanswer.Id);
                    if (getDropdownQuestion == null)
                    {
                        return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"Unable to find id : {dto.Dropdownanswer.Id} for Dropdown Question" };
                    }
                    else
                    {
                        if (getDropdownQuestion.IsRequired && string.IsNullOrWhiteSpace(dto.Dropdownanswer.Answer))
                        {
                            return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"This question : {getDropdownQuestion.Label} is compulsory." };
                        }
                    }
                }

                if (dto.MultipleChoiceAnswer != null && !string.IsNullOrWhiteSpace(dto.MultipleChoiceAnswer.Id))
                {
                    var getMultichoiceQuestion = await _setUpRepository.GetQuestionById(dto.MultipleChoiceAnswer.Id);
                    if (getMultichoiceQuestion == null)
                    {
                        return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"Unable to find id : {dto.MultipleChoiceAnswer.Id} for Multichoice Question" };
                    }
                    else
                    {
                        if (getMultichoiceQuestion.IsRequired && dto.MultipleChoiceAnswer?.Answer.Count() == 0)
                        {
                            return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"This question : {getMultichoiceQuestion.Label} is compulsory." };
                        }
                    }
                }

                var model = new ApplicationModel
                {
                    ParagraphAnswer = dto.ParagraphAnswer,
                    MultipleChoiceAnswer = dto.MultipleChoiceAnswer,
                    NumberAnswer = dto.NumberAnswer,
                    DateAnswer = dto.DateAnswer,
                    YesNoAnswer = dto.YesNoAnswer,
                    CurrentResidence = dto.CurrentResidence,
                    Dropdownanswer = dto.Dropdownanswer,
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    Gender = dto.Gender,
                    IdNumber = dto.IdNumber,    
                    LastName = dto.LastName,
                    Nationality = dto.Nationality,
                    Others = dto.Others,
                    Phone = dto.Phone,
                };
                await _setUpRepository.SaveApplication(model);
                return new GenericResponse<dynamic> { IsSuccessful = true, ResponseCode = "00", ResponseDescription = "Success" };
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return new GenericResponse<dynamic> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = $"internal error occured" };
            }
        }
        private Question MapToEntity(QuestionDto questionDto)
        {
            return questionDto.Type switch
            {
                "Paragraph" => new ParagraphQuestion
                {
                    Label = questionDto.Label,
                    IsRequired = questionDto.IsRequired,
                    Type = questionDto.Type,
                },
                "YesNo" => new YesNoQuestion
                {
                    Label = questionDto.Label,
                    IsRequired = questionDto.IsRequired,
                    Type= questionDto.Type,
                },
                "Dropdown" => new DropdownQuestion
                {
                    Label = questionDto.Label,
                    IsRequired = questionDto.IsRequired,
                    Options = questionDto.Options,
                    Type = questionDto.Type,
                },
                "MultipleChoice" => new MultipleChoiceQuestion
                {
                    Label = questionDto.Label,
                    IsRequired = questionDto.IsRequired,
                    Options = questionDto.Options,
                    Type = questionDto.Type,
                },
                "Date" => new DateQuestion
                {
                    Label = questionDto.Label,
                    IsRequired = questionDto.IsRequired,
                    Type = questionDto.Type,
                },
                "Number" => new NumberQuestion
                {
                    Label = questionDto.Label,
                    IsRequired = questionDto.IsRequired,
                    Type = questionDto.Type,
                },
                _ => throw new ArgumentException("Invalid question type")
            };
        }
    }
}
