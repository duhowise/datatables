using System.Linq.Expressions;
using TransPorter.Shared.Enums;
using TransPorter.Shared.Interfaces;
using TransPorter.Shared.SpecificationsBase;

namespace Keed_Agent_Loans.Shared.SpecificationsBase;

public abstract class HeroSpecification<T> : ISpecification<T> where T : class, IEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; } = null!;
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public Expression<Func<T, object>> OrderBy { get; private set; } = null!;
    public SortDirection SortDirection { get; private set; }

    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);

    protected virtual void AddInclude(string includeString) => IncludeStrings.Add(includeString);

    protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression, SortDirection sortDirection = SortDirection.Descending)
    {
        OrderBy = orderByExpression;
        SortDirection = sortDirection;
    }
}