﻿using AutoMapper;
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
        private readonly IMyLogger _logger;
        private readonly IEmployeesRepository _employeesRepository;

        public AccountController(ICustomersRepository customerRepository, IMapper mapper, ITokenService tokenService,
            IMyLogger logger, IEmployeesRepository employeesRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _logger = logger;
            _employeesRepository = employeesRepository;
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

            var dto = new UserDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = _tokenService.CreateToken(customer.ID, "customer")
            };

            return Ok(dto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto customerLoginDto)
        {
            await _logger.LogAsync($"Attempting login for user with email: {customerLoginDto.Email}");
            var customer = await _customerRepository.GetCustomerByEmailAsync(customerLoginDto.Email);

            if (customer == null)
            {
                await _logger.LogWarningAsync($"User with email {customerLoginDto.Email} not found. Invalid email address.");
                return Unauthorized("Invalid email");
            }

            using var hmac = new HMACSHA512(customer.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(customerLoginDto.Password));

            if (!computedHash.SequenceEqual(customer.PasswordHash))
            {
                await _logger.LogWarningAsync($"Attempted login for user with email {customerLoginDto.Email} with incorrect password.");
                return Unauthorized("Invalid password");
            }

            var dto = new UserDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Token = _tokenService.CreateToken(customer.ID, "customer")
            };
            await _logger.LogAsync($"Successful login for user with email: {customerLoginDto.Email}");

            return Ok(dto);
        }

        [HttpPost("login-employee")]
        public async Task<IActionResult> LoginEmployee(UserLoginDto employeeLoginDto)
        {
            await _logger.LogAsync($"Attempting login for employee with email: {employeeLoginDto.Email}");
            Employee employee = await _employeesRepository.GetEmployeeByEmailAsync(employeeLoginDto.Email);

            if (employee == null)
            {
                await _logger.LogWarningAsync($"Employee with email {employeeLoginDto.Email} not found. Invalid email address.");
                return Unauthorized("Invalid email");
            }

            string role = employee.Position switch
            {
                "Web Development Specialist" => "developer",
                _ => "employee"
            };

            using var hmac = new HMACSHA512(employee.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(employeeLoginDto.Password));

            if (!computedHash.SequenceEqual(employee.PasswordHash))
            {
                await _logger.LogWarningAsync($"Attempted login for employee with email {employeeLoginDto.Email} with incorrect password.");
                return Unauthorized("Invalid password");
            }

            var dto = new UserDto
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Token = _tokenService.CreateToken(employee.ID, role)
            };
            await _logger.LogAsync($"Successful login for employee with email: {employeeLoginDto.Email}");

            return Ok(dto);
        }
    }
}
