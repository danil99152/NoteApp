using System;
using System.Globalization;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace NoteApp.GeneratorProviders
{
    public class GeneratorProvider : IValueProvider
    {
        public bool ContainsPrefix(string prefix)
        {
            return string.Compare("generator", prefix, true) == 0;
        }

        public ValueProviderResult GetValue(string key)
        {
            var type = HttpContext.Current.Request.Params["Type"];
            var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (var item in assemblies)
            {
                var neededassembly = Assembly.Load(item.FullName);
                var neededclass = Type.GetType(Assembly.CreateQualifiedName("GeneratorLibrary", type));
                ConstructorInfo ci = neededclass.GetConstructor(new Type[] { });
                object Obj = ci.Invoke(new object[] { });
                return new ValueProviderResult(Obj, null, CultureInfo.CurrentCulture);
            }
            return null;
        }
    }
}