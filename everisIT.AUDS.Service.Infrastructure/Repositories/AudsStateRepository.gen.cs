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
    public partial class AudsStateRepository : IAudsStateRepository
    {
        private readonly AUDSContext _aUDSContext;
        private bool disposed = false;

        public AudsStateRepository(AUDSContext audsStateContext)
        {
            _aUDSContext = audsStateContext ?? throw new ArgumentNullException(nameof(audsStateContext));
        }

        public async Task<IList<AudsState>> GetList(IAudsStateFilter filter)
        {            
            /* Debido que esta consulta será de solo lectura y no realizaremos cambioss obre sus entidades,
                le marcamos el AsNoTracking para ahorrar que se registren los resultados en el ChangeTracker.*/
            IQueryable<AudsState> entityFiltered = _aUDSContext.AudsState.AsNoTracking();

            if (filter != null)
            {
                entityFiltered = filter.Filtrate(entityFiltered);
            }

            return await entityFiltered.ToListAsync();
        }            

        public async Task<AudsState> Create(AudsState dataModel)
        {
            if (dataModel != null)
            {
                _aUDSContext.Add(dataModel);
                await _aUDSContext.SaveChangesAsync();
            }
            return dataModel;
        }            

        public async Task<AudsState> Delete(int id)
        {            
            if (id > 0)
            {
                var entityModel = _aUDSContext.AudsState.Where(model => model.StateId == id && model.CodeStatus == true).FirstOrDefault();
                if (entityModel is null || entityModel.StateId == 0)
                {
                    return new AudsState();
                }
                else
                {
                    entityModel.CodeStatus = false;

                    _aUDSContext.AudsState.Remove(entityModel);
                    await _aUDSContext.SaveChangesAsync();

                    return entityModel;
                }                        
            }
            return new AudsState();            
        }

        public async Task<AudsState> Update(AudsState dataModel)
        {
			if (dataModel != null)
			{
				_aUDSContext.AudsState.Attach(dataModel);
				_aUDSContext.Entry(dataModel).State = EntityState.Modified;

				await _aUDSContext.SaveChangesAsync();

				return dataModel;
			}
			return new AudsState();            
        }

        public async Task<AudsState> Get(int id)
        {
            var entityModel = _aUDSContext.AudsState.FirstOrDefault(model => model.StateId == id);

            if (entityModel != null && entityModel.StateId > 0)
            {
                return entityModel;
            }
            else
            {
                return new AudsState();
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
        ~AudsStateRepository()
        {
            Dispose(false);
        }
        #endregion
    }
}