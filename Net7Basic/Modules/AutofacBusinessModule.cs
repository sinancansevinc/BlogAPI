using Autofac;
using System.Reflection;
using Module = Autofac.Module;
namespace Net7Basic.Modules
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            var apiAssembly = Assembly.GetExecutingAssembly();


            builder.RegisterAssemblyTypes(apiAssembly)
                .Where(x => x.Name.EndsWith("Repository") || x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();  //InstancePerLifetimeScope means Scope 

        }
    }
}
