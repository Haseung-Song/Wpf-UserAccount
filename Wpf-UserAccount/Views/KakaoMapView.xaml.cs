using Microsoft.Web.WebView2.Core;
using System;
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

        // This method ensures that the [WebView2] control is properly initialized.
        private async void InitializeAsync()
        {
            try
            {
                await KakaoMapWebView.EnsureCoreWebView2Async(null);
                KakaoMapWebView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
                // Navigate to [Initial] URI.
                KakaoMapWebView.CoreWebView2.Navigate("http://localhost:6161/kakaomap.html");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize WebView2: {ex.Message}");
            }

        }

        // This event is triggered when the navigation is completed. Here, you can handle [Permission] requests.
        private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            KakaoMapWebView.CoreWebView2.PermissionRequested += CoreWebView2_PermissionRequested;
        }

        // This event handles [Permission] of [Geolocation] request.
        private void CoreWebView2_PermissionRequested(object sender, CoreWebView2PermissionRequestedEventArgs e)
        {
            if (e.PermissionKind == CoreWebView2PermissionKind.Geolocation)
            {
                e.State = CoreWebView2PermissionState.Allow;
            }

        }

    }

}