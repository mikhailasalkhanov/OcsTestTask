namespace Ordering.Domain;

public enum OrderStatus
{
    New,
    Pending,
    Paid,
    SentForDelivery,
    Delivered,
    Completed
}