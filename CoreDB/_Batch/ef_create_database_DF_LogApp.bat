cd ../

dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=db_LogApp;User=root;Password=pass1234" Pomelo.EntityFrameworkCore.MySql --output-dir DBLogApp --force --context DbLogAppContext --use-database-names --schema dbo --no-pluralize --no-onconfiguring

dotnet build
