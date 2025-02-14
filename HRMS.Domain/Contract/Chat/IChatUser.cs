using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Domain.Interfaces.Chat
{
    public interface IChatUser
    {
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string ProfilePictureDataUrl { get; set; }
    }
}
