# 상위 프로젝트인 코어 시스템을 빌드하여 복사해온다.
cd ../../CoreSystem/CoreLibrary
dotnet build
cp ./bin/Debug/net8.0/CoreLibrary.dll ../../PushApp/PushApp/Dll/
