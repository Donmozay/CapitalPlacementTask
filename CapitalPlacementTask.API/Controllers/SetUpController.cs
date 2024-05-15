using CapitalPlacementTask.API.Dtos;
using CapitalPlacementTask.API.Models;
using CapitalPlacementTask.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly ISetUpService _questionService;

        public QuestionsController(ISetUpService questionService)
        {
            _questionService = questionService;
        }
        [HttpGet]
        [Authorize(Roles = "Employer")]
        [Route("get-questions{type}")]
        public async Task<IActionResult> GetQuestions(string type)
        {
            var userInDb = await _questionService.GetQuestions(type);
            return Ok(userInDb);
        }

        [Authorize(Roles = "Employer")]
        [HttpPost]
        [Route("add-questions")]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _questionService.AddQuestionAsync(dto);
            return Ok(response);
        }

        [Authorize(Roles = "Employer")]
        [HttpPut]
        [Route("update-questions{id}")]
        public async Task<IActionResult> UpdateQuestion([Required(ErrorMessage ="id is required")] string id, [FromBody] QuestionDto question)
        {
            var response = await _questionService.UpdateQuestionAsync(id, question);
            return Ok(response);
        }

        [Authorize(Roles = "Candidate")]
        [HttpPost]
        [Route("add-application")]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _questionService.SaveApplication(dto);
            return Ok(response);
        }
    }
}
