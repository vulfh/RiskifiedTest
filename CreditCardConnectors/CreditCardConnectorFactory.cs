using CreditCardConnectors.MasterCard;
using CreditCardConnectors.Visa;

namespace CreditCardConnectors
{
    public static class  CreditCardConnectorFactory
    {
        public static ICreditCardConnector Create(string creditCompanyName)
        {
            ICreditCardConnector connector = null; ;
            switch (creditCompanyName)
            {
                case CreditCardCompanies.Visa:
                    connector = new VisaConnector();
                    break;
                case CreditCardCompanies.MasterCard:
                    connector = new MasterCardConnector();
                    break;
            }

            return connector;
        }

    }
}