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
    public partial class AudsAuditRepository : IAudsAuditRepository
    {
        private readonly AUDSContext _aUDSContext;
        private bool disposed = false;

        public AudsAuditRepository(AUDSContext audsAuditContext)
        {
            _aUDSContext = audsAuditContext ?? throw new ArgumentNullException(nameof(audsAuditContext));
        }

        public async Task<IList<AudsAudit>> GetList(IAudsAuditFilter filter)
        {            
            /* Debido que esta consulta será de solo lectura y no realizaremos cambioss obre sus entidades,
                le marcamos el AsNoTracking para ahorrar que se registren los resultados en el ChangeTracker.*/
            IQueryable<AudsAudit> entityFiltered = _aUDSContext.AudsAudit.AsNoTracking();

            if (filter != null)
            {
                entityFiltered = filter.Filtrate(entityFiltered);
            }

            return await entityFiltered.ToListAsync();
        }            

        public async Task<AudsAudit> Create(AudsAudit dataModel)
        {
            if (dataModel != null)
            {
                _aUDSContext.Add(dataModel);
                await _aUDSContext.SaveChangesAsync();
            }
            return dataModel;
        }            

        public async Task<AudsAudit> Delete(int id)
        {            
            if (id > 0)
            {
                var entityModel = _aUDSContext.AudsAudit.Where(model => model.AuditId == id && model.CodeStatus == true).FirstOrDefault();
                if (entityModel is null || entityModel.AuditId == 0)
                {
                    return new AudsAudit();
                }
                else
                {
                    entityModel.CodeStatus = false;

                    _aUDSContext.AudsAudit.Remove(entityModel);
                    await _aUDSContext.SaveChangesAsync();

                    return entityModel;
                }                        
            }
            return new AudsAudit();            
        }

        public async Task<AudsAudit> Update(AudsAudit dataModel)
        {
			if (dataModel != null)
			{
                var entry = _aUDSContext.AudsAudit.First(x => x.AuditId == dataModel.AuditId);

                _aUDSContext.Entry(entry).CurrentValues.SetValues(dataModel);

                await _aUDSContext.SaveChangesAsync();

				return dataModel;
			}
			return new AudsAudit();            
        }

        public async Task<AudsAudit> Get(int id)
        {
            var entityModel = _aUDSContext.AudsAudit.FirstOrDefault(model => model.AuditId == id);

            if (entityModel != null && entityModel.AuditId > 0)
            {
                return entityModel;
            }
            else
            {
                return new AudsAudit();
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
        ~AudsAuditRepository()
        {
            Dispose(false);
        }
        #endregion
    }
}