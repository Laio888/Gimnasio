using lib_presentacion.Implementaciones;
using lib_presentacion.Interfaces;

namespace asp_presentacion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            services.AddScoped<IUsuarioPresentacion, UsuarioPresentacion>();
            services.AddScoped<IRolPresentacion, RolPresentacion>();
            services.AddScoped<IPermisoPresentacion, PermisoPresentacion>();
            services.AddScoped<IRolPermisoPresentacion, RolPermisoPresentacion>();
            services.AddScoped<ICoachPresentacion, CoachPresentacion>();
            services.AddScoped<IRutinaPresentacion, RutinaPresentacion>();
            services.AddScoped<IEjercicioPresentacion, EjercicioPresentacion>();
            services.AddScoped<IEjercicioRutinaPresentacion, EjercicioRutinaPresentacion>();
            services.AddScoped<IAsignacionRutinaPresentacion, AsignacionRutinaPresentacion>();
            services.AddScoped<IHorarioPresentacion, HorarioPresentacion>();
            services.AddScoped<IClasePresentacion, ClasePresentacion>();
            services.AddScoped<IReservaClasePresentacion, ReservaClasePresentacion>();
            services.AddScoped<IMensajePresentacion, MensajePresentacion>();
            services.AddScoped<INotificacionPresentacion, NotificacionPresentacion>();
            services.AddScoped<IPagoPresentacion, PagoPresentacion>();
            services.AddScoped<IAuditoriaPresentacion, AuditoriaPresentacion>();

            // Infraestructura básica
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddRazorPages();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.MapRazorPages();        }
    }
}