@echo off
setlocal

:: 원본 DLL 파일 경로
set SOURCE=bin\Debug\net8.0\CoreLibrary.dll

:: 복사 대상 디렉토리
set DEST1=..\PushApp\Dll\
set DEST2=..\WebApp\Dll\
set DEST3=..\BackOfficeApp\Dll\
set DEST4=..\LogApp\Dll\

:: 각 대상 디렉토리로 파일 복사
echo Copying CoreLibrary.dll to %DEST1%
xcopy /Y "%SOURCE%" "%DEST1%"

echo Copying CoreLibrary.dll to %DEST2%
xcopy /Y "%SOURCE%" "%DEST2%"

echo Copying CoreLibrary.dll to %DEST3%
xcopy /Y "%SOURCE%" "%DEST3%"

echo Copying CoreLibrary.dll to %DEST4%
xcopy /Y "%SOURCE%" "%DEST4%"

:: 작업 완료 메시지
echo All files copied successfully!

pause
