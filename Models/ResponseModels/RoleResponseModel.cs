﻿namespace HMSMVC.Models.ResponseModels
{
    public class RoleResponseModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
