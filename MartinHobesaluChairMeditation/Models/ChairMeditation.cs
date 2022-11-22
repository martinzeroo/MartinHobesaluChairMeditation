namespace MartinHobesaluChairMeditation.Models
{
    // Id=1, Tone=Blue ,OrerAmount=10, CompleteAmount=0, Price = 15
    public class ChairMeditation
    {
        public int Id { get; set; } 

        public string? Tone { get; set; }

        public int OrderAmount { get; set; }

        public int CompleteAmount { get; set; }

        public int Price { get; set; }

    }
}
