개발 사항
서버 코어 시스템의 라이브러리를 테스트 하는 콘솔 프로그램
차후 유닛 테스트로 발전 시킨다.

--------------------------------------------------------------


2024.03.28
해당 dll를 받아서 쓰는 콘솔 프로젝트 만들기.
dotnet new console -o UnitTester

코어 시스템의 dll을 받아서 쓸 수 있게 프로젝트 참조를 건다.
솔루션 파일에 프로젝트를 add 하지 않고 프로젝트간 참조를 추가한다.
dotnet add reference ../../CoreSystem/CoreLibrary/CoreLibrary.csproj

프로젝트 코드를 추가한다.
Program.cs 코드 작성

빌드 및 테스트
dotnet build 

콘솔 실행 확인하기
dotnet run 

실행 환경 확인.
vscode 실행 환경 파일 작성하기 
launch.json

TODO: 닷넷 빌드 환경 만들기.

2024.03.30
vscode 빌드 환경에 대한 정리 하기.
디버깅 -> F5

실행 -> cmd + shift + p 메뉴 선택하고
.Net : Generate Assets for Build and Debug 선택

기본적인 콘솔 환경의 .vscode 폴더의 하위 파일
launch.json / task.json 파일이 생성됨.
녹색 디버그 메뉴에서 생성된 빌드 환경을 선택한다. 
.Net Core launch(console)

실행을 하기 위해 ctrl + f5
디버깅 f5

TODO : 
web app 코드 기반 작성하기.

2024.04.07
문자열 출렉 테스트 라이브러리 코드 적용