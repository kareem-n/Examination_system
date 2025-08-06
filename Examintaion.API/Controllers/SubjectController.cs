using Examination.Application.DTOs.Subject;
using Examination.Application.Interfaces.SubjectService;
using Examination.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Template.API.Response;

namespace Examination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("allSubjects")]
        public async Task<IActionResult> GetAllSubjects([FromQuery] GetAllSubjectsParams @params)
        {

            var result = await _subjectService.GetAllSubjects(@params);
            if (result is null || !result.Items.Any())
            {
                return NotFound(ApiResponse<object>.Error(404, "No Subjects Found"));
            }
            return Ok(ApiResponse<PageModel<SubjectDto>>.Success(200, "success", result));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectDto createSubjectDto)
        {

            if (createSubjectDto == null)
            {
                return BadRequest(ApiResponse<object>.Error(400, "Invalid subject data"));
            }


            if (createSubjectDto.Easy + createSubjectDto.Hard + createSubjectDto.Normal != 100)
            {
                return BadRequest(ApiResponse<object>.Error(400, "The sum of Easy, Normal, and Hard must equal 100"));
            }


            var createdSubject = await _subjectService.CreateSubject(createSubjectDto);

            if (createdSubject == null)
            {
                return BadRequest(ApiResponse<object>.Error(400, "Failed to create subject. Please check the provided data."));
            }

            return CreatedAtAction(nameof(CreateSubject), new { id = createdSubject.Title }, ApiResponse<SubjectDto>.Success(201, "created succesfully", createdSubject));

        }

        [HttpDelete("DeleteSubject/{subjectId:guid}")]
        public async Task<IActionResult> DeleteSubject([FromRoute] Guid subjectId)
        {
            if (Guid.Empty == subjectId)
            {
                return BadRequest(ApiResponse<object>.Error(400, "Invalid Subject ID"));
            }

            var result = await _subjectService.DeleteSubject(subjectId);
            if (!result)
            {
                return NotFound(ApiResponse<object>.Error(404, "Subject not found"));
            }


            return Ok(ApiResponse<object>.Success(200, "Subject deleted successfully"));

        }

        [HttpPut("UpdateSubject/{SubjectId:guid}")]
        public async Task<IActionResult> UpdateSubject([FromRoute] Guid SubjectId, [FromBody] UpdateSubjectDto updateSubjectDto)
        {
            if (updateSubjectDto == null || SubjectId == Guid.Empty)
            {
                return BadRequest(ApiResponse<object>.Error(400, "Invalid subject data"));
            }
            var updatedSubject = await _subjectService.UpdateSubject(SubjectId, updateSubjectDto);
            if (updatedSubject == null)
            {
                return NotFound(ApiResponse<object>.Error(404, "Subject not found"));
            }
            return Ok(ApiResponse<SubjectDto>.Success(200, "Subject updated successfully", updatedSubject));
        }




    }
}
