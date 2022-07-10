using FileManagerOOP.Interfaces.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerOOP
{
    public static class Helper
    {

        private static ILogger _logger;

        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Добавляет элемент в массив.
        /// </summary>
        /// <param name="array">Массив в котором необходимо сделать разширение.</param>
        /// <param name="item">Элемент который необходимо добавить.</param>
        /// <returns>Возвращает значение типа string[]</returns>
        public static string[] AddItemInArray(string[] array, string item)
        {
            string[] copy = array;
            string[] result = new string[copy.Length + 1];

            for (int i = 0; i < copy.Length; i++)
            {
                result[i] = copy[i];
            }
            result[copy.Length] = item;

            return result;
        }
        /// <summary>
        /// Возвращает общее количество байт в запрашиваемой директории.
        /// </summary>
        /// <param name="path">Путь к директории.</param>
        /// <returns>Возвращает значение типа long</returns>
        public static long GetTotalLength(string path)
        {
            long totalLength = 0;

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] dirs = Directory.GetDirectories(path);
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(files[i]);
                    if (fileInfo.Exists)
                    {
                        totalLength += fileInfo.Length;
                    }
                }
                for (int i = 0; i < dirs.Length; i++)
                {
                    totalLength += GetTotalLength(dirs[i]);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.Log(ex);
            }

            return totalLength;
        }
        /// <summary>
        /// Возвращает общее количество файлов и директорий по запрашиваемому пути.
        /// </summary>
        /// <param name="path">>Путь к директории.</param>
        /// <returns>Возращает кортеж типа (long, long), количество файлов и директорий.</returns>
        public static (long, long) GetTotalItem(string path)
        {
            long countFiles = 0;
            long countDirs = 0;

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] dirs = Directory.GetDirectories(path);
                countFiles += files.Length;
                countDirs += dirs.Length;

                for (int i = 0; i < dirs.Length; i++)
                {
                    (long, long) counter = GetTotalItem(dirs[i]);
                    countFiles += counter.Item1;
                    countDirs += counter.Item2;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.Log(ex);
            }

            return (countFiles, countDirs);
        }
        /// <summary>
        /// Возвращает информацию о файле или директории.
        /// </summary>
        /// <param name="path">Путь к файлу или директории.</param>
        /// <returns>Массив строк с информацией о файле или директории.</returns>
        public static List<string> GetInfo(string path)
        {
            DirectoryInfo infoDir;
            FileInfo infoFile;

            List<string> dataInfo = new List<string>();

            try
            {
                infoDir = new DirectoryInfo(path);
                infoFile = new FileInfo(path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.Log(ex);
                return dataInfo;
            }

            if (infoDir.Exists)
            {

                dataInfo.Add("Directory info:");
                dataInfo.Add($"Creation Time - {infoDir.CreationTime}");
                dataInfo.Add($"Last Access Time - {infoDir.LastAccessTime}");
                dataInfo.Add($"Last Write Time - {infoDir.LastWriteTime}");
                dataInfo.Add($"Attributes - {infoDir.Attributes}");
                dataInfo.Add($"Files and Dirs - {GetTotalItem(path)}");

                long length = GetTotalLength(path);
                string stringLength = length == 0 ? "0 Bytes" : length.ToString("#,# Bytes");
                dataInfo.Add($"Length - {stringLength}");

                return dataInfo;
            }
            else if (infoFile.Exists)
            {

                dataInfo.Add($"File info:");
                dataInfo.Add($"Creation Time - {infoFile.CreationTime}");
                dataInfo.Add($"Last Access Time - {infoFile.LastAccessTime}");
                dataInfo.Add($"Last Write Time - {infoFile.LastWriteTime}");
                dataInfo.Add($"Attributes - {infoFile.Attributes}");

                long length = infoFile.Length;
                string stringLength = length == 0 ? "0 Bytes" : length.ToString("#,# Bytes");
                dataInfo.Add($"Length - {stringLength}");

                return dataInfo;
            }
            else
            {

                dataInfo.Add($"Directory info: NOT FOUND!");
                dataInfo.Add($"Creation Time - ");
                dataInfo.Add($"Last Access Time - ");
                dataInfo.Add($"Last Write Time - ");
                dataInfo.Add($"Attributes - ");
                dataInfo.Add($"Files and Dirs - ");
                dataInfo.Add($"Length - ");

                return dataInfo;
            }
        }
    }
}
