using TopSDKDataModel;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class TopSDKAndroid : TopSDKBase
{
    static TopSDKAndroid()
    {
        InitManager();
    }

    private static readonly AndroidJavaClass PluginClass = new AndroidJavaClass("com.sino.topsdk.unity.TOPUnityPlugin");


    private static AndroidJavaObject JavaArrayFromCS(string[] values)
    {
        AndroidJavaClass arrayClass = new AndroidJavaClass("java.lang.reflect.Array");
        AndroidJavaObject arrayObject = arrayClass.CallStatic<AndroidJavaObject>("newInstance", new AndroidJavaClass("java.lang.String"), values.Length);
        for (int i = 0; i < values.Length; i++)
        {
            arrayClass.CallStatic("set", arrayObject, i, new AndroidJavaObject("java.lang.String", values[i]));
        }
        return arrayObject;
    }

    #region SdkSetup

    protected static string GetSDKVersion()
    {
        return PluginClass.CallStatic<string>("getSDKVersion");
    }

    public static void SetDebugEnabled(bool isEnabled)
    {
        PluginClass.CallStatic("setDebugEnabled", isEnabled);
    }


    public static void Init(string appid)
    {
        PluginClass.CallStatic("init", appid);
    }

    public static void Login()
    {
        PluginClass.CallStatic("login");
    }

    public static void Logout()
    {
        PluginClass.CallStatic("logout");
    }

    public static void EnterUserCenter()
    {
        PluginClass.CallStatic("enterUserCenter");
    }
    public static void GetUserInfo()
    {
        PluginClass.CallStatic("getUserInfo");
    }
    public static void GetUserBindInfo()
    {
        PluginClass.CallStatic("getUserBindInfo");
    }
    public static void BindPlatform(TOPPlatformType platformType)
    {
        String platformStr = "";
        switch (platformType)
        {
            case TOPPlatformType.APPLE:
                platformStr = "APPLE";
                break;
            case TOPPlatformType.FACEBOOK:
                platformStr = "FACEBOOK";
                break;
            case TOPPlatformType.GOOGLE:
                platformStr = "GOOGLE";
                break;
            case TOPPlatformType.KAKAO:
                platformStr = "KAKAO";
                break;
            case TOPPlatformType.LINE:
                platformStr = "LINE";
                break;
            case TOPPlatformType.NAVER:
                platformStr = "NAVER";
                break;
            case TOPPlatformType.SNAPCHAT:
                platformStr = "SNAPCHAT";
                break;
            case TOPPlatformType.TWITTER:
                platformStr = "TWITTER";
                break;
        }
        PluginClass.CallStatic("bindPlatform", platformStr);
    }

    public static void Pay(TOPPayParameters productInfo, TOPRoleInfo roleInfo)
    {
        PluginClass.CallStatic("pay", JsonUtility.ToJson(productInfo), JsonUtility.ToJson(roleInfo));
    }


    public static void Release()
    {
        PluginClass.CallStatic("release");
    }

    #endregion SdkSetup
}
