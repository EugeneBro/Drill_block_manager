using System.Reflection;
using DrillBlockManager.Services;

namespace DrillBlockManager.Helpers
{
    public static class ServicesDIExtension
    {
        private static readonly Type ServiceBaseType = typeof(ServiceBase);

        /// <summary>
        /// Производит регистрацию используемых сервисов
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection RegistrateServices(this IServiceCollection serviceCollection)
        {

            // Добавляем сервисы по базовому классу
            foreach (var serviceType in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsClass && type.IsSubclassOf(ServiceBaseType)))
            {
                serviceCollection.AddTransient(serviceType);
            }

            return serviceCollection;
        }
    }
}
