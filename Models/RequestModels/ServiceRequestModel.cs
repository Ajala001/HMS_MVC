using HMSMVC.Entities;
using HMSMVC.Entity;

namespace HMSMVC.Models.RequestModels
{
    public class ServiceRequestModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
    }
}
