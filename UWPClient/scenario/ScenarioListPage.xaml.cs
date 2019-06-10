using Graighle.Triping.UWPClient.Scenario.Editor;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Graighle.Triping.UWPClient.Scenario
{
    /// <summary>
    /// シナリオ一覧ページ。
    /// </summary>
    public sealed partial class ScenarioListPage : Page
    {
        private ScenarioListViewModel viewModel = null;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioListPage()
        {
            this.InitializeComponent();

            this.DataContext = this.viewModel = new ScenarioListViewModel();
        }

        /// <summary>
        /// ページが読み込まれたときの処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await this.UpdateScenarioList();
        }

        /// <summary>
        /// シナリオ追加ボタンが押された処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            if(Window.Current.Content is Frame rootFrame)
            {
                rootFrame.Navigate(typeof(ScenarioEditorMainPage));
            }
        }

        /// <summary>
        /// シナリオリストを更新する。
        /// </summary>
        /// <returns></returns>
        private async Task UpdateScenarioList()
        {
            try
            {
                await this.viewModel.LoadScenarioList();
            }
            catch(Exception)
            {
                //ERRMSG
            }
        }
    }
}
