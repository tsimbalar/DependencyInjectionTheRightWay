namespace BoringBank.WebPortal.Data
{
    public class Account
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string Title { get; set; }
        public decimal Balance { get; set; }
    }
}