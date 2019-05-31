using System.ComponentModel;

namespace Graighle.Triping.UWPClient.Scenario.Editor
{
    /// <summary>
    /// シナリオの全情報をまとめたパッケージの編集モデル。
    /// </summary>
    public class ScenarioPackageEditor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsEdited { get; set; } = false;

        /// <summary>
        /// 編集中かどうか。
        /// </summary>
        private bool isEditing = false;
        public bool IsEditing {
            get => this.isEditing;
            set {
                if(value != this.isEditing)
                {
                    this.isEditing = value;
                    this.PropertyChanged?.Invoke(this, IsEditingPropertyChangedEventArgs);
                }
            }
        }
        private static readonly PropertyChangedEventArgs IsEditingPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(IsEditing));

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
                    this.IsEdited = true;
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
                    this.IsEdited = true;
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
                    this.IsEdited = true;
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

        /// <summary>
        /// コピーコンストラクタ。
        /// </summary>
        /// <param name="source"></param>
        public ScenarioPackageEditor(ScenarioPackageEditor source)
        {
            this.Assign(source);
        }

        /// <summary>
        /// 全ての値をコピーする。
        /// </summary>
        /// </summary>
        /// <param name="source">コピー元。</param>
        /// <returns></returns>
        public void Assign(ScenarioPackageEditor source)
        {
            this.IsEdited = source.IsEdited;
            this.IsEditing = source.IsEditing;
            this.Title = source.Title;
            this.Author = source.Author;
            this.Scenery = source.Scenery;
        }

    }
}
