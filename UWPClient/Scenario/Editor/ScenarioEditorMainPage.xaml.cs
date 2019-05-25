using System;
using System.Collections.Generic;
using System.Linq;
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
        };

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioEditorMainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 戻るボタンを押された処理。
        /// メインページに戻る。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenarioEditorNavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs e)
        {
            if(Window.Current.Content is Frame rootFrame)
            {
                rootFrame.GoBack();
            }
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
                this.rootContentFrame.Navigate(page, null, transitionInfo);
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
