using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class RolPermisoPrueba
    {
        private readonly IConexion? iConexion;
        private List<RolPermiso>? lista;
        private RolPermiso? entidad;

        public RolPermisoPrueba()
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
            this.lista = this.iConexion!.RolPermiso!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            // Para pruebas, asignamos rolId=1 y permisoId=1
            this.entidad = RolPermisoNucleo.RolPermiso(1, 1)!;
            this.iConexion!.RolPermiso!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            // Cambiamos el permisoId para simular modificación
            this.entidad!.PermisoId = 2;
            var entry = this.iConexion!.Entry<RolPermiso>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.RolPermiso!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}