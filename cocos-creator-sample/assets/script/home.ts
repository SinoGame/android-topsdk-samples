
import { Component, Label, RichText, sys, _decorator } from 'cc';
import { TOPPayment, TOPPlatformType, TOPRoleInfo, TopSDK, TopSDKEmitter } from '../TopSdk/TopSDK';
const { ccclass, property } = _decorator;


@ccclass('Home')
export class Home extends Component {
    // [1]
    // dummy = '';

    // [2]
    // @property
    // serializableDummy = 0;
    EmitInitSuccessEvent(eventName: string) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
    }

    EmitInitFailedEvent(eventName: string) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
    }

    EmitLoginSuccessEvent(eventName: string, userInfo: Map<string, any>) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
        if (this.logDetailLabel) {
            this.logDetailLabel.string = String(userInfo);
        }
    }

    EmitLoginFailedEvent(eventName: string, errorInfo: Map<string, any>) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
        if (this.logDetailLabel) {
            this.logDetailLabel.string = String(errorInfo);
        }
    }

    EmitPaySuccessEvent(eventName: string, payInfo: Map<string, any>) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
        if (this.logDetailLabel) {
            this.logDetailLabel.string = String(payInfo);
        }
    }

    EmitPayFailedEvent(eventName: string, errorInfo: Map<string, any>) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
        if (this.logDetailLabel) {
            this.logDetailLabel.string = String(errorInfo);
        }
    }

    EmitLogoutSuccessEvent(eventName: string) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
    }

    EmitUserInfoSuccessEvent(eventName: string, userInfo: Map<string, any>) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
        if (this.logDetailLabel) {
            this.logDetailLabel.string = String(userInfo);
        }
    }

    EmitUserInfoFailedEvent(eventName: string, errorInfo: Map<string, any>) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
        if (this.logDetailLabel) {
            this.logDetailLabel.string = String(errorInfo);
        }
    }

    EmitUserBindInfoSuccessEvent(eventName: string, bindInfo: Array<string>) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
        if (this.logDetailLabel) {
            this.logDetailLabel.string = String(bindInfo);
        }
    }

    EmitUserBindInfoFailedEvent(eventName: string, errorInfo: Map<string, any>) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
        if (this.logDetailLabel) {
            this.logDetailLabel.string = String(errorInfo);
        }
    }

    EmitBindSuccessEvent(eventName: string, bindInfo: Map<string, any>) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
        if (this.logDetailLabel) {
            this.logDetailLabel.string = String(bindInfo);
        }
    }

    EmitBindFailedEvent(eventName: string, errorInfo: Map<string, any>) {
        if (this.logLabel) {
            this.logLabel.string = eventName;
        }
        if (this.logDetailLabel) {
            this.logDetailLabel.string = String(errorInfo);
        }
    }

    logLabel: Label | null = null;
    logDetailLabel: RichText | null = null;

    start() {
        var loginBtn = this.node.getChildByName("loginBtn");
        if (loginBtn) {
            loginBtn.on("click", this.loginHanlder);
        }

        var userCenterBtn = this.node.getChildByName("userCenterBtn");
        if (userCenterBtn) {
            userCenterBtn.on("click", this.userCenterHanlder);
        }

        var logoutBtn = this.node.getChildByName("logoutBtn");
        if (logoutBtn) {
            logoutBtn.on("click", this.logoutHandler);
        }

        var bindFacebookBtn = this.node.getChildByName("bindFacebookBtn");
        if (bindFacebookBtn) {
            bindFacebookBtn.on("click", this.bindFacebookHandler);
        }

        var getUserInfoBtn = this.node.getChildByName("getUserInfoBtn");
        if (getUserInfoBtn) {
            getUserInfoBtn.on("click", this.getUserInfoHandler);
        }

        var getBindInfoBtn = this.node.getChildByName("getBindInfoBtn");
        if (getBindInfoBtn) {
            getBindInfoBtn.on("click", this.getBindInfoHandler);
        }

        var buyBtn = this.node.getChildByName("buyBtn");
        if (buyBtn) {
            buyBtn.on("click", this.buy);
        }

        var logLabel = this.node.getChildByName("logLabel");
        if (logLabel) {
            if (logLabel.getComponent(Label)) {
                this.logLabel = logLabel.getComponent(Label);
            }
        }

        var logDetailLabel = this.node.getChildByName("logDetailLabel");
        if (logDetailLabel) {
            if (logDetailLabel.getComponent(RichText)) {
                this.logDetailLabel = logDetailLabel.getComponent(RichText);
            }
        }

        TopSDKEmitter.register("EmitInitSuccessEvent", this.EmitInitSuccessEvent, this);
        TopSDKEmitter.register("EmitInitFailedEvent", this.EmitInitFailedEvent, this);
        TopSDKEmitter.register("EmitLoginSuccessEvent", this.EmitLoginSuccessEvent, this);
        TopSDKEmitter.register("EmitLoginFailedEvent", this.EmitLoginFailedEvent, this);
        TopSDKEmitter.register("EmitPaySuccessEvent", this.EmitPaySuccessEvent, this);
        TopSDKEmitter.register("EmitPayFailedEvent", this.EmitPayFailedEvent, this);
        TopSDKEmitter.register("EmitLogoutSuccessEvent", this.EmitLogoutSuccessEvent, this);
        TopSDKEmitter.register("EmitUserInfoSuccessEvent", this.EmitUserInfoSuccessEvent, this);
        TopSDKEmitter.register("EmitUserInfoFailedEvent", this.EmitUserInfoFailedEvent, this);
        TopSDKEmitter.register("EmitUserBindInfoSuccessEvent", this.EmitUserBindInfoSuccessEvent, this);
        TopSDKEmitter.register("EmitUserBindInfoFailedEvent", this.EmitUserBindInfoFailedEvent, this);
        TopSDKEmitter.register("EmitBindSuccessEvent", this.EmitBindSuccessEvent, this);
        TopSDKEmitter.register("EmitBindFailedEvent", this.EmitBindFailedEvent, this);

        TopSDK.init("132168550394630144");
        //TopSDK.init("173100807035879424");
    }

    loginHanlder() {
        console.log("点击登录");
        TopSDK.login();
    }

    userCenterHanlder() {
        console.log("点击用户中心");
        TopSDK.enterUserCenter();
    }

    logoutHandler() {
        console.log("点击登出");
        TopSDK.logout();
    }

    bindFacebookHandler() {
        console.log("点击Facebook绑定");
        TopSDK.bindPlatform(TOPPlatformType.FACEBOOK);
    }

    getUserInfoHandler() {
        console.log("点击获取用户信息");
        TopSDK.getUserInfo();
    }

    getBindInfoHandler() {
        console.log("点击获取用户绑定信息");
        TopSDK.getUserBindInfo();
    }

    buy() {
        console.log("点击购买");
        if (sys.OS_IOS == sys.os) {
            let payment = new TOPPayment("com.gameplus.product.01", "无敌小礼包", 1.0);
            let roleInfo = new TOPRoleInfo("001", "小白", "100", "一区", "10");
            TopSDK.pay(payment, roleInfo);
        } else if (sys.OS_ANDROID == sys.os) {
            let payment = new TOPPayment("item_01", "无敌小礼包", 1.0);
            let roleInfo = new TOPRoleInfo("001", "小白", "100", "一区", "10");
            TopSDK.pay(payment, roleInfo);
        }
    }
    // update (deltaTime: number) {
    //     // [4]
    // }
}

