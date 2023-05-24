using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters;
using System.Linq;
using everis.everisIT.EmployeeClient;
using Microsoft.Extensions.Configuration;
using everisIT.AUDS.Service.Application.Utils;
using System.Text.RegularExpressions;
using everisIT.AUDS.Service.Application.Utils.Resources;

namespace everisIT.AUDS.Service.Application.Services
{

    public enum NotificationType
    {
        a, b, c, d
    }
    public partial class NotificationService : INotificationService
    {

        private readonly IAudsAuditService _audsAuditService;
        private readonly IAudsVulnerabilityService _audsVulnerabilityService;
        private readonly IAudsRiskService _audsRiskService;
        private readonly IDataMasterZeusService _dataMasterZeusService;
        private readonly IAudsApplicationService _audsApplicationService;
        private readonly IAudsAuditResponsibleService _audsAuditResponsibleService;

        public NotificationService(IAudsAuditService audsAuditService,
            IAudsVulnerabilityService audsVulnerabilityService,
            IAudsRiskService audsRiskService,
            IDataMasterZeusService dataMasterZeusService,
            IAudsApplicationService audsApplicationService,
            IAudsAuditResponsibleService audsAuditResponsibleService)
        {
            _audsAuditService = audsAuditService ?? throw new ArgumentNullException(nameof(audsAuditService));
            _audsVulnerabilityService = audsVulnerabilityService ?? throw new ArgumentNullException(nameof(audsVulnerabilityService));
            _audsRiskService = audsRiskService ?? throw new ArgumentNullException(nameof(audsRiskService));
            _dataMasterZeusService = dataMasterZeusService ?? throw new ArgumentNullException(nameof(dataMasterZeusService));
            _audsApplicationService = audsApplicationService ?? throw new ArgumentNullException(nameof(audsApplicationService));
            _audsAuditResponsibleService = audsAuditResponsibleService ?? throw new ArgumentNullException(nameof(audsAuditResponsibleService));

        }



        ///Se notifican las vulnerabilidades en las cuales ha pasado la fecha de mitigacion
        public async Task<IList<MessageDto>> NotificationsMitigation()
        {

            var NotificationsReturn = new List<MessageDto>();
            var dateInit = new DateTime(2000, 1, 1);

            var vulnerabilitiesList = await this.GetAllVulnerabilities();
            AudsAuditFilter filter = new AudsAuditFilter() { CodeStatus = true };
            var audits = await _audsAuditService.GetList(filter);

            //Auditor Mitigation Date Notification
            var yesterday = DateTime.Today.AddDays(-1);
            var vulnerabilitiesListMitigation = vulnerabilitiesList.Where(vulnerability => vulnerability.VulnerabilityDateCommitment != null).ToList();
            vulnerabilitiesListMitigation = vulnerabilitiesListMitigation.Where(vulnerability => vulnerability.VulnerabilityDateCommitment.Value.Date == yesterday.Date).ToList();

            var vulnerabilitiesMittitment = vulnerabilitiesListMitigation.GroupBy(x => x.AuditId).Select(x => x.FirstOrDefault());
            var applicationList = await AppliactionsList(vulnerabilitiesMittitment.Select(x => x.AuditId).ToList(), audits);

            foreach (var application in applicationList)
            {
                var notificationDate = await this.AuditorMitigationDateNotification(application, vulnerabilitiesListMitigation, audits);

                if (notificationDate.To != null && notificationDate.To.Any())
                {
                    NotificationsReturn.Add(notificationDate);
                }
            }
            return NotificationsReturn;
        }

        ///Se notifican las vulnerabilidades en las cuales ha pasado la fecha de resolucion dependiendo del riesgo
        public async Task<IList<MessageDto>> NotificationsResponsible()
        {
            var NotificationsReturn = new List<MessageDto>();
            var dateInit = new DateTime(2000, 1, 1);

            var vulnerabilitiesList = await this.GetAllVulnerabilities();
            AudsAuditFilter filter = new AudsAuditFilter() { CodeStatus = true };
            var audits = await _audsAuditService.GetList(filter);
            //Responsible Notification
            var vulnerabilitiesResponsibleList = vulnerabilitiesList.Where(vulnerability => vulnerability.RiskReference.RiskName.Contains("Critical") ||
                                                                             vulnerability.RiskReference.RiskName.Contains("High") ||
                                                                             vulnerability.RiskReference.RiskName.Contains("Medium")).ToList();

            vulnerabilitiesResponsibleList = vulnerabilitiesResponsibleList.Where(vulnerability => vulnerability.VulnerabilityDateMitigation == null &&
                                                                                    vulnerability.StateIdResolution == 13).ToList();

            var vulnerabilitiesResponsible = vulnerabilitiesResponsibleList.GroupBy(x => x.AuditId).Select(x => x.FirstOrDefault());
            var applicationList = await AppliactionsList(vulnerabilitiesResponsible.Select(x => x.AuditId).ToList(), audits);

            foreach (var application in applicationList)
            {               

                var notificationResponsible = await this.ResponsibleNotification(application, vulnerabilitiesResponsibleList, audits);
                foreach (var item in notificationResponsible)
                {
                    NotificationsReturn.Add(item);
                }

            }

            return NotificationsReturn;
        }


        ///Se notifican las vulnerabilidades en las cuales se ha realizado una modificacion
        public async Task<IList<MessageDto>> NotificationsUpdate()
        {
            var NotificationsReturn = new List<MessageDto>();
            var dateInit = new DateTime(2000, 1, 1);

            var vulnerabilitiesList = await this.GetAllVulnerabilities();
            AudsAuditFilter filter = new AudsAuditFilter() { CodeStatus = true };
            var audits = await _audsAuditService.GetList(filter);

            //Update Notification
            var yesterday = DateTime.Today.AddDays(-1);
            var vulnerabilitiesUpdateList = vulnerabilitiesList.Where(vulnerability => vulnerability.DateLastUpdateRegister.Date == yesterday.Date
                                                                                       && vulnerability.DateNewRegister.Date != vulnerability.DateLastUpdateRegister.Date).ToList();

            var AuditList = vulnerabilitiesUpdateList.Select(idAudit => idAudit.AuditId).ToList();
            AuditList = AuditList.Distinct().ToList();

            foreach (var id in AuditList)
            {             
                var notificationResponsible = await this.VulnerabilityUpdateNotification(id, vulnerabilitiesUpdateList);

                if (notificationResponsible != null && notificationResponsible.Any())
                {
                    NotificationsReturn.AddRange(notificationResponsible);
                }

            }

            return NotificationsReturn;
        }

        
        public async Task<IList<MessageDto>> NotificationEscalation()
        {
            var NotificationsReturn = new List<MessageDto>();
            var dateInit = new DateTime(2000, 1, 1);

            var vulnerabilitiesList = await this.GetAllVulnerabilities();
            AudsAuditFilter filter = new AudsAuditFilter() { CodeStatus = true };
            var audits = await _audsAuditService.GetList(filter);

            //Update Notification 
            var vulnerabilitiesResponsibleList = vulnerabilitiesList.Where(vulnerability => (vulnerability.RiskReference.RiskName.Contains("Critical") &&
                                                                                    vulnerability.VulnerabilityDateCommitment != null &&
                                                                                    vulnerability.VulnerabilityDateCommitment.Value.DaysBetween(DateTime.Now).IsMultiple(10)) ||
                                                                                            (vulnerability.RiskReference.RiskName.Contains("High") &&
                                                                                    vulnerability.VulnerabilityDateCommitment != null &&
                                                                                    vulnerability.VulnerabilityDateCommitment.Value.DaysBetween(DateTime.Now).IsMultiple(20))).ToList();

            vulnerabilitiesResponsibleList = vulnerabilitiesResponsibleList.Where(vulnerability => vulnerability.VulnerabilityDateResolution == null &&
                                                                                    !(vulnerability.StateIdResolution == 17
                                                                                    || vulnerability.StateIdResolution == 18
                                                                                    || vulnerability.StateIdResolution == 22)).ToList();

            var vulnerabilitiesResponsible = vulnerabilitiesResponsibleList.GroupBy(x => x.AuditId).Select(x => x.FirstOrDefault());

            foreach (var audit in vulnerabilitiesResponsible)
            {
                if (audit.RiskReference.RiskName == "Critical" && audit.DateNewRegister.DaysBetween(DateTime.Now).IsMultiple(10))
                {
                    var notification = await BodyEscalation(audit, vulnerabilitiesResponsibleList, audits, "críticas");
                    if (notification != null)
                    {
                        NotificationsReturn.AddRange(notification);
                    }
                } 
                else if (audit.RiskReference.RiskName == "High" && audit.DateNewRegister.DaysBetween(DateTime.Now).IsMultiple(20))
                {
                    var notification = await BodyEscalation(audit, vulnerabilitiesResponsibleList, audits, "altas");
                    if (notification != null)
                    {
                        NotificationsReturn.AddRange(notification);
                    }
                }            
            }

            return NotificationsReturn;
        }


        public async Task<IList<MessageDto>> NotificationEndFollowUp()
        {
            var NotificationsReturn = new List<MessageDto>();
            var dateInit = new DateTime(2000, 1, 1);

            var vulnerabilitiesList = await this.GetAllVulnerabilities();
            AudsAuditFilter filter = new AudsAuditFilter() { CodeStatus = true };
            var audits = await _audsAuditService.GetList(filter);

            //Update Notification
            var yesterday = DateTime.Today.AddDays(-1);
            var vulnerabilitiesResponsibleList = vulnerabilitiesList.Where(vulnerability => ((
                                                                             vulnerability.RiskReference.RiskName.Contains("Critical") ||
                                                                             vulnerability.RiskReference.RiskName.Contains("High") ||
                                                                             vulnerability.RiskReference.RiskName.Contains("Medium")
                                                                             ) && (
                                                                             vulnerability.StateReference.StateName != "Mitigated" &&
                                                                             vulnerability.StateReference.StateName != "Obsolete"
                                                                             ))).ToList();

            var AuditList = vulnerabilitiesResponsibleList.Select(id => id.AuditId).ToList();

            audits = audits.Where(aud => !AuditList.Contains(aud.AuditId) && aud.AuditIsNotificationEndSent == false).ToList();

            foreach (var audit in audits)
            {
                if (!audit.AuditIsNotificationEndSent)
                {
                    var notification = await BodyEndFollowUp(audit);
                    if (notification != null)
                    {
                        NotificationsReturn.AddRange(notification);
                    }
                }

            }

            return NotificationsReturn;
        }

        private async Task<IList<MessageDto>> BodyEscalation(AudsVulnerabilityDto vulnerability, IList<AudsVulnerabilityDto> vulnerabilitiesList, IList<AudsAuditDto> audits, string risk)
        {
            IList<MessageDto> notification = new List<MessageDto>();
            var responsableName = "";

            var subject = Constants.Subject_Escalation_Notification;
            var body = EscapeBrackets(ResourceHandler.GetResource("BodyEscalation", "MailData"));
            string lineaTablaInsert = "<tr><td style=\"border: 1px solid black;\">{0}</td><td style=\"border: 1px solid black;\">{1}</td><td style=\"border: 1px solid black;\">{2}</td></tr>";
            string cuerpoMailInsert = "";

            var responsableList = audits.Where(x => x.AuditId == vulnerability.AuditId).ToList();
            var responsable = await this.GetAuditResponsible(responsableList.LastOrDefault().AuditResponsible);

            foreach (var resp in responsable)
            {
                //if (responsable.Count() > 0)
                //{
                    responsableName = resp.TxtNombre + " " + resp.TxtApellido1 + " " + resp.TxtApellido2;
                //}

                var vulList = vulnerabilitiesList.Where(x => x.AuditId == vulnerability.AuditId).ToList();

                foreach (var item in vulList)
                {
                    var cuerpo = string.Format(lineaTablaInsert,
                                                       item.VulnerabilityId,
                                                       item.VulnerabilityIdControl,
                                                       item.VulnerabilityDescription);

                    cuerpoMailInsert = cuerpoMailInsert + cuerpo;

                }

                body = string.Format(body, responsableName, responsableList.FirstOrDefault().AuditDescription, cuerpoMailInsert, risk);
                notification.Add(await LoadMessage(responsableList.FirstOrDefault(), subject, body, resp, 0));
            }

            return notification;
        }

        private async Task<IList<MessageDto>> BodyEndFollowUp(AudsAuditDto audit)
        {
            IList<MessageDto> notification = new List<MessageDto>();
            var responsableName = "";

            var subject = Constants.Subject_End_Follow_Up;
            var body = EscapeBrackets(ResourceHandler.GetResource("BodyEndFollowUp", "MailData"));
            string lineaTablaInsert = "<tr><td style=\"border: 1px solid black;\">{0}</td><td style=\"border: 1px solid black;\">{1}</td><td style=\"border: 1px solid black;\">{2}</td></tr>";
            string cuerpoMailInsert = "";
            string tableInsert = "<table style=\"border:1px solid black;border-collapse:collapse;\"> <thead> <tr> <th style=\"border:1px solid black;\"> Id vulnerability </th> <th style =\"border:1px solid black;\">Id Control</th> <th style=\"border:1px solid black;\">Vulnerability Description</th> </tr> </thead> <tbody>{0}</tbody> </table>";

            var responsable = await this.GetAuditResponsible(audit.AuditId);
            var aplicacion = await _audsApplicationService.Get(audit.ApplicationId);

            foreach (var resp in responsable)
            {
                //if (responsable.Count() > 0)
                //{
                    responsableName = resp.TxtNombre + " " + resp.TxtApellido1 + " " + resp.TxtApellido2;
                //}

                AudsVulnerabilityFilter filter = new AudsVulnerabilityFilter() { AuditId = audit.AuditId, CodeStatus = true };
                var vulnerabilitys = await _audsVulnerabilityService.GetList(filter);
                vulnerabilitys = vulnerabilitys.Where(risk => (risk.RiskId != 4 &&
                                                    risk.RiskId != 5) &&
                                                    (risk.StateReference.StateName != "Mitigated" &&
                                                     risk.StateReference.StateName != "Obsolete")).ToList();

                foreach (var item in vulnerabilitys)
                {
                    var cuerpo = string.Format(lineaTablaInsert,
                                                       item.VulnerabilityId,
                                                       item.VulnerabilityIdControl,
                                                       item.VulnerabilityDescription);

                    cuerpoMailInsert = cuerpoMailInsert + cuerpo;

                }
                if (vulnerabilitys.Count() > 0)
                {
                    cuerpoMailInsert = string.Format(tableInsert, cuerpoMailInsert);
                }
              
                body = string.Format(body, responsableName, aplicacion.ApplicationName, cuerpoMailInsert);
                notification.Add(await LoadMessage(audit, subject, body, resp, 0));
            }
            await UpdateAuditEndFollowUp(audit.AuditId);
            return notification;
        }

        public async Task<List<AudsApplicationDto>> AppliactionsList(List<int> auditList, IList<AudsAuditDto> audits)
        {
            List<AudsApplicationDto> applicationList = new List<AudsApplicationDto>();

            audits = audits.Where(x => auditList.Contains(x.AuditId)).ToList();
            var auditsId = audits.GroupBy(x => x.ApplicationId).Select(x => x.FirstOrDefault()).Select(x => x.ApplicationId).ToList();

            foreach (var item in auditsId)
            {
                var application = await _audsApplicationService.Get(item);
                if (application != null && application.ApplicationId > 0)
                {
                    applicationList.Add(application);
                }
            }
            return applicationList;
        }

        private async Task<List<MessageDto>> ResponsibleNotification(AudsApplicationDto application, List<AudsVulnerabilityDto> vulnerabilityList, IList<AudsAuditDto> audits)
        {

            List<MessageDto> notification = new List<MessageDto>();
            var responsableName = "";

            var subject = Constants.Subject_Responsible_Notification;
            var body = EscapeBrackets(ResourceHandler.GetResource("BodyCreateVulnerability", "MailData"));
            audits = audits.Where(x => x.ApplicationId == application.ApplicationId).ToList();
            var responsable = await this.GetAuditResponsible(audits.LastOrDefault().AuditId);

            foreach (var resp in responsable)
            {
                //if (responsable.Count() > 0)
                //{
                    responsableName = resp.TxtNombre + " " + resp.TxtApellido1 + " " + resp.TxtApellido2;
                //}

                //case "Critical"
                var auditsId = audits.Select(x => x.AuditId).ToList();
                var criticaList = vulnerabilityList.Where(vulnerability => auditsId.Contains(vulnerability.AuditId) &&
                                                                                        vulnerability.RiskId == 5).ToList();

                if (criticaList != null && criticaList.Count() > 0)
                {
                    var mail = await BodyFormat(body, responsableName, subject, application.ApplicationName, criticaList);

                    if (mail.To != null)
                    {
                        mail.To.Add(resp.Empleado.TxtEmail);
                        notification.Add(mail);
                    }
                }

                //case "high"
                var highList = vulnerabilityList.Where(vulnerability => auditsId.Contains(vulnerability.AuditId) &&
                                                                                        vulnerability.RiskId == 4).ToList();
                if (highList != null && highList.Count() > 0)
                {
                    var mail = await BodyFormat(body, responsableName, subject, application.ApplicationName, highList);
                    if (mail.To != null)
                     {
                         mail.To.Add(resp.Empleado.TxtEmail);
                        notification.Add(mail);
                    }
                }

                //case "medium"
                var mediumList = vulnerabilityList.Where(vulnerability => auditsId.Contains(vulnerability.AuditId) &&
                                                                                         vulnerability.RiskId == 3).ToList();

                if (mediumList != null && mediumList.Count() > 0)
                {
                    var mail = await BodyFormat(body, responsableName, subject, application.ApplicationName, mediumList);

                    if (mail.To != null)
                    {
                        mail.To.Add(resp.Empleado.TxtEmail);
                        notification.Add(mail);
                    }

                }
            }

            

            return notification;
        }

        private async Task<MessageDto> BodyFormat(string body, string responsableName, string subject, string application, List<AudsVulnerabilityDto> vulnerabilityList)
        {
            MessageDto notification = new MessageDto();
            var notificationCheck = false;
            string lineaTablaInsert = "<tr><td style=\"border: 1px solid black;\">{0}</td><td style=\"border: 1px solid black;\">{1}</td><td style=\"border: 1px solid black;\">{2}</td></tr>";
            string cuerpoMailInsert = "";

            if (vulnerabilityList != null && vulnerabilityList.Count() > 0)
            {
                var daysTillNotification = await GetDaysUntilNotification(vulnerabilityList.FirstOrDefault().VulnerabilityId);

                foreach (var item in vulnerabilityList)
                {
                    var cuerpo = string.Format(lineaTablaInsert,
                                                       item.VulnerabilityId,
                                                       item.VulnerabilityIdControl,
                                                       item.VulnerabilityDescription);

                    cuerpoMailInsert = cuerpoMailInsert + cuerpo;

                    if (item.DateNewRegister.DaysBetween(DateTime.Now).IsMultiple(daysTillNotification.HowManyDaysUntilNotification))
                    {
                        notificationCheck = true;
                    }
                }

                if (notificationCheck)
                {
                    body = string.Format(body, responsableName, application, daysTillNotification.RiskName, daysTillNotification.HowManyDaysUntilNotification, cuerpoMailInsert);
                    notification = await this.LoadMessage(vulnerabilityList.FirstOrDefault(), subject, body, 2);
                }
                else
                {
                    notification = new MessageDto();
                }
            }
            return notification;
        }

        private async Task<MessageDto> AuditorMitigationDateNotification(AudsApplicationDto application, List<AudsVulnerabilityDto> vulnerabilityList, IList<AudsAuditDto> audits)
        {
            MessageDto notification = new MessageDto();
            string lineaTablaInsert = "<tr><td style=\"border: 1px solid black;\">{0}</td><td style=\"border: 1px solid black;\">{1}</td><td style=\"border: 1px solid black;\">{2}</td></tr>";
            string cuerpoMailInsert = "";

            audits = audits.Where(x => x.ApplicationId == application.ApplicationId).ToList();

            var responsable = await _dataMasterZeusService.GetEmployeeDataById(audits.FirstOrDefault().AuditResolutor);
            var responsableName = "";
            if (true)
            {
                responsableName = responsable.TxtNombre + " " + responsable.TxtApellido1 + " " + responsable.TxtApellido2;
            }

            var auditsId = audits.Select(x => x.AuditId).ToList();
            vulnerabilityList = vulnerabilityList.Where(x => auditsId.Contains(x.AuditId)).ToList();

            foreach (var item in vulnerabilityList)
            {
                var cuerpo = string.Format(lineaTablaInsert,
                                                   item.VulnerabilityId,
                                                   item.VulnerabilityIdControl,
                                                   item.VulnerabilityDescription);

                cuerpoMailInsert = cuerpoMailInsert + cuerpo; 
            }

                var subject = Constants.Subject_Mitigation_Date;
                var Body = EscapeBrackets(ResourceHandler.GetResource("BodyMitigate", "MailData"));

                Body = string.Format(Body, responsableName, application.ApplicationDescription, cuerpoMailInsert);
                notification = await this.LoadMessage(vulnerabilityList.FirstOrDefault(), subject, Body, 1);
            

            return notification;
        }

        private async Task<IList<MessageDto>> VulnerabilityUpdateNotification(int idAudit, List<AudsVulnerabilityDto> vulnerabilityList)
        {
            IList<MessageDto> notification = new List<MessageDto>();
            var audit = await this.GetAudit(idAudit);
            var responsable = await this.GetAuditResponsible(audit.AuditId);

            foreach (var resp in responsable)
            {
                var responsableName = resp.TxtNombre + " " + resp.TxtApellido1 + " " + resp.TxtApellido2;
                var application = await _audsApplicationService.Get(audit.ApplicationId);

                vulnerabilityList = vulnerabilityList.Where(x => x.AuditId == idAudit).ToList();
                string lineaTablaInsert = "<tr><td style=\"border: 1px solid black;\">{0}</td><td style=\"border: 1px solid black;\">{1}</td><td style=\"border: 1px solid black;\">{2}</td></tr>";
                string cuerpoMailInsert = "";
                foreach (var item in vulnerabilityList)
                {
                    var cuerpo = string.Format(lineaTablaInsert,
                                                          item.VulnerabilityId,
                                                          item.VulnerabilityIdControl,
                                                          item.VulnerabilityDescription);

                    cuerpoMailInsert = cuerpoMailInsert + cuerpo;
                }

                var subject = Constants.Subject_Update_Notification;
                var Body = EscapeBrackets(ResourceHandler.GetResource("BodyUpdateState", "MailData"));

                Body = string.Format(Body, responsableName, application.ApplicationName, cuerpoMailInsert);
                notification.Add(await this.LoadMessage(audit, subject, Body, resp, 1));
            }                  

            return notification;
        }

        private async Task<MessageDto> LoadMessage(AudsAuditDto audit, string Subject, string Body, DatosPersonalesDto responsable, int type)
        {
            MessageDto message = new MessageDto();
            string buzon = accedeBuzon();
            List<string> to = new List<string>();

            //foreach (var item in responsable)
            //{
            if (responsable.Empleado.TxtEmail!=null) { 
                to.Add(responsable.Empleado.TxtEmail);
            }
            //}
            to.Add(buzon);

            message = new MessageDto()
            {
                    To = to,
                    Subject = Subject,
                    Body = Body
            };
            
            return message;
        }

        private static string accedeBuzon()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
            .SetBasePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true);
            var configuration = builder.Build();

            var buzon = configuration.GetSection("ruta")["buzon"];
            return buzon;
        }

        #region Utils
        private async Task<MessageDto> LoadMessage(AudsVulnerabilityDto vulnerabilityDto, string Subject, string Body, int type)
        {
            var To = new List<string>();
            MessageDto message = new MessageDto();

            string buzon = accedeBuzon();


            switch (type)
            {
                case 1:
                    if (vulnerabilityDto.VulnerabilityResponsibleReference != null)
                    {
                        To.Add(vulnerabilityDto.VulnerabilityResponsibleReference.Empleado.TxtEmail);
                        To.Add(buzon);
                    }
                    break;
                case 2:       
                    
                    To.Add(buzon);    
                    
                    break;
                default:
                    break;
            }

            if (To != null && To.Count > 0)
            {
                message = new MessageDto() 
                { 
                    To = To,
                    Subject = Subject,
                    Body = Body
                };
            }

            return message;
        }


        private async Task<AudsRiskDto> GetDaysUntilNotification(int vulnerabilityId)
        {
            int daysUntilNotification = -1;

                var vulnerability = await this.GetVulnerability(vulnerabilityId);

                var risk = await this.GetRisk(vulnerability.RiskId);

            return risk;
        }

        private async Task<IList<DatosPersonalesDto>> GetAuditResponsible(int auditId)
        {
            List<DatosPersonalesDto> responsable = new List<DatosPersonalesDto>(); 

            var responsibleList = await GetAuditResponsibles(auditId);

            foreach (var auditResponsible in responsibleList) 
            { 
                responsable.Add(await _dataMasterZeusService.GetEmployeeDataById(auditResponsible.IdEmployee));
            }

            return responsable;
        }

        private async Task<AudsAuditDto> UpdateAuditEndFollowUp(int auditId)
        {

            var audit = await GetAudit(auditId);
            audit.AuditIsNotificationEndSent = true;
            await _audsAuditService.Update(audit);

            return audit;
        }
        #endregion

        #region Getters
        private async Task<AudsAuditDto> GetAudit(int auditId)
        {
            return await this._audsAuditService.Get(auditId);
        }

        private async Task<IList<AudsAuditResponsibleDto>> GetAuditResponsibles(int auditId)
        {
            return await this._audsAuditResponsibleService.GetList(new AudsAuditResponsibleFilter() { AuditId =  auditId, CodeStatus = true } );
        }

        private async Task<IList<AudsVulnerabilityDto>> GetAllVulnerabilities()
        {
            return await this._audsVulnerabilityService.GetList(new AudsVulnerabilityFilter() { CodeStatus = true });
        }

        private async Task<AudsVulnerabilityDto> GetVulnerability(int vulnerabilityId)
        {
            return await this._audsVulnerabilityService.Get(vulnerabilityId);
        }

        private async Task<AudsRiskDto> GetRisk(int riskId)
        {
            return await this._audsRiskService.Get(riskId);
        }

        private string EscapeBrackets(string body)
        {
            string sBodyChanged;
            string sPattern1 = @"{(?!\d)";
            string sPattern2 = @"\D}";
            sBodyChanged = Regex.Replace(body, sPattern1, "{{");
            sBodyChanged = Regex.Replace(sBodyChanged, sPattern2, "}}");

            return sBodyChanged;
        }
        #endregion
    }

    public static class UtilsExtensions
    {
        /// <summary>
        /// Indicates whether an int is multiple of <paramref name="multipleOf"/>.
        /// </summary>
        /// <param name="number">Base Number</param>
        /// <param name="multipleOf"></param>
        /// <returns><see langword="true"/> if is multiple</returns>
        public static bool IsMultiple(this int number, int multipleOf)
        {
            return (number % multipleOf) == 0;
        }

        /// <summary>
        /// Returns the total days that have passed between two dates
        /// </summary>
        /// <param name="startDate">Date from which to start counting</param>
        /// <param name="deadline">Date to count</param>
        /// <returns>An int, representing the amount of full days that have passed</returns>
        public static int DaysBetween(this DateTime startDate, DateTime deadline)
        {
            return (deadline - startDate).Days;
        }
        
    }
}
