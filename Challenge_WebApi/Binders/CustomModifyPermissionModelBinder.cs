using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Challenge_WebApi.Binders
{
    public class CustomModifyPermissionModelBinderProvider : IModelBinderProvider
    {
        private readonly IList<IInputFormatter> formatters;
        private readonly IHttpRequestStreamReaderFactory readerFactory;

        public CustomModifyPermissionModelBinderProvider(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
        {
            this.formatters = formatters;
            this.readerFactory = readerFactory;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(ViewModel.ViewModelChangePermission))
                return new CustomModifyPermissionModelBinder(formatters, readerFactory);

            return null;
        }
    }
    public class CustomModifyPermissionModelBinder : IModelBinder
    {

        private BodyModelBinder defaultBinder;

        public CustomModifyPermissionModelBinder(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory) // : base(formatters, readerFactory)
        {
            defaultBinder = new BodyModelBinder(formatters, readerFactory);
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // callinng the default body binder
            await defaultBinder.BindModelAsync(bindingContext);

            if (bindingContext.Result.IsModelSet)
            {
                var data = bindingContext.Result.Model as ViewModel.ViewModelChangePermission;
                if (data != null)
                {
                    var intValue = bindingContext.ValueProvider.GetValue("id").FirstValue;
                    var oldvalue = bindingContext.ValueProvider.GetValue("OldPermission").FirstValue;
                    var newvalue = bindingContext.ValueProvider.GetValue("NewPermission").FirstValue;
                    int newInt = 0;
                    if (int.TryParse(intValue, out newInt) && !String.IsNullOrEmpty(data.OldPermission) && !String.IsNullOrEmpty(data.NewPermission) && data.NewPermission.Length >= 3)
                    {
                        data.Id = newInt;
                        bindingContext.Result = ModelBindingResult.Success(data);
                    }
                    else if (!int.TryParse(intValue, out newInt) || String.IsNullOrEmpty(data.OldPermission) || String.IsNullOrEmpty(data.NewPermission) || data.NewPermission.Length < 3)
                    { 
                        bindingContext.Result = ModelBindingResult.Failed();
                    }
                }
            }
        }
    }
}
