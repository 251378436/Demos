namespace VehicleService.Domain.Entity;

public class Customer
{
    public Guid CustomerId { get; set; }
    public string Description { get; set; }
    public CustomerType CustomerType { get; set; }
}