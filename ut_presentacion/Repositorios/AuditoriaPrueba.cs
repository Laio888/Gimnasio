using lib_dominio.Entidades;
using lib_repositorio.Implementaciones;
using lib_repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;

namespace ut_presentacion.Repositorios
{
    [TestClass]
    public class AuditoriaPrueba
    {
        private readonly IConexion? iConexion;
        private List<Auditoria>? lista;
        private Auditoria? entidad;

        public AuditoriaPrueba()
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
            this.lista = this.iConexion!.Auditoria!.ToList();
            return lista.Count > 0;
        }

        public bool Guardar()
        {
            // Para pruebas, asignamos UsuarioId=1, Entidad="Rutina", EntidadId=1
            this.entidad = AuditoriaNucleo.Auditoria(1, "Rutina", 1)!;
            this.iConexion!.Auditoria!.Add(this.entidad);
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Modificar()
        {
            this.entidad!.Accion = "UPDATE";
            this.entidad!.Detalle = "Auditoría modificada en pruebas";
            var entry = this.iConexion!.Entry<Auditoria>(this.entidad);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();
            return true;
        }

        public bool Borrar()
        {
            this.iConexion!.Auditoria!.Remove(this.entidad!);
            this.iConexion!.SaveChanges();
            return true;
        }
    }
}