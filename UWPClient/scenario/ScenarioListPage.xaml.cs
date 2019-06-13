using Graighle.Triping.UWPClient.Scenario.Editor;
using Graighle.Triping.UWPClient.Scenario.Models;
using Graighle.Triping.UWPClient.Scenario.ViewModels;
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
        /// シナリオが選択されたときの処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenarioListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems.Count == 0 ? null : e.AddedItems[0] as ScenarioOutlineListItem;

            this.selectingScenarioFrame.DataContext = selected;
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
        /// シナリオの詳細を開くボタンが押された処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenDetailButton_Click(object sender, RoutedEventArgs e)
        {
            var outline = this.selectingScenarioFrame.DataContext as ScenarioOutlineListItem;
            if(outline == null)
            {
                return;
            }

            if(Window.Current.Content is Frame rootFrame)
            {
                rootFrame.Navigate(typeof(ScenarioEditorMainPage), outline.FileName);
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
