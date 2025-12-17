using Mapster;
using SHOP.BLL.Services.Interfaces;
using SHOP.DAL.Models;
using SHOP.DAL.Repositories.Interfaces;

namespace SHOP.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();

            return _genericRepository.Add(entity);
        }

        public int Delete(int id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null)
            {
                return 0;
            }
            return _genericRepository.Remove(entity);
        }

        public IEnumerable<TResponse> GetAll(bool onlyActive = false)
        {
            var entities = _genericRepository.GetAll();
            if (onlyActive)
            {
                entities = entities.Where(e => e.status == Status.Active);
            }
            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse? GetById(int id)
        {
            var entity = _genericRepository.GetById(id);
            return entity is null ? default : entity.Adapt<TResponse>();
        }

        public bool ToggleStatus(int id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null)
            {
                return false;
            }
            entity.status = entity.status == Status.Active ? Status.Inactive : Status.Active;
            _genericRepository.Update(entity);
            return true;
        }

        public int Update(int id, TRequest request)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null)
            {
                return 0;
            }
            var updatedEntity = request.Adapt(entity);
            return _genericRepository.Update(entity);
        }
    }
}
