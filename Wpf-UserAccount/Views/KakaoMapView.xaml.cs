using System;
using System.Windows.Controls;

namespace Wpf_UserAccount.Views
{
    /// <summary>
    /// KakaoMapView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class KakaoMapView : UserControl
    {
        public KakaoMapView()
        {
            InitializeComponent();
            LoadKakaoMap();
        }

        private void LoadKakaoMap()
        {
            try
            {
                KakaoMapWebBrowser.BeginInit();
                string htmlUrl = "http://localhost:6161/kakaomap.html";
                KakaoMapWebBrowser.Navigate(new Uri(htmlUrl));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fail to Load the KakaoMap: {ex.Message}");
            }

        }

    }

}
