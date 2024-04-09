using System.Reflection;

namespace JetStreamData.Kernel.AspNet.Configurations;

public class JetStreamDataModulesConfiguration
{
    public JetStreamDataModulesConfiguration()
    {
        AssembliesModules = new List<Assembly>();
        AssembliesModules.AddRange
        (
            Assembly.GetExecutingAssembly(),
            Assembly.GetAssembly(typeof(KernelAssembly))
        );
    }

    public IList<Assembly> AssembliesModules { get; }

    public JetStreamDataModulesConfiguration RegisterApplicationAssetsFromAssemblies(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies.ToList())
        {
            AssembliesModules.AddIfNotContains(assembly);
        }

        return this;
    }
}
