using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class EjercicioPrueba
    {
        private readonly IConexion? iConexion;
        private List<Ejercicio>? lista;
        private Ejercicio? entidad;

        public EjercicioPrueba()
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
            this.lista = this.iConexion!.Ejercicio!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = EjercicioNucleo.Ejercicio()!;
            this.iConexion!.Ejercicio!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Dificultad = "Avanzado";
            var entry = this.iConexion!.Entry<Ejercicio>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Ejercicio!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}