using System.ComponentModel;

namespace Graighle.Triping.UWPClient.Scenario.Editor
{
    /// <summary>
    /// シナリオ編集ページ用のViewModel。
    /// </summary>
    public class ScenarioEditorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ScenarioPackageEditor ScenarioEditor { get; private set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioEditorViewModel()
        {
            this.ScenarioEditor = new ScenarioPackageEditor();
        }

    }
}
