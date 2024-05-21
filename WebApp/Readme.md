개발 사항
서버 코어 라이브러리를 이용하여 web Request를 처리하는 OutGame 서비스를 제공한다.


예를 들어
로그인, 로그아웃, 아이템 강화 등 거의 대부분의 비즈리스 로직을 처리한다.

--------------------------------------------------------------


2024.03.28
해당 dll를 받아서 쓰는 프로젝트 만들기.


2024.03.31
mvc 프로젝트 기본을 생성한다.
dotnet new mvc -o WebApp

빌드를 수행한다. 
디버깅 - (F5)
또는
dotnet build

여기 기본 프로젝트에서 불필요한 코드 제거 (필요한가?)
- mvc의 모든 구성 요소가 필요하진 않다?

CoreSystem에서 각 serverApp을 생성할 공용 모듈을 어떤 식으로 구성하고 상속 받아 구현 할 것인가?

2024.04.07
코어 시스템의 테스트 함수 적용 시켜보기
문자열 그대로 출력 메서드 사용

코어 시스템의 dll을 받아서 쓸 수 있게 프로젝트 참조를 건다.
솔루션 파일에 프로젝트를 add 하지 않고 프로젝트간 참조를 추가한다.

프로젝트 파일 (.csproj) 의 있는 경로로 이동.
dotnet add reference ../../CoreSystem/CoreLibrary/CoreLibrary.csproj

프로젝트 파일에 참조가 걸렸는지 확인하기.

웹페이지가 뜨는 프로젝트므로 디버그 콘솔로 출력하기.

2024.04.08
코어 프로젝트가 제공할 기능 정리해보기.
서버 공용의 기능이 어떤 것이 있는가?


2024.05.21
4단계 레이어 구조와 각 폴더 역활 초안 작성

Presentation Layer
Controller : 사용자(클라이언트)의 입력을 받고 응답 값을 반환 합니다.
DtoModel : 서버와 db 간의 데이터 정보를 모델링
ViewModel : 클라이언트와 서버간의 payload 데이터를 모델링


Business Logic Layer
Service : 비즈니스 로직을 처리하는 단계

Data Access Layer
Repository : 데이터 베이스 접근 및 비즈니스 로직을 처리하는 쿼리.

Database Layer
DbContext : 실제 데이터베이스 접근


