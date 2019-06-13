using Graighle.Triping.Scenario;
using Graighle.Triping.UWPClient.FileSystems;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Graighle.Triping.UWPClient.Scenario.Models
{
    /// <summary>
    /// シナリオ概要リスト。
    /// </summary>
    public class ScenarioOutlineList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ScenarioOutlineListItem> Items { get; } = new ObservableCollection<ScenarioOutlineListItem>();

        /// <summary>
        /// シナリオファイルの一覧を読込みリストを更新する。
        /// </summary>
        /// <returns></returns>
        public async Task LoadScenarioFiles()
        {
            this.Items.Clear();

            var fileOperator = new ScenarioFileOperator();
            var deserializer = new ScenarioDeserializer();

            var fileNames = await fileOperator.ScanScenarioFileNames();
            foreach(var fileName in fileNames)
            {
                var serialized = await fileOperator.ReadFromFile(fileName);
                var outline = deserializer.DeserializeOutlineFromPortableFormat(serialized);

                this.Items.Add(new ScenarioOutlineListItem(fileName, outline));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
        }

    }
}
