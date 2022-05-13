using PaymentGatewayService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processors.Validators
{
    public class ChargeDetailsValidator:IValidator
    {
        public bool IsValid(object chargeDetailsToValidate)

        {
            var chargeDetails = chargeDetailsToValidate as ChargeDetails;
            if (chargeDetails != null)
            {
                return !string.IsNullOrWhiteSpace(chargeDetails.fullName)
                   && !string.IsNullOrWhiteSpace(chargeDetails.creditCardCompany)
                   && !string.IsNullOrWhiteSpace(chargeDetails.cvv)
                   && chargeDetails.amount > 0;
            }
            else
                return false;
        }
    }
}
