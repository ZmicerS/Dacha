# Dacha
Dacha is Single Page Application (SPA) with ASP.NET Web API and AngularJS.

Legend. Maintenance of a database on small land plots near cities.

There are 3 tiers in this application.
Dacha.Web –presentation layer.
Dacha.Bll   - buseness layer.
Dacha.Dll - data access layer.

Dacha.Web uses AngularJS for front end and api controllers for back end.
Dacha.Dll uses Entity Framework for access to MS SQL.
AspNet Identity uses for authorization. Authorization is through JWT token with refresh token.

Autofac uses for  container IoC.

These assemblies must be installed through NuGet Packages.
EntityFramework,
Microsoft.AspNet.Cors,
Microsoft.AspNet.Identity.Core,
Microsoft.AspNet.Identity.EntityFramework,
Microsoft.AspNet.Identity.Owin,
Microsoft.Owin.Cors,
Microsoft.AspNet.WebApi.Owin,
Microsoft.Owin.Cors,
Microsoft.Owin.Security.Jwt,
Microsoft.Owin.Host.SystemWeb,
System.IdentityModel.Tokens.Jwt,
Thinktecture.IdentityModel.Core,
AngularJS.Core,
Angular.UI.UI-Router,
Angular.UI.Bootstrap,
Bootstrap,
jQuery,
Autofac,
Autofac.Owin,
Autofac.WebApi2


