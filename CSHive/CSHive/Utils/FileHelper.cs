namespace CS.Utils
{
    /// <summary>
    /// 文件辅助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 返回文件或目录的全路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFullPath(string fileName)
        {
            return App.CombinePath(fileName);
        }
    }
}