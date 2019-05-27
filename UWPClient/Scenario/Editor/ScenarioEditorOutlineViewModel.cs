using System.ComponentModel;

namespace Graighle.Triping.UWPClient.Scenario.Editor
{
    public class ScenarioEditorOutlineViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ScenarioPackageEditor ScenarioEditor { get; private set; }

        public ScenarioEditorOutlineViewModel(ScenarioPackageEditor scenarioEditor)
        {
            this.ScenarioEditor = scenarioEditor;
        }
    }
}
