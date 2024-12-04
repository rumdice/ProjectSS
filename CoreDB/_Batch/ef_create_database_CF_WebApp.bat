cd ../
pwd

rmdir Migrations

dotnet ef database drop --force --context DbWebAppContext

dotnet ef migrations add InitialCreate --context DbWebAppContext

dotnet ef database update --context DbWebAppContext

dotnet build



