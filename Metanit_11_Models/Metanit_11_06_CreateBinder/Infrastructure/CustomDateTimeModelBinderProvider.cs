using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_11_06_CreateBinder.Infrastructure
{
    public class CustomDateTimeModelBinderProvider : IModelBinderProvider
    {
        ILoggerFactory log = AppContext.GetData[log];

        private readonly IModelBinder binder =
            new CustomDateTimeModelBinder(new SimpleTypeModelBinder(typeof(DateTime),log));

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(DateTime) ? binder : null;
        }
    }
}
