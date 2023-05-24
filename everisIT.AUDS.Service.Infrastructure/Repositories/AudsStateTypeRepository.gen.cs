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
    public partial class AudsStateTypeRepository : IAudsStateTypeRepository
    {
        private readonly AUDSContext _aUDSContext;
        private bool disposed = false;

        public AudsStateTypeRepository(AUDSContext audsStateTypeContext)
        {
            _aUDSContext = audsStateTypeContext ?? throw new ArgumentNullException(nameof(audsStateTypeContext));
        }

        public async Task<IList<AudsStateType>> GetList(IAudsStateTypeFilter filter)
        {            
            /* Debido que esta consulta será de solo lectura y no realizaremos cambioss obre sus entidades,
                le marcamos el AsNoTracking para ahorrar que se registren los resultados en el ChangeTracker.*/
            IQueryable<AudsStateType> entityFiltered = _aUDSContext.AudsStateType.AsNoTracking();

            if (filter != null)
            {
                entityFiltered = filter.Filtrate(entityFiltered);
            }

            return await entityFiltered.ToListAsync();
        }            

        public async Task<AudsStateType> Create(AudsStateType dataModel)
        {
            if (dataModel != null)
            {
                _aUDSContext.Add(dataModel);
                await _aUDSContext.SaveChangesAsync();
            }
            return dataModel;
        }            

        public async Task<AudsStateType> Delete(int id)
        {            
            if (id > 0)
            {
                var entityModel = _aUDSContext.AudsStateType.Where(model => model.StateTypeId == id && model.CodeStatus == true).FirstOrDefault();
                if (entityModel is null || entityModel.StateTypeId == 0)
                {
                    return new AudsStateType();
                }
                else
                {
                    entityModel.CodeStatus = false;

                    _aUDSContext.AudsStateType.Remove(entityModel);
                    await _aUDSContext.SaveChangesAsync();

                    return entityModel;
                }                        
            }
            return new AudsStateType();            
        }

        public async Task<AudsStateType> Update(AudsStateType dataModel)
        {
			if (dataModel != null)
			{
				_aUDSContext.AudsStateType.Attach(dataModel);
				_aUDSContext.Entry(dataModel).State = EntityState.Modified;

				await _aUDSContext.SaveChangesAsync();

				return dataModel;
			}
			return new AudsStateType();            
        }

        public async Task<AudsStateType> Get(int id)
        {
            var entityModel = _aUDSContext.AudsStateType.FirstOrDefault(model => model.StateTypeId == id);

            if (entityModel != null && entityModel.StateTypeId > 0)
            {
                return entityModel;
            }
            else
            {
                return new AudsStateType();
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
        ~AudsStateTypeRepository()
        {
            Dispose(false);
        }
        #endregion
    }
}