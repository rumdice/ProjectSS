cd ../CoreLibrary
pwd
dotnet ef dbcontext scaffold -f "Server=localhost;Port=3306;Database=db_WebApp;User=root;Password=pass1234" Pomelo.EntityFrameworkCore.MySql -o Database
