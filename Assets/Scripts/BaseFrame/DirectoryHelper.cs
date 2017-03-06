using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine; using LoLSDK;


public class DirectoryHelper  {


    public static List<string> GetAllFilesInPath(string path)
    {
        List<string> toRet = new List<string>();

        string[] fullPaths = Directory.GetFiles(path);
        foreach (string s in fullPaths)
        {
            if (Path.GetExtension(s) != ".meta")
                toRet.Add(Path.GetFileNameWithoutExtension(s));
        }
        return toRet;
    }

    public static string GetRandomPlant()
    {
        List<string> filePaths = GetAllFilesInPath("Assets/Resources/Prefabs/Trees");
        return filePaths[Random.Range(0, filePaths.Count)];
    }
}
