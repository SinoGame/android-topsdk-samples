using System;
using System.IO;
using System.Text.RegularExpressions;

public class BuildGradleParser
{
    public static void ParseAndUpdateBuildGradle(string filePath)
    {
        // Read the build.gradle file content
        string buildGradleContent = File.ReadAllText(filePath);

        // Extract repositories and dependencies
        string[] repositories = ExtractRepositories(buildGradleContent);
        string[] dependencies = ExtractDependencies(buildGradleContent);

        // Add new entries to repositories and dependencies
        Array.Resize(ref repositories, repositories.Length + 1);
        repositories[repositories.Length - 1] = "maven { url 'https://storage-sdk-gameplus.meetsocial.com/repository/TopSdk/' }";

        Array.Resize(ref dependencies, dependencies.Length + 1);
        dependencies[dependencies.Length - 1] = "classpath 'com.sino.topsdk.dev:plugin:0.0.2'";

        // Generate the updated content
        string updatedContent = GenerateUpdatedBuildGradleContent(
            buildGradleContent,
            repositories,
            dependencies
        );

        // Write the updated content back to the build.gradle file
        File.WriteAllText(filePath, updatedContent);
    }

    private static string[] ExtractRepositories(string buildGradleContent)
    {
        Regex repositoryRegex = new Regex(
            @"repositories\s*{\s*([\w\W]*?)\s*}",
            RegexOptions.Multiline
        );
        Match repositoryMatch = repositoryRegex.Match(buildGradleContent);

        if (repositoryMatch.Success)
        {
            string repositoriesBlock = repositoryMatch.Groups[1].Value;
            Regex urlRegex = new Regex(@"url\s*'([\w:/\.]+)'");
            MatchCollection urlMatches = urlRegex.Matches(repositoriesBlock);

            string[] repositories = new string[urlMatches.Count];
            for (int i = 0; i < urlMatches.Count; i++)
            {
                repositories[i] = urlMatches[i].Groups[1].Value;
            }

            return repositories;
        }

        return new string[0];
    }

    private static string[] ExtractDependencies(string buildGradleContent)
    {
        Regex dependenciesRegex = new Regex(
            @"dependencies\s*{\s*([\w\W]*?)\s*}",
            RegexOptions.Multiline
        );
        Match dependenciesMatch = dependenciesRegex.Match(buildGradleContent);

        if (dependenciesMatch.Success)
        {
            string dependenciesBlock = dependenciesMatch.Groups[1].Value;
            Regex classpathRegex = new Regex(@"classpath\s*'([\w\.:]+)'");
            MatchCollection classpathMatches = classpathRegex.Matches(dependenciesBlock);

            string[] dependencies = new string[classpathMatches.Count];
            for (int i = 0; i < classpathMatches.Count; i++)
            {
                dependencies[i] = classpathMatches[i].Groups[1].Value;
            }

            return dependencies;
        }

        return new string[0];
    }

    private static string GenerateUpdatedBuildGradleContent(
        string buildGradleContent,
        string[] repositories,
        string[] dependencies
    )
    {
        // Update repositories
        string updatedRepositoriesBlock = "repositories {\n";
        foreach (string repository in repositories)
        {
            updatedRepositoriesBlock += $"\t{repository}\n";
        }
        updatedRepositoriesBlock += "}\n";

        // Update dependencies
        string updatedDependenciesBlock = "dependencies {\n";
        foreach (string dependency in dependencies)
        {
            updatedDependenciesBlock += $"\t{dependency}\n";
        }
        updatedDependenciesBlock += "}\n";

        // Replace existing repositories and dependencies blocks with the updated ones
        string updatedContent = Regex.Replace(
            buildGradleContent,
            @"repositories\s*{\s*([\w\W]*?)\s*}",
            updatedRepositoriesBlock
        );
        updatedContent = Regex.Replace(
            updatedContent,
            @"dependencies\s*{\s*([\w\W]*?)\s*}",
            updatedDependenciesBlock
        );

        return updatedContent;
    }
}
