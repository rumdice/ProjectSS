# ProjectSS
인프라 : AWS EKS, AWS SNS, AWS SQS
빌드/배포 : Github CI/CD
프레임워크 : Blazor
언어 : C# (dotnet 8.0)
DB : mariaDB, redis
컨테이너 : k8s, docker
기타 : minikube



# 구성
CoreLib : 코어 라이브러리 코드 (dll)
CoreDB : 코어 라이브러리 코드 중 DB 기능 담당 전용
WebApp : 웹 서비스 API 서버
PushApp : 알람 서비스
LogApp : 운영툴
UnitTest : 서비스 통합 유닛 테스트 코드



# branch
dev : 개발 브랜치
main : 메인 브랜치



# 로컬 환경 실행
1. 도커 실행
2. Minikube start
3. Minikube tunnel
- (패스워드 입력)
4. WebApp 실행
5. ChatConsoleApp 실행
- 채팅 메시지 전송 가능


# 프로젝트 구조 리뷰
CoreSystem
- 코어 공용모듈

WebApp
- 기본 API 서버
- messageHub으로 채팅 서버 역활도 함

WebApp.Tests
- WepApp 단위테스트 프로젝트

ChatConsoleApp
- 단순 채팅 콘솔 앱 (메시지 송수신 확인)

PushApp
- 알람서버 (제작중)

LogApp
- 사진을 다루는 앱 기반 이것저것 기능 제작중 (운영툴)


rabbit mq 설치
docker run -it --rm --name rabbitmq -p 5552:5552 -p 15672:15672 -p 5672:5672  -e RABBITMQ_SERVER_ADDITIONAL_ERL_ARGS='-rabbitmq_stream advertised_host localhost' rabbitmq:3.13    

확장 설치
docker exec rabbitmq rabbitmq-plugins enable rabbitmq_stream rabbitmq_stream_management 

마리아 DB 설치
docker run -d \
  --name mariadb \
  -e MARIADB_ROOT_PASSWORD=pass1234 \
  -p 13306:3306 \
  mariadb:latest


