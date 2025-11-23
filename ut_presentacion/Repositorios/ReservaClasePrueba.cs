using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class ReservaClasePrueba
    {
        private readonly IConexion? iConexion;
        private List<ReservaClase>? lista;
        private ReservaClase? entidad;

        public ReservaClasePrueba()
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
            this.lista = this.iConexion!.ReservaClase!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            // Para pruebas, asignamos UsuarioId=1 y ClaseId=1
            this.entidad = ReservaClaseNucleo.ReservaClase(1, 1)!;
            this.iConexion!.ReservaClase!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Estado = "Confirmada";
            var entry = this.iConexion!.Entry<ReservaClase>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.ReservaClase!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}