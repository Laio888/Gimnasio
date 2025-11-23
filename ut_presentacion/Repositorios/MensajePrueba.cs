using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class MensajePrueba
    {
        private readonly IConexion? iConexion;
        private List<Mensaje>? lista;
        private Mensaje? entidad;

        public MensajePrueba()
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
            this.lista = this.iConexion!.Mensaje!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            // Para pruebas, asignamos UsuarioId=1 y CoachId=1
            this.entidad = MensajeNucleo.Mensaje(1, 1)!;
            this.iConexion!.Mensaje!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Estado = "Leído";
            var entry = this.iConexion!.Entry<Mensaje>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Mensaje!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}