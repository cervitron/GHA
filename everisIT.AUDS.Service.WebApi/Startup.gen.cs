using everis.everisIT.EmployeeClient;
using everis.everisIT.EmployeeClient.Interfaces;
using everisIT.AUDS.Service.Application.Adapters;
using everisIT.AUDS.Service.Application.Adapters.Interfaces;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Services;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Repositories;
using everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces;
using everisIT.Fen2.MicroserviceClientBase.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace everisIT.AUDS.Service.WebApi
{
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class Startup
    {
        //TODO Change the value of the following value with the name of the database that the application will use
        private readonly string nameDataBase = "CERES";

        /// <summary>
        /// Add dependency injection for each dbContext
        /// </summary>
        /// <param name="services">Service collection</param>
        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<AUDSContext>(options => options.UseSqlServer(Configuration.GetSection(nameDataBase)["Connection"]), ServiceLifetime.Scoped);

            //Obtienen las opciones del contexto de base de datos para cargarlas en el singelton
            var optionsBuilder = new DbContextOptionsBuilder<AUDSContext>();
            optionsBuilder.UseSqlServer(Configuration.GetSection(nameDataBase)["Connection"]);

            services.AddTransient<DbContext, AUDSContext>();
        }

        /// <summary>
        /// Add dependency injection for each existing service
        /// </summary>
        /// <param name="services">Service collection</param>
        private void AddServicesTransient(IServiceCollection services)
        {
            //Services
            services.AddTransient<IAudsAppTagService, AudsAppTagService>();
            services.AddTransient<IAudsApplicationService, AudsApplicationService>();
            services.AddTransient<IAudsAuditService, AudsAuditService>();
            services.AddTransient<IAudsAuditHcoService, AudsAuditHcoService>();
            services.AddTransient<IAudsDocumentService, AudsDocumentService>();
            services.AddTransient<IAudsGroupService, AudsGroupService>();
            services.AddTransient<IAudsRiskService, AudsRiskService>();
            services.AddTransient<IAudsStateService, AudsStateService>();
            services.AddTransient<IAudsStateTypeService, AudsStateTypeService>();
            services.AddTransient<IAudsTagService, AudsTagService>();
            services.AddTransient<IAudsTypeService, AudsTypeService>();
            services.AddTransient<IAudsVulnerabilityService, AudsVulnerabilityService>();
            services.AddTransient<IAudsVulnerabilityHcoService, AudsVulnerabilityHcoService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDataMasterZeusService, DataMasterZeusService>();
            services.AddTransient<IAudsAuditResponsibleService, AudsAuditResponsibleService>();

            //Mappers
            services.AddTransient<IBaseAdapter<AudsAppTagDto, AudsAppTag>,AudsAppTagAdapter>();
            services.AddTransient<IBaseAdapter<AudsApplicationDto, AudsApplication>,AudsApplicationAdapter>();
            services.AddTransient<IBaseAdapter<AudsAuditDto, AudsAudit>,AudsAuditAdapter>();
            services.AddTransient<IBaseAdapter<AudsAuditHcoDto, AudsAuditHco>,AudsAuditHcoAdapter>();
            services.AddTransient<IBaseAdapter<AudsDocumentDto, AudsDocument>,AudsDocumentAdapter>();
            services.AddTransient<IBaseAdapter<AudsGroupDto, AudsGroup>,AudsGroupAdapter>();
            services.AddTransient<IBaseAdapter<AudsRiskDto, AudsRisk>,AudsRiskAdapter>();
            services.AddTransient<IBaseAdapter<AudsStateDto, AudsState>,AudsStateAdapter>();
            services.AddTransient<IBaseAdapter<AudsStateTypeDto, AudsStateType>,AudsStateTypeAdapter>();
            services.AddTransient<IBaseAdapter<AudsTagDto, AudsTag>,AudsTagAdapter>();
            services.AddTransient<IBaseAdapter<AudsTypeDto, AudsType>,AudsTypeAdapter>();
            services.AddTransient<IBaseAdapter<AudsVulnerabilityDto, AudsVulnerability>,AudsVulnerabilityAdapter>();
            services.AddTransient<IBaseAdapter<AudsVulnerabilityHcoDto, AudsVulnerabilityHco>,AudsVulnerabilityHcoAdapter>();
            services.AddTransient<AudsAuditResponsibleAdapter, AudsAuditResponsibleAdapter>();

            //Clients
            services.AddMicroserviceClient().SetRequestInformation(c_int_idApp)
                .AddClient<IEmployee, Employee>("Microservice:Employee");
        }

		private void AddControllerTransient(IServiceCollection services)
        {
            //Controllers
            
        }

        private void AddRepositoryTransient(IServiceCollection services)
        {
            //Repositories
            services.AddTransient<IAudsAppTagRepository, AudsAppTagRepository>();
            services.AddTransient<IAudsApplicationRepository, AudsApplicationRepository>();
            services.AddTransient<IAudsAuditRepository, AudsAuditRepository>();
            services.AddTransient<IAudsAuditHcoRepository, AudsAuditHcoRepository>();
            services.AddTransient<IAudsDocumentRepository, AudsDocumentRepository>();
            services.AddTransient<IAudsGroupRepository, AudsGroupRepository>();
            services.AddTransient<IAudsRiskRepository, AudsRiskRepository>();
            services.AddTransient<IAudsStateRepository, AudsStateRepository>();
            services.AddTransient<IAudsStateTypeRepository, AudsStateTypeRepository>();
            services.AddTransient<IAudsTagRepository, AudsTagRepository>();
            services.AddTransient<IAudsTypeRepository, AudsTypeRepository>();
            services.AddTransient<IAudsVulnerabilityRepository, AudsVulnerabilityRepository>();
            services.AddTransient<IAudsVulnerabilityHcoRepository, AudsVulnerabilityHcoRepository>();
        }
    }
}