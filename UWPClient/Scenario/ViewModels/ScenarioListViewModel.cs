using Graighle.Triping.UWPClient.Scenario.Models;
using System.Threading.Tasks;

namespace Graighle.Triping.UWPClient.Scenario.ViewModels
{
    /// <summary>
    /// シナリオ一覧ページ用のViewModel。
    /// </summary>
    public class ScenarioListViewModel
    {
        public ScenarioOutlineList ScenarioList { get; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioListViewModel()
        {
            this.ScenarioList = new ScenarioOutlineList();
        }

        /// <summary>
        /// シナリオ一覧を読込む。
        /// </summary>
        public async Task LoadScenarioList()
        {
            await this.ScenarioList.LoadScenarioFiles();
        }

    }
}
