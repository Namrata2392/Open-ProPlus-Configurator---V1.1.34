using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
namespace OpenProPlusConfigurator
{
   public class FileDirectoryOperations
    {
        public static bool CompressDirectory(string sInDir, string sOutFile)
        {
            string FileName = Path.GetFileName(sOutFile);
            string sFinalDir = Path.GetDirectoryName(sOutFile);
            if (File.Exists(sOutFile))
            {
                if (DialogResult.Yes == (MessageBox.Show("'" + FileName + "' file already exists." + Environment.NewLine + "Do you want to replace it?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)))
                {
                    string[] sFiles = Directory.GetFiles(sInDir, "*.*", SearchOption.AllDirectories);
                    int iDirLen = sInDir[sInDir.Length - 1] == Path.DirectorySeparatorChar ? sInDir.Length : sInDir.Length + 1;

                    using (FileStream outFile = new FileStream(sOutFile, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (GZipStream str = new GZipStream(outFile, CompressionMode.Compress))
                        foreach (string sFilePath in sFiles)
                        {
                            string sRelativePath = sFilePath.Substring(iDirLen);

                            CompressFile(sInDir, sRelativePath, str);
                        }
                }
                else { return false; }
            }
            else
            {
                string[] sFiles = Directory.GetFiles(sInDir, "*.*", SearchOption.AllDirectories);
                int iDirLen = sInDir[sInDir.Length - 1] == Path.DirectorySeparatorChar ? sInDir.Length : sInDir.Length + 1;

                using (FileStream outFile = new FileStream(sOutFile, FileMode.Create, FileAccess.Write, FileShare.None))
                using (GZipStream str = new GZipStream(outFile, CompressionMode.Compress))
                    foreach (string sFilePath in sFiles)
                    {
                        string sRelativePath = sFilePath.Substring(iDirLen);

                        CompressFile(sInDir, sRelativePath, str);
                    }
            }
            return true;
        }
        public static void CompressFile(string sDir, string sRelativePath, GZipStream zipStream)
        {
            //Compress file name
            char[] chars = sRelativePath.ToCharArray();
            zipStream.Write(BitConverter.GetBytes(chars.Length), 0, sizeof(int));
            foreach (char c in chars)
                zipStream.Write(BitConverter.GetBytes(c), 0, sizeof(char));

            //Compress file content
            byte[] bytes = File.ReadAllBytes(Path.Combine(sDir, sRelativePath));
            zipStream.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int));
            zipStream.Write(bytes, 0, bytes.Length);
        }
        public static bool DecompressToDirectory(string sCompressedFile, string sDir)
        {
            bool valid = true;
            using (FileStream inFile = new FileStream(sCompressedFile, FileMode.Open, FileAccess.ReadWrite))
            {
                if (inFile.Length != 0)
                {
                    using (GZipStream zipStream = new GZipStream(inFile, CompressionMode.Decompress, true))
                        while (DecompressFile(sDir, zipStream)) ;
                }
                else
                {
                    valid = false;
                    Console.WriteLine(Path.GetFileNameWithoutExtension(sCompressedFile) + " configuration not found.");
                }
            }
            //File.Delete(sCompressedFile);
            return valid;
        }
        public static bool DecompressFile(string sDir, GZipStream zipStream)
        {
            //Decompress file name
            byte[] bytes = new byte[sizeof(int)];
            int Readed = zipStream.Read(bytes, 0, sizeof(int));
            if (Readed < sizeof(int))
                return false;

            int iNameLen = BitConverter.ToInt32(bytes, 0);
            bytes = new byte[sizeof(char)];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iNameLen; i++)
            {
                zipStream.Read(bytes, 0, sizeof(char));
                char c = BitConverter.ToChar(bytes, 0);
                sb.Append(c);
            }
            string sFileName = sb.ToString();
            //if (progress != null)
            //    progress(sFileName);

            //Decompress file content
            bytes = new byte[sizeof(int)];
            zipStream.Read(bytes, 0, sizeof(int));
            int iFileLen = BitConverter.ToInt32(bytes, 0);

            bytes = new byte[iFileLen];
            zipStream.Read(bytes, 0, bytes.Length);

            string sFilePath = Path.Combine(sDir, sFileName);
            string sFinalDir = Path.GetDirectoryName(sFilePath);
            if (!Directory.Exists(sFinalDir))
                Directory.CreateDirectory(sFinalDir);

            using (FileStream outFile = new FileStream(sFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                outFile.Write(bytes, 0, iFileLen);

            return true;
        }

        public static bool DeleteDirectory(string destDirName)
        {
            try
            {
                if (Directory.Exists(destDirName))
                {
                    foreach (string file in Directory.GetFiles(destDirName))
                    {
                        File.Delete(file);
                    }
                    Application.DoEvents();
                    foreach (string subDir in Directory.GetDirectories(destDirName))
                    {
                        DeleteDirectory(subDir);
                    }

                    System.Threading.Thread.Sleep(1);
                    Directory.Delete(destDirName);
                }
            }
            catch (IOException e)
            {
                return false;
            }
            catch (Exception ex1)
            {
                return false;
            }
            return true;
        }
    }
}
