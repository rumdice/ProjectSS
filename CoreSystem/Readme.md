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


데이터 베이스 퍼스트 시도
필요 도구 설치
dotnet tool install --global dotnet-ef

프로젝트 경로에 필요 페키지 설치 및 버전 확인
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Pomelo.EntityFrameworkCore.MySql


스케폴드 명령어 수행
dotnet ef dbcontext scaffold "Name=DefaultConnection" Pomelo.EntityFrameworkCore.MySql -o Models
-> 실패

dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=db_WebApp;User=root;Password=pass1234" Pomelo.EntityFrameworkCore.MySql -o Models
-> 성공

스케폴딩 명령어를 bash 파일로 빼야 하겠다.

dotnet build를 수행하여 하위 클래스에 사용 할 수 있게 하기


2024.06.03

DDD와 EF 의 개념 연관을 위해
database 이름은 dbWebContext (~Context)
테이블 이름은 (~Entity)로 결정

데이터베이스 퍼스트와 코드퍼스트의 장단점 확인

코드 퍼스트는 이력이 남는다.
대신 데이터베이스의 복잡한 관계를 코드로 표현 하려면 학습과정이 필요하다.

데이터베이스 퍼스트는 사용하기 편하다.
DB의 설계에 익숙한 사람이 있다면 그냥 가져다 쓰기만 하면 된다.

공통으로 신경써야 하는 부분.
- 모델링 크리에이팅 할때 완전 리셋하는게 과연 옳은가?
- 예를 들어 코드 퍼스트 코드에서 db리셋을 하고 migration 이력을 날리는게 옳은가?
