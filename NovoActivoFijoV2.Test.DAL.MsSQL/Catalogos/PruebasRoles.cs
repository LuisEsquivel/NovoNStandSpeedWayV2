namespace NovoActivoFijoV2.Test.DAL.MsSQL.Catalogos
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using COMMON.Entidades;
    using System.Linq;
    using global::DAL.MsSQL;

    [TestClass]
    public   class PruebasRoles
    {
        [TestMethod]
        public void TestRoles()
        {
            try
            {
                var repositorio = FabricRepository.Roles();
                int numAntes = repositorio.Read.Count();
                var AgObject = repositorio.Create(new Roles()
                {
                    Descripcion = "Prueba Descripción",
                    EstaActivo = true,
                    UsuarioModificaID = "PruebaUsuarioModificador",
                    UsuarioRegistroID = "PruebaUsuarioCreador",
                    FechaAlta = DateTime.Now,
                    FechaModificacion = default(DateTime)
                });

                Assert.IsNotNull(AgObject, "No se pudo crear el objeto" + repositorio.Error);
                AgObject.Descripcion = "Prueba Descripción Modificado";
                AgObject.FechaModificacion = DateTime.Now;
                var AgObjectMod = repositorio.Update(AgObject);
                Assert.IsNotNull(AgObjectMod, "No se pudo crear el objeto Modificador" + repositorio.Error);
                Assert.AreEqual(AgObject.Descripcion, AgObjectMod.Descripcion);
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message);
            }

        }
    }
}
