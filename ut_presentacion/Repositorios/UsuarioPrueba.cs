using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class UsuarioPrueba
    {
        private readonly IConexion? iConexion;
        private List<Usuario>? lista;
        private Usuario? entidad;

        public UsuarioPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());     // 1. Crear usuario
            Assert.AreEqual(true, Listar());      // 2. Confirmar que existe
            Assert.AreEqual(true, Login());       // 3. Probar login con datos iniciales
            Assert.AreEqual(true, Registrar());   // 4. Registrar (ej. fecha de registro)
            Assert.AreEqual(true, Modificar());   // 5. Cambiar atributos
            Assert.AreEqual(true, Borrar());      // 6. Eliminar al final
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.Usuario!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = UsuarioNucleo.Usuario()!;
            this.iConexion!.Usuario!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return this.entidad.Id > 0;
        }

        public bool Modificar()
        {
            this.entidad!.PasswordHash = "nuevoHash";
            var entry = this.iConexion!.Entry<Usuario>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Usuario!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Login()
        {
            var usuario = this.iConexion!.Usuario!
                .FirstOrDefault(u => u.Correo == this.entidad!.Correo);
            return usuario != null;
        }

        public bool Registrar()
        {
            // Reutilizamos la misma entidad en lugar de crear un duplicado
            this.entidad!.FechaRegistro = DateTime.Now;
            var entry = this.iConexion!.Entry<Usuario>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return this.entidad.Id > 0;
        }
    }
}