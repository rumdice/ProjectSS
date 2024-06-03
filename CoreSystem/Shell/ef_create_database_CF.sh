cd ../CoreLibrary
pwd

dotnet ef migrations add InitialCreate
dotnet ef database update

dotnet build



