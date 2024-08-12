using AspNetCoreRestfulApi.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRestfulApi.Services.Ipml;

public class BlacklistService(AppDbContext context) : IBlacklistService
{
    public Task<bool> IsTokenBlacklistedAsync(string token)
    {
        return context.TokenBlackLists.AnyAsync(x => x.Token == token);
    }
}