namespace Entities.SeedWork;

internal interface IValidator<in T>
{
    bool IsValid(T instance, out string error);
}

internal interface ISpecification<in T>
{
    bool IsSatisfiedBy(T instance, out string error);
}

internal class Specifications<T>(params ISpecification<T>[] specs) : ISpecification<T>
{
    private readonly IEnumerable<ISpecification<T>> _specs = specs;

    public bool IsSatisfiedBy(T t, out string error)
    {
        foreach (var spec in _specs)
            if (spec.IsSatisfiedBy(t, out error) is false)
                return false;

        error = string.Empty;
        return true;
    }
}