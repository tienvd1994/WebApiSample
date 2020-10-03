using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace S0D.Infrastructure.ModelBinders
{
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return DateTimeModelBinder.SupportedTypes.Contains(context.Metadata.ModelType)
                ? new BinderTypeModelBinder(typeof(DateTimeModelBinder))
                : null;
        }
    }
}