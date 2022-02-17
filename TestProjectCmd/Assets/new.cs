
using System.IO;
using UnityEditor;
using UnityEngine;

public class ImportAssetExample : MonoBehaviour
{
    [MenuItem("APIExamples/ImportAsset")]
    static void ImportAssetOnlyImportsSingleAsset()
    {
        string[] fileNames = new[] { "test_file01.txt", "test_file02.txt" };

        for (int i = 0; i < fileNames.Length; ++i)
        {
            var unimportedFileName = fileNames[i];
            var assetsPath = Application.dataPath + "/Artifacts/" + unimportedFileName;
            File.WriteAllText(assetsPath, "Testing 123");
        }

        var relativePath = "Assets/Artifacts/test_file01.txt";
        AssetDatabase.ImportAsset(relativePath);
    }
}

public class PostProcessImportAsset : AssetPostprocessor
{
    //Based on this example, the output from this function should be:
    //  OnPostprocessAllAssets
    //  Imported: Assets/Artifacts/test_file01.txt
    //
    //test_file02.txt should not even show up on the Project Browser
    //until a refresh happens.
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        Debug.Log("OnPostprocessAllAssets");

        foreach (var imported in importedAssets)
            Debug.Log("Imported: " + imported);

        foreach (var deleted in deletedAssets)
            Debug.Log("Deleted: " + deleted);

        foreach (var moved in movedAssets)
            Debug.Log("Moved: " + moved);

        foreach (var movedFromAsset in movedFromAssetPaths)
            Debug.Log("Moved from Asset: " + movedFromAsset);
    }
}
