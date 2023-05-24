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
    public partial class AudsGroupRepository : IAudsGroupRepository
    {
        private readonly AUDSContext _aUDSContext;
        private bool disposed = false;

        public AudsGroupRepository(AUDSContext audsGroupContext)
        {
            _aUDSContext = audsGroupContext ?? throw new ArgumentNullException(nameof(audsGroupContext));
        }

        public async Task<IList<AudsGroup>> GetList(IAudsGroupFilter filter)
        {            
            /* Debido que esta consulta será de solo lectura y no realizaremos cambioss obre sus entidades,
                le marcamos el AsNoTracking para ahorrar que se registren los resultados en el ChangeTracker.*/
            IQueryable<AudsGroup> entityFiltered = _aUDSContext.AudsGroup.AsNoTracking();

            if (filter != null)
            {
                entityFiltered = filter.Filtrate(entityFiltered);
            }

            return await entityFiltered.ToListAsync();
        }            

        public async Task<AudsGroup> Create(AudsGroup dataModel)
        {
            if (dataModel != null)
            {
                _aUDSContext.Add(dataModel);
                await _aUDSContext.SaveChangesAsync();
            }
            return dataModel;
        }            

        public async Task<AudsGroup> Delete(int id)
        {            
            if (id > 0)
            {
                var entityModel = _aUDSContext.AudsGroup.Where(model => model.GroupId == id && model.CodeStatus == true).FirstOrDefault();
                if (entityModel is null || entityModel.GroupId == 0)
                {
                    return new AudsGroup();
                }
                else
                {
                    entityModel.CodeStatus = false;

                    _aUDSContext.AudsGroup.Remove(entityModel);
                    await _aUDSContext.SaveChangesAsync();

                    return entityModel;
                }                        
            }
            return new AudsGroup();            
        }

        public async Task<AudsGroup> Update(AudsGroup dataModel)
        {
			if (dataModel != null)
			{
				_aUDSContext.AudsGroup.Attach(dataModel);
				_aUDSContext.Entry(dataModel).State = EntityState.Modified;

				await _aUDSContext.SaveChangesAsync();

				return dataModel;
			}
			return new AudsGroup();            
        }

        public async Task<AudsGroup> Get(int id)
        {
            var entityModel = _aUDSContext.AudsGroup.FirstOrDefault(model => model.GroupId == id);

            if (entityModel != null && entityModel.GroupId > 0)
            {
                return entityModel;
            }
            else
            {
                return new AudsGroup();
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
        ~AudsGroupRepository()
        {
            Dispose(false);
        }
        #endregion
    }
}