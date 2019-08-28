using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SimpleCalculatorAPI.Models
{
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

            if (elementType.Name != "Single")
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var converter = TypeDescriptor.GetConverter(elementType);

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

        private static string Clean(string str)
        {
            return str.Trim('(', ')').Trim('[', ']').Trim();
        }
    }
}
