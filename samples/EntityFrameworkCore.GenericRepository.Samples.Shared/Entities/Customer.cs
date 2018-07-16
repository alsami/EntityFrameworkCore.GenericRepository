using System;
using System.Collections.Generic;
using EntityFrameworkCore.GenericRepository.Shared;

namespace EntityFrameworkCore.GenericRepository.Samples.Shared.Entities
{
    public class Customer : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}