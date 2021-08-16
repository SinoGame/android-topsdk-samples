using System;
using System.Collections.Generic;
using UnityEngine;
using TopSDKDataModel;
using TopSDKJsonUtils;

public class TopSDKManager : MonoBehaviour
{
    public static TopSDKManager Instance { get; private set; }
    //init
    public static event Action OnInitSuccessEvent;
    public static event Action<TOPErrorResults> OnInitFailedEvent;
    //login
    public static event Action<TOPUserInfo> OnLoginSuccessEvent;
    public static event Action<TOPErrorResults> OnLoginFailedEvent;

    // logout
    public static event Action OnLogoutSuccessEvent;
    public static event Action<TOPErrorResults> OnLogoutFailedEvent;

    //userInfo
    public static event Action<TOPUserInfo> OnUserInfoSuccessEvent;
    public static event Action<TOPErrorResults> OnUserInfoFailedEvent;

    //userBindInfo
    public static event Action<List<string>> OnUserBindInfoSuccessEvent;
    public static event Action<TOPErrorResults> OnUserBindInfoFailedEvent;

    //bind/unbind平台
    public static event Action<TOPBindData> OnBindSuccessEvent;
    public static event Action<TOPBindErrorResults> OnBindFailedEvent;

    //支付
    public static event Action<TOPPaymentData> OnPaySuccessEvent;
    public static event Action<TOPErrorResults> OnPayFailedEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);
    }


    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    // init
    public void EmitInitSuccessEvent(string argsJson)
    {
        Debug.Log("EmitInitSuccessEvent json --> " + argsJson);
        var evt = OnInitSuccessEvent;
        if (evt != null) evt();
    }
    public void EmitInitFailedEvent(string argsJson)
    {
        Debug.Log("EmitInitFailedEvent json --> " + argsJson);
        TOPErrorResults errorResults = JsonUtility.FromJson<TOPErrorResults>(argsJson);
        var evt = OnLoginFailedEvent;
        if (evt != null) evt(errorResults);
    }

    // login
    public void EmitLoginSuccessEvent(string argsJson)
    {
        Debug.Log("EmitLoginSuccessEvent json --> " + argsJson);
        TOPUserInfo accountInfo = JsonUtility.FromJson<TOPUserInfo>(argsJson);
        var evt = OnLoginSuccessEvent;
        if (evt != null) evt(accountInfo);
    }
    public void EmitLoginFailedEvent(string argsJson)
    {
        Debug.Log("EmitLoginFailedEvent json --> " + argsJson);
        TOPErrorResults errorResults = JsonUtility.FromJson<TOPErrorResults>(argsJson);
        var evt = OnLoginFailedEvent;
        if (evt != null) evt(errorResults);
    }
    // logout
    public void EmitLogoutSuccessEvent(string argsJson)
    {
        Debug.Log("EmitLogoutSuccessEvent json --> " + argsJson);
        var evt = OnLogoutSuccessEvent;
        if (evt != null) evt();
    }
    public void EmitLogoutFailedEvent(string argsJson)
    {
        Debug.Log("EmitLogoutFailedEvent json --> " + argsJson);
        TOPErrorResults errorResults = JsonUtility.FromJson<TOPErrorResults>(argsJson);
        var evt = OnLogoutFailedEvent;
        if (evt != null) evt(errorResults);
    }
    // bind/unbind绑定和解绑
    public void EmitBindSuccessEvent(string argsJson)
    {
        Debug.Log("EmitBindSuccessEvent json --> " + argsJson);
        TOPBindData bindResult = JsonUtility.FromJson<TOPBindData>(argsJson);
        var evt = OnBindSuccessEvent;
        if (evt != null) evt(bindResult);
    }
    public void EmitBindFailedEvent(string argsJson)
    {
        Debug.Log("EmitBindFailedEvent json --> " + argsJson);
        TOPBindErrorResults bindErrorResults = JsonUtility.FromJson<TOPBindErrorResults>(argsJson);
        var evt = OnBindFailedEvent;
        if (evt != null) evt(bindErrorResults);
    }
    // getUserInfo
    public void EmitUserInfoSuccessEvent(string argsJson)
    {
        Debug.Log("EmitUserInfoSuccessEvent json --> " + argsJson);
        TOPUserInfo userInfo = JsonUtility.FromJson<TOPUserInfo>(argsJson);
        var evt = OnUserInfoSuccessEvent;
        if (evt != null) evt(userInfo);
    }
    public void EmitUserInfoFailedEvent(string argsJson)
    {
        Debug.Log("EmitUserInfoFailedEvent json --> " + argsJson);
        TOPErrorResults errorResults = JsonUtility.FromJson<TOPErrorResults>(argsJson);
        var evt = OnUserInfoFailedEvent;
        if (evt != null) evt(errorResults);
    }

    // getUserBindInfo
    public void EmitUserBindInfoSuccessEvent(string argsJson)
    {
        Debug.Log("EmitUserBindInfoSuccessEvent json --> " + argsJson);
        List<string> bindItems = TopSDKJsonHelper.FromJson<string>(argsJson);
        var evt = OnUserBindInfoSuccessEvent;
        if (evt != null) evt(bindItems);
    }
    public void EmitUserBindInfoFailedEvent(string argsJson)
    {
        Debug.Log("EmitUserBindInfoFailedEvent json --> " + argsJson);
        TOPErrorResults errorResults = JsonUtility.FromJson<TOPErrorResults>(argsJson);
        var evt = OnUserBindInfoFailedEvent;
        if (evt != null) evt(errorResults);
    }
    // pay

    public void EmitPaySuccessEvent(string argsJson)
    {
        Debug.Log("EmitPaySuccessEvent json --> " + argsJson);
        TOPPaymentData payResult = JsonUtility.FromJson<TOPPaymentData>(argsJson);
        var evt = OnPaySuccessEvent;
        if (evt != null) evt(payResult);
    }
    public void EmitPayFailedEvent(string argsJson)
    {
        Debug.Log("EmitPayFailedEvent json --> " + argsJson);
        TOPErrorResults errorResults = JsonUtility.FromJson<TOPErrorResults>(argsJson);
        var evt = OnPayFailedEvent;
        if (evt != null) evt(errorResults);
    }
}
