# 061_PermisionBasedAuthorization_OdyMbegbu_EF48

- OMAR MBEGBU: ASP.NET from Scratch
	- https://www.youtube.com/watch?v=Fk64W-Q-6PA&list=PLWlWcpwzY4Vke2i3vMD319qdCR3r2sf7A&index=1
	- ASP.NET Web Application(.NET Framework)
		- https://github.com/odytrice/Addressbook
		- Crear aplicación (061_PermisionBasedAuthorization_OdyMbegbu)
			- ASP.NET Web Application(.NET Framework)
			- Nombre:
				- Project: Addressbook.Web
				- Solution: Addressbook
			- Tipo:
				- Empty
				- MVC
				- Also create a project for unit test con nombre Addressbook.Test
				
		- Configurar OWIN middleware
			- https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-middleware-in-the-iis-integrated-pipeline
			- Ir a archivo Global.asax.cs
			- Ir a Nuget Packages
				- Install-Package Microsoft.Owin.Host.SystemWeb.es -Version 4.2.0
				
		- Configurar UI
			- Agregar bootstrap
				- Install-Package bootstrap -Version 3.4.1
					- Esto agregará bootstrap, agregará jQuery
					- Modifica archivo packages.config
					- Crea carpeta fonts
					- Modifica archivo xxx.web.csproj
					
			- Agregar jquery
				- Install-Package jQuery -Version 3.4.1
					
			- Agregar jQuery.Validation
				- Install-Package jQuery.Validation -Version 1.17.0
				
			- Agregar Microsoft.jQuery.Unobtrusive.Validation
				- Install-Package Microsoft.jQuery.Unobtrusive.Validation -Version 3.2.11
				
			- Agregar Modernizr
				- Install-Package Modernizr -Version 2.8.3
				
			- Agregar Microsoft.AspNet.Web.Optimization
				- Install-Package Microsoft.AspNet.Web.Optimization -Version 1.1.3
					- Esto agregará paquetes
						- WebGrease
						- Newtonsoft.Json
						- Antlr
						
			- Agregar OWIN StartUP Class
				- Agregar a raíz del proyecto un nuevo elemento OWIN Startup class con nombre Startup.cs
				
			- Agregar controllador Home y la vista index
				- Esto como es un proyecto vacío agregará los siguientes componentes:
					- Addressbook.Web/Content/Site.css
					- Addressbook.Web/Views/Shared/_Layout.cshtml
					- Addressbook.Web/Views/_ViewStart.cshtml
					- Además el controlador y la vista antes indicados.

			- Agregar un BundleConfig y usarlo en _Layout
				- End-to-End ASP.NET MVC: Adding BundleConfig
					- https://www.techjunkieblog.com/2015/05/aspnet-mvc-empty-project-adding.html
						- Agregar a carpeta App_Start la clase BundleConfig
						- Llenar el contenido de los Bundles que se deseen.
						- En _Layout llamar al arroba Styles.Render("~/Content/css") y todos los que se requieran.
						- En el Views/web.config sección namespaces agregar
							- <add namespace="System.Web.Optimization"></add>
						- Agregar en el Global.asx.cs la referencia al BundleConfig
							- BundleConfig.RegisterBundles(BundleTable.Bundles);
							
			- Agregar tema de bootstrap Lummen
				- Ir a https://startbootstrap.com/themes
				- Copiar el bootstrap que se indica
				- Cambiar en la aplicación.
				
		- Configurar OWIN middleware continuación
			- Colocar en la clase HomeController
				- [Authorize]
					- Esto no me permite ingresar y desplegará mensajde de IIS con error 401
			- Agregar paquete Microsoft.Owin.Security.Cookies
				- Install-Package Microsoft.Owin.Security.Cookies -Version 3.0.1
					- Agrega automáticamente  Microsoft.Owin.Security
				- Permite hacer autenticación usando cookies
				
			- Reenviar aplicación a página de error propia.
				- En owin startup crear un método que llame pagina de error.
					```cs
					private static void ConfigureAuth(IAppBuilder app)
					{
						app.UseCookieAuthentication(new CookieAuthenticationOptions
						{
							LoginPath = new PathString("/account/login"),
						});
					}
					```
					
					Con esto cada vez que tratemos de ir a la página del index del homeController nos enviará a la página de login x q no estamos autorizados.
					
				- Agregar AccountController y crear una vista para método Login
					- Ir a la página de bootstrap/Components/css/forms y compiar el ejemplo Horizontal form.
					- 
					
				- Crear el método login para el post y direccionar la vista login a este método.
				
		- Signning
			- Agregar paquete Microsoft.AspNet.Identity.Core
				- Install-Package Microsoft.AspNet.Identity.Core -Version 2.2.3
			- Definir en el owin startup el tipo de autenticación
				- AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
			- Definir en web.config el modo de authentication dentro de tags system.web
				- <authentication mode="None" />
			- Agregar métodos de login  y logout.
			
		- Implement Role Base Segurity (V.8 7.32)
			- Poner en algun método [Authorize(Roles ="Admin")]
			- Asegurar que el usuario tiene un claim role
			
		- Uso de User Manager (V.9)
			- Crear las EF clases
				- Agregar un nuevo proyecto "Class Library (.NET Framework) A project for creating a C# class library (.dll)" 
					- con nombre Addressbook.Core
				- Agregar un nuevo proyecto "Class Library (.NET Framework) A project for creating a C# class library (.dll)" 
					- con nombre Addressbook.Infrastructure
					
				- Agregar referencias en web a Core e Infrastructure
				- Agregar referencias en Infrastructure a Core
				
			- Code First
				- En libraría Core 
					- Crear en la raíz folder Services
					- Agregar clase AccountService
					- Agregar bussiness objets
						- Crear folder Models
						- Crear clases: 
							- UserModel
							- RoleModel
							- PermissionModel
				- En librería Infrastructure 
					- Crear en la raíz folder Utilities
					- Crear en la raíz folder DataAccess
						- Crear folder Entities
							- Agregar clase User
							- Agregar clase UserRole
							- Agregar clase Role
							- Agregar clase RolePermission
							- Agregar clase Permissions
							
							- Users has UserRoles, UserRoles to Roles, Roles to RolePermissions, RolePremissions to Permissions
				
					- Crear DBContext
						- Agregar paquete EntityFramework
							- Debe ser agregado en Infrastructure, Web 
							- Install-Package EntityFramework -Version 6.4.4
						- En carpeta DataAccess crear clase AddressBookContext : DBContext
						
					- Agregar DBSets a DBContext
						- Para las clases que definimos anteriormente.
						
				- Agregar conexionString y hacer migración.
					- En project .web ir a Web.config debajo del tag appSettings agregar nuevo connectionStrings y configurarlo. (www.connectionstrings.com/)
						<connectionStrings>
							<add name="DataContext" connectionString="Data Source=(local);Initial Catalog=db_AddressBook;Integrated Security=True;User ID=sa;Password=123456;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient"/>
						</connectionStrings>
						
						<connectionStrings>
							<add name="DataContext" connectionString="Data Source=localhost;Initial Catalog=db_AddressBook;persist security info=True;User ID=sa;Password=123456;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient"/>
						</connectionStrings>
						
					- En el Package Manager ir al proyecto donde está el DBContext
						- enable-migrations -verbose
							- Esto creará dentro de project infraestructure una carpeta Migrations con una clase Configurations.
					
					- Habilitar migraciones
						- En project .Infrastructure Migrations/Configurations.cs modificar a true AutomaticMigrationsEnabled
						
						- Ejecutar en Package Manager del proyecto infraestructure 
							- update-database -verbose
							
							
					- Seed Users Data
						- En En project .Infrastructure Migrations/Configurations.cs/Seed agregar el seed

			- Adding DI
				- Agregar en raíz de .Core folder Interface
					- Agregar dentro de este folders Managers y Queries
				- Cambiar el nombre del folder Services a Managers.
					- Crear clase AccountManager.cs
					
				- Crear interfases
					- Interface/Manager/IAccountManager
					- Interface/Manager/IAccountQueries
					
				- Mover contenido de carpeta Addressbook.Infrastructure.DataAccess a raíz del proyecto y cambiar namespaces en
					- Archivos dentro de entidades
					- DBContext
					- Configuration.cs
					
				- Implementar las queries.
					- En .Infrastructure agregar Queries/AccountQueries : IAccountQueries
					
				- Instalar en .web paquete Ninject
					- Install-Package Ninject -Version 3.3.6
					
				- Instalar en .web paquete Ninject.Web.Common
					- Install-Package Ninject.Web.Common -Version 3.3.2
						- Esto añade
						
				- configure Ninject
					- En: gist.github.com/odytrice/5821087
						- A small Library to configure Ninject (A Dependency Injection Library) with an ASP.NET Application.
					- En .Web/App_Start crear archivo de tipo clase con nombre: Ninject.Mvc.cs
						- Quitar del namespace el .App_Start
						- Copiar contenido de pag. web ahí.
						
					- Registrar Ninject library en Global.asax.cs
						- NinjectContainer.RegisterAssembly();
						
				- Definir que inyectar en Ninject
					- Agregar clase Addressbook.Web.Modules/MainModule
					
				- Testear
					- En el .Test añadir referencia a los otros 3 proyectos.
					- Crear en raíz clase 
					- Instalar paquetes:
						- Install-Package Moq -Version 4.18.2
						- Install-Package Ninject -Version 3.3.6
						
				- Agregar clase NinjectTests
					- https://gist.github.com/odytrice/243fe6c4bf14aedb584c3fc876b9fe42
				
					- Use Operational class
						- github.com/odytrice/Operation
						- Instalar en todos los proyectos
							- Install-Package Operation -Version 1.1.2
							
		- Quitar manejo manual de claims y dejar que UserManager cree automáticamente.(V.14)
			- Crear un objeto que representa al usuario. Usaremos UserModel en .Core/Models
			- Renombar .Web/Models/UserModel a .Web/Models/User
			- .Web/Models/User hereda de IUser<int>
			
			- Implementar UserStore
				- Este es el que le dice a EF como CRUD
				- Addressbook.Web.Utils/UserStore : IUserStore<User, int>
				- Crear constructor inyectar IAccountManager account
				
			- 
			
		- Hasta V16. 5.20
		
		- V16 5.20 - 8.39 Create Identity in SignIn Method