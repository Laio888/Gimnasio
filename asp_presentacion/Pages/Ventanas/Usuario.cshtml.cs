using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using lib_presentacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using asp_presentacion.Pages.Shared;

namespace asp_presentacion.Pages.Ventanas
{
    public class UsuarioModel : PageModel
    {
        private readonly IUsuarioPresentacion? iPresentacion;

        public UsuarioModel(IUsuarioPresentacion iPresentacion)
        {
            try
            {
                this.iPresentacion = iPresentacion;
                Filtro = new Usuario();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Usuario? Actual { get; set; }
        [BindProperty] public Usuario? Filtro { get; set; }
        [BindProperty] public List<Usuario>? Lista { get; set; }

        public virtual void OnGet() => OnPostBtRefrescar();

        public void OnPostBtRefrescar()
        {
            try
            {
                Filtro!.Nombre = Filtro!.Nombre ?? "";
                Accion = Enumerables.Ventanas.Listas;

                var task = this.iPresentacion!.PorNombre(Filtro!); // debe devolver lista
                task.Wait();
                Lista = task.Result;

                Actual = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtNuevo()
        {
            try
            {
                Accion = Enumerables.Ventanas.Editar;
                Actual = new Usuario();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtModificar(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Editar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtGuardar()
        {
            try
            {
                Task<Usuario>? task = null;
                if (Actual!.Id == 0)
                {
                    Actual.RolId = 1; // Usuario por defecto
                    task = this.iPresentacion!.Guardar(Actual!)!;
                }
                else
                {
                    task = this.iPresentacion!.Modificar(Actual!)!;
                }

                task.Wait();
                Actual = task.Result;

                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrarVal(string data)
        {
            try
            {
                OnPostBtRefrescar();
                Accion = Enumerables.Ventanas.Borrar;
                Actual = Lista!.FirstOrDefault(x => x.Id.ToString() == data);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public virtual void OnPostBtBorrar()
        {
            try
            {
                var task = this.iPresentacion!.Borrar(Actual!);
                task.Wait();
                Actual = task.Result;

                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCancelar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCerrar()
        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
                    OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtLogin()
        {
            try
            {
                var task = this.iPresentacion!.Login(Actual!.Correo, Actual!.PasswordHash);
                task.Wait();
                var usuario = task.Result;

                if (usuario != null)
                {
                    HttpContext.Session.SetString("Usuario", usuario.Correo);
                    Response.Redirect("/Index"); 
                }
                else
                {
                    ViewData["Mensaje"] = "Credenciales inválidas";
                }

                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtRegistrar()
        {
            try
            {
                Actual!.RolId = 1; // Usuario por defecto
                var task = this.iPresentacion!.Registrar(Actual!);
                task.Wait();
                var usuario = task.Result;

                if (usuario != null)
                {
                    ViewData["Mensaje"] = "Usuario registrado correctamente";
                }
                else
                {
                    ViewData["Mensaje"] = "Error al registrar usuario";
                }

                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}