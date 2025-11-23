using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class CoachPrueba
    {
        private readonly IConexion? iConexion;
        private List<Coach>? lista;
        private Coach? entidad;

        public CoachPrueba()
        {
            iConexion = new Conexion();
            iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
        }

        [TestMethod]
        public void Ejecutar()
        {
            Assert.AreEqual(true, Guardar());
            Assert.AreEqual(true, Listar());
            Assert.AreEqual(true, Login());
            Assert.AreEqual(true, Modificar());
            Assert.AreEqual(true, Borrar());
        }


        public bool Listar()
        {
            this.lista = this.iConexion!.Coach!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            this.entidad = CoachNucleo.Coach()!;
            this.iConexion!.Coach!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Especialidad = "Yoga";
            var entry = this.iConexion!.Entry<Coach>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Coach!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Login()
        {
            var coach = this.iConexion!.Coach!
                .FirstOrDefault(c => c.Correo == this.entidad!.Correo);
            return coach != null;
        }
    }
}