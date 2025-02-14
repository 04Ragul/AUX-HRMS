using HRMS.Application.Features.Departments.Commands.AddEdit;
using HRMS.Application.Features.Departments.Commands.Delete;
using HRMS.Application.Features.Departments.Queries.GetById;
using HRMS.Application.Features.Departments.Queries.GetPaginated;
using HRMS.Shared.Constants.Permission;
using HRMS.Shared.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Api.Controllers.V1
{
    public class DepartmentController : BaseApiController<DepartmentController>
    {
        /// <summary>
        /// Get All Departments
        /// </summary
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Department.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string? searchString, string? orderBy)
        {
            PaginatedResult<GetDepartmentPaginatedResponse> brands = await _mediator.Send(new GetDepartmentPaginatedQuery(pageNumber, pageSize, searchString!, orderBy!));
            return Ok(brands);
        }

        ///// <summary>
        ///// Get All Departments for AutoComplete
        ///// </summary
        /////<returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Department.View)]
        //[HttpGet("GetAllSelectView")]
        //public async Task<IActionResult> GetAllSelectView()
        //{
        //    Result<List<GetAllDepartmentResponse>> brands = await _mediator.Send(new GetAllDepartmentQuery());
        //    return Ok(brands);
        //}

        ///// <summary>
        ///// Get a Brand By Id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>Status 200 Ok</returns>
        //[Authorize(Policy = Permissions.Department.View)]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    Result<GetDepartmentByIdResponse> brand = await _mediator.Send(new GetDepartmentByIdQuery() {  Id = int.Parse(id) });
        //    return Ok(brand);
        //}


        /// <summary>
        /// Create/Update a Department
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Department.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditDeparmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Department
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Department.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _mediator.Send(new DeleteDepartmentCommand { Id = int.Parse(id) }));
        }

        ///// <summary>
        ///// Search Departments and Export to Excel
        ///// </summary>
        ///// <param name="searchString"></param>
        ///// <returns></returns>
        //[Authorize(Policy = Permissions.Department.Export)]
        //[HttpGet("export")]
        //public async Task<IActionResult> Export(string? searchString = "")
        //{
        //    return Ok(await _mediator.Send(new ExportDepartmentsQuery(searchString)));
        //}

        ///// <summary>
        ///// Import Brands from Excel
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[Authorize(Policy = Permissions.Department.Import)]
        //[HttpPost("import")]
        //public async Task<IActionResult> Import(ImportDepartmentCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}


    }
    public class DepartmentController : BaseApiController<DepartmentController>
    {
        /// <summary>
        /// Get All Departments
        /// </summary
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Department.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string? searchString, string? orderBy)
        {
            PaginatedResult<GetDepartmentPaginatedResponse> brands = await _mediator.Send(new GetDepartmentPaginatedQuery(pageNumber, pageSize, searchString!, orderBy!));
            return Ok(brands);
        }

        ///// <summary>
        ///// Get All Departments for AutoComplete
        ///// </summary
        /////<returns>Status 200 OK</returns>
        //[Authorize(Policy = Permissions.Department.View)]
        //[HttpGet("GetAllSelectView")]
        //public async Task<IActionResult> GetAllSelectView()
        //{
        //    Result<List<GetAllDepartmentResponse>> brands = await _mediator.Send(new GetAllDepartmentQuery());
        //    return Ok(brands);
        //}

        ///// <summary>
        ///// Get a Brand By Id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>Status 200 Ok</returns>
        //[Authorize(Policy = Permissions.Department.View)]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    Result<GetDepartmentByIdResponse> brand = await _mediator.Send(new GetDepartmentByIdQuery() {  Id = int.Parse(id) });
        //    return Ok(brand);
        //}


        /// <summary>
        /// Create/Update a Department
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Department.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditDeparmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Department
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Department.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _mediator.Send(new DeleteDepartmentCommand { Id = int.Parse(id) }));
        }

        ///// <summary>
        ///// Search Departments and Export to Excel
        ///// </summary>
        ///// <param name="searchString"></param>
        ///// <returns></returns>
        //[Authorize(Policy = Permissions.Department.Export)]
        //[HttpGet("export")]
        //public async Task<IActionResult> Export(string? searchString = "")
        //{
        //    return Ok(await _mediator.Send(new ExportDepartmentsQuery(searchString)));
        //}

        ///// <summary>
        ///// Import Brands from Excel
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[Authorize(Policy = Permissions.Department.Import)]
        //[HttpPost("import")]
        //public async Task<IActionResult> Import(ImportDepartmentCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}
    }
