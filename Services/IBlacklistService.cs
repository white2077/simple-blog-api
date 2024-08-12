namespace AspNetCoreRestfulApi.Services;

public interface IBlacklistService
{
    Task<bool> IsTokenBlacklistedAsync(string token);

}