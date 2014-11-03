
#
# this project is for testing the various nuget packages in this solution. 
#
# this script will
# - run package command for each dependency 
# - copy packages to LocalPackages folder
# - install packages to integration test project


$0 = $myInvocation.MyCommand.Definition
$dp0 = [System.IO.Path]::GetDirectoryName($0)


cd $dp0
del *.nupkg



# SSW.Data.Entities
cd ..\..\SSW.Data.Entities
.\CreatePackage.bat
cd $dp0
install-package SSW.Data.Entities -ProjectName SSW.Data.Tests.Integration -Source ..\..\SSW.Data.Entities


# SSW.Data.EF
cd ..\..\SSW.Data.EF
.\CreatePackage.bat
cd $dp0
install-package SSW.Data.EF -ProjectName SSW.Data.Tests.Integration -Source ..\..\SSW.Data.EF


# SSW.Data.DbContext.Generator
cd ..\..\SSW.Data.DbContext.Generator
.\CreatePackage.bat
cd $dp0
install-package SSW.Data.DbContext.Generator -ProjectName SSW.Data.Tests.Integration -Source ..\..\SSW.Data.DbContext.Generator


# SSW.Data.RepositoryInterfaces.Generator
cd ..\..\SSW.Data.RepositoryInterfaces.Generator
.\CreatePackage.bat
cd $dp0
install-package SSW.Data.RepositoryInterfaces.Generator -ProjectName SSW.Data.Tests.Integration -Source ..\..\SSW.Data.RepositoryInterfaces.Generator


# SSW.Data.Repositories.Generator
cd ..\..\SSW.Data.Repositories.Generator
.\CreatePackage.bat
cd $dp0
install-package SSW.Data.Repositories.Generator -ProjectName SSW.Data.Tests.Integration -Source ..\..\SSW.Data.Repositories.Generator

