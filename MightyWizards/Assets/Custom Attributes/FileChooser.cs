using UnityEngine;
using System.Collections;
using System.IO;

public class FileChooser : PropertyAttribute {

    public readonly string[] files;

    public FileChooser (string rootPath, string fileExtension)
    {
        files = Directory.GetFiles(rootPath, "*." + fileExtension, SearchOption.AllDirectories);
        
        for (int i = 0; i < files.Length; ++i)
            files[i] = files[i].Replace('\\', '/').Replace("." + fileExtension, "").Replace(rootPath, "");
    }
}
