using Microsoft.EntityFrameworkCore;

namespace DrillBlockManager.Services
{
    public class ServiceBase
    {
        /// <summary>
        /// Поставщик сервисов. Может быть использован в контроллере для получения специфических сервисов
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }
        protected ILogger Logger { get; }


        public ServiceBase(IServiceProvider serviceProvider,
                                    ILogger<ServiceBase> logger)
        {
            Logger = logger;
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Получить контекст базы
        /// </summary>
        /// <typeparam name="TContext">Тип контекста</typeparam>
        /// <returns>Инстанс контекста базы</returns>
        protected TContext GetContext<TContext>()
            where TContext : DbContext
        {
            return ServiceProvider.GetRequiredService<TContext>();
        }
    }
}
