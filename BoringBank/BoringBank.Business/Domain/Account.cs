namespace BoringBank.Business.Domain
{
    public class Account
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public int CustomerId { get; set; }

        public int Id { get; set; }
    }
}