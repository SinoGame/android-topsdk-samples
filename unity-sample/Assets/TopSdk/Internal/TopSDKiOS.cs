using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using TopSDKDataModel;



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

    public static void Release()
    {

    }

    #endregion SdkSetup
}
