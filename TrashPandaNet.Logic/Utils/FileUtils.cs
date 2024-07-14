namespace TrashPandaNet.Logic.Utils
{
    public static class FileUtils
    {
        public static DirectoryInfo CreateFolder(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            try
            {
                if (!directoryInfo.Exists)
                    directoryInfo.Create();

                return directoryInfo;
            }
            catch
            {
                return null;
            }
        }

        public static FileInfo CreateFile(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            try
            {
                if (!fileInfo.Exists)
                {
                    using (var fs = fileInfo.Create()) { };
                }

                return fileInfo;
            }
            catch
            {
                return null;
            }
        }
    }
}
