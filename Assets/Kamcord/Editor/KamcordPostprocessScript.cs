

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

using System;
using System.Diagnostics;
using System.IO;

public class KamcordPostprocessScript : MonoBehaviour
{
#if UNITY_IPHONE || (UNITY_ANDROID && ((!(UNITY_2_6 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_1))))

    // Replaces PostprocessBuildPlayer functionality
    [PostProcessBuild(-100)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
    {
        Console.WriteLine ("--- Kamcord --- Executing post process build phase.");

#if UNITY_IPHONE
        Process p = new Process();
        p.StartInfo.FileName = "perl";

        string kamcordUnityVersion = "";
#if UNITY_3_5
        kamcordUnityVersion = "350";
#elif (UNITY_4_0 || UNITY_4_0_1)
        kamcordUnityVersion = "400";
#elif UNITY_4_1
        kamcordUnityVersion = "410";
#endif // Unity version check

        p.StartInfo.Arguments = string.Format("Assets/Kamcord/Editor/KamcordPostprocessbuildPlayer1 \"{0}\" \"{1}\"", pathToBuildProject, kamcordUnityVersion);

        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.OutputDataReceived += (sender, args) => Console.WriteLine("--- Kamcord output: {0}", args.Data);
        p.Start();
        p.BeginOutputReadLine();

        p.WaitForExit();

#elif UNITY_ANDROID

        Console.WriteLine ("                ... for Android.");

        string lib_in_assets = Path.Combine(pathToBuildProject, Path.Combine(PlayerSettings.productName, Path.Combine("assets", Path.Combine("libs", Path.Combine("armeabi-v7a", "libunity.so")))));
        string lib_in_libs = Path.Combine(pathToBuildProject, Path.Combine(PlayerSettings.productName, Path.Combine("libs", Path.Combine("armeabi-v7a", "libunity.so"))));

        if( File.Exists(lib_in_assets) )
        {
            try
            {
                if( File.Exists(lib_in_libs) )
                {
                    File.Delete(lib_in_libs);
                }

                File.Copy(lib_in_assets, lib_in_libs); // Needs to be in both locations.
            }
            catch( Exception e )
            {
                Console.WriteLine (e.ToString());
            }
        }

#endif // Platform check

        Console.WriteLine("--- Kamcord --- Finished executing post process build phase.");
    }
#endif // Valid build check
}
