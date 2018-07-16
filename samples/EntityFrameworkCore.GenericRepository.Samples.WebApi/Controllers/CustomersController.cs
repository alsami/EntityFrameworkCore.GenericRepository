using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkCore.GenericRepository.Abstractions;
using EntityFrameworkCore.GenericRepository.Samples.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCore.GenericRepository.Samples.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IEntityRepository<Customer, Guid> customersRepository;
        private readonly IMapper mapper;

        public CustomersController(IEntityRepository<Customer, Guid> customersRepository, IMapper mapper)
        {
            this.customersRepository = customersRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var customers = await this.customersRepository.GetAllAsync(customer => customer.Addresses);

            return this.mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        [HttpPost]
        public async Task<CustomerDto> Create([FromBody] CustomerDto customerDto)
        {
            var customer = this.mapper.Map<Customer>(customerDto);

            await this.customersRepository.AddAsync(customer);

            await this.customersRepository.EnsureChangesAsync();

            return this.mapper.Map<CustomerDto>(customer);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerDto customerDto)
        {
            var customer = await
                this.customersRepository.FindAsync(existingCustomer => existingCustomer.Id == customerDto.Id);

            if (customer == null)
            {
                return this.NotFound();
            }

            this.mapper.Map(customerDto, customer);
            
            this.customersRepository.Edit(customer);

            await this.customersRepository.EnsureChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid customerId)
        {
            var customer = await this.customersRepository.FindAsync(customerId);

            if (customer == null)
            {
                return this.NotFound();
            }
            
            this.customersRepository.Delete(customer);

            await this.customersRepository.EnsureChangesAsync();

            return this.NoContent();
        }
    }
}