

namespace SHOP.BLL.Services.Interfaces
{
    public interface IGenericService<TRequest, TResponse, TEntity>
    {
        int Create(TRequest request);
        IEnumerable<TResponse> GetAll(bool onlyActive = false);
        TResponse? GetById(int id);
        int Update(int id, TRequest request);
        int Delete(int id);
        bool ToggleStatus(int id);

    }
}
