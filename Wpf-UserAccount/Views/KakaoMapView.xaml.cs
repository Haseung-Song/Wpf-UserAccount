using Microsoft.Web.WebView2.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Wpf_UserAccount.Views
{
    public partial class KakaoMapView : UserControl
    {
        public KakaoMapView()
        {
            InitializeComponent();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            try
            {
                await KakaoMapWebView.EnsureCoreWebView2Async(null, null);
                KakaoMapWebView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
                KakaoMapWebView.CoreWebView2.PermissionRequested += CoreWebView2_PermissionRequested;

                // 캐시 방지를 위한 쿼리 매개변수를 URL에 추가합니다.
                Uri uri = new Uri($"http://localhost:6161/kakaomap.html?nocache={DateTime.Now.Ticks}");
                KakaoMapWebView.CoreWebView2.Navigate(uri.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WebView2 초기화 실패: {ex.Message}");
                _ = MessageBox.Show("지도를 초기화하는 데 실패했습니다. 나중에 다시 시도하세요.", "초기화 오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            Console.WriteLine("Navigation completed.");
        }

        // 이 이벤트는 [Geolocation] 요청의 [Permission]을 처리합니다.
        private void CoreWebView2_PermissionRequested(object sender, CoreWebView2PermissionRequestedEventArgs e)
        {
            if (e.PermissionKind == CoreWebView2PermissionKind.Geolocation)
            {
                // 사용자에게 [위치 권한]을 허용할지 묻는 팝업 창을 표시합니다.
                var result = MessageBox.Show("이 사이트가 당신의 위치에 접근하도록 허용하시겠습니까?", "위치 권한 요청", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // 사용자의 선택에 따라 [위치 권한]을 설정합니다.
                if (result == MessageBoxResult.Yes)
                {
                    e.State = CoreWebView2PermissionState.Allow;
                }
                else
                {
                    e.State = CoreWebView2PermissionState.Deny;
                }

            }
            else
            {
                // [Geolocation] 요청이 아닌 경우에는,
                // 자동으로, [위치 권한]을 거부합니다.
                e.State = CoreWebView2PermissionState.Deny;
            }

        }

    }

}
