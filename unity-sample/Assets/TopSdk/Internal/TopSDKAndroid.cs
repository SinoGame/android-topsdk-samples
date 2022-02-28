using TopSDKDataModel;
using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using System.Collections.Generic;
using TopSDKJsonUtils;

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
    //private static AndroidJavaObject convertDictionaryToJavaMap(Dictionary<string, string> dictionary)
    //{
    //    AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap");
    //    IntPtr putMethod = AndroidJNIHelper.GetMethodID(map.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
    //    jvalue[] val;
    //    if (dictionary != null)
    //    {
    //        foreach (var entry in dictionary)
    //        {
    //            val = AndroidJNIHelper.CreateJNIArgArray(new object[] { entry.Key, entry.Value });
    //            AndroidJNI.CallObjectMethod(map.GetRawObject(), putMethod, val);
    //            AndroidJNI.DeleteLocalRef(val[0].l);
    //            AndroidJNI.DeleteLocalRef(val[1].l);
    //        }
    //    }

    //    return map;
    //}
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

    public static void LoginEvent(string method)
    {
        PluginClass.CallStatic("loginEvent", method);
    }

    public static void SignupEvent(string method)
    {
        PluginClass.CallStatic("signupEvent", method);
    }

    public static void PurchaseEvent(TOPPurchaseData data)
    {
        PluginClass.CallStatic("purchaseEvent", JsonUtility.ToJson(data));
    }

    public static void TutorialBeginEvent()
    {
        PluginClass.CallStatic("tutorialBeginEvent");
    }

    public static void TutorialCompleteEvent()
    {
        PluginClass.CallStatic("tutorialCompleteEvent");
    }

    public static void LevelUpEvent(int level, string roleName)
    {
        PluginClass.CallStatic("levelUpEvent", level, roleName);
    }

    public static void UnlockAchievementEvent(string achievementId)
    {
        PluginClass.CallStatic("unlockAchievementEvent", achievementId);
    }

    public static void ShareEvent(string method, string contentType, string contentId)
    {
        PluginClass.CallStatic("shareEvent", method, contentType, contentId);
    }

    public static void EarnVirtualCurrencyEvent(string name, int count)
    {
        PluginClass.CallStatic("earnVirtualCurrencyEvent", name, count);
    }

    public static void SpendVirtualCurrencyEvent(string name, int count, string goodsName)
    {
        PluginClass.CallStatic("spendVirtualCurrencyEvent", name, count, goodsName);
    }

    public static void LevelStartEvent(string levelName)
    {
        PluginClass.CallStatic("levelStartEvent", levelName);
    }

    public static void LevelEndEvent(string levelName, bool success)
    {
        PluginClass.CallStatic("levelEndEvent", levelName, success);
    }

    public static void Report(string eventName, Dictionary<string, string> eventValues, TOPDataChannelType channelType)
    {
        string channelStr = "";
        switch (channelType)
        {
            case TOPDataChannelType.All:
                channelStr = "All";
                break;
            case TOPDataChannelType.Appsflyer:
                channelStr = "Appsflyer";
                break;
            case TOPDataChannelType.Firebase:
                channelStr = "Firebase";
                break;
            case TOPDataChannelType.Adjust:
                channelStr = "Adjust";
                break;

        }
        string eventParams = null;
        if (eventValues != null)
        {
            eventParams = TopSDKJsonHelper.DictionaryToJson(eventValues);
        }
        PluginClass.CallStatic("report", eventName, eventParams, channelStr);
    }

    public static void Release()
    {
        PluginClass.CallStatic("release");
    }

    #endregion SdkSetup
}
