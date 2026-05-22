using FluentValidation;
using HairSalon_Booking_Engine.Models.DTOs;
using HairSalon_Booking_Engine.Services;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon_Booking_Engine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<CreateCustomerRequest> _createCustomerValidator;

        public CustomerController(ICustomerService customerService, IValidator<CreateCustomerRequest> createCustomerValidator)
        {
            _customerService = customerService;
            _createCustomerValidator = createCustomerValidator;
        }

        [HttpGet(Name = "GetAllCustomers")]
        public async Task<ActionResult<ICollection<GetCustomerResponse>>> GetAll()
        {
            return Ok(await _customerService.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public async Task<ActionResult<GetCustomerResponse>> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer is null)
            {
                return NotFound($"Kunde inte hitta någon kund med ID: {id}");
            }

            return Ok(customer);
        }

        [HttpPost(Name = "CreateCustomer")]
        public async Task<ActionResult> Create(CreateCustomerRequest request)
        {
            var validationResult = await _createCustomerValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    field = error.PropertyName,
                    message = error.ErrorMessage,
                });

                return BadRequest(errors);
            }

            var result = await _customerService.CreateAsync(request);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetById), result.Data);
        }

        [HttpPut("{id}", Name = "UpdateCustomer")]
        public async Task<ActionResult> Update(int id, CreateCustomerRequest request)
        {
            var validationResult = await _createCustomerValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    field = error.PropertyName,
                    message = error.ErrorMessage,
                });

                return BadRequest(errors);
            }

            var result = await _customerService.UpdateAsync(id, request);
            if (!result.Success)
            {
                return ToErrorResponse(result);
            }

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteCustomerById")]
        public async Task<IActionResult> DeleteByID(int id)
        {
            var result = await _customerService.DeleteAsync(id);

            if (!result.Success)
            {
                return ToErrorResponse(result);
            }
            
            return NoContent();
        }

        // kanske bör flyttas om den ska användas inuti BookingService också
        private ActionResult ToErrorResponse(ServiceResult result)
        {
            return result.Status switch
            {
                ServiceResultStatus.NotFound => NotFound(result.ErrorMessage),
                ServiceResultStatus.ValidationError => BadRequest(result.ErrorMessage),
                _ => BadRequest(result.ErrorMessage)
            };
        }
    }
}
