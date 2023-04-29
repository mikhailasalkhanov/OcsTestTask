namespace Ordering.Domain;

public class Order
{
    public Guid Id { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsDeleted { get; set; }
    public List<OrderLine> Lines { get; private set; }

    private Order()
    {
    }

    public Order(Guid id, List<OrderLine> lines)
    {
        Id = id;
        Status = OrderStatus.New;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
        Lines = lines;
    }
    public bool CanBeDeleted()
    {
        return !IsDeleted && Status is not 
              (OrderStatus.Completed 
            or OrderStatus.Delivered
            or OrderStatus.SentForDelivery);
    }

    public bool CanBeEdited()
    {
        return !IsDeleted && Status is not
              (OrderStatus.Completed
            or OrderStatus.Delivered
            or OrderStatus.SentForDelivery
            or OrderStatus.Paid);
    }

    public bool TryEditFrom(Order order)
    {
        if (!CanBeEdited())
        {
            return false;
        }

        Status = order.Status;
        MapLinesIds(order);
        Lines = order.Lines;
        
        return true;
    }

    private void MapLinesIds(Order order)
    {
        foreach (var line in order.Lines)
        {
            var existingLine = Lines.FirstOrDefault(l => l.ProductId == line.ProductId);
            if (existingLine is not null)
            {
                line.Id = existingLine.Id;
            }
        }
    }
}