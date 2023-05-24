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
    public partial class AudsDocumentRepository : IAudsDocumentRepository
    {
        private readonly AUDSContext _aUDSContext;
        private bool disposed = false;

        public AudsDocumentRepository(AUDSContext audsDocumentContext)
        {
            _aUDSContext = audsDocumentContext ?? throw new ArgumentNullException(nameof(audsDocumentContext));
        }

        public async Task<IList<AudsDocument>> GetList(IAudsDocumentFilter filter)
        {            
            /* Debido que esta consulta será de solo lectura y no realizaremos cambioss obre sus entidades,
                le marcamos el AsNoTracking para ahorrar que se registren los resultados en el ChangeTracker.*/
            IQueryable<AudsDocument> entityFiltered = _aUDSContext.AudsDocument.AsNoTracking();

            if (filter != null)
            {
                entityFiltered = filter.Filtrate(entityFiltered);
            }

            return await entityFiltered.ToListAsync();
        }            

        public async Task<AudsDocument> Create(AudsDocument dataModel)
        {
            if (dataModel != null)
            {
                _aUDSContext.Add(dataModel);
                await _aUDSContext.SaveChangesAsync();
            }
            return dataModel;
        }            

        public async Task<AudsDocument> Delete(int id)
        {            
            if (id > 0)
            {
                var entityModel = _aUDSContext.AudsDocument.Where(model => model.DocumentId == id && model.CodeStatus == true).FirstOrDefault();
                if (entityModel is null || entityModel.DocumentId == 0)
                {
                    return new AudsDocument();
                }
                else
                {
                    entityModel.CodeStatus = false;

                    _aUDSContext.AudsDocument.Remove(entityModel);
                    await _aUDSContext.SaveChangesAsync();

                    return entityModel;
                }                        
            }
            return new AudsDocument();            
        }

        public async Task<AudsDocument> Update(AudsDocument dataModel)
        {
			if (dataModel != null)
			{
				_aUDSContext.AudsDocument.Attach(dataModel);
				_aUDSContext.Entry(dataModel).State = EntityState.Modified;

				await _aUDSContext.SaveChangesAsync();

				return dataModel;
			}
			return new AudsDocument();            
        }

        public async Task<AudsDocument> Get(int id)
        {
            var entityModel = _aUDSContext.AudsDocument.FirstOrDefault(model => model.DocumentId == id);

            if (entityModel != null && entityModel.DocumentId > 0)
            {
                return entityModel;
            }
            else
            {
                return new AudsDocument();
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
        ~AudsDocumentRepository()
        {
            Dispose(false);
        }
        #endregion
    }
}