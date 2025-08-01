#!/bin/bash

cd ../
pwd

# 기존 마이그레이션 폴더 삭제
rm -rf Migrations/

# 데이터베이스 삭제 (명확하게 context 지정)
dotnet ef database drop --force --context CoreDB.DBWebApp.DbWebAppContext

# 새 마이그레이션 생성
dotnet ef migrations add InitialCreate --context CoreDB.DBWebApp.DbWebAppContext 

# (선택) 마이그레이션 적용 단계 - 현재 주석처리된 상태 유지
dotnet ef database update --context CoreDB.DBWebApp.DbWebAppContext

# 프로젝트 빌드
dotnet build