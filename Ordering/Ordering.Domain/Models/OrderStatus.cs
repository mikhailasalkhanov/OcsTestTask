namespace Ordering.Domain.Models;

public enum OrderStatus
{
    New,
    Pending,
    Paid,
    SentForDelivery,
    Delivered,
    Completed
}