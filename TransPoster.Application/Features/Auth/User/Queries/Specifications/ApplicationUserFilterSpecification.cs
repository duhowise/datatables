using Keed_Agent_Loans.Shared.SpecificationsBase;
using System.Linq.Expressions;
using TransPorter.Domain;
using TransPoster.Application.Features.Auth.User.DTOs.Request;

namespace TransPoster.Application.Features.Auth.User.Queries.Specifications;

public class ApplicationUserFilterSpecification : HeroSpecification<ApplicationUser>
{
    public ApplicationUserFilterSpecification(GetPagedUsersRequest request)
    {
        Criteria = u => true;
        if (!string.IsNullOrWhiteSpace(request.SearchString)) ApplicationUserFiltering(request);
        else if (request.UserId is not null && request.UserId != Guid.Empty) Criteria = u => u.Id == request.UserId;
        else Criteria = u => true;
        if (!string.IsNullOrEmpty(request.SortLabel) && request.SortDirection != TransPorter.Shared.Enums.SortDirection.None)
        {
            var orderBy = GetOrderBy(request.SortLabel);
            ApplyOrderBy(orderBy, request.SortDirection);
        }
    }

    private void ApplicationUserFiltering(GetPagedUsersRequest request)
    {
        var query = request.SearchString.ToString();
        Criteria = u => u.FirstName.Contains(query) ||
                         u.LastName.Contains(query) ||
                         u.Email != null && u.Email.Contains(query);
    }

    private static Expression<Func<ApplicationUser, object>> GetOrderBy(string sortLabel)
    {
        var user = new ApplicationUser();
        return sortLabel.ToLower() switch
        {
            "firstname" => p => p.FirstName,
            "surname" => p => p.LastName,
            "createdon" => p => p.CreatedOn,
            _ => p => p.CreatedOn
        };
    }
}
