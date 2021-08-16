using System;

namespace TopSDKDataModel
{
    //平台类型
    public enum TOPPlatformType
    {
        APPLE,
        FACEBOOK,
        GOOGLE,
        NAVER,
        KAKAO,
        SNAPCHAT,
        LINE,
        TWITTER
    };
    //账号信息
    [Serializable]
    public class TOPUserInfo
    {
        public string id;//用户id
        public string name;//用户名
        //
        public string token;//用户token
        public bool isGuest;//是否是游客（仅在获取用户信息接口返回数据有效）
    }

    //错误信息
    [Serializable]
    public class TOPErrorResults
    {
        public int code;
        public string message;
    }
    //绑定解绑错误信息
    [Serializable]
    public class TOPBindErrorResults : TOPErrorResults
    {
        public string platform;//平台google/facebook
    }
    //绑定解绑返回数据
    [Serializable]
    public class TOPBindData
    {
        public string platform;//平台google/facebook
        public int bindStatus;//0:未绑定/解绑 1:已绑定/绑定
    }
    //支付返回数据
    [Serializable]
    public class TOPPaymentData
    {
        public string productId;//商品id
        public string orderNo;//gameplus订单号
        public string payPlatformOrderNo;//支付平台交易号/订单号  
    }

    //支付商品信息
    [Serializable]
    public class TOPPayParameters
    {

        public string productId;//必传
        public string productName;
        public double amount;


        public TOPPayParameters(string productId, string productName, double amount)
        {
            this.productId = productId;
            this.productName = productName;
            this.amount = amount;
        }
    }

    //游戏角色信息
    [Serializable]
    public class TOPRoleInfo
    {
        public string roleId;//角色ID
        public string roleName;//角色名称
        public string roleLevel;//角色等级
        public string serverName;//服务器名称
        public string vipLevel;//vip等级
        public TOPRoleInfo()
        {
        }
        public TOPRoleInfo(string roleId)
        {
            this.roleId = roleId;
        }

        public TOPRoleInfo(string roleId, string roleName, string roleLevel, string serverName, string vipLevel)
        {
            this.roleId = roleId;
            this.roleName = roleName;
            this.roleLevel = roleLevel;
            this.serverName = serverName;
            this.vipLevel = vipLevel;
        }
    }
}