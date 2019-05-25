using Graighle.Triping.UWPClient.Scenario.Editor;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Graighle.Triping.UWPClient.Scenario
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class ScenarioListPage : Page
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioListPage()
        {
            this.InitializeComponent();
        }

        public void AddScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            if(Window.Current.Content is Frame rootFrame)
            {
                rootFrame.Navigate(typeof(ScenarioEditorMainPage));
            }
        }
    }
}
