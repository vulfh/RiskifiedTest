using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardConnectors
{
    public static class Utils
    {
        public static  async Task<T>  RetryActionAsync<T>(Func<Task<T>> actionToRetry,Func<T,bool> wasSuccesfulAttempt,int maxRetries)
        {
            int retryCount = 0;
            bool success = false;
            T result = default(T);

            do
            {
                retryCount++;
                result = await actionToRetry();
                success = wasSuccesfulAttempt(result);
                if (!success)
                    Thread.Sleep((int)Math.Pow(retryCount,2)*1000);

            }while(retryCount < maxRetries && !success);

            return result;
        }
    }
}
