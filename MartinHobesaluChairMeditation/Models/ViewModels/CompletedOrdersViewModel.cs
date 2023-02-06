namespace MartinHobesaluChairMeditation.Models.ViewModels
{
    public class CompletedOrdersViewModel
    {
        public ICollection<Order> CompletedOrders { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCompletedOrders { get; set; }
    }
}
