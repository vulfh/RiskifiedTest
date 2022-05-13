namespace PaymentGatewayService.Data
{
    public class ChargeDetails
    {
        public string fullName { get; set; }
        public string creditCardNumber { get; set; }
        public string creditCardCompany { get; set; }
        public string expirationDate { get; set; }
        public string cvv { get; set; }
        public decimal amount { get; set; }
    }
}
