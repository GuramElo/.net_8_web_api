using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Reddit.Requests
{
    public class GetCommunitiesRequest
    {
        [FromQuery]
        public string? SearchTerm { get; set; }

        [FromQuery]
        public string? sortKey { get; set; }

        [FromQuery]
        public bool? isAscending { get; set; } = true;

        [FromQuery]
        public int Page { get; set; } = 1;

        [FromQuery]
        [Range(1, 50, ErrorMessage = "Page size must be in range [1, 50].")]
        public int PageSize { get; set; } = 10;
    }
}
