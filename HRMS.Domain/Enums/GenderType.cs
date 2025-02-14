using System.ComponentModel;

namespace HRMS.Domain.Enums
{
    public enum GenderType : byte
    {
        [Description("MALE")]
        Male,
        [Description("FEMALE")]
        Female,
        [Description("OTHERS")]
        Others
    }
}
