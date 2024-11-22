# 코어 프로젝트가 빌드되면 DLL을 참조하는 프로젝트에 배포 시킨다.
#cd bin/
#ls
cp bin/Debug/net8.0/CoreLibrary.dll ../PushApp/Dll/
cp bin/Debug/net8.0/CoreLibrary.dll ../WebApp/Dll/
cp bin/Debug/net8.0/CoreLibrary.dll ../BackOfficeApp/Dll/
