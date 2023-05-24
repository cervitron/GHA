using everisIT.AUDS.Service.Application.Adapters;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace everisIT.AUDS.Service.Application.Services
{
    public partial class AudsAuditResponsibleService : IAudsAuditResponsibleService
    {
        private readonly AudsAuditResponsibleAdapter _adapter;
        private readonly AUDSContext _aUDSContext;
        private bool disposedValue;
        private readonly IDataMasterZeusService _dataMasterZeusService;

        /// <summary>
        /// AudsAuditService constructor
        /// </summary>        
        /// <param name="audsAuditContext></param>        
        /// <param name="adapter"></param>
        public AudsAuditResponsibleService(AudsAuditResponsibleAdapter adapter,
           AUDSContext audsAuditResponsibleContext,
           IDataMasterZeusService dataMasterZeusService)
        {
            _aUDSContext = audsAuditResponsibleContext ?? throw new ArgumentNullException(nameof(audsAuditResponsibleContext));
            _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
            _dataMasterZeusService = dataMasterZeusService ?? throw new ArgumentNullException(nameof(dataMasterZeusService));
        }

        /// <summary>
        /// Get AudsAudits filtered
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>AudsAudit list</returns>
        public async Task<IList<AudsAuditResponsibleDto>> GetList(IAudsAuditResponsibleFilter filter)
        {
            /* Debido que esta consulta será de solo lectura y no realizaremos cambioss obre sus entidades,
                le marcamos el AsNoTracking para ahorrar que se registren los resultados en el ChangeTracker.*/
            IQueryable<AudsAuditResponsible> entityFiltered = _aUDSContext.AudsAuditResponsible.AsNoTracking();

            if (filter != null )
            {
                entityFiltered = filter.Filtrate(entityFiltered);
            }

            return _adapter.Map(await entityFiltered.ToListAsync());
        }

        /// <summary>
        /// Insert an AudsAudit
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsAuditDto</returns>
        public async Task<AudsAuditResponsibleDto> Create(AudsAuditResponsibleDto dataDto)
        {
            AudsAuditResponsible dataModel = _adapter.Map(dataDto);
            _aUDSContext.Add(dataModel);

            await _aUDSContext.SaveChangesAsync();
            return _adapter.Map(dataModel);
        }

        /// <summary>
        /// Update the AudsAudit Status to Disable it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsAuditDto object</returns>
        public async Task<AudsAuditResponsibleDto> Delete(int id)
        {
            if (id > 0)
            {
                var entityModel = _aUDSContext.AudsAuditResponsible.FirstOrDefault(model => model.Id == id && model.CodeStatus == true);
                if (entityModel is null || entityModel.Id == 0)
                {
                    return new AudsAuditResponsibleDto();
                }
                else
                {
                    entityModel.CodeStatus = false;

                    _aUDSContext.AudsAuditResponsible.Remove(entityModel);
                    await _aUDSContext.SaveChangesAsync();

                    return _adapter.Map(entityModel);
                }
            }
            return new AudsAuditResponsibleDto();
        }

        /// <summary>
        /// Update an audsAudit to a new audsAudit object passed by parameter
        /// </summary>
        /// <param name="dataDto"></param>
        /// <returns>AudsAuditDto object</returns>
        public async Task<AudsAuditResponsibleDto> Update(AudsAuditResponsibleDto dataDto)
        {
            if (dataDto != null)
            {
                AudsAuditResponsible dataModel = _adapter.Map(dataDto);
                _aUDSContext.AudsAuditResponsible.Attach(dataModel);
                _aUDSContext.Entry(dataModel).State = EntityState.Modified;

                await _aUDSContext.SaveChangesAsync();
                return _adapter.Map(dataModel);
            }
            return new AudsAuditResponsibleDto();
        }

        /// <summary>
        /// Get an AudsTypeDto filtered by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AudsTypeDto object</returns>
        public async Task<AudsAuditResponsibleDto> Get(int id)
        {
            var entityModel = await _aUDSContext.AudsAuditResponsible.FirstOrDefaultAsync(model => model.Id == id);

            if (entityModel != null && entityModel.Id > 0)
            {
                return _adapter.Map(entityModel);
            }
            else
            {
                return new AudsAuditResponsibleDto();
            }
        }

        

    }
}
