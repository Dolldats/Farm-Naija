namespace Application.DTOs.Order;

public class CreateOrderDto
{
    public int CustomerId { get; set; }

    public int AddressId { get; set; }

    public List<CreateOrderItemDto> Items { get; set; } = new();
}