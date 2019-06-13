using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Graighle.Triping.UWPClient.Scenario.Editor
{
    /// <summary>
    /// シナリオ編集メインページ。
    /// </summary>
    public sealed partial class ScenarioEditorMainPage : Page
    {
        private static readonly List<(string Tag, Type Page)> ScenarioEditorTabs = new List<(string Tag, Type Page)>
        {
            ("Outline", typeof(ScenarioEditorOutlinePage)),
            ("Prologue", typeof(ScenarioEditorProloguePage)),
        };

        private ScenarioEditorViewModel viewModel { get; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioEditorMainPage()
        {
            this.InitializeComponent();

            this.DataContext = this.viewModel = new ScenarioEditorViewModel();
        }

        /// <summary>
        /// ページに遷移したときの処理。
        /// シナリオファイルが指定されている場合、シナリオファイルを開く。
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if(e.Parameter is string fileName)
            {
                await this.viewModel.LoadScenario(fileName);
            }
        }

        /// <summary>
        /// 戻るボタンを押された処理。
        /// メインページに戻る。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if(Window.Current.Content is Frame windowFrame)
            {
                windowFrame.GoBack();
            }
        }

        /// <summary>
        /// 編集ボタンを押された処理。
        /// 編集を開始する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.BeginEditing();
        }

        /// <summary>
        /// 保存ボタンを押された処理。
        /// 編集中のシナリオを保存する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var fileName = await this.viewModel.SaveEditing();
        }

        /// <summary>
        /// キャンセルボタンを押された処理。
        /// 変更内容を破棄して編集を終了する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.viewModel.ScenarioEditor.IsEdited)
            {
                var rc = new ResourceLoader();
                var confirmDialog = new ContentDialog
                {
                    Title = rc.GetString("ScenarioEditorRevertConfirmTitle"),
                    Content = rc.GetString("ScenarioEditorRevertConfirmContent"),
                    PrimaryButtonText = rc.GetString("ScenarioEditorRevertConfirmYes"),
                    CloseButtonText = rc.GetString("ScenarioEditorRevertConfirmNo"),
                };
                var result = await confirmDialog.ShowAsync();
                if(result != ContentDialogResult.Primary)
                {
                    return;
                }
            }

            this.viewModel.RevertEditing();
        }

        /// <summary>
        /// タブの画面遷移をする。
        /// </summary>
        /// <param name="navItemTag"></param>
        /// <param name="transitionInfo"></param>
        private void NavigateRootContent(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type page = ScenarioEditorMainPage.ScenarioEditorTabs.FirstOrDefault(p => p.Tag.Equals(navItemTag)).Page;

            // 画面が違う場合のみ遷移を実行する。
            var preNavPageType = this.rootContentFrame.CurrentSourcePageType;
            if (!(page is null) && !Type.Equals(preNavPageType, page))
            {
                this.rootContentFrame.Navigate(page, this.viewModel.ScenarioEditor, transitionInfo);
            }
        }

        /// <summary>
        /// シナリオエディタのNavigationViewの読込時初期化処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenarioEditorNavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            // トップが画面遷移した時にコールバックを設定。
            this.rootContentFrame.Navigated += On_Navigated;

            // 最初はhome画面を表示する。
            this.NavigateRootContent("Outline", new EntranceNavigationTransitionInfo());
        }

        /// <summary>
        /// シナリオエディタのナビゲーションを選択された時のコールバック。
        /// 画面遷移を呼び出す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void RootNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                this.NavigateRootContent(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        /// <summary>
        /// シナリオエディタのタブが画面遷移した時のコールバック。
        /// 遷移先の画面からナビゲーションの表示を更新する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            if (this.rootContentFrame.SourcePageType != null)
            {
                var item = ScenarioEditorMainPage.ScenarioEditorTabs.FirstOrDefault(p => p.Page == e.SourcePageType);
                this.scenarioEditorNavigationView.SelectedItem = this.scenarioEditorNavigationView.MenuItems.OfType<NavigationViewItem>().First(n => n.Tag.Equals(item.Tag));
            }
        }

    }
}
