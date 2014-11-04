Thanks you for installing SSW's DbContext gererator
This provides a tt template that will update your db context to include your domain entities.

### Before you compile ###
the following files will need to be updated.

Generated/Configurations/DbContextConfigurations.ttinclude
DbContext.cs


1. Generated/Configurations/DbContextConfigurations.ttinclude

Update this file to generate DbContext database sets based on your domain model. You will need to have a reference 
to domain model project (if using a separate project) and project should be built (to make sure dll is copied 
to the bin folder). Ideally you would have the following class library project structure in your solution

-YourSolution
--YourNamespace.DomainModel
--YourNamespace.Data (current project)
--YourNamespace.WebUI


2. DbContext.cs
We can't tell at install time what your DBContext will be called.
Update all references to "YourDbContext" and "YourConnectionStringName"