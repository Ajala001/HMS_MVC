﻿namespace HMSMVC.Models.ResponseModels
{
    public class ServiceResponseModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
    }
}
