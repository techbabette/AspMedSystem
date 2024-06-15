using AspMedSystem.Application.UseCases.Commands.Auth;
using AspMedSystem.Application.UseCases.Commands.Groups;
using AspMedSystem.Application.UseCases.Commands;
using AspMedSystem.Application;
using AspMedSystem.Implementation.Logging.UseCases;
using AspMedSystem.Implementation.UseCases.Commands.Auth;
using AspMedSystem.Implementation.UseCases.Commands.Groups;
using AspMedSystem.Implementation.UseCases.Commands;
using AspMedSystem.Implementation.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspMedSystem.Application.DTO;
using System.Linq.Expressions;

namespace AspMedSystem.Implementation.Extensions
{
    public static class QueryExtensions
    {
        public static PagedResponse<TResultDto> AsPagedResponse<TSearch, TResultDto>
            (this IQueryable<TSearch> query, PagedSearch search, Expression<Func<TSearch, TResultDto>> expression)
            where TResultDto : class
        {
            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<TResultDto>
            {
                CurrentPage = page,
                Data = query.Select(expression).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };
        }
    }
}
