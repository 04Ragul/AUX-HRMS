using HRMS.Application.Features.RecruitmentProcess.JobLocations.Commands.AddEdit;
using HRMS.Application.Features.RecruitmentProcess.JobLocations.Commands.Delete;
using HRMS.Application.Features.RecruitmentProcess.JobLocations.Queries.GetPaged;
using HRMS.Shared.Constants.Permission;
using HRMS.Shared.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Api.Controllers.V1.RecruitMentProcess
{
    public class JobLocationController : BaseApiController<JobLocationController>
    {
        /// <summary>
        /// Get All JobLocations
        /// </summary
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.JobLocation.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string? searchString, string? orderBy)
        {
            PaginatedResult<GetPagedJobLocationResponse> brands = await _mediator.Send(new GetPagedJobLocationQuery(pageNumber, pageSize, searchString!, orderBy!));
            return Ok(brands);
        }

        ///// <summary>
        ///// Get All JobLocations for AutoComplete
        ///// </summary
        /////<returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.JobLocation.View)]
        //[HttpGet("GetAllSelectView")]
        //public async Task<IActionResult> GetAllSelectView()
        //{
        //    Result<List<GetAllJobLocationResponse>> brands = await _mediator.Send(new GetAllJobLocationQuery());
        //    return Ok(brands);
        //}

        ///// <summary>
        ///// Get a Brand By Id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>Status 200 Ok</returns>
        //[Authorize(Policy = Permissions.JobLocation.View)]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    Result<GetJobLocationByIdResponse> brand = await _mediator.Send(new GetJobLocationByIdQuery() {  Id = int.Parse(id) });
        //    return Ok(brand);
        //}


        /// <summary>
        /// Create/Update a JobLocation
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.JobLocation.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditJobLocationCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a JobLocation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.JobLocation.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _mediator.Send(new DeleteJobLocationCommand { Id = int.Parse(id) }));
        }

        ///// <summary>
        ///// Search JobLocations and Export to Excel
        ///// </summary>
        ///// <param name="searchString"></param>
        ///// <returns></returns>
        //[Authorize(Policy = Permissions.JobLocation.Export)]
        //[HttpGet("export")]
        //public async Task<IActionResult> Export(string? searchString = "")
        //{
        //    return Ok(await _mediator.Send(new ExportJobLocationsQuery(searchString)));
        //}

        ///// <summary>
        ///// Import Brands from Excel
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[Authorize(Policy = Permissions.JobLocation.Import)]
        //[HttpPost("import")]
        //public async Task<IActionResult> Import(ImportJobLocationCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}

    }
}
