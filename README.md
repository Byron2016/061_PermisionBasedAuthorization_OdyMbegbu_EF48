# 061_PermisionBasedAuthorization_OdyMbegbu_EF48

- OMAR MBEGBU: ASP.NET from Scratch
	- https://www.youtube.com/watch?v=Fk64W-Q-6PA&list=PLWlWcpwzY4Vke2i3vMD319qdCR3r2sf7A&index=1
	- ASP.NET Web Application(.NET Framework)
		- https://github.com/odytrice/Addressbook
		- Crear aplicación
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
				
				