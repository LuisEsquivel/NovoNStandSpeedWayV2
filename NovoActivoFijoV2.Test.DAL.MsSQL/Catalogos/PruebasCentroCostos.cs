namespace NovoActivoFijoV2.Test.DAL.MsSQL.Catalogos
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using COMMON.Entidades;
    using global::DAL.MsSQL;

    [TestClass]
    public class PruebasCentroCostos
    {
        [TestMethod]
        public void TestCentroCostos()
        {
            try
            {
                var repositorio = FabricRepository.CentroCostos();
                int numAntes = repositorio.Read.Count();
                var dato = repositorio.Create(new CentroCostos()
                {
                    Descripcion = "Prueba Descripción",
                    EstaActivo = true,
                    UsuarioModificaID = "PruebaUsuarioModificador",
                    UsuarioRegistroID = "PruebaUsuarioCreador",
                    FechaAlta = DateTime.Now,
                    FechaModificacion = default(DateTime)
                    
                });

                Assert.IsNotNull(dato, "No se pudo crear el objeto: " + repositorio.Error);
                dato.Descripcion = "Prueba Modificado";
                dato.FechaModificacion = DateTime.Now;
                var datoModificado = repositorio.Update(dato);
                Assert.IsNotNull(datoModificado, "No se modifico el dato: " + repositorio.Error);
                Assert.AreEqual(dato.Descripcion, datoModificado.Descripcion);
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
