using System.ComponentModel;

namespace Graighle.Triping.UWPClient.Scenario.Editor
{
    /// <summary>
    /// シナリオの全情報をまとめたパッケージの編集モデル。
    /// </summary>
    public class ScenarioPackageEditor : INotifyPropertyChanged
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
        /// プロローグ/背景。
        /// </summary>
        private string scenery;
        public string Scenery {
            get => this.scenery;
            set {
                if(value != this.scenery)
                {
                    this.scenery = value;
                    this.PropertyChanged?.Invoke(this, SceneryPropertyChangedEventArgs);
                }
            }
        }
        private static readonly PropertyChangedEventArgs SceneryPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Scenery));

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ScenarioPackageEditor()
        {
        }
    }
}
