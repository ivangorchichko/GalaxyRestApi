using GalaxyRestApi.DAL.Models;

namespace GalaxyRestApi.DAL.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        public Task<List<T>> GetAll();
        public Task<T?> Get(int id);
        public Task<T> Create(T model);
        public Task<T> Update(T model);
        public Task<T?> Delete(int id);
    }
}
