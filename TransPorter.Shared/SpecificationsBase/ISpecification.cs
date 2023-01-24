using TransPorter.Shared.Interfaces;
using System.Linq.Expressions;
using TransPorter.Shared.Enums;

namespace TransPorter.Shared.SpecificationsBase;

public interface ISpecification<T> where T : class, IEntity
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>> OrderBy { get; }
    SortDirection SortDirection { get; }
}