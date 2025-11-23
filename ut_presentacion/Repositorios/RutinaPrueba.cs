using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class RutinaPrueba
    {
        private readonly IConexion? iConexion;
        private List<Rutina>? lista;
        private Rutina? entidad;

        public RutinaPrueba()
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
            this.lista = this.iConexion!.Rutina!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            // Para pruebas, asignamos UsuarioId = 1
            this.entidad = RutinaNucleo.Rutina(1)!;
            this.iConexion!.Rutina!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Nivel = "Avanzado";
            var entry = this.iConexion!.Entry<Rutina>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Rutina!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}