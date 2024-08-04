using AspNetCoreRestfulApi.Core.Page;

namespace AspNetCoreRestfulApi.Services
{
    public interface ISevice<REQ,RES,K> where REQ:class where RES:class
    {
        public Pageable<RES> GetAll(int page,int size);
        public RES GetById(K id);
        public RES Create(REQ entity);
        public RES Update(K id,REQ entity);
        public void Delete(K id);
    }
}
