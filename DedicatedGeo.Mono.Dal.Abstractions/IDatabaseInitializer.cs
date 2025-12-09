namespace DedicatedGeo.Mono.Dal.Abstractions;

public interface IDatabaseInitializer
{
    public Task InitDatabaseAsync();
}