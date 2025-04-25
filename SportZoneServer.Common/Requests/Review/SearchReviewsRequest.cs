using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using SportZoneServer.Data.PaginationAndFiltering;

namespace SportZoneServer.Common.Requests.Review;

public class SearchReviewsRequest : PaginationModel
{
    [Required]
    public required Guid ProductId { get; set; }
    
    public Expression<Func<Data.Entities.Review, bool>> GetPredicate()
    {
        Expression<Func<Data.Entities.Review, bool>> result = s => !s.IsDeleted;
        
        result = ExpressionExtension<Data.Entities.Review>.AndAlso(result, FilterByProductId());
        
        return result;
    }

    private Expression<Func<Data.Entities.Review, bool>> FilterByProductId()
    {
        return x => x.ProductId == ProductId;
    }
}
