using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace Wpf_UserAccount.CustomControls
{
    /// <summary>
    /// BindablePasswordBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SecurePasswordProperty =
               DependencyProperty.Register("SecurePassword", typeof(SecureString), typeof(BindablePasswordBox), new PropertyMetadata(default(SecureString)));

        public SecureString SecurePassword
        {
            get => (SecureString)GetValue(SecurePasswordProperty);
            set => SetValue(SecurePasswordProperty, value);
        }

        public static readonly DependencyProperty IsPasswordVisibleProperty =
               DependencyProperty.Register("IsPasswordVisible", typeof(bool), typeof(BindablePasswordBox), new PropertyMetadata(false));

        public bool IsPasswordVisible
        {
            get => (bool)GetValue(IsPasswordVisibleProperty);
            set => SetValue(IsPasswordVisibleProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty =
               DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox), new PropertyMetadata(string.Empty));

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                SecurePassword = passwordBox.SecurePassword;
                Password = passwordBox.Password;
            }

        }

        public static readonly DependencyProperty IsClearProperty =
               DependencyProperty.Register("IsClear", typeof(bool), typeof(BindablePasswordBox), new PropertyMetadata(false, OnIsClearChanged));

        public bool IsClear
        {
            get => (bool)GetValue(IsClearProperty);
            set => SetValue(IsClearProperty, value);
        }

        private static void OnIsClearChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is BindablePasswordBox bindablePasswordBox)
            {
                if (e.NewValue is bool isClear && isClear)
                {
                    bindablePasswordBox.passwordBox.Clear();
                    bindablePasswordBox.IsPasswordVisible = false;
                    bindablePasswordBox.IsClear = false;
                }

            }

        }

    }

}