using System.IO;
using UnityEditor.Android;
using UnityEngine;

public class SupportAndroidXGradlePropertiesBuildProcessor : IPostGenerateGradleAndroidProject
{
    public int callbackOrder
    {
        // 同种插件的优先级
        get { return 999; }
    }
    public void OnPostGenerateGradleAndroidProject(string path)
    {
       // MainTemplateGradle();
        Debug.Log("Bulid path : " + path);
        string gradlePropertiesFile = path + "/gradle.properties";
        if (File.Exists(gradlePropertiesFile))
        {
            File.Delete(gradlePropertiesFile);
        }
        StreamWriter writer = File.CreateText(gradlePropertiesFile);
        writer.WriteLine("org.gradle.jvmargs=-Xmx4096M");
        writer.WriteLine("android.useAndroidX=true");
        writer.WriteLine("android.enableJetifier=true");
        writer.Flush();
        writer.Close();
    }

    private void MainTemplateGradle()
    {
        string mainTemplatePath = Directory.GetCurrentDirectory() + "/Assets/Plugins/Android/mainTemplate.gradle";
        if (File.Exists(mainTemplatePath))
        {
            Debug.Log("mainTemplate.gradle 文件存在");
            StreamWriter writer = new StreamWriter(mainTemplatePath, true);
            writer.WriteLine("坐落");
            writer.Flush();
            writer.Close();
        }
        else
        {
            Debug.Log("mainTemplate.gradle 文件不存在");
        }
    }
}
