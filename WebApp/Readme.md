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
Entity : 실제 데이터 테이블의 모델링.


2024.05.22
1. repository 와 entity 그리고 Dto Model에 대하어 정립

repository : 비즈리스 로직 처리를 위한 쿼리 
entity : orm에서 쓰이는 DB에 바로맵펭되는 테이블 정보 
dbcontext : 데이터베이스 직접 접근
dto Model : 프레젠테이션 레이어와 서비스 계층 간에 데이터를 전달 목적의 모델링. 


2. 이전에 쿠버네티스에 (로컬 미니쿠베)에 생성해둔 db 사용 및 데이터베이스 생성 (db_WebApp)
관련 이전에 작성했던 블로그 포스팅
https://blog.naver.com/muzinmind/223447482635

appsettings에 관련 DB 정보 입력 -> 나중에 따로 빼자. 

에러 로그

예외 발생: 'MySqlConnector.MySqlException'(System.Private.CoreLib.dll)
crit: WebApp.Controllers.ItemController[0]
      Table 'db_WebApp.ItemSimpleEntity' doesn't exist
WebApp.Controllers.ItemController: Critical: Table 'db_WebApp.ItemSimpleEntity' doesn't exist

해결 :
테이블 직접 생성 초기니까 일단 직접 생성함
(나중에 EF에서 생성하도록 하기)
create table ItemSimpleEntity


예외 발생: 'MySqlConnector.MySqlException'(System.Private.CoreLib.dll)
crit: WebApp.Controllers.ItemController[0]
      Unknown column 'i.UserUid' in 'field list'
WebApp.Controllers.ItemController: Critical: Unknown column 'i.UserUid' in 'field list'

해결 : 
테이블 모양이 entity와 일치 해야 함
CREATE TABLE `ItemSimpleEntity` (
  `UserUid` bigint(11) NOT NULL,
  `ItemTid` bigint(11) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Grade` int(11) DEFAULT NULL,
  PRIMARY KEY (`UserUid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;



2024.05.23
서비스 레이어 추가 및 레이어 계층 다시 정리 하기

표현 레이어
Controller, Model(view, dto)
접근 가능한 것 - Service, Model(view, dto)

Business Logic Layer
Service
접근 가능 한 것 - repository

Data Access Layer
Repository
접근 가능 한 것 - DBContext, Entity

Database Layer
DBContext, Entity
접근 가능한 것 - x 

특징
각 레이어마다 접근 가능한 순서대로 넘어가야한다. 
2단계 3단계 넘어가서 바로 접근 불가능하게 하기.
이 접근 단계 제한을 일단 의존성 주입으로 하고 나중에 코드에서 강제할 방법 찾아보기.

같은 레이어 레벨에서는 상호 접근은 가능하다.
ex) 
controller - model
dbContext - entity


아이템 테이블 모델링 수정 (ORM을 통하여 테이블 생성 및 변경이 되도록 해야 한다!)
- 유저가 어려가지 아이템을 소유 할 수 있음.

CREATE TABLE `ItemSimpleEntity` (
  `ItemUid` bigint(11) NOT NULL AUTO_INCREMENT,
  `UserUid` bigint(11) DEFAULT NULL,
  `ItemTid` bigint(11) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Grade` int(11) DEFAULT NULL,
  PRIMARY KEY (`ItemUid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


테이블과 entity 간의 nullable도 신경 쓰기.


2024.05.27
4계층 레이어를 다시 설정 및 정리. (구성 요소 까지)
각 역활을 명확하게 해야 한다.

Presentation Layer (프레젠테이션 계층):
역활
사용자 인터페이스를 담당하며, 사용자의 요청을 처리하고, 결과를 표시합니다.
구성요소 
ViewModel, DtoModel, Controller  (DtoModel은 걸쳐 있다)

Application Layer (애플리케이션 계층):
역할
비즈니스 로직을 담당합니다. 요청을 처리하고, 도메인 로직을 실행하며, 결과를 Presentation Layer에 전달합니다.
구성요소
Service, Converter

Domain Layer (도메인 계층):
역할
애플리케이션의 핵심 비즈니스 로직과 규칙을 담고 있습니다.
구성요소
Entity, Repository 

Infrastructure/Data Access Layer (인프라/데이터 접근 계층):
역할
데이터의 영속성을 관리하며, 데이터베이스와 상호작용합니다.
구성요소
DBContext

현재 배치한 구성요소가 적합한가? 아직 확신 하기는 어렵다.

MVC 패턴과 4Layered 아키텍쳐에 관하여.
MVC와 4 Layered Architecture는 서로 배타적인 패턴이 아니며, 조합하여 사용할 수 있습니다. 예를 들어, ASP.NET MVC 프로젝트에서 프레젠테이션 계층(Presentation Layer)을 MVC 패턴으로 구성하고, 비즈니스 로직(Application Layer), 도메인 로직(Domain Layer), 데이터 접근 계층(Data Access Layer)을 별도로 분리하여 4 Layered Architecture로 설계할 수 있습니다.

DTOModel의 4계층 설계 위치
DTO (Data Transfer Object) 모델은 4 Layered Architecture에서 주로 Application Layer와 Presentation Layer 사이의 데이터를 전달하는 데 사용됩니다. 이를 통해 각 계층 간의 데이터 통신을 용이하게 하고, 데이터의 형식을 명확히 정의할 수 있습니다.


2024.06.04

오늘 트러블 슈팅 노트 기록

db 접근이 안되는 이슈
db를 쿠버네티스를 통해 db 서비스를 띄운다.
미니쿠베 확인

minikube start
minikube dashboard

서비스 연결이 안된다면 터널링 확인
minikube tunnel

실행이 안되는 원인?
예외가 발생했습니다. CLR/System.Reflection.ReflectionTypeLoadException
'System.Reflection.ReflectionTypeLoadException' 형식의 예외가 Microsoft.AspNetCore.Mvc.Core.dll에서 발생했지만 사용자 코드에서 처리되지 않았습니다.: 'Unable to load one or more of the requested types.'

[해결]프로젝트 참조에 코어 시스템에 있는 것 전부 추가
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" Version="2.2.8" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.2.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.6" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.6">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.6" />
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="8.0.2" />
  </ItemGroup>




2024.06.08
Service 와 Repository

Repository : 쿼리 하나에 대한 명령처리
Service : 컨텐츠의 속성에 따라 여러 reposotory를 연계하여 처리한다.

// 경우에 따라선 단순 쿼리 실행이 아닌 복잡한 비즈니스 로직이 들어갈 수 있다.
// 혹은 여러가지 repository 쿼리의 연계는 보통 이곳 service에서 진행 하자.

// 스코프 처리는 Service에 두는게 좋겠다. 컨트롤러 레벨에 두는건 이상하다.