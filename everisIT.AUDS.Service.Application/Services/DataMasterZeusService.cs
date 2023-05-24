using everis.everisIT.EmployeeClient;
using everis.everisIT.EmployeeClient.Interfaces;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using everis.everisIT.EmployeeClient.Dtos.Partials;
using System.Linq;

namespace everisIT.AUDS.Service.Application.Services
{
    public partial class DataMasterZeusService : IDataMasterZeusService
    {
        private bool disposed = false;
        private readonly IEmployee _clientEmployee;


        /// <summary>
        /// TproEmployeeDegreeService constructor
        /// </summary>        
        /// <param name="tproEmployeeDegreeContext></param>        
        /// <param name="adapter"></param>
        public DataMasterZeusService(
            IEmployee clientEmployee)
        {
            _clientEmployee = clientEmployee ?? throw new ArgumentNullException(nameof(clientEmployee));
        }

        /// <summary>
        /// Return an Employee
        /// </summary>
        /// <param name="idEmployee">Employee Id</param>
        /// <returns>Employee</returns>
        public async Task<DatosPersonalesDto> GetEmployeeDataById(int idEmployee)
        {
            var employees =  await _clientEmployee.GetPersonalData(idEmployee);
            if (employees != null)
            {
                return employees.FirstOrDefault();
            }
            else
            {
                return new DatosPersonalesDto();
            }

            
        }
    }
}
