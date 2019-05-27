using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Graighle.Triping.UWPClient.Scenario.Editor
{
    /// <summary>
    /// シナリオエディタのプロローグタブのページ処理。
    /// </summary>
    public sealed partial class ScenarioEditorProloguePage : Page
    {
        private ScenarioEditorPrologueViewModel viewModel;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioEditorProloguePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// このページに遷移してきた時の処理。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // シナリオパッケージ編集データが渡される。
            var scenarioEditor = (ScenarioPackageEditor)e.Parameter;
            this.DataContext = this.viewModel = new ScenarioEditorPrologueViewModel(scenarioEditor);
        }
    }
}
