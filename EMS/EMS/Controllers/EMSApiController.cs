using EMS.Services;
using EMS.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EMSApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EMSApiController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// This api will return all the available Employees from the datatabase
        /// </summary>
        /// <returns></returns>
        [Route("~/api/GetEmployees")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetAllResponse response = new GetAllResponse();
            try
            {
                response.ResponseData = await _employeeService.GetAll();
                response.ResponseCodes = ResponseCodes.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCodes = ResponseCodes.InternalServerError;
                response.ResponseMessage = ex.Message;
                return BadRequest(response);
            }
        }

        /// <summary>
        /// This api will return the Employee details by requested employee id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/api/GetEmployeeById/{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            GetSingleResponse response = new GetSingleResponse();
            try
            {
                response.ResponseData = await _employeeService.GetById(id);
                response.ResponseCodes = ResponseCodes.OK;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCodes = ResponseCodes.InternalServerError;
                response.ResponseMessage = ex.Message;
                return BadRequest(response);
            }
        }

        /// <summary>
        /// This api will create a new Employee in the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("~/api/CreateEmployee")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EMSRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var res = await _employeeService.Create(request);
                response.ResponseCodes = ResponseCodes.OK;
                response.ResponseMessage = "Employee created successfully";

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCodes = ResponseCodes.InternalServerError;
                response.ResponseMessage = ex.Message;
                return BadRequest(response);
            }
        }

        /// <summary>
        /// This api will update the requested Employee details in the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("~/api/UpdateEmployee/{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] EMSRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var res = await _employeeService.Update(id, request);
                response.ResponseCodes = ResponseCodes.OK;
                response.ResponseMessage = "Employee details updated successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCodes = ResponseCodes.InternalServerError;
                response.ResponseMessage = ex.Message;
                return BadRequest(response);
            }
        }

        /// <summary>
        /// This api will delete the requetsed Employee from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("~/api/DeleteEmployee/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var res = await _employeeService.Delete(id);
                response.ResponseCodes = ResponseCodes.OK;
                response.ResponseMessage = "Employee deleted successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCodes = ResponseCodes.InternalServerError;
                response.ResponseMessage = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
