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
        private ScenarioPackageEditor originalScenarioEditor = null;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioEditorViewModel()
        {
            this.ScenarioEditor = new ScenarioPackageEditor();
        }

        /// <summary>
        /// 編集を開始する。
        /// </summary>
        public void StartEditing()
        {
            this.originalScenarioEditor = new ScenarioPackageEditor(this.ScenarioEditor);
            this.ScenarioEditor.IsEdited = false;
            this.ScenarioEditor.IsEditing = true;
        }

        /// <summary>
        /// 編集を破棄して終了する。
        /// </summary>
        public void RevertEditing()
        {
            this.ScenarioEditor.Assign(this.originalScenarioEditor);
            this.originalScenarioEditor = null;
        }

    }
}
