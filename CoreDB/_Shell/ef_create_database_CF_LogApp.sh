#!/bin/bash

cd ../CoreDB || exit 1

rm -rf Migrations/

dotnet ef migrations add InitialCreate \
  --context CoreDB.DBLogApp.DbLogAppContext \
  --project CoreDB \
  --startup-project CoreDB

dotnet ef database update \
  --context CoreDB.DBLogApp.DbLogAppContext \
  --project CoreDB \
  --startup-project CoreDB

docker exec -it mariadb-container mysql -uroot -ppass -e "USE db_LogApp; SHOW TABLES;"