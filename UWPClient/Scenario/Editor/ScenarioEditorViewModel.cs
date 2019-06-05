using Graighle.Triping.Scenario;
using Graighle.Triping.UWPClient.FileSystems;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Graighle.Triping.UWPClient.Scenario.Editor
{
    /// <summary>
    /// シナリオ編集ページ用のViewModel。
    /// </summary>
    public class ScenarioEditorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string scenarioFileName;
        private ScenarioPackage scenario;
        public ScenarioPackageEditor ScenarioEditor { get; private set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioEditorViewModel()
        {
            this.scenarioFileName = string.Empty;
            this.scenario = new ScenarioPackage();
            this.ScenarioEditor = new ScenarioPackageEditor();
        }

        /// <summary>
        /// シナリオファイルを読込む。
        /// </summary>
        /// <param name="fileName">シナリオファイルのファイル名。</param>
        public void LoadScenario(string fileName)
        {
            // ファイルから読込み。
            //var reader = new ScenarioFileOperator();
            //var serialized = reader.ReadFromFile(fileName);

            // ポータブル形式を解析。
            //var deserializer = new ScenarioPackageDeserializer();
            //this.scenario = deserializer.DeserializeFromPortableFormat(serialized);

            //this.ScenarioEditor.ImportPackage(this.scenario);
            this.scenarioFileName = fileName;
        }

        /// <summary>
        /// 編集を開始する。
        /// </summary>
        public void BeginEditing()
        {
            this.ScenarioEditor.BeginEditing();
        }

        /// <summary>
        /// 編集を保存して終了する。
        /// </summary>
        /// <returns>保存したファイル名。</returns>
        public async Task<string> SaveEditing()
        {
            var edited = this.ScenarioEditor.ExportPackage();

            // ポータブル形式にシリアライズ。
            var serializer = new ScenarioPackageSerializer();
            var serialized = serializer.SerializeToPortableFormat(edited);

            // ファイルへ書込み。
            var writer = new ScenarioFileOperator();
            var fileName = this.scenarioFileName;
            bool allowOverwrite = true;
            if(string.IsNullOrEmpty(fileName))
            {
                fileName = await writer.ScanNewFileName();
                allowOverwrite = false;
            }
            this.scenarioFileName = await writer.WriteToFile(fileName, serialized, allowOverwrite);

            this.ScenarioEditor.EndEditing();

            return this.scenarioFileName;
        }

        /// <summary>
        /// 編集を破棄して終了する。
        /// </summary>
        public void RevertEditing()
        {
            this.ScenarioEditor.EndEditing();
            this.ScenarioEditor.ImportPackage(this.scenario);
        }

    }
}
