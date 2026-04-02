# 🧑‍💻 [Side Project]_사용자 계정 생성 및 로그인

---

## 1. 사용 언어, [UI], 사용 기술 및 [DB]
### 1) 사용 언어: C#
### 2) 사용 개발 프레임워크(UI): Wpf
### 3) 사용 기술: MVVM 아키텍처(디자인) 패턴
### 4) 파일 시스템(File System): [DB] 생성
### 5) 카카오 맵 API 사용: [MAP] 도시

---

## 2. [Wpf-UserAccount]_프로젝트
### 1) [Common]_Folder
   <1> ParameterRelayCommand.cs 파일
   - ICommand를 상속 받아 UI에서 이벤트 받아 처리하는 함수 구현 2
     
   <2> RelayCommand.cs 파일
   - ICommand를 상속 받아 UI에서 이벤트 받아 처리하는 함수 구현 1


### 2) [Converters]_Folder
   <1> SecureStringHelper.cs 파일
   - [사용자 패스워드] 입력 칸: [기밀 텍스트(SecureString) => 일반 텍스트(UnsecureString)] 변환 함수
   - [사용자 패스워드] 입력 칸: [일반 텍스트(SecureString) => 기밀 텍스트(UnsecureString)] 변환 함수
     
   <2> VisibilityConverter.cs 파일
   - [사용자 패스워드 표시] 체크 박스: [BooleanToVisibilityConverter] 변환 함수
   - [사용자 패스워드 표시] 체크 박스: [InverseBooleanToVisibilityConverter] 역변환 함수

   
### 3) [CustomControls]_Folder
   <1> BindablePasswordBox.xaml 파일
   - [View]: 마이너 [APP]의 [Wpf] 및 [View] 담당
   - [Wpf]: [PasswordBox] 정보 화면
   - [PasswordChanged]="OnPasswordChanged" (패스워드 입력 및 변화 시 인식)
     
   <2> BindablePasswordBox.xaml.cs 파일
   - [DependencyProperty] SecurePasswordProperty 추가
   - [DependencyProperty] IsPasswordVisibleProperty 추가
   - [DependencyProperty] PasswordProperty 추가
   - [DependencyProperty] IsClearProperty 추가

     
### 4) [Images]_Folder
   <1> back-image.jpg 이미지 파일
   - [배경(백그라운드)] 아이콘
     
   <2> displayPw-icon.png 이미지 파일
   - [패스워드 표시] 아이콘
     
   <3> key-icon.png 이미지 파일
   - [패스워드 입력] 아이콘
     
   <4> kakao-image.png 이미지 파일
   - [카카오(Kakao) 기업] 아이콘
     
   <5> user-icon.png 이미지 파일
   - [사용자 이름] 아이콘

   
### 5) [ViewModels]_Folder
   <1> LoginVM.cs 파일
   - [Model] 및 [ViewModel] 통합
   - [Model]: 프로퍼티(데이터 처리 및 저장)
   - [ViewModel]: [View]에서 들어온 데이터 가공 및 [Model]과 통신 후, 데이터 바인딩으로 [View] 갱신
   - [Main] 기능 및 함수 구현
   - [Action] 사용, 특정 이벤트(사용자 정보 초기화, 사용자 로그인 후) 처리
   - [userinfo.dat] 파일 생성을 위한 특정 경로 설정
   - [UserInfoCollection] => [UserName] + [SecurePassword] [동적 데이터 컬렉션] 저장(회원가입 목적)

   
### 6) [Views]_Folder
   <1> LoginView.xaml 파일
   - [View]: 메인 [App]의 [Wpf] 및 [View] 담당 1
   - [Wpf]: [사용자] 정보 화면 (틀)
     
   <2> LoginView.xaml.cs 파일
   - [로그인 배경 화면]
   - [DataContext 연결] (LoginVM)
   - [사용자 정보] 초기화 후, [txtUser] to Focus() 이벤트 발생
   - [사용자 로그인] 후, 카카오맵 브라우저 to Navigate() 이벤트 발생
   - [Window 크기 변환] 이벤트 발생
   - [New 너비 및 높이] 메서드 함수
   - [Mouse 좌클릭] 이벤트 발생
   - [최소화 버튼] 클릭 이벤트
   - [최대화 버튼] 클릭 이벤트
     * 버튼 클릭 시, 해상도에 맞춰 확대 및 원본 축소 기능 구현
   - [창닫기 버튼] 클릭 이벤트

   <3> LoginContentView.xaml 파일
   - [View]: 메인 [App]의 [Wpf] 및 [View] 담당 2
   - [Wpf]: [사용자] 정보 화면 (내용)

   <4> LoginContentView.xaml.cs 파일
   - [로그인 내용 화면]
   - [사용자 정의 컨트롤 화면]
   - [DataContext 연결] (LoginVM)

   <5> kakaoMapView.xaml 파일
   - [View]: [카카오맵] [View] 담당
   - [Wpf]: [카카오맵] 배경 화면
   - [NuGet] 패키지 [WebView2] 설치

   <6> kakaoMapView.xaml.cs 파일
   - [로그인 이후 화면]
   - [사용자 정의 컨트롤 화면]
   - [카카오맵 Uri 추가] : http://localhost:6161/kakaomap.html
   - [WebView2] 초기화 및 캐시 방지 쿼리 매개변수 추가
   - [Geolocation] 요청의 [Permission] 처리 (사용자 위치 설정)
   - [위치 권한] 허용 여부 팝업 창 표시 (사용자 선택 설정)

---

## 3. 버튼 및 텍스트 블럭
#### [1] [사용자 회원가입]_버튼

#### [2] [사용자 로그인]_버튼

#### [3] [사용자 패스워드 초기화]_텍스트 블럭

---

## 4. 기능 및 함수
### 1) 기능
   <1> [사용자 회원가입] 기능
   
####    *  [함수] Sign_Up() 구성
#####   [1] [함수] ValidateUsername(): [사용자 이름] 유효성 검사 함수
   
#####   [2] [함수] IsUserDuplicateCheck(): [사용자 이름] 중복성 확인 함수
   
#####   [3] [함수] ValidatePassword(): [사용자 패스워드] 유효성 검사 함수
   
#####   [4] [함수] SaveUsersInfo(): [사용자 정보저장] 기능
   
#####   [5] [함수] ResetUserInfo(): [사용자 패스워드 초기화] 기능

   
   <2> [사용자 정보저장] 기능
   
####    * [함수] SaveUsersInfo(): (File System)을 활용하여, 데이터베이스(DB)를 대체 구현

     
   <3> [사용자 로그인 이전] 기능
   
####    * [함수] Log_In() 구성
#####    [1] [함수] GetUsersPassword(UserName, DisplayPassword): [사용자 패스워드] 불러오기 함수

#####    [2] [함수] SecureStringHelper.ConvertToUnsecureString() 함수: [SecureString] to [UnSecureString] 변환

#####    [3] [함수] ResetUserInfo(): [사용자 정보 초기화] 기능

#####    [4] [함수] ResetPassword(): [사용자 패스워드 초기화] 기능


   <4> [사용자 로그인 이후] 기능

#####   [1] [파일] kakaomap.html: [사용자 로그인] 성공 후, [카카오 맵] 내 현재 위치 또는 사는 장소 도시 기능
-       [kakaomap.html] WEB 파일(Javascript + html) 추가 완료

   
   <5> [사용자 정보 초기화] 기능
   
####    * [함수] ResetUserInfo(): 사용자 정보를 초기화


   <6> [사용자 패스워드 초기화] 기능
   
####    * [함수]: ResetPassword(): 사용자 패스워드를 초기화


### 2) 함수
   <1> [사용자 이름] 유효성 검사 함수
   
####    * [함수] ValidateUsername()
-       사용자 이름 입력 시, 유효성 검사 => 예외 처리

   
   <2> [사용자 이름] 중복성 확인 함수
   
####    * [함수] IsUserDuplicateCheck()
-      사용자 이름 입력 시, 중복성 확인 => 중복 제외

   
   <3> [사용자 패스워드] 유효성 검사 함수
   
####    * [함수] ValidatePassword()
-      사용자 패스워드 입력 시, 유효성 검사 => 예외 처리

   
   <4> [사용자 패스워드] 불러오기 함수
   
####    * [함수] GetUsersPassword(UserName, DisplayPassword)
-      사용자 패스워드 불러오기

   
   <5> [userInfo.dat] 파일 삭제 함수
   
####    * [함수] OnProcessExit(object sender, EventArgs e)
-      [사용자 정보파일] 삭제하기

---

## 5. 기술적 성과
### 1) MVVM 기반 구조 설계
      - View / ViewModel / Model 역할 분리로 유지보수성과 확장성 확보
      - Command 패턴(RelayCommand, ParameterRelayCommand) 직접 구현
      - DataBinding 중심 구조로 UI와 로직 완전 분리

### 2) SecureString 기반 보안 처리
      - SecureString 활용하여 비밀번호 메모리 누출 최소화
      - Custom Converter TO Secure <-> Unsecure 변환 구현
      - 일반 PasswordBox 한계를 해결한 BindablePasswordBox 직접 구현

### 3) 파일 시스템 기반 DB 설계
      - 별도 DB 없이 userInfo.dat 활용하여 경량 사용자 관리 시스템 구현
      - 사용자 정보 저장 / 조회 / 삭제 로직 직접 설계
      - 중복 체크 + 유효성 검사 + 데이터 영속성 처리

### 4) 사용자 인증 흐름 구현
      - 회원가입 / 로그인 / 초기화 전체 로직 설계
      - Username & Password 유효성 검사 로직 구축
      - 로그인 성공 시, View 전환 및 후처리 이벤트 구성

### 5) Custom Control 개발 (WPF 심화)
      - BindablePasswordBox 제작 (DependencyProperty 활용)
      - Password 표시/숨김 기능 구현
      - WPF 기본 컨트롤 한계를 확장한 사례

### 6) WebView2 + 외부 API 연동
      - kakao Map API 연동
      - WebView2 기반 웹 컨텐츠 로드
      - 위치 권한 처리 및 지도 표시 기능 구현

### 7) 사용자 경험(UX) 개선 기능
      - 로그인 후, 자동 포커스 및 화면 전환 처리
      - 창 크기 변경 / 최소화 / 최대화 이벤트 구현
      - 직관적인 UI 흐름 설계

### 8) 예외 처리 및 안정성 강화
      - 입력값 검증 (Username / Password)
      - 중복 사용자 방지 로직
      - 중료 시, 사용자 데이터 삭제 처리 (OnProcessExit)

---

## 6. 한 줄 요약
      - WPF MVVM 기반 사용자 인증 시스템을 설계 및 보안.UI.데이터 처리 전반 직접 구현 프로젝트

---

## 7. 프로그램 UI 구성
---
### 7-1. 사용자 정보 화면
<img width="467" height="344" alt="사용자 정보 화면" src="https://github.com/user-attachments/assets/04b2e495-5708-4044-aa36-8c629b44d35e" />

### 7-2. 회원가입 성공 화면
<img width="563" height="412" alt="회원가입 성공 화면" src="https://github.com/user-attachments/assets/e3414c17-1de6-4717-ad6f-207d922300ba" />

### 7-3. [Login DB] File System 저장 화면
<img width="928" height="469" alt="Login DB  File System 저장 화면" src="https://github.com/user-attachments/assets/203dc4a8-54a2-4dbc-a92d-adc0870023d3" />

### 7-4. 로그인 성공 화면
<img width="563" height="411" alt="로그인 성공 화면" src="https://github.com/user-attachments/assets/e5d684a9-c471-4134-9b1d-f4b88d293140" />

### 7-5. 사용자 위치 표시 화면 (지도)
<img width="563" height="413" alt="사용자 위치 표시 화면 (지도)" src="https://github.com/user-attachments/assets/a87d6285-f685-43cb-993d-bdc8eb67a414" />

### 7-6. 사용자 위치 표시 화면 (스카이뷰)
<img width="563" height="413" alt="사용자 위치 표시 화면 (스카이뷰)" src="https://github.com/user-attachments/assets/c9a2a2e9-0b8d-4c9a-93ff-4acf34fe1200" />

---

### 8. 프로젝트 코드 화면

<img width="1280" height="764" alt="프로젝트 코드 화면" src="https://github.com/user-attachments/assets/dc51f860-db4f-47c0-9338-e592cb23c192" />

---

## 9. 실행 방법
```bash
git clone https://github.com/사용자명/Wpf-UserAccount.git
