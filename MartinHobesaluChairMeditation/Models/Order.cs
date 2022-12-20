namespace MartinHobesaluChairMeditation.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string? Tone { get; set; }

        public int CompletedAmount { get; set; } = 0;

        public int OrderAmount { get; set; }

        public DateTime TimeOfArrival { get; set; }

        public int Price { get; set; }

    }
}
