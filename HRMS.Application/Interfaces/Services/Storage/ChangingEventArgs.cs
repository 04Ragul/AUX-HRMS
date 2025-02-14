using System.Diagnostics.CodeAnalysis;

namespace HRMS.Application.Interfaces.Services.Storage
{
    [ExcludeFromCodeCoverage]
    public class ChangingEventArgs : ChangedEventArgs
    {
        public bool Cancel { get; set; }
    }
}
