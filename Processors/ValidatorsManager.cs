using PaymentGatewayService.Data;
using Processors.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processors
{
    public class ValidatorsManager
    {
        public static ValidatorsManager Instance { get; private set; }

        private Dictionary<Type, IValidator> _validators;

        static ValidatorsManager()
        {
            Instance = new ValidatorsManager(); 
        }
        private ValidatorsManager()
        {
            _validators = new Dictionary<Type, IValidator>();
            _validators.Add(typeof(ChargeDetails), new ChargeDetailsValidator());
        }

        public bool Validate<T>(T objectToValidate)
        {
            if(objectToValidate == null)
            {
                return false;
            }
            else
            {
                var type = objectToValidate.GetType();
                if (_validators.ContainsKey(type))
                {
                    var validator = _validators[type];
                    return validator.IsValid(objectToValidate);
                }
                else
                {
                    return false;
                }
            }
           
        }
    }
}
