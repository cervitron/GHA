using everis.everisIT.EmployeeClient;
using everis.everisIT.EmployeeClient.Dtos.Partials;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace everisIT.AUDS.Service.Application.Services.Interfaces
{
    public partial interface IDataMasterZeusService
    {
        Task<DatosPersonalesDto> GetEmployeeDataById(int idEmployee);
    }
}
