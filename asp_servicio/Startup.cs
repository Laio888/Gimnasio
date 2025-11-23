using asp_servicio.Controllers;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace asp_servicio
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
            services.Configure<KestrelServerOptions>(x => {
                x.AllowSynchronousIO = true;
            });
            services.Configure<IISServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();

            // Repositorios base
            services.AddScoped<IConexion, Conexion>();
            services.AddScoped<TokenAplicacion, TokenAplicacion>();

            services.AddScoped<IUsuarioAplicacion, UsuarioAplicacion>();
            services.AddScoped<IRolAplicacion, RolAplicacion>();
            services.AddScoped<IPermisoAplicacion, PermisoAplicacion>();
            services.AddScoped<IRolPermisoAplicacion, RolPermisoAplicacion>();
            services.AddScoped<ICoachAplicacion, CoachAplicacion>();
            services.AddScoped<IRutinaAplicacion, RutinaAplicacion>();
            services.AddScoped<IEjercicioAplicacion, EjercicioAplicacion>();
            services.AddScoped<IEjercicioRutinaAplicacion, EjercicioRutinaAplicacion>();
            services.AddScoped<IAsignacionRutinaAplicacion, AsignacionRutinaAplicacion>();
            services.AddScoped<IHorarioAplicacion, HorarioAplicacion>();
            services.AddScoped<IClaseAplicacion, ClaseAplicacion>();
            services.AddScoped<IReservaClaseAplicacion, ReservaClaseAplicacion>();
            services.AddScoped<IMensajeAplicacion, MensajeAplicacion>();
            services.AddScoped<INotificacionAplicacion, NotificacionAplicacion>();
            services.AddScoped<IPagoAplicacion, PagoAplicacion>();
            services.AddScoped<IAuditoriaAplicacion, AuditoriaAplicacion>();

            services.AddScoped<TokenController, TokenController>();

            // CORS
            services.AddCors(o => o.AddDefaultPolicy(b => b.AllowAnyOrigin()));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseRouting();
            app.UseCors();
            app.MapControllers();
            app.Run();
        }
    }
}