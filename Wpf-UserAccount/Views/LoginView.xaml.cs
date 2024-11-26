using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Wpf_UserAccount.ViewModels;

namespace Wpf_UserAccount.Views
{
    /// <summary>
    /// LoginView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginContentView LoginContentView { get; } = new LoginContentView();

        private readonly ScaleTransform scale = new ScaleTransform();

        private bool isDoubleClicking = true;
        private readonly double originalWidth, originalHeight = 0;

        /// <summary>
        /// [로그인 화면]
        /// </summary>
        public LoginView()
        {
            InitializeComponent();
            LoginVM viewModel = new LoginVM();
            DataContext = viewModel;
            originalWidth = Width;
            originalHeight = Height;
            SizeChanged += Window_SizeChanged;
            viewModel.ResetInfoFocusAction += OnResetInfoFocusAction;
            viewModel.AfterUserLoginAction += NavigateToKakaoMapView;
            LoginContentView.DataContext = viewModel;
            MainFrame.Navigate(LoginContentView);
        }

        /// <summary>
        /// [사용자 정보] 초기화 후, [txtUser] to Focus() 이벤트 발생
        /// </summary>
        public void OnResetInfoFocusAction()
        {
            _ = (LoginContentView?.txtUser.Focus());
        }

        /// <summary>
        /// [사용자 로그인] 이후 [KakaoMap] 브라우저 향해 이벤트 발생
        /// </summary>
        public void NavigateToKakaoMapView()
        {
            _ = MainFrame.Navigate(new KakaoMapView());
            txtblkUserInfo.Text = "카카오 맵 화면";
        }

        /// <summary>
        /// [Window 크기 변환] 이벤트 발생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize(e.NewSize.Width, e.NewSize.Height); // New 너비 및 높이
        }

        /// <summary>
        /// [New 너비 및 높이] 메서드 함수
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void ChangeSize(double width, double height)
        {
            scale.ScaleX = width / originalWidth;
            scale.ScaleY = height / originalHeight;
            FrameworkElement rootElement = Content as FrameworkElement;
            rootElement.LayoutTransform = scale;
        }

        /// <summary>
        /// [Mouse 좌클릭] 이벤트 발생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        /// <summary>
        /// [최소화 버튼] 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// [최대화 버튼] 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (isDoubleClicking)
            {
                WindowState = WindowState.Maximized;
                ChangeSize(ActualWidth, ActualHeight);
                isDoubleClicking = false;
            }
            else
            {
                WindowState = WindowState.Normal;
                isDoubleClicking = true;
            }

        }

        /// <summary>
        /// [창닫기 버튼] 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }

}
