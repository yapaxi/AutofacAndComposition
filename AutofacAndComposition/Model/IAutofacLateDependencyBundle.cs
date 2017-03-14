using Autofac;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAndComposition.Model
{
    public interface IAutofacLateDependencyBundle
    {
        void Register(ContainerBuilder builder);
    }

    public static class Extentions
    {
        private const string CONFIG_NAME = "____config_for_late_DI";

        public static void SetLateDependencyBundle(this JobDataMap map, IAutofacLateDependencyBundle bundle)
        {
            map.Add(CONFIG_NAME, bundle);
        }

        public static IAutofacLateDependencyBundle TryGetLateDependencyBundle(this JobDataMap map)
        {
            object bundle;

            if (map.TryGetValue(CONFIG_NAME, out bundle))
            {
                var result = bundle as IAutofacLateDependencyBundle;
                if (result == null)
                {
                    throw new InvalidOperationException($"Expected object of type \"{nameof(IAutofacLateDependencyBundle)}\" but given \"{result?.GetType()?.FullName ?? "NULL"}\"");
                }
                return result;
            }

            return null;
        }
    }
}
