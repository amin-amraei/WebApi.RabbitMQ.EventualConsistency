namespace SharedKernel
{
    // Event که بعد از ایجاد سفارش ارسال می‌شود
    public class OrderCreatedEvent
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
    }

}
