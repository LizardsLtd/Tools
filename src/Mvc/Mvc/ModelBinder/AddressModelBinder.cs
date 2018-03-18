using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Picums.Data.Types;

namespace Picums.Mvc.ApplicationServices
{
    public sealed class AddressModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var location = this.CreateAddress(bindingContext);
            bindingContext.Result = ModelBindingResult.Success(location);

            return Task.CompletedTask;
        }

        private Address CreateAddress(ModelBindingContext bindingContext)
        {
            var houseNumber = GetProvidedValue(bindingContext, "HouseNumber");
            var streetName = GetProvidedValue(bindingContext, "StreetName");
            var district = GetProvidedValue(bindingContext, "District");
            var city = GetProvidedValue(bindingContext, "City");
            var province = GetProvidedValue(bindingContext, "Province");
            var country = GetProvidedValue(bindingContext, "Country");
            var postCode = GetProvidedValue(bindingContext, "PostCode");

            return new Address(
                houseNumber
                , streetName
                , district
                , city
                , postCode
                , province
                , country);
        }

        private string GetProvidedValue(ModelBindingContext bindingContext, string key)
            => bindingContext
                    .ValueProvider
                    .GetValue($"{bindingContext.ModelName}.{key}")
                    .FirstValue;
    }
}