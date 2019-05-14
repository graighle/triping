using System;
using System.Collections.Generic;
using System.Linq;
using UWPClient.character;
using UWPClient.client;
using UWPClient.home;
using UWPClient.host;
using UWPClient.setting;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace UWPClient {

	/// <summary>
	/// UWPクライアントのホーム画面。
	/// </summary>
	public sealed partial class MainPage : Page {

		private readonly List<(string Tag, Type Page)> _rootTabs = new List<(string Tag, Type Page)>
		{
			("home", typeof(HomePage)),
			("characters", typeof(CharacterListPage)),
			("client", typeof(ClientEntryPage)),
			("host", typeof(HostEntryPage)),
			("settings", typeof(SettingPage)),
		};

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public MainPage(){
			this.InitializeComponent();

			ApplicationView.PreferredLaunchViewSize = new Size { Width = 1600, Height = 900 };
			ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
		}

		/// <summary>
		/// トップの画面遷移をする。
		/// </summary>
		/// <param name="navItemTag"></param>
		/// <param name="transitionInfo"></param>
		private void NavigateRootContent(string navItemTag, NavigationTransitionInfo transitionInfo){
			Type page = _rootTabs.FirstOrDefault(p => p.Tag.Equals(navItemTag)).Page;

			// 画面が違う場合のみ遷移を実行する。
			var preNavPageType = RootContentFrame.CurrentSourcePageType;
			if(!(page is null) && !Type.Equals(preNavPageType, page)){
				RootContentFrame.Navigate(page, null, transitionInfo);
			}
		}

		/// <summary>
		/// トップのNavigationViewの読込時初期化処理。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RootNavigationView_Loaded(object sender, RoutedEventArgs e){
			// トップが画面遷移した時にコールバックを設定。
			RootContentFrame.Navigated += On_Navigated;

			// 最初はhome画面を表示する。
			NavigateRootContent("home", new EntranceNavigationTransitionInfo());
		}

		/// <summary>
		/// トップのナビゲーションを選択された時のコールバック。
		/// 画面遷移を呼び出す。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void RootNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args){
			if(args.IsSettingsInvoked){
				NavigateRootContent("settings", args.RecommendedNavigationTransitionInfo);
			}else if(args.InvokedItemContainer != null){
				var navItemTag = args.InvokedItemContainer.Tag.ToString();
				NavigateRootContent(navItemTag, args.RecommendedNavigationTransitionInfo);
			}
		}

		/// <summary>
		/// トップが画面遷移した時のコールバック。
		/// 遷移先の画面からナビゲーションの表示を更新する。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void On_Navigated(object sender, NavigationEventArgs e){

			// 設定ページは組込みの専用値を指定。
			if(RootContentFrame.SourcePageType == typeof(SettingPage)){
				RootNavigationView.SelectedItem = (NavigationViewItem)RootNavigationView.SettingsItem;
			}else if(RootContentFrame.SourcePageType != null){
				var item = _rootTabs.FirstOrDefault(p => p.Page == e.SourcePageType);
				RootNavigationView.SelectedItem = RootNavigationView.MenuItems.OfType<NavigationViewItem>().First(n => n.Tag.Equals(item.Tag));
			}
		}


	}
}
