namespace Graighle.Triping.Scenario
{
    /// <summary>
    /// シナリオの全情報をまとめたパッケージ。
    /// </summary>
    public class ScenarioPackage
    {
        // 概要
        public ScenarioOutline Outline { get; set; } = new ScenarioOutline();

        // プロローグ
        public string Scenery { get; set; }
    }
}
