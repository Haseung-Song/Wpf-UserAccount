﻿using Soletop.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;
using Wpf_UserAccount.Common;
using Wpf_UserAccount.Converters;

namespace Wpf_UserAccount.ViewModels
{
    public class LoginVM : PropertyModel
    {
        #region [프로퍼티]

        // [IsUserNameExist]
        public bool IsUserNameExist { get; set; }

        // [IsPassWordExist]
        public bool IsPassWordExist { get; set; }

        /// <summary>
        /// Event to Notify After the User Login;
        /// </summary>
        public event Action AfterUserLoginAction;

        /// <summary>
        /// Event to Notify If UserInfo is Reset;
        /// </summary>
        public event Action ResetInfoFocusAction;

        /// <summary>
        /// [userInfo.dat] 파일 경로
        /// </summary>
        public string FilePath = @"C:\Users\user\source\repos\Wpf-UserAccount (2)\Wpf-UserAccount\Wpf-UserAccount\Wpf-UserAccount\bin\Debug\userInfo.dat";

        /// <summary>
        /// [UserInfoCollection] : 회원가입을 위한 [UserName] + [SecurePassword] (동적 데이터 컬렉션) 저장
        /// </summary>
        public ObservableCollection<LoginVM> UserInfoCollection { get => (ObservableCollection<LoginVM>)this["UserInfoCollection"]; set => this["UserInfoCollection"] = value; }

        /// <summary>
        /// [사용자 이름]
        /// </summary>
        public string UserName { get => (string)this["UserName"]; set => this["UserName"] = value; }

        /// <summary>
        /// [사용자 패스워드]
        /// </summary>
        public SecureString SecurePassword { get => (SecureString)this["SecurePassword"]; set => this["SecurePassword"] = value; }

        /// <summary>
        /// [에러 메시지]
        /// </summary>
        public string ErrorMessage { get => (string)this["ErrorMessage"]; set => this["ErrorMessage"] = value; }

        /// <summary>
        /// [사용자 패스워드 표시]
        /// </summary>
        public string DisplayPassword
        {
            get => IsPasswordVisible ? SecureStringHelper.ConvertToUnsecureString(SecurePassword) : (string)this["DisplayPassword"];
            set => this["DisplayPassword"] = value;
        }

        /// <summary>
        /// [사용자 패스워드 표시 여부]
        /// </summary>
        public bool IsPasswordVisible { get => (bool)this["IsPasswordVisible"]; set => this["IsPasswordVisible"] = value; }

        /// <summary>
        /// [사용자 패스워드 리셋 여부]
        /// </summary>
        public bool ClearPasswordFlag { get => (bool)this["ClearPasswordFlag"]; set => this["ClearPasswordFlag"] = value; }

        #endregion

        #region [ICommand]

        // 1. 사용자 회원가입
        public ICommand Sign_UpCommand { get; }

        // 2. 사용자 로그인
        public ICommand Log_InCommand { get; }

        // 3. 사용자 패스워드 초기화
        public ICommand ResetPasswordCommand { get; }

        #endregion

        #region 생성자 (Initialize)

        public LoginVM()
        {
            IsPasswordVisible = false;
            ClearPasswordFlag = false;
            UserInfoCollection = new ObservableCollection<LoginVM>();
            Sign_UpCommand = new RelayCommand(Sign_Up);
            Log_InCommand = new RelayCommand(Log_In);
            ResetPasswordCommand = new RelayCommand(ResetPassword);
            // Add event handler for process exit to delete file.
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        }

        #endregion

        #region [버튼기능]

        /// <summary>
        /// 1. [사용자 회원가입] 기능
        /// </summary>
        private void Sign_Up()
        {
            ValidateUsername();

            if (IsUserNameExist)
            {
                if (IsUserDuplicateCheck())
                {
                    _ = MessageBox.Show("해당 Username이 중복입니다. 다른 Username을 입력하세요.", "Username 재입력 요청", MessageBoxButton.OK, MessageBoxImage.Error);
                    ResetUserInfo();
                    return;
                }

                ValidatePassword();

                if (IsPassWordExist)
                {
                    SaveUsersInfo();
                    _ = MessageBox.Show("Password가 유효합니다. 회원가입에 성공하였습니다.", "사용자 회원가입 성공", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetUserInfo();
                    return;
                }

            }

        }

        /// <summary>
        /// [사용자 이름] 유효성 검사 함수
        /// </summary>
        /// <returns></returns>
        private void ValidateUsername()
        {
            IsUserNameExist = true;
            try
            {
                if (string.IsNullOrWhiteSpace(UserName))
                {
                    IsUserNameExist = false;
                    _ = MessageBox.Show("Username은 비어 있거나 공백 문자로만 구성될 수 없습니다.", "Username 재입력 요청", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new Exception("입력된 문자가 없습니다.");
                }
                else if (!(UserName.Length >= 4 && UserName.Length <= 20))
                {
                    IsUserNameExist = false;
                    _ = MessageBox.Show("Username은 최소 4자 이상, 20자 이하로 구성되어야 합니다.", "Username 재입력 요청", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new Exception("올바른 개수가 아닙니다.");
                }
                else
                {
                    foreach (char c in UserName)
                    {
                        if (!((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9')))
                        {
                            IsUserNameExist = false;
                            _ = MessageBox.Show("Username은 알파벳 소문자(a-z), 숫자(0-9)로 구성되어야 합니다.", "Username 재입력 요청", MessageBoxButton.OK, MessageBoxImage.Error);
                            throw new Exception("유효한 구성이 아닙니다.");
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Invalid Username: {ex.Message}";
            }
            finally
            {
                if (IsUserNameExist && ErrorMessage == null)
                {
                    Console.WriteLine($"Valid Username: 유효한 사용자 이름입니다.");
                }

            }

        }

        /// <summary>
        /// [사용자 이름] 중복성 확인 함수
        /// </summary>
        /// <returns></returns>
        private bool IsUserDuplicateCheck()
        {
            // 재시작 시, [UserInfoCollection]이 초기화 되므로 주의.
            // 즉, 프로그램 실행 중에만 회원가입 후, DB 저장이 가능.
            foreach (LoginVM userInfo in UserInfoCollection)
            {
                if (userInfo.UserName == UserName)
                {
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// [사용자 패스워드] 유효성 검사 함수
        /// </summary>
        /// <returns></returns>
        private void ValidatePassword()
        {
            if (IsUserNameExist)
            {
                IsPassWordExist = true;
                if (SecurePassword == null || SecurePassword.Length == 0)
                {
                    IsPassWordExist = false;
                    _ = MessageBox.Show("Password가 입력되지 않았습니다.", "Password 재입력 요청", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                IntPtr unmanagedString = IntPtr.Zero; // [SecureString(unmanagedString)] 초기화
                try
                {
                    // Convert [SecureString(unmanagedString)] to [SecureString(managedString)]
                    unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(SecurePassword);
                    string securePassword = Marshal.PtrToStringUni(unmanagedString);

                    // [securePassword] 소문자(a-z) 요소 존재 여부 확인
                    bool hasLowerCase = securePassword.Any(c => char.IsLower(c));

                    // [securePassword] 대문자(A-Z) 요소 존재 여부 확인
                    bool hasUpperCase = securePassword.Any(c => char.IsUpper(c));

                    // [securePassword] 숫자(0 - 9) 요소 존재 여부 확인
                    bool hasDigitCase = securePassword.Any(c => char.IsDigit(c));

                    // [securePassword] 특수문자 존재 여부 확인
                    bool hasSpecialCharCase = securePassword.Any(c => char.IsPunctuation(c));

                    if (SecurePassword != null && SecurePassword.Length > 0)
                    {
                        if (!(securePassword.Length >= 10))
                        {
                            IsPassWordExist = false;
                            _ = MessageBox.Show("Password는 최소 10자 이상으로 구성되어야 합니다.", "Password 재입력 요청", MessageBoxButton.OK, MessageBoxImage.Error);
                            throw new Exception("올바른 개수가 아닙니다.");
                        }
                        else if (!(hasLowerCase && hasUpperCase && hasDigitCase && hasSpecialCharCase))
                        {
                            IsPassWordExist = false;
                            _ = MessageBox.Show("Password는 대문자(A-Z)와 소문자(a-z), 숫자(0-9) 및 특수문자의 혼합으로 구성되어야 합니다.", "Password 재입력 요청", MessageBoxButton.OK, MessageBoxImage.Error);
                            throw new Exception("유효한 구성이 아닙니다.");
                        }
                        else
                        {
                            IsPasswordVisible = false;
                            ErrorMessage = null;
                        }

                    }

                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Invalid Password: {ex.Message}";
                }
                finally
                {
                    if (unmanagedString != IntPtr.Zero)
                    {
                        Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString); // 할당한 [unmanagedString] 메모리 해제
                    }

                }

            }
            return;
        }

        /// <summary>
        /// 2. [사용자 정보저장] 기능
        /// </summary>
        private void SaveUsersInfo()
        {
            if (UserName?.Length != 0 && DisplayPassword?.Length != 0)
            {
                Dictionary<string, string> userTable = new Dictionary<string, string> { { UserName, DisplayPassword } };
                foreach (KeyValuePair<string, string> item in userTable)
                {
                    UserInfoCollection?.Add(new LoginVM { UserName = item.Key, DisplayPassword = item.Value });
                }

                try
                {
                    // [userInfo.dat] 파일 생성!
                    // [기본 경로]는 [bin] 폴더에 생성!
                    FileStream fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);

                    // [userInfo.dat] 파일 쓰기!
                    StreamWriter streamWriter = new StreamWriter(fileStream);
                    foreach (LoginVM userInfo in UserInfoCollection)
                    {
                        streamWriter?.WriteLine(userInfo.UserName);
                        streamWriter?.WriteLine(userInfo.DisplayPassword);
                    }
                    streamWriter?.Close();
                    streamWriter?.Dispose();
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Fail to Save: {ex.Message}";
                }

            }

        }

        /// <summary>
        /// 3. [사용자 로그인] 기능
        /// </summary>
        private void Log_In()
        {
            string storedPassword = GetUsersPassword(UserName, DisplayPassword);
            if (IsPassWordExist)
            {
                if (storedPassword?.Length != 0)
                {
                    string inputPassword = SecureStringHelper.ConvertToUnsecureString(SecurePassword);
                    if (inputPassword?.Length == 0)
                    {
                        IsPassWordExist = false;
                        _ = MessageBox.Show("해당 Password를 입력하세요.", "UserName 활성화", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (inputPassword == storedPassword)
                        {
                            _ = MessageBox.Show("Password가 맞았습니다. 로그인에 성공하였습니다.", "사용자 로그인 성공", MessageBoxButton.OK, MessageBoxImage.Information);
                            ResetUserInfo();
                            AfterUserLoginAction?.Invoke();
                            return;
                        }
                        else
                        {
                            _ = MessageBox.Show("Password가 틀렸습니다. 로그인에 실패하였습니다.", "사용자 로그인 실패", MessageBoxButton.OK, MessageBoxImage.Information);
                            ResetPassword();
                            return;
                        }

                    }

                }

            }

        }

        /// <summary>
        /// [사용자 패스워드] 불러오기 함수
        /// </summary>
        /// <param name="displayPassword"></param>
        /// <returns></returns>
        private string GetUsersPassword(string userName, string displayPassword)
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    using (StreamReader streamReader = new StreamReader(FilePath))
                    {
                        string line = string.Empty;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            string fileUserName = line;
                            string filePassword = streamReader.ReadLine();
                            if (fileUserName == userName)
                            {
                                Console.WriteLine($"Right Username: 올바른 사용자 이름입니다.");
                                displayPassword = filePassword;
                                IsPassWordExist = true;
                                break;
                            }

                        }
                        if (line != userName)
                        {
                            _ = MessageBox.Show("UserName을 다시 확인하세요.", "UserName 미등록", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        streamReader?.Close();
                        streamReader?.Dispose();
                    }

                }
                else
                {
                    _ = MessageBox.Show("파일이 먼저 생성되어야 합니다.", "[userInfo.dat] 파일 미생성", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Fail to Read: {ex.Message}";
            }
            finally
            {
                if (IsPassWordExist == false)
                {
                    if (displayPassword == null || displayPassword == string.Empty)
                    {
                        Console.WriteLine($"Reading Failure: [사용자 패스워드] 불러오기 실패");
                    }

                }

            }
            return displayPassword;
        }

        /// <summary>
        /// 4. [사용자 정보 초기화] 기능
        /// </summary>
        private void ResetUserInfo()
        {
            ClearPasswordFlag = false;
            if (UserName != null && DisplayPassword != null)
            {
                UserName = string.Empty;
                SecurePassword?.Clear();
                DisplayPassword = string.Empty;
                ClearPasswordFlag = true;
            }
            ErrorMessage = string.Empty;
            ResetInfoFocusAction?.Invoke();
        }

        /// <summary>
        /// 5. [사용자 패스워드 초기화] 기능
        /// </summary>
        private void ResetPassword()
        {
            ClearPasswordFlag = false;
            if (DisplayPassword != null)
            {
                SecurePassword?.Clear();
                DisplayPassword = string.Empty;
                ClearPasswordFlag = true;
            }
            ErrorMessage = string.Empty;
        }

        /// <summary>
        /// [userInfo.dat] 파일 삭제 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnProcessExit(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    Console.WriteLine($"Succeed to Delete: [userInfo.dat] 파일을 성공적으로 삭제했습니다.");
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Fail to Delete: {ex.Message}";
            }

        }
        #endregion
    }

}
