using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Shared.Utilities.Enums
{
    public enum UploadType : byte
    {

        [Description(@"Images\ProfilePictures")]
        ProfilePicture,
        [Description(@"Images\PatientProfilePictures")]
        PatientProfilePicture,
        [Description(@"Documents")]
        Document
    }
}
