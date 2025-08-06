using Examination.Application.DTOs.Subject;
using Examination.Domain.Common;
using Examination.Domain.Models;
using Examination.Infrastructure.Specifications;

namespace Examination.Application.Specifications
{
    public class SubjectSpecification : Spicification<Subject>
    {

        public SubjectSpecification(GetAllSubjectsParams @params)
        {

            if (!string.IsNullOrEmpty(@params.SearchTerm))
            {
                AddCriteria(s => s.Title.Contains(@params.SearchTerm));
            }



            if (@params.PageIndex > 0 && @params.PageSize > 0)
                AddPagging(@params.PageSize, @params.PageIndex);



            if (!string.IsNullOrEmpty(@params.sortby))
            {
                var sortOption = @params.sortby.ToLower() switch
                {
                    "title" => new SortOption<Subject>(sub => sub.Title),
                    "description" => new SortOption<Subject>(sub => sub.Description),
                    "numberofquestions" => new SortOption<Subject>(sub => sub.SubjectConfiguration.NumberOsQuestions),
                    "createdat" => new SortOption<Subject>(sub => sub.CreatedAt),
                    _ => new SortOption<Subject>(sub => sub.Id)
                };
                AddSort(sortOption);
            }

            AddIncludes([s => s.SubjectConfiguration]);


            AddProjection(sub => new SubjectDto
            {
                Id = sub.Id.ToString(),
                Description = sub.Description,
                Title = sub.Title,
                TotalQuestions = sub.SubjectConfiguration.NumberOsQuestions,
                Easy = sub.SubjectConfiguration.Easy,
                Hard = sub.SubjectConfiguration.Hard,
                Meduim = sub.SubjectConfiguration.Miduiem
            });

        }

    }
}
