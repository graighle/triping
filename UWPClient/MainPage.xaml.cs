using System;
using System.Collections.Generic;
using System.Linq;
using Graighle.Triping.UWPClient.Character;
using Graighle.Triping.UWPClient.Client;
using Graighle.Triping.UWPClient.Home;
using Graighle.Triping.UWPClient.Host;
using Graighle.Triping.UWPClient.Scenario;
using Graighle.Triping.UWPClient.Setting;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Graighle.Triping.UWPClient
{

    /// <summary>
    /// UWPクライアントのホーム画面。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private static readonly List<(string Tag, Type Page)> RootTabs = new List<(string Tag, Type Page)>
        {
            ("home", typeof(HomePage)),
            ("characters", typeof(CharacterListPage)),
            ("client", typeof(ClientEntryPage)),
            ("host", typeof(HostEntryPage)),
            ("settings", typeof(SettingPage)),
            ("scenario", typeof(ScenarioListPage)),
        };

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size { Width = 1600, Height = 900 };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        /// <summary>
        /// トップの画面遷移をする。
        /// </summary>
        /// <param name="navItemTag"></param>
        /// <param name="transitionInfo"></param>
        private void NavigateRootContent(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type page = MainPage.RootTabs.FirstOrDefault(p => p.Tag.Equals(navItemTag)).Page;

            // 画面が違う場合のみ遷移を実行する。
            var preNavPageType = this.rootContentFrame.CurrentSourcePageType;
            if (!(page is null) && !Type.Equals(preNavPageType, page))
            {
                this.rootContentFrame.Navigate(page, null, transitionInfo);
            }
        }

        /// <summary>
        /// トップのNavigationViewの読込時初期化処理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RootNavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            // トップが画面遷移した時にコールバックを設定。
            this.rootContentFrame.Navigated += On_Navigated;

            // 最初はhome画面を表示する。
            this.NavigateRootContent("home", new EntranceNavigationTransitionInfo());
        }

        /// <summary>
        /// トップのナビゲーションを選択された時のコールバック。
        /// 画面遷移を呼び出す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void RootNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                this.NavigateRootContent("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                this.NavigateRootContent(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        /// <summary>
        /// トップが画面遷移した時のコールバック。
        /// 遷移先の画面からナビゲーションの表示を更新する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void On_Navigated(object sender, NavigationEventArgs e)
        {

            // 設定ページは組込みの専用値を指定。
            if (this.rootContentFrame.SourcePageType == typeof(SettingPage))
            {
                this.rootNavigationView.SelectedItem = (NavigationViewItem)this.rootNavigationView.SettingsItem;
            }
            else if (this.rootContentFrame.SourcePageType != null)
            {
                var item = MainPage.RootTabs.FirstOrDefault(p => p.Page == e.SourcePageType);
                this.rootNavigationView.SelectedItem = this.rootNavigationView.MenuItems.OfType<NavigationViewItem>().First(n => n.Tag.Equals(item.Tag));
            }
        }


    }
}
