using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.MsSQL;
using COMMON.Entidades;

namespace NovoActivoFijoV2.Test.DAL.MsSQL.Catalogos
{

    [TestClass]
    public class PruebasUsuarios
    {
        [TestMethod]
        public void TestUsuarios()
        {
            try
            {
                var repositorio = FabricRepository.Usuarios();
                int numAntes = repositorio.Read.Count();
                var dato = repositorio.Create(new Usuarios()
                {
                    Nombre = "Prueba Nombre",
                    Usuario = "Prueba Usuario",
                    EstaActivo = true,
                    UsuarioModificaID = "PruebaUsuarioModificador",
                    UsuarioRegistroID = "PruebaUsuarioCreador",
                    Password = "123456789",
                    FechaAlta = DateTime.Now,
                    FechaModificacion = default(DateTime),
                    RolID="1"
                });

                Assert.IsNotNull(dato, "No se pudo crear el objeto: " + repositorio.Error);
                dato.Nombre = "Modificado";
                dato.FechaModificacion = DateTime.Now;
                var datoModificado = repositorio.Update(dato);
                Assert.IsNotNull(datoModificado, "No se modifico el dato: " + repositorio.Error);
                Assert.AreEqual(dato.Nombre, datoModificado.Nombre);
                //Assert.IsTrue(repositorio.Delete(datoModificado.Id));
                //Assert.AreEqual(numAntes, repositorio.Read.Count(), "La cantidad de registros despues del CRUD no es igual");
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
