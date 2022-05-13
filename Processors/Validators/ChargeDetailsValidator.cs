using CreditCardConnectors;
using PaymentGatewayService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private bool IsExpirationValid(ChargeDetails chargeDetails)
        {
            if (!chargeDetails.expirationDate.Contains("/"))
                return false;

            if(chargeDetails.expirationDate.Length!=5)
                return false;

            var parts = chargeDetails.expirationDate.Split('/');
            if(parts.Length != 2)
                return false;

            if(!int.TryParse(parts[0], out int month))
                return false;
            if (!int.TryParse(parts[1], out int year))
                return false;

            if(month <1 && month > 12)
                return false;

            if(year < DateTime.Now.Year)
                return false;

            return true;
        }
    }
   
}
