using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC_CSharpTurk.Models.ModelBinder
{
    public class EtiketModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            int EtiketId = (int) bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "[EtiketId]").ConvertTo(typeof(int));
            string EtiketAdi = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "[EtiketAdi]").AttemptedValue;
            string EtiketRadi = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "[EtiketRadi]").AttemptedValue;

            return new Etiket { EtiketAdi = EtiketAdi, EtiketId = EtiketId, EtiketRadi = EtiketRadi };
        }
    }
}