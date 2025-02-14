using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.AddEdit;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Commands.Delete;
using HRMS.Application.Features.RecruitmentProcess.JobCategories.Queries.GetPaged;
using HRMS.Shared.Constants.Permission;
using HRMS.Shared.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Api.Controllers.V1.RecruitMentProcess
{
    
    public class JobCategoryController : BaseApiController<JobCategoryController>
    {
        /// <summary>
        /// Get All JobCategorys
        /// </summary
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.JobCategory.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string? searchString, string? orderBy)
        {
            PaginatedResult<GetPaginatedJobCategoryResponse> brands = await _mediator.Send(new GetPaginatedJobCategoryQuery(pageNumber, pageSize, searchString!, orderBy!));
            return Ok(brands);
        }

        ///// <summary>
        ///// Get All JobCategorys for AutoComplete
        ///// </summary
        /////<returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.JobCategory.View)]
        //[HttpGet("GetAllSelectView")]
        //public async Task<IActionResult> GetAllSelectView()
        //{
        //    Result<List<GetAllJobCategoryResponse>> brands = await _mediator.Send(new GetAllJobCategoryQuery());
        //    return Ok(brands);
        //}

        ///// <summary>
        ///// Get a Brand By Id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>Status 200 Ok</returns>
        //[Authorize(Policy = Permissions.JobCategory.View)]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    Result<GetJobCategoryByIdResponse> brand = await _mediator.Send(new GetJobCategoryByIdQuery() {  Id = int.Parse(id) });
        //    return Ok(brand);
        //}


        /// <summary>
        /// Create/Update a JobCategory
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.JobCategory.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditJobCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a JobCategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.JobCategory.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _mediator.Send(new DeleteJobCategoryCommand { Id = int.Parse(id) }));
        }

        ///// <summary>
        ///// Search JobCategorys and Export to Excel
        ///// </summary>
        ///// <param name="searchString"></param>
        ///// <returns></returns>
        //[Authorize(Policy = Permissions.JobCategory.Export)]
        //[HttpGet("export")]
        //public async Task<IActionResult> Export(string? searchString = "")
        //{
        //    return Ok(await _mediator.Send(new ExportJobCategorysQuery(searchString)));
        //}

        ///// <summary>
        ///// Import Brands from Excel
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[Authorize(Policy = Permissions.JobCategory.Import)]
        //[HttpPost("import")]
        //public async Task<IActionResult> Import(ImportJobCategoryCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}

    }
}
