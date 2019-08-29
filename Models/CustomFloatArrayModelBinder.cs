using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SimpleCalculatorAPI.Models
{
    /**
     *  Custom model binder that allows us to grab semicolon-separated operands from request URI
     *  and bind them to a float array in our controller
     */
    public class CustomFloatArrayModelBinder: IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName)
                .ToString();

            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var elementType = bindingContext.ModelType.GetElementType();

            // We only accept parameters of type "Single", also known as Float
            if (elementType.Name != "Single")
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var converter = TypeDescriptor.GetConverter(elementType);

            // Attempt conversion to float array and bind to model
            try
            {
                var values = value.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => converter.ConvertFromString(Clean(x)))
                    .ToArray();

                var typedValues = Array.CreateInstance(elementType, values.Length);
                values.CopyTo(typedValues, 0);
                bindingContext.Model = typedValues;

                bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
                return Task.CompletedTask;
            }
            catch
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
        }

        // Allows us to optionally wrap operands in parenthesis () or square brackets [] for easier visibility
        private static string Clean(string str)
        {
            return str.Trim('(', ')').Trim('[', ']').Trim();
        }
    }
}
