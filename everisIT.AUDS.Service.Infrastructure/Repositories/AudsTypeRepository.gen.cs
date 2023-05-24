using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everisIT.AUDS.Service.Infrastructure.Repositories
{
    public partial class AudsTypeRepository : IAudsTypeRepository
    {
        private readonly AUDSContext _aUDSContext;
        private bool disposed = false;

        public AudsTypeRepository(AUDSContext audsTypeContext)
        {
            _aUDSContext = audsTypeContext ?? throw new ArgumentNullException(nameof(audsTypeContext));
        }

        public async Task<IList<AudsType>> GetList(IAudsTypeFilter filter)
        {            
            /* Debido que esta consulta será de solo lectura y no realizaremos cambioss obre sus entidades,
                le marcamos el AsNoTracking para ahorrar que se registren los resultados en el ChangeTracker.*/
            IQueryable<AudsType> entityFiltered = _aUDSContext.AudsType.AsNoTracking();

            if (filter != null)
            {
                entityFiltered = filter.Filtrate(entityFiltered);
            }

            return await entityFiltered.ToListAsync();
        }            

        public async Task<AudsType> Create(AudsType dataModel)
        {
            if (dataModel != null)
            {
                _aUDSContext.Add(dataModel);
                await _aUDSContext.SaveChangesAsync();
            }
            return dataModel;
        }            

        public async Task<AudsType> Delete(int id)
        {            
            if (id > 0)
            {
                var entityModel = _aUDSContext.AudsType.Where(model => model.IdType == id && model.CodeStatus == true).FirstOrDefault();
                if (entityModel is null || entityModel.IdType == 0)
                {
                    return new AudsType();
                }
                else
                {
                    entityModel.CodeStatus = false;

                    _aUDSContext.AudsType.Remove(entityModel);
                    await _aUDSContext.SaveChangesAsync();

                    return entityModel;
                }                        
            }
            return new AudsType();            
        }

        public async Task<AudsType> Update(AudsType dataModel)
        {
			if (dataModel != null)
			{
				_aUDSContext.AudsType.Attach(dataModel);
				_aUDSContext.Entry(dataModel).State = EntityState.Modified;

				await _aUDSContext.SaveChangesAsync();

				return dataModel;
			}
			return new AudsType();            
        }

        public async Task<AudsType> Get(int id)
        {
            var entityModel = _aUDSContext.AudsType.FirstOrDefault(model => model.IdType == id);

            if (entityModel != null && entityModel.IdType > 0)
            {
                return entityModel;
            }
            else
            {
                return new AudsType();
            }            
        }

        #region Dispose

		public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //Si ya hemos sido 'Disposeado', nos saltamos la limpieza ya que generará una excepción.
            if (!disposed)
            {
                //Si nos llaman de forma explícita al dispose:
                if (disposing)
                {
                    //Limpiamos las variables de clases 'managed' [Todo lo que tiene IDisposable, llamamos a su dispose aqui]
                    _aUDSContext?.Dispose();
                }

                //Limpiamos el resto de variables de clases 'unmanaged' [Objetos sin IDisposable que NECESITEN ser liberados]
                //Establecemos que ya hemos sido llamados.
                disposed = true;
                Dispose(disposing);
            }
        }
        //Hacemos que el finalizador de la clase llame al dispose indicando que solo libere el codigo unmanaged
        ~AudsTypeRepository()
        {
            Dispose(false);
        }
        #endregion
    }
}