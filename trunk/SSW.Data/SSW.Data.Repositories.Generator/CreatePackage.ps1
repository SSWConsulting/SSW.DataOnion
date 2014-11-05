$0 = $myInvocation.MyCommand.Definition
$dp0 = [System.IO.Path]::GetDirectoryName($0)

cd $dp0

echo $dp0

echo "Cleaning up old packages..."
del *.nupkg

echo "Building and packaging new version of package..."
nuget pack SSW.Data.Repositories.Generator.csproj -Prop Configuration=Release -IncludeReferencedProjects -Build