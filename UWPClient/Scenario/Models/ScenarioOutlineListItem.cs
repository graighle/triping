using Graighle.Triping.Scenario;
using System.ComponentModel;

namespace Graighle.Triping.UWPClient.Scenario.Models
{
    /// <summary>
    /// シナリオ概要リスト用のアイテム。
    /// </summary>
    public class ScenarioOutlineListItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 概要/シナリオのタイトル。
        /// </summary>
        private string title;
        public string Title {
            get => this.title;
            set {
                if(value != this.title)
                {
                    this.title = value;
                    this.PropertyChanged?.Invoke(this, TitlePropertyChangedEventArgs);
                }
            }
        }
        private static readonly PropertyChangedEventArgs TitlePropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Title));

        /// <summary>
        /// 概要/シナリオの作者。
        /// </summary>
        private string author;
        public string Author {
            get => this.author;
            set {
                if(value != this.author)
                {
                    this.author = value;
                    this.PropertyChanged?.Invoke(this, AuthorPropertyChangedEventArgs);
                }
            }
        }
        private static readonly PropertyChangedEventArgs AuthorPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Author));

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioOutlineListItem()
        {
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="outline">シナリオ概要。</param>
        public ScenarioOutlineListItem(ScenarioOutline outline)
        {
            this.ImportOutline(outline);
        }

        /// <summary>
        /// シナリオ概要をパッケージから読込む。
        /// </summary>
        /// <param name="outline">シナリオ概要。</param>
        public void ImportOutline(ScenarioOutline outline)
        {
            this.Title = outline.Title;
            this.Author = outline.Author;
        }

        /// <summary>
        /// 編集中のシナリオ概要を取得する。
        /// </summary>
        /// <returns>シナリオ概要。</returns>
        public ScenarioOutline ExportOutline()
        {
            var outline = new ScenarioOutline();

            // 概要
            outline.Title = this.Title;
            outline.Author = this.Author;

            return outline;
        }
    }
}
