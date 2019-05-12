using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace UWPClient {

	/// <summary>
	/// UWPクライアントのホーム画面。
	/// </summary>
	public sealed partial class MainPage : Page {

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		public MainPage(){
			this.InitializeComponent();

			ApplicationView.PreferredLaunchViewSize = new Size { Width = 1600, Height = 900 };
			ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
		}

	}
}
