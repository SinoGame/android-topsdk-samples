using System.IO;
using UnityEditor;
using UnityEditor.Android;
using UnityEngine;

public class SupportAndroidXGradlePropertiesBuildProcessor : IPostGenerateGradleAndroidProject
{
    public int callbackOrder
    {
        // 同种插件的优先级,数字越大优先级越低
        get { return 999; }
    }

    public void OnPostGenerateGradleAndroidProject(string path)
    {
        Debug.Log("TopSdk OnPostGenerateGradleAndroidProject path: " + path);
        // Debug.Log(
        //     "Zorro Bulid path : "
        //         + UnityEditor.EditorUserBuildSettings.GetBuildLocation(BuildTarget.Android)
        // );
        // Debug.Log("Zorro androidPath path : " + androidPath);

        // // Use project's root folder instead of /unityLibrary
        // Debug.Log("Zorro GetDirectoryName path : " + Path.GetDirectoryName(path));
        templateGradle(path);
    }

    private void templateGradle(string path)
    {
        string assetsPath = Application.dataPath;
        Debug.Log("TopSdk assetsPath : " + assetsPath);
        string androidPath = Path.Combine(assetsPath, "Plugins", "Android");
        Debug.Log("TopSdk androidPath : " + androidPath);
        string gradleOutPath = Path.GetDirectoryName(path);
        Debug.Log("TopSdk gradleOutPath : " + gradleOutPath);
        string unityVersion = Application.unityVersion;
        Debug.Log("TopSdk unityVersion : " + unityVersion);
        string[] versions = unityVersion.Split('.');
        Debug.Log("TopSdk unityVersion : " + versions[0]);
        addAndroidX(gradleOutPath);
        if (int.Parse(versions[0]) > 2018)
        {
            //unity 2018以上版本
            Debug.Log("TopSdk unityVersion >2018 ");
            string launcherBuildGradlePath = Path.Combine(
                gradleOutPath,
                "launcher",
                "build.gradle"
            );
            Debug.Log("TopSdk launcherBuildGradlePath : " + launcherBuildGradlePath);
            // BuildGradleParser.ParseAndUpdateBuildGradle(
            //     Path.Combine(gradleOutPath, "build.gradle")
            // );
            // Debug.Log("TopSdk build.gradle file has been updated successfully.");

            if (File.Exists(launcherBuildGradlePath))
            {
                Debug.Log("TopSdk gradleOut/launcher/build.gradle file exists");
                StreamWriter writer = new StreamWriter(launcherBuildGradlePath, true);
                writer.WriteLine("\napply from: \"config.gradle\"");
                writer.Flush();
                writer.Close();
                Debug.Log(
                    "TopSdk gradleOut/launcher/build.gradle file writer apply from: \"config.gradle\" successfully."
                );
                Debug.Log(
                    "TopSdk copy unity project Assets/Plugins/Android/config.gradle file to Temp/gradleOut/launcher"
                );
                FileUtil.CopyFileOrDirectory(
                    Path.Combine(androidPath, "config.gradle"),
                    Path.Combine(gradleOutPath, "launcher", "config.gradle")
                );
                Debug.Log("TopSdk copy config.gradle file successfully");
            }
            else
            {
                Debug.Log("TopSdk gradleOut/launcher/build.gradle file not found");
            }
        }
        else
        {
            //unity 2018及以下版本
            Debug.Log("TopSdk unityVersion <=2018 ");
            string mainTemplateGradlePath = Path.Combine(androidPath, "mainTemplate.gradle");
            Debug.Log("TopSdk appBuildGradlePath : " + mainTemplateGradlePath);
            if (File.Exists(mainTemplateGradlePath))
            {
                Debug.Log("mainTemplate.gradle 文件存在");
                StreamWriter writer = new StreamWriter(mainTemplateGradlePath, true);
                writer.WriteLine("apply from: \"config.gradle\"");
                writer.Flush();
                writer.Close();
            }
            else
            {
                Debug.Log("mainTemplate.gradle file not found");
            }
        }
    }

    private void addAndroidX(string gradleOutPath)
    {
        string gradlePropertiesFile = Path.Combine(gradleOutPath, "gradle.properties");
        if (File.Exists(gradlePropertiesFile))
        {
            Debug.Log("TopSdk gradleOut/gradle.properties file exists");
            // File.Delete(gradlePropertiesFile);
            // StreamWriter writer = File.CreateText(gradlePropertiesFile);
            StreamWriter writer = new StreamWriter(gradlePropertiesFile, true);
            // writer.WriteLine("org.gradle.jvmargs=-Xmx2048m");
            writer.WriteLine("\nandroid.useAndroidX=true");
            writer.WriteLine("android.enableJetifier=true");
            writer.Flush();
            writer.Close();
            Debug.Log(
                "TopSdk gradleOut/gradle.properties file writer androidx config successfully."
            );
        }
        else
        {
            Debug.Log("TopSdk gradleOut/gradle.properties file not found");
        }
    }
}
