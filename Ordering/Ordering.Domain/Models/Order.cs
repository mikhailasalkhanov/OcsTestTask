namespace Ordering.Domain.Models;

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
        if (IsDeleted)
        {
            return false;
        }
        
        return Status is not 
              (OrderStatus.Completed 
            or OrderStatus.Delivered
            or OrderStatus.SentForDelivery);
    }

    public bool CanBeEdited()
    {
        if (IsDeleted)
        {
            return false;
        }
        
        return Status is not
              (OrderStatus.Completed
            or OrderStatus.Delivered
            or OrderStatus.SentForDelivery
            or OrderStatus.Paid);
    }

    public bool TryUpdateFrom(Order order)
    {
        if (!CanBeEdited())
        {
            return false;
        }

        Status = order.Status;
        UpdateLinesFrom(order.Lines);

        return true;
    }

    private void UpdateLinesFrom(List<OrderLine> lines)
    {
        foreach (var line in lines)
        {
            var matchingLine = FindMatchingLine(line);
            if (matchingLine is not null)
            {
                line.Id = matchingLine.Id;
            }
        }
        
        Lines = lines;
    }
    
    private OrderLine? FindMatchingLine(OrderLine line)
    {
        return Lines.FirstOrDefault(l => l.ProductId == line.ProductId);
    }
}