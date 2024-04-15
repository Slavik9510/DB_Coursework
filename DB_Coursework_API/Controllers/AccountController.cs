using AutoMapper;
using DB_Coursework_API.Interfaces;
using DB_Coursework_API.Models.Domain;
using DB_Coursework_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DB_Coursework_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ICustomersRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(ICustomersRepository customerRepository, IMapper mapper, ITokenService tokenService)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCustomerDto registerCustomerDto)
        {
            if (await _customerRepository.CustomerExistsAsync(registerCustomerDto.Email, registerCustomerDto.PhoneNumber))
            {
                return BadRequest("There already is account with specified email or phone number");
            }

            var customer = _mapper.Map<Customer>(registerCustomerDto);

            using var hmac = new HMACSHA512();
            customer.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerCustomerDto.Password));
            customer.PasswordSalt = hmac.Key;

            if (!await _customerRepository.AddCustomerAsync(customer))
            {
                return BadRequest();
            }

            var dto = new CustomerDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = _tokenService.CreateToken(customer)
            };

            return Ok(dto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLoginDto customerLoginDto)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(customerLoginDto.Email);

            if (customer == null)
            {
                return Unauthorized("Invalid email");
            }

            using var hmac = new HMACSHA512(customer.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(customerLoginDto.Password));

            if (!computedHash.SequenceEqual(customer.PasswordHash))
            {
                return Unauthorized("Invalid password");
            }

            var dto = new CustomerDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = _tokenService.CreateToken(customer)
            };

            return Ok(dto);
        }
    }
}
