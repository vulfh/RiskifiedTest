using Newtonsoft.Json;
using PaymentGatewayService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardConnectors.Visa

{
    public class VisaConnector : ICreditCardConnector
    {
        const string url = "https://interview.riskxint.com/visa/api/chargeCard";
        public async Task<Response> sendChargeRequest(ChargeDetails chargeDetails)
        {
            try
            {
                var payload = new VisaPayload(chargeDetails);
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var client = new HttpClient();
                HttpContent content = new StringContent(jsonPayload);
                content.Headers.Add("identifier", "Vulf");
                content.Headers.Add("Content-Type", "application/json");
                var cdResponse = await client.PostAsync(url, content);
                var response = await ParseVisaResponse(cdResponse);
                return response;
            }
            catch (Exception ex)
            {
                return new Response(false, ex.Message,true);
            }
        }

        private async Task<Response> ParseVisaResponse(HttpResponseMessage visaResponse)
        {
            Response response = null;
            if(visaResponse.StatusCode == HttpStatusCode.OK)
            {
                var content = await visaResponse.Content.ReadAsStringAsync();
                var parsedResponse = JsonConvert.DeserializeObject<VisaResponse>(content);
                if (parsedResponse != null)
                {
                    if(parsedResponse.chargeResult == "Success")
                    {
                        response = new Response(true, "OK");
                    }
                    else
                    {
                        response = new Response(false, parsedResponse.resultReason);
                    }
                }
                else
                {
                    response = new Response(false, "Unexpected response",true);
                }
            }
            else
            {
                response = new Response(false, "Connectivity error",true);
            }
            return response;

        }
    }
}
