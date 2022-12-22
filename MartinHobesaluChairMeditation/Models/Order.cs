using System.ComponentModel.DataAnnotations;

namespace MartinHobesaluChairMeditation.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string? Tone { get; set; }
        [Display(Name = "Completed Amount")]
        [Range(25, 50)]
        public int CompletedAmount { get; set; } = 0;
        [Display(Name = "Order Amount")]
        [Range (25,50)]
        public int OrderAmount { get; set; }
        [Display(Name = "Time Of Arrival")]
        public DateTime TimeOfArrival { get; set; }

        public int Price { get; set; }

    }
}
