namespace NovoActivoFijoV2.Test.DAL.MsSQL.Catalogos
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using COMMON.Entidades;
    using System.Linq;
    using global::DAL.MsSQL;

    [TestClass]
    public class PruebasFormaAdquisicion
    {
        [TestMethod]
        public void TestFormaAdquisicion()
        {
            try
            {
                var repositorio = FabricRepository.FormaAdquisicion();
                int numAntes = repositorio.Read.Count();
                var agFormaAd = repositorio.Create(new FormaAdquisicion()
                {
                    Descripcion = "Prueba Descripción",
                    EstaActivo = true,
                    UsuarioModificaID = "PruebaUsuarioModificador",
                    UsuarioRegistroID = "PruebaUsuarioCreador",
                    FechaAlta = DateTime.Now,
                    FechaModificacion = default(DateTime)
                });

                Assert.IsNotNull(agFormaAd, "No se pudo crear el objeto" + repositorio.Error);
                agFormaAd.Descripcion = "Prueba Descripción Modificado";
                agFormaAd.FechaModificacion = DateTime.Now;
                var agFormaAdModificado = repositorio.Update(agFormaAd);
                Assert.IsNotNull(agFormaAdModificado, "No se pudo crear el objeto Modificador" + repositorio.Error);
                Assert.AreEqual(agFormaAd.Descripcion, agFormaAdModificado.Descripcion);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

        }
    }
}
