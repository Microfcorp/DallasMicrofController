using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DallasMicrofOperator
{
    public abstract class ST
    {
        /// <summary>
        /// Загрузить настройки из фалйа
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        public static T[] Load<T>(string path, bool None)
        {
            BinaryFormatter bf = new BinaryFormatter();

            if (File.Exists(path))
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    var t2 = (T[])bf.Deserialize(fs);
                    return t2;
                }
            }
            return new T[] { default };
        }
        /// <summary>
        /// Загрузить настройки из фалйа
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        public static T[] Load<T>(string file)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MicrofDev\\" + file + ".dat";
            return Load<T>(path, false);
        }
        /// <summary>
        /// Сохранить настройки в файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="str">Структура настроек</param>
        public static void Save<T>(T[] str, string path, bool NONE)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var tmp = str.Where(tmpa => tmpa != null).ToArray();

            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream fs = File.Create(path))
                bf.Serialize(fs, tmp);
        }
        /// <summary>
        /// Получает ID ноды
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="str">Структура настроек</param>
        public static int GetID<T>(T node, string file)
        {
            var tmp = Load<T>(file);
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i].Equals(node)) return i;
            }
            return -1;
        }
        /// <summary>
        /// Сохранить настройки в файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="str">Структура настроек</param>
        public static void Save<T>(T[] str, string file)
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MicrofDev\\");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MicrofDev\\" + file + ".dat";
            Save<T>(str, path, false);
        }
        /// <summary>
        /// Существует ли данный файл с настройками
        /// </summary>
        /// <param name="path">Имя файла</param>
        /// <returns></returns>
        public static bool IsFile(string file)
        {
            string patha = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MicrofDev\\" + file + ".dat";
            return File.Exists(patha);
        }
        /// <summary>
        /// Добавить настройку в файл
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="str">Структура настроек</param>
        public static void AddToFile<T>(T str, string file)
        {
            var t = Load<T>(file).ToList();
            t.Add(str);
            Save<T>(t.ToArray(), file);
        }
        /// <summary>
        /// Изменить настройку в файле
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="str">Структура настроек</param>
        public static void ReplaceToFile<T>(T str, string file, int id)
        {
            var t = Load<T>(file).ToList();
            t[id] = str;
            Save<T>(t.ToArray(), file);
        }
        /// <summary>
        /// Удалить настройку в файле
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="str">Структура настроек</param>
        public static void DeleteToFile<T>(string file, int id)
        {
            var t = Load<T>(file).ToList();
            t.RemoveAt(id);
            Save<T>(t.ToArray(), file);
        }
        /// <summary>
        /// Удалить файл настроек
        /// </summary>
        public static void DeleteSetting(string file)
        {
            DeleteSetting(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MicrofDev\\" + file + ".dat", false);
        }
        /// <summary>
        /// Удалить файл настроек
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public static void DeleteSetting(string path, bool NONE)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        /// <summary>
        /// Импортирует файл настроек
        /// </summary>
        public static void ReplaceSetting(string newpath, string file)
        {
            ReplaceSetting(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MicrofDev\\" + file + ".dat", newpath, false);
        }
        /// <summary>
        /// Импортирует файл настроек
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public static void ReplaceSetting(string path, string newpath, bool NONE)
        {
            if (File.Exists(newpath))
                File.Copy(newpath, path, true);
        }
        /// <summary>
        /// Копирует файл настроек
        /// </summary>
        public static void CopySetting(string newpath, string file)
        {
            ReplaceSetting(newpath, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MicrofDev\\" + file + ".dat");
        }
    }
}
