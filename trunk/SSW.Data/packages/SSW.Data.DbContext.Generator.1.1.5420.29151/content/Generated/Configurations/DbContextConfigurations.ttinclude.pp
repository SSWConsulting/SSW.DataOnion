<#+
public static partial class Configurations
{
	// OPTIONAL BASE TYPE FILTER - LEAVE BLANK IF NOT USED
	public const string BaseEntityClass = @"BaseEntity"; // optional base entity to filter only entities that inherit from specific base class
	public const string BaseEntityClassDll = @"SSW.Data.Entities.dll"; // optional base entity dll to filter only entities that inherit from specific base class
	///////////////////////////////////////////
	
	public const string DomainModelProjectDll = @"$rootnamespace$.DomainModel.dll"; // dll file name for domain models
	public const string DomainModelProjectNamespace = @"$rootnamespace$.DomainModel.Entities"; // custom domain model namespace, usualy the same as dll name
	public const string DbContextName = @"YourDbContext"; // name of your db context
	public const string DataProjectNamespace = @"$rootnamespace$.Data"; // namespace to be used by generated db context
}
#>

