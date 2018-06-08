namespace BeautySalonModel
{
    public class OrderService
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int serviceId { get; set; }
        public int count { get; set; }

        public virtual Order order { get; set; }
        public virtual Service service { get; set; }
    }
}
