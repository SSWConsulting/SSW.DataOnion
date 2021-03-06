﻿<#@ template debug="true" hostSpecific="true" #>
<#@ output extension=".cs" #>
<#@ Assembly Name="System.Core" #>
<#@ Assembly Name="System.Windows.Forms" #>
<#@ import namespace="System" #>
<#@ import namespace="System.Reflection" #>
<#@ import namespace="System.IO" #>
<#@ import namespace="System.Diagnostics" #>
<#@ import namespace="System.Linq" #>
<#@ import namespace="System.Linq.Expressions" #>
<#@ import namespace="System.Collections" #>
<#@ import namespace="System.Collections.Generic" #> 
<#@ include file="Configurations\DbContextConfigurations.ttinclude" #>

namespace <#=Configurations.DataProjectNamespace#>
{
	using System.Data.Entity;
	
	<#
foreach (string entityNamespace in Configurations.DomainModelProjectNamespace.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))	        
{
#>
	using <#=entityNamespace#>;
<# 
}
#>


	using SSW.Data.Entities;

	public partial class <#=Configurations.DbContextName#>
	{
<#
	var entityTypes = AssemblyHelper.GetDomainTypes(Host.ResolvePath(@"..\bin\Debug"));

	foreach (var entityType in entityTypes)	        
	{
#>
		public IDbSet<<#=entityType.Name#>> <#=entityType.Name#>s { get; set; }

<#
	}
#>
	}
}

<#+
public class ProxyDomain : MarshalByRefObject
{
    public Assembly GetAssembly(string assemblyPath)
    {
        try
        {
            return Assembly.LoadFrom(assemblyPath);
        }
        catch (Exception ex)
        {
			throw new InvalidOperationException(ex.Message);
        }
    }
}

public static partial class AssemblyHelper 
{
	public static Type[] GetDomainTypes(string binFolderPath)
	{
		Type[] entityTypes;
		var entityNamespaces = Configurations.DomainModelProjectNamespace.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries).ToList();

	
		try 
		{
			var proxyDomain = new ProxyDomain();
			var assembly = proxyDomain.GetAssembly(Path.Combine(binFolderPath, Configurations.DomainModelProjectDll));

			var func = Configurations.DomainTypeFilter;

		    var queryable = assembly.GetTypes()
				.Where(t => string.IsNullOrEmpty(Configurations.DomainModelProjectNamespace) || entityNamespaces.Any(n => t.Namespace.StartsWith(n)))
				.Where(t => func(t));

		    if (!string.IsNullOrEmpty(Configurations.BaseEntityClass))
			{
				var baseEntityAssembly = proxyDomain.GetAssembly(Path.Combine(binFolderPath, Configurations.BaseEntityClassDll));
				Type baseType = baseEntityAssembly.GetTypes().First(t => t.Name == Configurations.BaseEntityClass);

				queryable  = queryable.Where(t => baseType == null || (baseType.IsAssignableFrom(t) && t != baseType));
			}

			entityTypes = queryable.ToArray();
			
        }
		catch
		{
			throw;
        }

		return entityTypes;
    }
}
#>

