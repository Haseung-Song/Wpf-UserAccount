# [Side Project]_사용자 계정 생성 및 로그인


## 1. 사용 언어, [UI], 사용 기술 및 [DB]
### 1) 사용 언어: C#
### 2) 사용 개발 프레임워크(UI): Wpf
### 3) 사용 기술: MVVM 아키텍처(디자인) 패턴
### 4) 파일 시스템(File System): [DB] 생성
### 5) 카카오 맵 API 사용: [MAP] 도시




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
     
   <4> SOLETOP_03.png 이미지 파일
   - [솔탑(SOLETOP) 기업] 아이콘
     
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
   - [UserInfoCollection] => 회원가입 목적 [UserName] + [SecurePassword] (동적 데이터 컬렉션) 저장

   
### 6) [Views]_Folder
   <1> LoginView.xaml 파일
   - [View]: 메인 [App]의 [Wpf] 및 [View] 담당 1
   - [Wpf]: [사용자] 정보 화면 (틀)
     
   <2> LoginView.xaml.cs 파일
   - [로그인 배경 화면]
   - [DataContext 연결] (LoginVM)
   - [사용자 정보] 초기화 후, [txtUser] to Focus() 이벤트 발생
   - [사용자 로그인] 후, 카카오맵 브라우저 향해 이벤트 발생
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



## 3. 버튼 및 텍스트 블럭
#### [1] [사용자 회원가입]_버튼

#### [2] [사용자 로그인]_버튼

#### [3] [사용자 패스워드 초기화]_텍스트 블럭




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




## 5. [App] 실행 시, 초기 화면
<img width="551" alt="사용자 정보 화면" src="https://github.com/Haseung-Song/Wpf-UserAccount/assets/63398933/7bec9614-f28d-4663-ac2e-1361374e1981">




## 6. [App] 실행 시, 기능 녹화
https://github.com/Haseung-Song/Wpf-UserAccount/assets/63398933/af83086d-db28-436a-9d08-5a666a0b2d71



