namespace eStavba.Models
{
    public class Bills
    {
        public int Id { get; set; }
        public string BillType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string UserId { get; internal set; }
        public bool IsPaid { get; set; }

    }
}
