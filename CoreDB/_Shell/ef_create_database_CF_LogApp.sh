#!/bin/bash

cd ../
pwd

# 기존 마이그레이션 폴더 삭제
rm -rf Migrations/


dotnet ef database drop --force \
  --context CoreDB.DBLogApp.DbLogAppContext \
  --project ./ \
  --startup-project ./

dotnet ef migrations remove \
  --context CoreDB.DBLogApp.DbLogAppContext \
  --project ./ \
  --startup-project ./

dotnet ef migrations add InitialCreate \
  --context CoreDB.DBLogApp.DbLogAppContext \
  --project ./ \
  --startup-project ./

dotnet ef database update \
  --context CoreDB.DBLogApp.DbLogAppContext \
  --project ./ \
  --startup-project ./

# 프로젝트 빌드
dotnet build