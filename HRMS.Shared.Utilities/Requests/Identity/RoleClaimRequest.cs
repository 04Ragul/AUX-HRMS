﻿namespace HRMS.Shared.Utilities.Requests.Identity
{
    public class RoleClaimRequest
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public bool Selected { get; set; }
    }
}
