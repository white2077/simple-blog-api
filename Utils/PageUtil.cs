using AspNetCoreRestfulApi.Core.Page;

namespace AspNetCoreRestfulApi.Utils
{
    public static class PageUtil
    {
        public static Pageable<T> ToPageable<T>(this IQueryable<T> query, int page, int size) where T : class
        {
            page = page < 1 ? 1 : page;
            size = size < 1 ? 10 : size;
            var totalItems = query.Count();
            var items = query.Skip((page - 1) * size).Take(size).ToList();
            return new Pageable<T>(page, size, totalItems, items);
        }
    }
}
