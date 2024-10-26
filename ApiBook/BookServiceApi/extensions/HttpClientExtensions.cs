using Services.ServicesImpl;
using Services;
using Models;

namespace BookServiceApi.extensions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClientExtensions(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddControllers();
            service.AddSwaggerGen();

       
            service.Configure<BookServiceOptions>(configuration.GetSection(BookServiceOptions.Section));

            
            service.AddHttpClient<IBookService, BookService>(httpClient =>
            {
               
                var bookServiceOptions = new BookServiceOptions();
                configuration.GetSection(BookServiceOptions.Section).Bind(bookServiceOptions);

                // Asigna la dirección base usando la configuración
                httpClient.BaseAddress = new Uri(bookServiceOptions.BaseUrl);
            });

            return service;
        }

    }
}
