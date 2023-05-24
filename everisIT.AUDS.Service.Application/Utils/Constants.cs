namespace everisIT.AUDS.Service.Application.Utils
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class Constants
    {
        public static readonly string idApplication = "162";
        public static readonly int idApplicationInt = 162;
        public static readonly string Subject_Mitigation_Date = "MAIL_AUDITOR_MITIGATION_DATE_NOTIFICATION";
        public static readonly string Body_Mitigation_Date = "Vulnerability \"{0}\" has passed its mitigation date";

        public static readonly string Subject_Responsible_Notification = "MAIL_RESPONSIBLE_NOTIFICATION";
        public static readonly string Body_Responsible_Notification = "{0} Days have passed since the vulnerability \"{1}\" was created";

        public static readonly string Subject_Update_Notification = "MAIL_UPDATE_VULNETABILITY_NOTIFICATION";
        public static readonly string Body_Update_Notification = "Vulnerabilities have been modified in the audit \"{0}\" ";

        public static readonly string Subject_Escalation_Notification = "MAIL_LOCK_ESCALATION_NOTIFICATION";
        public static readonly string Subject_End_Follow_Up = "MAIL_END_FOLLOW_UP";

        public enum MailType
        {
	        ALL = 0,
            NEW_ABSENCE,
            MODIFY_ABSENCE,
            DELETE_ABSENCE,
            CANCELLATION_REQUEST,
            APPROVE_ABSENCE,
            REJECT_ABSENCE,
            MAIL_SUBJECT_CONFIRM_ABSENCE_CANCELLATION,
            MAIL_SUBJECT_DISMISS_ABSENCE_CANCELLATION,
        }
    }
}
