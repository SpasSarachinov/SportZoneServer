using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportZoneServer.API.Helpers;
using SportZoneServer.Common.Requests.Review;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReviewsController(IReviewService reviewService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] SearchReviewsRequest? request)
    {
        return await ControllerProcessor.ProcessAsync(() => reviewService.SearchReviewsAsync(request), this, true);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateReviewRequest request)
    {
        return await ControllerProcessor.ProcessAsync(() => reviewService.CreateAsync(request), this, true);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateReviewRequest request)
    {
        return await ControllerProcessor.ProcessAsync(() => reviewService.UpdateAsync(request), this, true);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return await ControllerProcessor.ProcessAsync<object>(
            async () => await reviewService.DeleteAsync(id), this);
    }
}
