using AspNetCoreRestfulApi.Core.Page;
using AspNetCoreRestfulApi.Dto.Response;

namespace AspNetCoreRestfulApi.Services;

public interface IChatService
{
    public  Task SendGlobalMessage(int userId, string message);

    public Pageable<GlobalChatResponseDto> AllChat(int page, int size);
}