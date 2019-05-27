namespace Graighle.Triping.UWPClient.Scenario.Editor
{
    /// <summary>
    /// シナリオエディタのプロローグタブ用のViewModel。
    /// </summary>
    public class ScenarioEditorPrologueViewModel
    {
        public ScenarioPackageEditor ScenarioEditor { get; private set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="scenarioEditor"></param>
        public ScenarioEditorPrologueViewModel(ScenarioPackageEditor scenarioEditor)
        {
            this.ScenarioEditor = scenarioEditor;
        }
    }
}
