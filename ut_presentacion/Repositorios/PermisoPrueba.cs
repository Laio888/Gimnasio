using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class PermisoPrueba
    {
        private readonly IConexion? iConexion;
        private List<Permiso>? lista;
        private Permiso? entidad;

        public PermisoPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Borrar());
        }

        public bool Listar()
        {
            this.lista = this.iConexion!.Permiso!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = PermisoNucleo.Permiso()!;
            this.iConexion!.Permiso!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Descripcion = "Permiso modificado en pruebas";
            var entry = this.iConexion!.Entry<Permiso>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Permiso!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}