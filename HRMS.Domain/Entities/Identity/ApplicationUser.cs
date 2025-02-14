using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HRMS.Domain.Interfaces.Chat;
using HRMS.Domain.Contract;
using HRMS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Entities.Chat;
using HRMS.Domain.Entities.Features;

namespace HRMS.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>, IChatUser, IAuditableEntity<int>
    {
        public string Name { get; set; }
        public GenderType Gender { get; set; }
        public string? CreatedBy { get; set; }

        [Column(TypeName = "text")]
        public string? ProfilePictureDataUrl { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<ChatHistory<ApplicationUser>> ChatHistoryFromUsers { get; set; }
        public virtual ICollection<ChatHistory<ApplicationUser>> ChatHistoryToUsers { get; set; }
        public string? IPAddress { get; set; }

        public ApplicationUser()
        {
            ChatHistoryFromUsers = new HashSet<ChatHistory<ApplicationUser>>();
            ChatHistoryToUsers = new HashSet<ChatHistory<ApplicationUser>>();
        }
    }
}
