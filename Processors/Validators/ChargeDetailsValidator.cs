using CreditCardConnectors;
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
                return AreValuesNull(chargeDetails)
                     && IsCreditCardCompanyNameValid(chargeDetails)
                     && IsFullNameValid(chargeDetails);
            }
            else
                return false;
        }
        private bool AreValuesNull(ChargeDetails chargeDetails)
        {
            return !string.IsNullOrWhiteSpace(chargeDetails.fullName)
                      && !string.IsNullOrWhiteSpace(chargeDetails.creditCardCompany)
                      && !string.IsNullOrWhiteSpace(chargeDetails.cvv)
                      && chargeDetails.amount > 0;
        }
        private bool IsCreditCardCompanyNameValid(ChargeDetails chargeDetails)
        {
            return CreditCardCompanies.CreditCards.Contains(chargeDetails.creditCardCompany);
        }

        private bool IsFullNameValid(ChargeDetails chargeDetails)
        {
            return chargeDetails.fullName.Contains(" ");
        }
    }
   
}
