using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class AsignacionRutinaPrueba
    {
        private readonly IConexion? iConexion;
        private List<AsignacionRutina>? lista;
        private AsignacionRutina? entidad;

        public AsignacionRutinaPrueba()
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
            this.lista = this.iConexion!.AsignacionRutina!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            // Para pruebas, asignamos UsuarioId=1 y RutinaId=1
            this.entidad = AsignacionRutinaNucleo.AsignacionRutina(1, 1)!;
            this.iConexion!.AsignacionRutina!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Estado = "Pendiente";
            var entry = this.iConexion!.Entry<AsignacionRutina>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.AsignacionRutina!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}