using System.Windows.Controls;
using Wpf_UserAccount.ViewModels;

namespace Wpf_UserAccount.Views
{
    /// <summary>
    /// LoginContentView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginContentView : UserControl
    {
        public LoginContentView()
        {
            InitializeComponent();
            DataContext = new LoginVM();
        }

    }

}
