using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class EjercicioRutinaPrueba
    {
        private readonly IConexion? iConexion;
        private List<EjercicioRutina>? lista;
        private EjercicioRutina? entidad;

        public EjercicioRutinaPrueba()
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
            this.lista = this.iConexion!.EjercicioRutina!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            // Para pruebas, asignamos RutinaId=1 y EjercicioId=1
            this.entidad = EjercicioRutinaNucleo.EjercicioRutina(1, 1)!;
            this.iConexion!.EjercicioRutina!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Series = 5;
            this.entidad!.Repeticiones = 15;
            var entry = this.iConexion!.Entry<EjercicioRutina>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.EjercicioRutina!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}