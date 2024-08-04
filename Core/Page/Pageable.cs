namespace AspNetCoreRestfulApi.Core.Page
{
    public class Pageable <T> where T : class
    {
        public int Page { get; set; }
        
        public int Size { get; set; }

        public int TotalItems { get; set; }

        public List<T> Items { get; set; }

        public int TotalPage => (int) Math.Ceiling((double)this.TotalItems / this.Size);

        public Pageable(int? page, int? size, int totalItems, List<T> items)
        {
            this.Page = page ?? 1;
            this.Size = size ?? 20;
            this.TotalItems = totalItems;
            this.Items = items;
        }

    }
}
