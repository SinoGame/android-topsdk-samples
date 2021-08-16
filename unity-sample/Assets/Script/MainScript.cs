using System.Collections.Generic;
using TopSDKDataModel;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public Button loginButton, userCenterButton, inappButton, bindGoogleButton, bindFacebookButton, logoutButton;
    public Text version, loginStatus, title, message;
    public GameObject dialog;


    private void OnEnable()
    {
        //init
        TopSDKManager.OnInitSuccessEvent += OnInitSuccessEvent;
        TopSDKManager.OnInitFailedEvent += OnInitFailedEvent;
        //login
        TopSDKManager.OnLoginSuccessEvent += OnLoginSuccessEvent;
        TopSDKManager.OnLoginFailedEvent += OnLoginFailedEvent;
        //logout
        TopSDKManager.OnLogoutSuccessEvent += OnLogoutSuccessEvent;
        TopSDKManager.OnLogoutFailedEvent += OnLogoutFailedEvent;
        // bind/unbind绑定和解绑
        TopSDKManager.OnBindSuccessEvent += OnBindSuccessEvent;
        TopSDKManager.OnBindFailedEvent += OnBindFailedEvent;
        //getUserInfo
        TopSDKManager.OnUserInfoSuccessEvent += OnUserInfoSuccessEvent;
        TopSDKManager.OnUserInfoFailedEvent += OnUserInfoFailedEvent;
        //getUserBindInfo
        TopSDKManager.OnUserBindInfoSuccessEvent += OnUserBindInfoSuccessEvent;
        TopSDKManager.OnUserBindInfoFailedEvent += OnUserBindInfoFailedEvent;
        //pay
        TopSDKManager.OnPaySuccessEvent += OnPaySuccessEvent;
        TopSDKManager.OnPayFailedEvent += OnPayFailedEvent;
    }



    // Start is called before the first frame update
    void Start()
    {
        TopSDK.SetDebugEnabled(true);
        //初始化SDK
        //google
        TopSDK.Init("132168550394630144");
        //onestore
        //TopSDK.Init("173100807035879424");
        loginButton.onClick.AddListener(LoginClick);
        userCenterButton.onClick.AddListener(UserCenterClick);
        inappButton.onClick.AddListener(InappClick);
        bindGoogleButton.onClick.AddListener(BindGoogleClick);
        bindFacebookButton.onClick.AddListener(BindFacebookClick);
        logoutButton.onClick.AddListener(LogoutClick);
        version.text = string.Format("SDK v{0} 演示DEMO", TopSDK.SdkName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoginClick()
    {
        TopSDK.Login();
    }
    public void UserCenterClick()
    {

        TopSDK.EnterUserCenter();
    }

    public void InappClick()
    {

#if UNITY_IOS
        TopSDK.Pay(new TOPPayParameters("com.gameplus.product.01", "王者荣耀全皮肤", 0.997), new TOPRoleInfo());
#elif UNITY_ANDROID || UNITY_EDITOR
        TopSDK.Pay(new TOPPayParameters("item_01", "王者荣耀全皮肤", 0.997), new TOPRoleInfo());
#endif
    }
    public void BindGoogleClick()
    {

        TopSDK.BindPlatform(TOPPlatformType.GOOGLE);
    }
    public void BindFacebookClick()
    {

        TopSDK.BindPlatform(TOPPlatformType.FACEBOOK);
    }
    public void LogoutClick()
    {

        TopSDK.Logout();

    }

    private void OnInitSuccessEvent()
    {
        Debug.Log("OnInitSuccessEvent");
        ShowDialog("初始化", "初始化成功");
    }
    private void OnInitFailedEvent(TOPErrorResults error)
    {
        Debug.Log("OnInitFailedEvent: " + error.code + "-----" + error.message);
    }

    //login
    private void OnLoginSuccessEvent(TOPUserInfo accountInfo)
    {
        Debug.Log("OnLoginSuccessEvent: " + accountInfo.id + "-----" + accountInfo.token);

        loginStatus.text = "登录状态：已登录";
        ShowDialog("登录成功", "用户名：" + accountInfo.name + "\n用户id：" + accountInfo.id);
    }
    private void OnLoginFailedEvent(TOPErrorResults error)
    {
        Debug.Log("OnLoginFailedEvent: " + error.code + "-----" + error.message);

        loginStatus.text = "登录状态：登录失败";
        ShowDialog("登录失败", "code：" + error.code + "\nmessage：" + error.message);
    }
    //logout
    private void OnLogoutSuccessEvent()
    {
        Debug.Log("OnLogoutSuccessEvent");

        loginStatus.text = "登录状态：未登录";
        ShowDialog("退出登录", "退出登录成功");
    }
    private void OnLogoutFailedEvent(TOPErrorResults error)
    {
        Debug.Log("OnLogoutFailedEvent: " + error.code + "-----" + error.message);
        ShowDialog("退出登录失败", "code：" + error.code + "\nmessage：" + error.message);
    }
    //bind/unbind
    private void OnBindSuccessEvent(TOPBindData bindResult)
    {
        Debug.Log("OnBindSuccessEvent");
        ShowDialog("绑定成功", "绑定类型：" + bindResult.platform);

    }
    private void OnBindFailedEvent(TOPBindErrorResults error)
    {
        Debug.Log("OnBindFailedEvent: " + error.code + "-----" + error.message);
        ShowDialog("绑定失败", "绑定类型：" + error.platform + "\ncode：" + error.code + "\nmessage：" + error.message);

    }

    //getUserInfo
    private void OnUserInfoSuccessEvent(TOPUserInfo userInfo)
    {
        Debug.Log("OnUseInfoSuccessEvent");
    }
    private void OnUserInfoFailedEvent(TOPErrorResults error)
    {
        Debug.Log("OnUseInfoFailedEvent: " + error.code + "-----" + error.message);
    }

    //getUserBindInfo
    private void OnUserBindInfoSuccessEvent(List<string> items)
    {
        foreach (string item in items)
        {
            Debug.Log("UserBindInfo==" + item);
        }
        Debug.Log("OnUseBindInfoSuccessEvent");

    }
    private void OnUserBindInfoFailedEvent(TOPErrorResults error)
    {
        Debug.Log("OnUseBindInfoFailedEvent: " + error.code + "-----" + error.message);

    }

    //pay
    private void OnPaySuccessEvent(TOPPaymentData payResult)
    {
        Debug.Log("OnPaySuccessEvent: " + payResult.orderNo + "-----" + payResult.payPlatformOrderNo);
        ShowDialog("支付成功", "商品ID：" + payResult.productId + "\nSDK订单号：" + payResult.orderNo + "\n三方订单号：" + payResult.payPlatformOrderNo);
    }

    private void OnPayFailedEvent(TOPErrorResults error)
    {
        Debug.Log("OnPayFailedEvent: " + error.code + "-----" + error.message);
        ShowDialog("支付失败", "code：" + error.code + "\nmessage：" + error.message);
    }
    public void ShowDialog(string titleString, string messageString)
    {
        title.text = titleString;
        message.text = messageString;
        dialog.SetActive(true);
    }

    private void OnDisable()
    {
        // Remove all event handlers
        //init
        TopSDKManager.OnInitSuccessEvent -= OnInitSuccessEvent;
        TopSDKManager.OnInitFailedEvent -= OnInitFailedEvent;
        //login
        TopSDKManager.OnLoginSuccessEvent -= OnLoginSuccessEvent;
        TopSDKManager.OnLoginFailedEvent -= OnLoginFailedEvent;
        //logout
        TopSDKManager.OnLogoutSuccessEvent -= OnLogoutSuccessEvent;
        TopSDKManager.OnLogoutFailedEvent -= OnLogoutFailedEvent;
        // bind/unbind绑定和解绑
        TopSDKManager.OnBindSuccessEvent -= OnBindSuccessEvent;
        TopSDKManager.OnBindFailedEvent -= OnBindFailedEvent;
        //getUserInfo
        TopSDKManager.OnUserInfoSuccessEvent -= OnUserInfoSuccessEvent;
        TopSDKManager.OnUserInfoFailedEvent -= OnUserInfoFailedEvent;
        //getUserBindInfo
        TopSDKManager.OnUserBindInfoSuccessEvent -= OnUserBindInfoSuccessEvent;
        TopSDKManager.OnUserBindInfoFailedEvent -= OnUserBindInfoFailedEvent;
        //pay
        TopSDKManager.OnPaySuccessEvent -= OnPaySuccessEvent;
        TopSDKManager.OnPayFailedEvent -= OnPayFailedEvent;
    }
}
