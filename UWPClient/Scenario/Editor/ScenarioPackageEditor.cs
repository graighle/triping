using Graighle.Triping.Scenario;
using System.ComponentModel;

namespace Graighle.Triping.UWPClient.Scenario.Editor
{
    /// <summary>
    /// シナリオの全情報をまとめたパッケージの編集モデル。
    /// </summary>
    public class ScenarioPackageEditor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsEdited { get; private set; } = false;

        /// <summary>
        /// 編集中かどうか。
        /// </summary>
        private bool isEditing = false;
        public bool IsEditing {
            get => this.isEditing;
            private set {
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
        /// 編集を開始する。
        /// </summary>
        public void BeginEditing()
        {
            this.IsEdited = false;
            this.IsEditing = true;
        }

        /// <summary>
        /// 編集を終了する。
        /// </summary>
        public void EndEditing()
        {
            this.IsEdited = false;
            this.IsEditing = false;
        }

        /// <summary>
        /// シナリオデータをパッケージから読込む。
        /// </summary>
        /// <param name="package">シナリオパッケージ。</param>
        public void ImportPackage(ScenarioPackage package)
        {
            // 概要
            this.Title = package.Outline.Title;
            this.Author = package.Outline.Author;

            // プロローグ
            this.Scenery = package.Scenery;

            this.IsEdited = false;
        }

        /// <summary>
        /// 編集中のシナリオデータをパッケージとして取得する。
        /// </summary>
        /// <returns>シナリオパッケージ。</returns>
        public ScenarioPackage ExportPackage()
        {
            var package = new ScenarioPackage();

            // 概要
            package.Outline.Title = this.Title;
            package.Outline.Author = this.Author;

            // プロローグ
            package.Scenery = this.Scenery;

            return package;
        }

    }
}
