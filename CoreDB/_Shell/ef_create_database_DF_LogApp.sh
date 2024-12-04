cd ../
pwd
rm -rf Database/

dotnet ef dbcontext scaffold -f "Server=localhost;Port=3306;Database=db_LogApp;User=root;Password=pass1234" Pomelo.EntityFrameworkCore.MySql -o DBLogApp

dotnet build
