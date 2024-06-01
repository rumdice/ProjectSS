개발 사항
서버 코어 라이브러리 제작

도입하고자 하는 것
- DDD (도메인 주도 설계)
- ORM
- 4 Layerd 구조

코어 시스템 코드는 dll 형태로 배포되며
각 서버가 해당 코어코드를 받아서 각자 App을 만들어 구동하는 구조.


## 도움되는 링크들
4 Layerd 아키텍쳐 관련 도움말.
https://ksh-coding.tistory.com/92


https://learn.microsoft.com/ko-kr/dotnet/core/tutorials/library-with-visual-studio-code?pivots=dotnet-7-0


--------------------------------------------------------------

2024.03.27
프로젝트 초기 셋팅
실행한 커맨드

디랙토리 생성 CoreSystem
mkdir 

솔루션 생성.
dotnet new sln

라이브러리 생성
dotnet new classlib -o StringLibrary

솔루션에 라이브러리 프로젝트 포함 시키기
dotnet sln add StringLibrary/StringLibrary.csproj

문자열을 처리하는 라이브러리 코드 작성
Class1.cs

빌드를 하여 dll 파일 생성하기
dotnet build
CoreLibrary.dll 생성.


2024.03.28
해당 dll를 받아서 쓰는 프로젝트 만들기.
해당 코드는 별도 디랙토리와 별도 프로젝트로 진행하여 연결하기.

2024.04.03
쿠버네티스로 이 프로젝트를 컨테이너 화 해보기.
- 갑작스럽게 쿠버네티스를 공부해야 할 일이 생김.

2024.04.07
문자열을 그대로 출력하는 확장 메서드 추가.
테스트 목적의 메서드

2024.04.08
코어 시스템 서버 공용 기능 추가하기.
- ef core 기능
- 유틸리티 기능?
- 코어 시스템은 어떤 구조로 설계를 해야 하나?


2024.05.31
코어 시스템에 필요할 것 같은 코드 추가
- 모든 API 요청에 대한 응답처리

필요한 패키지 추가
dotnet add package Microsoft.AspNetCore.Mvc
dotnet add package Microsoft.AspNetCore.App

코드 추가

빌드하여 라이브러리를 사용하는 시스템 (~App에 대하여 적용)
dotnet build

2024.06.01
DB 처리 하는 부분의 별도 프로젝트 분리 및 사용 필요
해당 프로젝트에서 DBContext를 생성하는 역활을 담당하도록 하기.
1. Code First 
2. Database First 
두 가지 방법 다 시도 하기