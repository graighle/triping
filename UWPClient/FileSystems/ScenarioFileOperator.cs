﻿using Graighle.Triping.Exceptions;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Graighle.Triping.UWPClient.FileSystems
{
    /// <summary>
    /// シナリオファイルの操作クラス。
    /// </summary>
    public class ScenarioFileOperator
    {
        private static readonly string ScenarioFolderName = "scenarios";
        private static readonly string ScenarioFilePrefix = "scenario_";
        private static readonly string ScenarioFileExtension = ".xml";
        private static readonly int ScenarioFileNumberLength = 5;

        /// <summary>
        /// シナリオデータをファイルに出力する。
        /// </summary>
        /// <param name="fileName">ファイル名。</param>
        /// <param name="text">シナリオデータ。</param>
        /// <param name="allowOverwrite">ファイルの上書きを許可するかどうか。</param>
        /// <returns>出力したファイル名。</returns>
        public async Task<string> WriteToFile(string fileName, string text, bool allowOverwrite)
        {
            try {
                // シナリオフォルダを開く。
                var localFolder = ApplicationData.Current.LocalFolder;
                StorageFolder scenarioFolder = null;
                try
                {
                    scenarioFolder = await localFolder.GetFolderAsync(ScenarioFolderName);
                }
                catch(FileNotFoundException)
                {
                }

                // シナリオフォルダが存在しなければ作成する。
                if(localFolder == null)
                {
                    scenarioFolder = await localFolder.CreateFolderAsync(ScenarioFolderName);
                }

                // シナリオファイルを開く。
                var createOptions = allowOverwrite ? CreationCollisionOption.ReplaceExisting : CreationCollisionOption.FailIfExists;
                StorageFile scenarioFile = await scenarioFolder.CreateFileAsync(fileName, createOptions);

                using(var transaction = await scenarioFile.OpenTransactedWriteAsync())
                {
                    using(var dataWriter = new DataWriter(transaction.Stream))
                    {
                        dataWriter.WriteString(text);
                        // 書込み後にファイルサイズを書込んだサイズにする。
                        transaction.Stream.Size = await dataWriter.StoreAsync();
                        await transaction.CommitAsync();
                    }
                }
            }
            catch(UnauthorizedAccessException)
            {
                var rc = new ResourceLoader();
                throw new ExternalDataOperationException(rc.GetString("UnauthorizedAccessToLocalFolder"));
            }

            return fileName;
        }

        /// <summary>
        /// 新しいシナリオファイルのファイル名をスキャンして生成する。
        /// scenario_yyyymmdd_nnnnn.xml
        /// </summary>
        /// <returns>新しいファイル名。</returns>
        public async Task<string> ScanNewFileName()
        {
            string fileNamePrefix = ScenarioFilePrefix + DateTime.Now.ToString("yyyyMMdd") + "_";
            string fileNameNumberFormat = new string('0', ScenarioFileNumberLength);
            int maxFileNameNumber = (int)Math.Pow(10, ScenarioFileNumberLength);

            // シナリオフォルダを開く。
            try
            {
                var localFolder = ApplicationData.Current.LocalFolder;
                var scenarioFolder = await localFolder.GetFolderAsync(ScenarioFolderName);

                // 連番でファイルが存在しなかったら新しいファイル名とする。
                for(int n=0; n<maxFileNameNumber; ++n)
                {
                    var fileName = fileNamePrefix + n.ToString(fileNameNumberFormat) + ScenarioFileExtension;
                    try
                    {
                        await scenarioFolder.GetFileAsync(fileName);
                    }
                    catch(FileNotFoundException)
                    {
                        return fileName;
                    }
                }

                var rc = new ResourceLoader();
                throw new ExternalDataOperationException(rc.GetString("FailedToGenerateFileName"));
            }
            catch(FileNotFoundException)
            {
                // シナリオフォルダが存在しない場合はn=0で生成する。
                return fileNamePrefix + (0).ToString(fileNameNumberFormat) + ScenarioFileExtension;
            }
            catch(UnauthorizedAccessException)
            {
                var rc = new ResourceLoader();
                throw new ExternalDataOperationException(rc.GetString("UnauthorizedAccessToLocalFolder"));
            }
        }
    }
}