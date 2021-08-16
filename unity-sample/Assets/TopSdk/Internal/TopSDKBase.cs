using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class TopSDKBase
{

    // Allocate the GamePlusManager singleton, which receives all callback events from the native SDKs.
    protected static void InitManager()
    {
        var type = typeof(TopSDKManager);
        var mgr = new GameObject("TopSDKManager", type).GetComponent<TopSDKManager>(); // Its Awake() method sets Instance.
        if (TopSDKManager.Instance != mgr)
            Debug.LogWarning(
                "It looks like you have the " + type.Name
                + " on a GameObject in your scene. Please remove the script from your scene.");
    }


    protected TopSDKBase() { }
}
