using PaymentGatewayService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardConnectors.Visa
{
    internal class VisaPayload
    {
        public string fullName { get; set; }
        public string number { get; set; }
        public string expiration { get; set; }  
        public string cvv { get; set; }
        public decimal totalAmount { get; set; }

        public VisaPayload(ChargeDetails chargeDetails)
        {
            fullName = chargeDetails.fullName;
            number = chargeDetails.creditCardNumber;
            expiration = chargeDetails.expirationDate;
            cvv = chargeDetails.cvv;
            totalAmount = chargeDetails.amount;
        }
    }
}
