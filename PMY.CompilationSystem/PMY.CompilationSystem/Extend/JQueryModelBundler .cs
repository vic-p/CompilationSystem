using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMY.CompilationSystem.Extend
{
    public class JQueryModelBundler : DefaultModelBinder//这里要集成DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType.IsEnumerableType()) //IsEnumerableType()是System.Type的一个扩展方法
            {
                var key = bindingContext.ModelName + "[]";
                var valueResult = bindingContext.ValueProvider.GetValue(key);
                if (valueResult != null && !string.IsNullOrEmpty(valueResult.AttemptedValue))
                {
                    bindingContext.ModelName = key;
                }
            }
            return base.BindModel(controllerContext, bindingContext);
        }

    }

}