using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using TopSDKDataModel;
using System.Collections.Generic;
using TopSDKJsonUtils;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class TopSDKiOS : TopSDKBase
{
#if UNITY_IOS
	// 访问本地插件接口的声明
	[DllImport ("__Internal")]
	private static extern void _init(string appId);

	[DllImport ("__Internal")]
	private static extern string _getSDKVersion();

    [DllImport ("__Internal")]
	private static extern void _login();

    [DllImport ("__Internal")]
	private static extern void _logout();

    [DllImport ("__Internal")]
	private static extern void _enterUserCenter();
  
    [DllImport ("__Internal")]
	private static extern void _getUserInfo();

    [DllImport ("__Internal")]
	private static extern void _getUserBindInfo();

    [DllImport ("__Internal")]
	private static extern void _pay(string paymentJson, string roleInfoJson);

    [DllImport ("__Internal")]
	private static extern void _bindPlatform(string platform);
    
    [DllImport ("__Internal")]
    private static extern void _loginEvent(string method);
    
    [DllImport ("__Internal")]
    private static extern void _signupEvent(string method);
    
    [DllImport ("__Internal")]
    private static extern void _purchaseEvent(string purchaseDataJson);
    
    [DllImport ("__Internal")]
    private static extern void _tutorialBeginEvent();
    
    [DllImport ("__Internal")]
    private static extern void _tutorialCompleteEvent();
    
    [DllImport ("__Internal")]
    private static extern void _levelUpEvent(int level, string roleName);
    
    [DllImport ("__Internal")]
    private static extern void _unlockAchievementEvent(string achievementId);
    
    [DllImport ("__Internal")]
    private static extern void _shareEvent(string method, string contentType, string contentId);
    
    [DllImport ("__Internal")]
    private static extern void _earnVirtualCurrencyEvent(string name, int count);
    
    [DllImport ("__Internal")]
    private static extern void _spendVirtualCurrencyEvent(string name, int count, string goodsName);
    
    [DllImport ("__Internal")]
    private static extern void _levelStartEvent(string levelName);
    
    [DllImport ("__Internal")]
    private static extern void _levelEndEvent(string levelName, bool success);
    
    [DllImport ("__Internal")]
    private static extern void _report(string eventName, string eventValuesJson, string channelType);
#else
    // 访问本地插件接口的声明
    private static void _init(string appId) { }

    private static string _getSDKVersion() { return ""; }

    private static void _login() { }

    private static void _logout() { }

    private static void _enterUserCenter() { }

    private static void _getUserInfo() { }

    private static void _getUserBindInfo() { }

    private static void _pay(string paymentJson, string roleInfoJson) { }

    private static void _bindPlatform(string platform) { }

    private static void _loginEvent(string method) { }

    private static void _signupEvent(string method) { }

    private static void _purchaseEvent(string purchaseDataJson) { }

    private static void _tutorialBeginEvent() { }

    private static void _tutorialCompleteEvent() { }

    private static void _levelUpEvent(int level, string roleName) { }

    private static void _unlockAchievementEvent(string achievementId) { }

    private static void _shareEvent(string method, string contentType, string contentId) { }

    private static void _earnVirtualCurrencyEvent(string name, int count) { }

    private static void _spendVirtualCurrencyEvent(string name, int count, string goodsName) { }
    private static void _levelStartEvent(string levelName) { }

    private static void _levelEndEvent(string levelName, bool success) { }

    private static void _report(string eventName, string eventValuesJson, string channelType) { }
#endif

    static TopSDKiOS()
    {
        InitManager();
    }

    #region SdkSetup

    protected static string GetSDKVersion()
    {
#if !UNITY_EDITOR
        return _getSDKVersion();
#else
        return "";
#endif
    }

    public static void SetDebugEnabled(bool isEnabled)
    {

    }


    public static void Init(string appid)
    {
#if !UNITY_EDITOR
		_init(appid);
#endif
    }

    public static void Login()
    {
#if !UNITY_EDITOR
		_login();
#endif
    }

    public static void Logout()
    {
#if !UNITY_EDITOR
		_logout();
#endif
    }

    public static void EnterUserCenter()
    {
#if !UNITY_EDITOR
		_enterUserCenter();
#endif
    }


    public static void GetUserInfo()
    {
#if !UNITY_EDITOR
		_getUserInfo();
#endif
    }
    public static void GetUserBindInfo()
    {
#if !UNITY_EDITOR
		_getUserBindInfo();
#endif
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
#if !UNITY_EDITOR
		_bindPlatform(platformStr);
#endif
    }

    public static void Pay(TOPPayParameters productInfo, TOPRoleInfo roleInfo)
    {
#if !UNITY_EDITOR
		_pay(JsonUtility.ToJson(productInfo), JsonUtility.ToJson(roleInfo));
#endif
    }
    public static void LoginEvent(string method)
    {
#if !UNITY_EDITOR
         _loginEvent(method);
#endif
    }

    public static void SignupEvent(string method)
    {
#if !UNITY_EDITOR
         _signupEvent(method);
#endif
    }

    public static void PurchaseEvent(TOPPurchaseData data)
    {
#if !UNITY_EDITOR
         _purchaseEvent(JsonUtility.ToJson(data));
#endif
    }

    public static void TutorialBeginEvent()
    {
#if !UNITY_EDITOR
         _tutorialBeginEvent();
#endif
    }

    public static void TutorialCompleteEvent()
    {
#if !UNITY_EDITOR
         _tutorialCompleteEvent();
#endif
    }

    public static void LevelUpEvent(int level, string roleName)
    {
#if !UNITY_EDITOR
         _levelUpEvent(level,roleName);
#endif
    }

    public static void UnlockAchievementEvent(string achievementId)
    {
#if !UNITY_EDITOR
         _unlockAchievementEvent(achievementId);
#endif
    }

    public static void ShareEvent(string method, string contentType, string contentId)
    {
#if !UNITY_EDITOR
         _shareEvent(method,contentType,contentId);
#endif
    }

    public static void EarnVirtualCurrencyEvent(string name, int count)
    {
#if !UNITY_EDITOR
         _earnVirtualCurrencyEvent(name,count);
#endif
    }

    public static void SpendVirtualCurrencyEvent(string name, int count, string goodsName)
    {
#if !UNITY_EDITOR
         _spendVirtualCurrencyEvent(name,count,goodsName);
#endif
    }

    public static void LevelStartEvent(string levelName)
    {
#if !UNITY_EDITOR
         _levelStartEvent(levelName);
#endif
    }

    public static void LevelEndEvent(string levelName, bool success)
    {
#if !UNITY_EDITOR
         _levelEndEvent(levelName,success);
#endif
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
#if !UNITY_EDITOR
         _report(eventName,eventParams,channelStr);
#endif
    }
    public static void Release()
    {

    }

    #endregion SdkSetup
}
