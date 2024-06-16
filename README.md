# [Side Project]_사용자 계정 생성 및 로그인


## 1. 사용 언어, [UI], 사용 기술 및 [DB]
### 1) 사용 언어: C#
### 2) 사용 개발 프레임워크(UI): Wpf
### 3) 사용 기술: MVVM 아키텍처(디자인) 패턴
### 4) 파일 시스템(File System): [DB] 생성




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
   - [Wpf]: [Passw리

   
   <4> [사용자 패스워드] 불러오기 함수
   
####    * [함수] GetUsersPassword(UserName, DisplayPassword): 사용자 패스워드 불러오기

   
   <5> [userInfo.dat] 파일 삭제 함수
   
####    * [함수] OnProcessExit(object sender, EventArgs e): [사용자 정보파일] 삭제하기




## 5. [App] 실행 시, 초기 화면
<img width="551" alt="사용자 정보 화면" src="https://github.com/Haseung-Song/Wpf-UserAccount/assets/63398933/7bec9614-f28d-4663-ac2e-1361374e1981">




## 6. [App] 실행 시, 기능 녹화
https://github.com/Haseung-Song/Wpf-UserAccount/assets/63398933/af83086d-db28-436a-9d08-5a666a0b2d71



