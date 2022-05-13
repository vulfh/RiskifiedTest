using PaymentGatewayService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardConnectors
{
    public interface ICreditCardConnector
    {
        Task<Response> sendChargeRequest(ChargeDetails chargeDetails);
    }
}
