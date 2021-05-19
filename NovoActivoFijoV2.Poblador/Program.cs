
using BIZ;
using COMMON.Entidades;
using COMMON.Interfaces.Catalogos;
using System;

namespace NovoActivoFijoV2.Poblador
{
    class Program
    {
        static IUsuariosManager usuarioManager;
        static IAppsManager appsManager;
        static IRolesManager rolesManager;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Poblador!");
            usuarioManager = FabricManager.UsuariosManager();
            appsManager = FabricManager.AppsManager();
            rolesManager = FabricManager.RolesManager();

            


            Usuarios adduser = new Usuarios()
            {
                Nombre = "Administrador",
                Usuario="Administrador",
                EstaActivo= true,
                RolID= "temporal",
                Password = "raypass",
                EsAdmin = true,
                FechaAlta = DateTime.Now,
                FechaModificacion = DateTime.Now,
                UsuarioModificaID = "usuario temporal",
                UsuarioRegistroID = "usuario temporal"
            };
            var AgUsuario = usuarioManager.Insertar(adduser);
            if (AgUsuario != null)
            {

                Console.WriteLine("Agregamos Rol Poblador");
                Roles addrol = new Roles()
                {
                    Descripcion = "Administrador",
                    FechaAlta = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioRegistroID = AgUsuario.Id,
                    UsuarioModificaID = AgUsuario.Id,
                    EstaActivo = true
                };
                var AgRol = rolesManager.Insertar(addrol);

                if (AgRol != null)
                {
                    Console.WriteLine("Modificmoas el usuario");
                    adduser.UsuarioModificaID = AgUsuario.Id;
                    adduser.UsuarioRegistroID = AgUsuario.Id;
                    adduser.Id = AgUsuario.Id;
                    adduser.RolID = AgRol.Id;
                    usuarioManager.Actualizar(adduser);

                    Console.WriteLine("id apps");

                    Apps addApps = new Apps()
                    {
                        FechaAlta = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioRegistroID = AgUsuario.Id,
                        UsuarioModificaID = AgUsuario.Id,
                        EstaActivo = true,
                        NombreApp = "Pruebas",
                        Clave= "123456789"
                    };
                    var AgApp = appsManager.Insertar(addApps);
                    if (AgApp != null)
                    {
                        Console.WriteLine("La Aplicación fue creada con éxito");
                    }
                    else 
                    {
                        Console.WriteLine("La Aplicación No fue creada: " + appsManager.Error);
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo crear el Rol: " + rolesManager.Error);
                }
            }
            else 
            {
                Console.WriteLine("No se pudo crear el usuario: " + usuarioManager.Error);
            }


        }
    }
}
