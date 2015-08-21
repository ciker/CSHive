namespace CS.Utils
{
    /// <summary>
    /// 文件辅助类
    /// </summary>
    public class FileHeper
    {
        /// <summary>
        /// 返回文件的全路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFilePath(string fileName)
        {
            return App.CombinePath(fileName);
        }
    }
}