import { sys } from 'cc';

export enum TOPPlatformType {
    APPLE = "APPLE",
    FACEBOOK = "FACEBOOK",
    GOOGLE = "GOOGLE",
    SNAPCHAT = "SNAPCHAT",
    LINE = "LINE",
    NAVER = "NAVER",
    KAKAO = "KAKAO",
    TWITTER = "TWITTER"
}

export class TOPPayment {
    productId: string;//必传
    amount: number;
    productName: string;

    constructor(productId: string, productName: string, amount: number) {
        this.productId = productId;
        this.amount = amount;
        this.productName = productName;
    }

    toJsonStr() {
        let jsonObj = {
            "productId": this.productId ? this.productId : "",
            "productName": this.productName ? this.productName : "",
            "amount": this.amount
        };

        return JSON.stringify(jsonObj);
    }
}

export class TOPRoleInfo {
    roleId: string;
    roleName: string;
    roleLevel: string;
    serverName: string;
    vipLevel: string;

    constructor(roleId: string, roleName: string, roleLevel: string, serverName: string, vipLevel: string) {
        this.roleId = roleId;
        this.roleName = roleName;
        this.roleLevel = roleLevel;
        this.serverName = serverName;
        this.vipLevel = vipLevel;
    }

    toJsonStr() {
        let jsonObj = {
            "roleId": this.roleId ? this.roleId : "",
            "roleName": this.roleName ? this.roleName : "",
            "roleLevel": this.roleLevel ? this.roleLevel : "",
            "serverName": this.serverName ? this.serverName : "",
            "vipLevel": this.vipLevel ? this.vipLevel : ""
        };

        return JSON.stringify(jsonObj);
    }
}

export class TopSDK {
    static androidClass: string = "com/sino/topsdk/cocos/TOPCocosPlugin";
    static getVersion() {
        if (sys.isNative) {
            if (sys.OS_IOS == sys.os) {
                return jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "getVersion");
            } else if (sys.OS_ANDROID == sys.os) {
                return jsb.reflection.callStaticMethod(this.androidClass, "getVersion", "()Ljava/lang/String;");
            }
        }
    }

    static init(appid: string) {
        if (sys.isNative) {
            if (sys.OS_IOS == sys.os) {
                jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "initWithAppid:", appid);
            } else if (sys.OS_ANDROID == sys.os) {
                return jsb.reflection.callStaticMethod(this.androidClass, "init", "(Ljava/lang/String;)V", appid);
            } else {

            }
        }
    }

    static login() {
        if (sys.isNative) {
            if (sys.OS_IOS == sys.os) {
                jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "login");
            } else if (sys.OS_ANDROID == sys.os) {
                return jsb.reflection.callStaticMethod(this.androidClass, "login", "()V");
            }
        }
    }

    static logout() {
        if (sys.isNative) {
            if (sys.OS_IOS == sys.os) {
                jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "logout");
            } else if (sys.OS_ANDROID == sys.os) {
                return jsb.reflection.callStaticMethod(this.androidClass, "logout", "()V");
            }
        }
    }

    static bindPlatform(platform: TOPPlatformType) {
        console.log("开始绑定:" + platform);
        if (sys.isNative) {
            if (sys.OS_IOS == sys.os) {
                jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "bindPlatform:", platform);
            } else if (sys.OS_ANDROID == sys.os) {
                return jsb.reflection.callStaticMethod(this.androidClass, "bindPlatform", "(Ljava/lang/String;)V", platform);
            }
        }
    }

    static enterUserCenter() {
        if (sys.isNative) {
            if (sys.OS_IOS == sys.os) {
                jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "enterUserCenter");
            } else if (sys.OS_ANDROID == sys.os) {
                return jsb.reflection.callStaticMethod(this.androidClass, "enterUserCenter", "()V");
            } else {

            }
        }
    }

    static getUserInfo() {
        if (sys.isNative) {
            if (sys.OS_IOS == sys.os) {
                jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "getUserInfo");
            } else if (sys.OS_ANDROID == sys.os) {
                return jsb.reflection.callStaticMethod(this.androidClass, "getUserInfo", "()V");
            }
        }
    }

    static getUserBindInfo() {
        if (sys.isNative) {
            if (sys.OS_IOS == sys.os) {
                jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "getUserBindInfo");
            } else if (sys.OS_ANDROID == sys.os) {
                return jsb.reflection.callStaticMethod(this.androidClass, "getUserBindInfo", "()V");
            }
        }
    }

    static pay(payment: TOPPayment, roleInfo: TOPRoleInfo) {
        if (!payment || !roleInfo) {
            return;
        }
        if (sys.isNative) {
            if (sys.OS_IOS == sys.os) {
                jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "payWithPayment:roleInfo:", payment.toJsonStr(), roleInfo.toJsonStr());
            } else if (sys.OS_ANDROID == sys.os) {
                return jsb.reflection.callStaticMethod(this.androidClass, "pay", "(Ljava/lang/String;Ljava/lang/String;)V", payment.toJsonStr(), roleInfo.toJsonStr());
            }
        }
    }
}

export class TopSDKEmitter {
    private static listeners: Map<string, any> = new Map();
    public static register(name: string, callback: Function, context: any) {
        let observers: TopSDKObserver[] = TopSDKEmitter.listeners.get(name);
        if (!observers) {
            TopSDKEmitter.listeners.set(name, []);
        }
        TopSDKEmitter.listeners.get(name).push(new TopSDKObserver(callback, context));
    }

    public static remove(name: string, callback: Function, context: any) {
        let observers: TopSDKObserver[] = TopSDKEmitter.listeners.get(name);
        if (!observers) return;
        let length = observers.length;
        for (let i = 0; i < length; i++) {
            let observer = observers[i];
            if (observer.compar(context)) {
                observers.splice(i, 1);
                break;
            }
        }
        if (observers.length == 0) {
            TopSDKEmitter.listeners.delete(name);
        }
    }

    public static fire(name: string, ...args: any[]) {
        let observers: TopSDKObserver[] = TopSDKEmitter.listeners.get(name);
        if (!observers) return;
        let length = observers.length;
        for (let i = 0; i < length; i++) {
            let observer = observers[i];
            observer.notify(name, ...args);
        }
    }
}

class TopSDKObserver {
    private callback: Function = () => { };
    private context: any = null;

    constructor(callback: Function, context: any) {
        let self = this;
        self.callback = callback;
        self.context = context;
    }

    notify(...args: any[]): void {
        let self = this;
        self.callback.call(self.context, ...args);
    }

    compar(context: any): boolean {
        return context == this.context;
    }
}

class TopSDKEvents {
    private static _instance: TopSDKEvents;

    public static getInatance() {
        if (this._instance == null) {
            this._instance = new TopSDKEvents();
        }
        return this._instance;
    }

    public constructor() { }

    EmitInitSuccessEvent() {
        console.log("初始化成功");
        TopSDKEmitter.fire("EmitInitSuccessEvent");
    }

    EmitInitFailedEvent() {
        console.log("初始化失败");
        TopSDKEmitter.fire("EmitInitFailedEvent");
    }

    EmitLoginSuccessEvent(userInfo: string) {
        var userInfoDic = decodeURIComponent(userInfo);
        console.log("登录成功:" + userInfoDic);
        TopSDKEmitter.fire("EmitLoginSuccessEvent", userInfoDic);
    }

    EmitLoginFailedEvent(errorInfo: string) {
        var errorInfoDic = decodeURIComponent(errorInfo);
        console.log("登录失败:" + errorInfoDic);
        TopSDKEmitter.fire("EmitLoginFailedEvent", errorInfoDic);
    }

    EmitPaySuccessEvent(payInfo: string) {
        var payInfoDic = decodeURIComponent(payInfo);
        console.log("支付成功:" + payInfoDic);
        TopSDKEmitter.fire("EmitPaySuccessEvent", payInfoDic);
    }

    EmitPayFailedEvent(errorInfo: string) {
        var errorInfoDic = decodeURIComponent(errorInfo);
        console.log("支付失败:" + errorInfoDic);
        TopSDKEmitter.fire("EmitPayFailedEvent", errorInfoDic);
    }

    EmitLogoutSuccessEvent() {
        console.log("登出成功");
        TopSDKEmitter.fire("EmitLogoutSuccessEvent");
    }

    EmitUserInfoSuccessEvent(userInfo: string) {
        var userInfoDic = decodeURIComponent(userInfo);
        console.log("获取用户信息成功:" + userInfoDic);
        TopSDKEmitter.fire("EmitUserInfoSuccessEvent", userInfoDic);
    }

    EmitUserInfoFailedEvent(errorInfo: string) {
        var errorInfoDic = decodeURIComponent(errorInfo);
        console.log("获取用户信息失败:" + errorInfoDic);
        TopSDKEmitter.fire("EmitUserInfoFailedEvent", errorInfoDic);
    }

    EmitUserBindInfoSuccessEvent(bindInfo: string) {
        var bindInfoList = decodeURIComponent(bindInfo);
        console.log("获取用户绑定信息成功:" + bindInfoList);
        TopSDKEmitter.fire("EmitUserBindInfoSuccessEvent", bindInfoList);
    }

    EmitUserBindInfoFailedEvent(errorInfo: string) {
        var errorInfoDic = decodeURIComponent(errorInfo);
        console.log("获取用户绑定信息失败:" + errorInfoDic);
        TopSDKEmitter.fire("EmitUserBindInfoFailedEvent", errorInfoDic);
    }

    EmitBindSuccessEvent(bindInfo: string) {
        var bindInfoDic = decodeURIComponent(bindInfo);
        console.log("绑定成功:" + bindInfoDic);
        TopSDKEmitter.fire("EmitBindSuccessEvent", bindInfoDic);
    }

    EmitBindFailedEvent(errorInfo: string) {
        var errorInfoDic = decodeURIComponent(errorInfo);
        console.log("绑定失败:" + errorInfoDic);
        TopSDKEmitter.fire("EmitBindFailedEvent", errorInfoDic);
    }
}

// export default new TopSDK();
(<any>window)["TopSDK"] = TopSDKEvents.getInatance();