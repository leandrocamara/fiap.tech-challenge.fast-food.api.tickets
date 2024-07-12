namespace External.Persistence
{
    internal interface IDatabaseContextInitializer
    {
        Task InitializeAsync(CancellationToken cancellationToken = default);
    }
}
