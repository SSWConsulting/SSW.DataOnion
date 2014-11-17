
echo "Publish SSW.Data.Interfaces"
cd SSW.Data.Interfaces
.\CreatePackageAndPublish.ps1
cd..


echo "Publish SSW.Data.Entities"
cd SSW.Data.Entities
.\CreatePackageAndPublish.ps1
cd..

echo "Publish SSW.Data.EF"
cd SSW.Data.EF
.\CreatePackageAndPublish.ps1
cd..

echo "Publish SSW.Data.DbContext.Generator"
cd SSW.Data.DbContext.Generator
.\CreatePackageAndPublish.ps1
cd..

echo "Publish SSW.Data.RepositoryInterfaces.Generator"
cd SSW.Data.RepositoryInterfaces.Generator
.\CreatePackageAndPublish.ps1
cd..

echo "Publish SSW.Data.Repositories.Generator"
cd SSW.Data.Repositories.Generator
.\CreatePackageAndPublish.ps1