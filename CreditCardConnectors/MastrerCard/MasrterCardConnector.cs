using Newtonsoft.Json;
using PaymentGatewayService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardConnectors.MasterCard
{
    public class MasterCardConnector : ICreditCardConnector
    {
        const string url = "https://interview.riskxint.com/mastercard/capture_card";
        public async Task<Response> sendChargeRequest(ChargeDetails chargeDetails)
        {
            try
            {
                var payload = new MasterCardPayload(chargeDetails);
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var client = new HttpClient();
                HttpContent content = new StringContent(jsonPayload,Encoding.Unicode, "application/json");
                
                content.Headers.Add("identifier", "Vulf");
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
                var parsedResponse = JsonConvert.DeserializeObject<MasterCardResponse>(content);
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
            else if (visaResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = await visaResponse.Content.ReadAsStringAsync();
                var parsedResponse = JsonConvert.DeserializeObject<MasterCardFailureResponse>(content);
                if (!string.IsNullOrEmpty(parsedResponse.decline_reason))
                {
                    response = new Response(false, parsedResponse.decline_reason);
                }
                else
                {
                    response = new Response(false, "Connectivity error", true);
                }
                
            }
            return response;

        }
    }
}
