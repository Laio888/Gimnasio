using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class RolPrueba
    {
        private readonly IConexion? iConexion;
        private List<Rol>? lista;
        private Rol? entidad;

        public RolPrueba()
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
            this.lista = this.iConexion!.Rol!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = RolNucleo.Rol()!;
            this.iConexion!.Rol!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Descripcion = "Rol modificado en pruebas";
            var entry = this.iConexion!.Entry<Rol>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Rol!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}