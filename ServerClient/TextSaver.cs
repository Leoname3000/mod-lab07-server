using System;
using System.IO;

namespace ServerClient;

public class TextSaver
{
    public static void Save(string text, string fileName)
    {
        string solutionRootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        string folderPath = $"{solutionRootPath}/";
        using (StreamWriter writer = new StreamWriter(folderPath + fileName, false))
        {
            writer.Write(text);
            writer.Close();
        }
    }
}