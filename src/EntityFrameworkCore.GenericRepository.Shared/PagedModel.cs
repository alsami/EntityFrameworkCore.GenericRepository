using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.GenericRepository.Shared
{
    public class PagedModel<TEntity, TId> where TEntity : class, IEntity<TId>, new() where TId : IEquatable<TId>
    {
        public int Size { get; }

        public int Page { get; }

        public int TotalCount { get; }

        public int TotalPages { get; }

        public string OrderByPropertyName { get; }

        public SortDirection SortDirection { get; }

        public IEnumerable<TEntity> Entities { get; }

        public PagedModel(IEnumerable<TEntity> entities,
            int page,
            int size,
            int totalCount, 
            string orderByPropertyName, 
            SortDirection sortDirection)
        {
            this.TotalPages = size == 0
                ? 0
                : (int)Math.Ceiling((decimal)totalCount / size);

            this.Entities = entities;
            this.Size = size;
            this.Page = page;
            this.TotalCount = totalCount;
            this.OrderByPropertyName = orderByPropertyName;
            this.SortDirection = sortDirection;
        }
    }
}