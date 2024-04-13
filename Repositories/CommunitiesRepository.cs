using Reddit.Models;
using Reddit.Requests;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Reddit.Repositories
{
    public class CommunitiesRepository : ICommunitiesRepository
    {
        private readonly ApplicationDbContext _context;

        public CommunitiesRepository(ApplicationDbContext applicationDBContext)
        {
            this._context = applicationDBContext;
        }

        public async Task<PagedList<Community>> GetAll(GetCommunitiesRequest getCommunitiesRequest)
        {
            IQueryable<Community> productsQuery = _context.Communities;

            if (!string.IsNullOrWhiteSpace(getCommunitiesRequest.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(getCommunitiesRequest.SearchTerm) ||
                    p.Description.Contains(getCommunitiesRequest.SearchTerm));
            }

            if ((bool)(getCommunitiesRequest?.isAscending))
            {
                productsQuery = productsQuery.OrderBy(GetSortProperty(getCommunitiesRequest.sortKey));
            }
            else
            {
                productsQuery = productsQuery.OrderByDescending(GetSortProperty(getCommunitiesRequest.sortKey));
            }


            return await PagedList<Community>.CreateAsync(productsQuery, getCommunitiesRequest.Page, getCommunitiesRequest.PageSize);
        }

        private static Expression<Func<Community, object>> GetSortProperty(string sortKey) =>
      sortKey?.ToLower() switch
      {
          "date" => comun => comun.CreateAt,
          "name" => comun => comun.Name,
          "subscribers" => comun => comun.Subscribers.Count(),
          "posts" => Comun => Comun.Posts.Count(),
          _ => comun => comun.Id
      };
    }
}
