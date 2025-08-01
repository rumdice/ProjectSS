cd ../

dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=db_WebApp;User=root;Password=pass1234" Pomelo.EntityFrameworkCore.MySql --output-dir DBWebApp --force --context DbWebAppContext --use-database-names --schema dbo --no-pluralize --no-onconfiguring

dotnet build
