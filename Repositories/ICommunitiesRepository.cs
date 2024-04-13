using Reddit.Models;
using Reddit.Requests;

namespace Reddit.Repositories
{
    public interface ICommunitiesRepository
    {
            public Task<PagedList<Community>> GetAll(GetCommunitiesRequest getCommunitiesRequest);
    }
}
