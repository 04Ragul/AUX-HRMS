using System.ComponentModel;
using System.Reflection;

namespace HRMS.Shared.Constants.Permission
{
    public static class Permissions
    {
        [DisplayName("JobCategory")]
        [Description("JobCategory Permissions")]
        public static class JobCategory
        {
            public const string View = "Permissions.JobCategory.View";
            public const string Create = "Permissions.JobCategory.Create";
            public const string Edit = "Permissions.JobCategory.Edit";
            public const string Delete = "Permissions.JobCategory.Delete";
            public const string Export = "Permissions.JobCategory.Export";
            public const string Search = "Permissions.JobCategory.Search";
        }


        [DisplayName("JobLocation")]
        [Description("JobLocation Permissions")]
        public static class JobLocation
        {
            public const string View = "Permissions.JobLocation.View";
            public const string Create = "Permissions.JobLocation.Create";
            public const string Edit = "Permissions.JobLocation.Edit";
            public const string Delete = "Permissions.JobLocation.Delete";
            public const string Export = "Permissions.JobLocation.Export";
            public const string Search = "Permissions.JobLocation.Search";
        }

        [DisplayName("Department")]
        [Description("Department Permissions")]
        public static class Department
        {
            public const string View = "Permissions.Department.View";
            public const string Create = "Permissions.Department.Create";
            public const string Edit = "Permissions.Department.Edit";
            public const string Delete = "Permissions.Department.Delete";
            public const string Export = "Permissions.Department.Export";
            public const string Search = "Permissions.Department.Search";
        }
        [DisplayName("Designation")]
        [Description("Designation Permissions")]
        public static class Designation
        {
            public const string View = "Permissions.Designation.View";
            public const string Create = "Permissions.Designation.Create";
            public const string Edit = "Permissions.Designation.Edit";
            public const string Delete = "Permissions.Designation.Delete";
            public const string Export = "Permissions.Designation.Export";
            public const string Search = "Permissions.Designation.Search";
        }
        [DisplayName("Employee")]
        [Description("Employee Permissions")]
        public static class Employee
        {
            public const string View = "Permissions.Employee.View";
            public const string Create = "Permissions.Employee.Create";
            public const string Edit = "Permissions.Employee.Edit";
            public const string Delete = "Permissions.Employee.Delete";
            public const string Export = "Permissions.Employee.Export";
            public const string Search = "Permissions.Employee.Search";
        }

       
        [DisplayName("Users")]
        [Description("Users Permissions")]
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string Export = "Permissions.Users.Export";
            public const string Search = "Permissions.Users.Search";
        }

        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string Search = "Permissions.Roles.Search";
        }

        [DisplayName("Role Claims")]
        [Description("Role Claims Permissions")]
        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
            public const string Search = "Permissions.RoleClaims.Search";
        }


        [DisplayName("Preferences")]
        [Description("Preferences Permissions")]
        public static class Preferences
        {
            public const string ChangeLanguage = "Permissions.Preferences.ChangeLanguage";

            //TODO - add permissions
        }

        [DisplayName("Dashboards")]
        [Description("Dashboards Permissions")]
        public static class Dashboards
        {
            public const string View = "Permissions.Dashboards.View";
        }

        [DisplayName("Hangfire")]
        [Description("Hangfire Permissions")]
        public static class Hangfire
        {
            public const string View = "Permissions.Hangfire.View";
        }

        [DisplayName("Audit Trails")]
        [Description("Audit Trails Permissions")]
        public static class AuditTrails
        {
            public const string View = "Permissions.AuditTrails.View";
            public const string Export = "Permissions.AuditTrails.Export";
            public const string Search = "Permissions.AuditTrails.Search";
        }

        /// <summary>
        /// Returns a list of Permissions.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            List<string> permissions = new();
            foreach (FieldInfo? prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                object? propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    permissions.Add(propertyValue.ToString()!);
                }
            }
            return permissions;
        }
    }
}
