using System.Linq.Expressions;
using SportZoneServer.Data.PaginationAndFiltering;

namespace SportZoneServer.Common.Requests.Order;

public class SearchOrderRequest : PaginationModel
{
    public Guid? UserId { get; set; }
    
    public Expression<Func<Data.Entities.Order, bool>> GetPredicate()
    {
        Expression<Func<Data.Entities.Order, bool>> result = s => !s.IsDeleted;

        if (UserId.HasValue)
        {
            result = ExpressionExtension<Data.Entities.Order>.AndAlso(result, FilterByUserId());
        }
        
        return result;
    }

    private Expression<Func<Data.Entities.Order, bool>> FilterByUserId()
    {
        return x => x.UserId == UserId;
    }
}
