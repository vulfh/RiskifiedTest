using PaymentGatewayService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardConnectors.MasterCard
{
    internal class MasterCardPayload
    {
        public string first_name { get; set; }

        public string last_name { get; set;}
        public string card_number { get; set; }
        public string expiration { get; set; }  
        public string cvv { get; set; }
        public decimal charge_amount { get; set; }

        public MasterCardPayload(ChargeDetails chargeDetails)
        {
            var nameParts = chargeDetails.fullName.Split(" ",StringSplitOptions.RemoveEmptyEntries);
            first_name = nameParts[0];
            last_name = nameParts[1];
            card_number = chargeDetails.creditCardNumber;
            expiration = chargeDetails.expirationDate;
            cvv = chargeDetails.cvv;
            charge_amount = chargeDetails.amount;
        }
    }
}
