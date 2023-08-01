import { sys } from "cc";

export enum TOPPlatformType {
  APPLE = "APPLE",
  FACEBOOK = "FACEBOOK",
  GOOGLE = "GOOGLE",
  SNAPCHAT = "SNAPCHAT",
  LINE = "LINE",
  NAVER = "NAVER",
  KAKAO = "KAKAO",
  TWITTER = "TWITTER",
  TIKTOK = "TIKTOK"
}

export enum TOPDataChannelType {
  ALL = 0,
  APPSFLYER = 1,
  FIREBASE = 2,
  ADJUST = 3,
}

export class TOPPayment {
  productId: string; //必传
  amount: number;
  productName: string;
  developerPayload: string;
  constructor(
    productId: string,
    productName: string,
    amount: number,
    developerPayload: string
  ) {
    this.productId = productId;
    this.amount = amount;
    this.productName = productName;
    this.developerPayload = developerPayload;
  }

  toJsonStr() {
    let jsonObj = {
      productId: this.productId ? this.productId : "",
      productName: this.productName ? this.productName : "",
      amount: this.amount,
      developerPayload: this.developerPayload ? this.developerPayload : "",
    };

    return JSON.stringify(jsonObj);
  }
}

export class TOPRoleInfo {
  roleId: string;
  roleName: string;
  roleLevel: string;
  serverId: string;
  serverName: string;
  vipLevel: string;

  constructor(
    roleId: string,
    roleName: string,
    roleLevel: string,
    serverId: string,
    serverName: string,
    vipLevel: string
  ) {
    this.roleId = roleId;
    this.roleName = roleName;
    this.roleLevel = roleLevel;
    this.serverId = serverId;
    this.serverName = serverName;
    this.vipLevel = vipLevel;
  }

  toJsonStr() {
    let jsonObj = {
      roleId: this.roleId ? this.roleId : "",
      roleName: this.roleName ? this.roleName : "",
      roleLevel: this.roleLevel ? this.roleLevel : "",
      serverId: this.serverId ? this.serverId : "",
      serverName: this.serverName ? this.serverName : "",
      vipLevel: this.vipLevel ? this.vipLevel : "",
    };

    return JSON.stringify(jsonObj);
  }
}

export class TOPPurchaseData {
  currency: string;
  revenue: number;
  quantity: number;
  productId: string;
  orderId: string;
  receiptId: string;

  constructor(
    revenue: number,
    currency: string,
    quantity: number,
    productId: string,
    orderId: string,
    receiptId: string
  ) {
    this.revenue = revenue;
    this.currency = currency;
    this.quantity = quantity;
    this.productId = productId;
    this.orderId = orderId;
    this.receiptId = receiptId;
  }

  toJsonStr() {
    let jsonObj = {
      revenue: this.revenue,
      currency: this.currency ? this.currency : "",
      quantity: this.quantity,
      productId: this.productId ? this.productId : "",
      orderId: this.orderId ? this.orderId : "",
      receiptId: this.receiptId ? this.receiptId : "",
    };

    return JSON.stringify(jsonObj);
  }
}

export class TopSDK {
  static androidClass: string = "com/sino/topsdk/cocos/TOPCocosPlugin";
  static getVersion() {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        return jsb.reflection.callStaticMethod(
          "TOPSDKCocosPlugin",
          "getVersion"
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "getVersion",
          "()Ljava/lang/String;"
        );
      }
    }
  }

  static init(appid: string) {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPSDKCocosPlugin",
          "initWithAppid:",
          appid
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "init",
          "(Ljava/lang/String;)V",
          appid
        );
      } else {
      }
    }
  }

  static login() {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "login");
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "login",
          "()V"
        );
      }
    }
  }

  static logout() {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "logout");
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "logout",
          "()V"
        );
      }
    }
  }

  static bindPlatform(platform: TOPPlatformType) {
    console.log("开始绑定:" + platform);
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPSDKCocosPlugin",
          "bindPlatform:",
          platform
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "bindPlatform",
          "(Ljava/lang/String;)V",
          platform
        );
      }
    }
  }

  static enterUserCenter() {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "enterUserCenter");
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "enterUserCenter",
          "()V"
        );
      } else {
      }
    }
  }

  static getUserInfo() {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "getUserInfo");
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "getUserInfo",
          "()V"
        );
      }
    }
  }

  static getUserBindInfo() {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod("TOPSDKCocosPlugin", "getUserBindInfo");
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "getUserBindInfo",
          "()V"
        );
      }
    }
  }
  static querySkuDetailsOut(productIds: Array<String>) {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "getUserBindInfo",
          "(Ljava/lang/String;Ljava/lang/String;)V",
          JSON.stringify(productIds)
        );
      }
    }
  }
  static pay(payment: TOPPayment, roleInfo: TOPRoleInfo) {
    if (!payment || !roleInfo) {
      return;
    }
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPSDKCocosPlugin",
          "payWithPayment:roleInfo:",
          payment.toJsonStr(),
          roleInfo.toJsonStr()
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "pay",
          "(Ljava/lang/String;Ljava/lang/String;)V",
          payment.toJsonStr(),
          roleInfo.toJsonStr()
        );
      }
    }
  }

  static disableCollectIDFA(isDisable: boolean) {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "disableCollectIDFA:",
          isDisable
        );
      }
    }
  }

  static loginEvent(method: string) {
    if (!method) {
      return;
    }
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "loginEventWithMethod:",
          method
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "loginEvent",
          "(Ljava/lang/String;)V",
          method
        );
      }
    }
  }

  static signupEvent(method: string) {
    if (!method) {
      return;
    }
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "signupEventWithMethod:",
          method
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "signupEvent",
          "(Ljava/lang/String;)V",
          method
        );
      }
    }
  }

  static purchaseEvent(purchase: TOPPurchaseData) {
    if (!purchase) {
      return;
    }
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "purchaseEventWithParams:",
          purchase.toJsonStr()
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "purchaseEvent",
          "(Ljava/lang/String;)V",
          purchase.toJsonStr()
        );
      }
    }
  }

  static tutorialBeginEvent() {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "tutorialBeginEvent"
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "tutorialBeginEvent",
          "()V"
        );
      }
    }
  }

  static tutorialCompleteEvent(tutorialName: string, success: boolean) {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "tutorialCompleteEventWithTutorialName:success:",
          tutorialName,
          success
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "tutorialCompleteEvent",
          "(Ljava/lang/String;Z)V",
          tutorialName,
          success
        );
      }
    }
  }

  static levelUpEvent(level: number, roleName: string) {
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "levelUpEventWithLevel:roleName:",
          level,
          roleName ? roleName : ""
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "levelUpEvent",
          "(ILjava/lang/String;)V",
          level,
          roleName ? roleName : ""
        );
      }
    }
  }

  static unlockAchievementEvent(achievementId: string) {
    if (!achievementId) {
      return;
    }
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "unlockAchievementEventWithAchievementId:",
          achievementId
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "unlockAchievementEvent",
          "(Ljava/lang/String;)V",
          achievementId
        );
      }
    }
  }

  static shareEvent(method: string, contentType: string, contentId: string) {
    if (!method || !contentType || !contentId) {
      return;
    }
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "shareEventWithMethod:contentType:contentId:",
          method,
          contentType,
          contentId
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "shareEvent",
          "(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V",
          method,
          contentType,
          contentId
        );
      }
    }
  }

  static earnVirtualCurrencyEvent(name: string, count: number) {
    if (!name) {
      return;
    }
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "earnVirtualCurrencyEventWithName:count:",
          name,
          count
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "earnVirtualCurrencyEvent",
          "(Ljava/lang/String;I)V",
          name,
          count
        );
      }
    }
  }

  static spendVirtualCurrencyEvent(
    name: string,
    count: number,
    goodsName: string
  ) {
    if (!name || !goodsName) {
      return;
    }
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "earnVirtualCurrencyEventWithName:count:goodsName:",
          name,
          count,
          goodsName
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "spendVirtualCurrencyEvent",
          "(Ljava/lang/String;ILjava/lang/String;)V",
          name,
          count,
          goodsName
        );
      }
    }
  }

  static levelStartEvent(levelName: string) {
    if (!levelName) {
      return;
    }
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "levelStartEventWithLevelName:",
          levelName
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "levelStartEvent",
          "(Ljava/lang/String;)V",
          levelName
        );
      }
    }
  }

  static levelEndEventWithLevelName(levelName: string, success: boolean) {
    if (!levelName) {
      return;
    }
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "levelEndEventWithLevelName:success:",
          levelName,
          success
        );
      } else if (sys.OS_ANDROID == sys.os) {
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "levelEndEvent",
          "(Ljava/lang/String;Z)V",
          levelName,
          success
        );
      }
    }
  }

  static reportEvent(
    eventName: string,
    params: Map<string, string>,
    channelType: TOPDataChannelType
  ) {
    if (!eventName) {
      return;
    }
    var paramsJsonStr = "";
    if (params) {
      paramsJsonStr = JSON.stringify(Array.from(params.entries()));
    }
    console.log("reportEvent==" + paramsJsonStr);
    if (sys.isNative) {
      if (sys.OS_IOS == sys.os) {
        jsb.reflection.callStaticMethod(
          "TOPDataCocosPlugin",
          "reportEvent:params:channelType:",
          eventName,
          paramsJsonStr,
          channelType
        );
      } else if (sys.OS_ANDROID == sys.os) {
        var channelString = "All";
        if (channelType == TOPDataChannelType.ALL) {
          channelString = "All";
        } else if (channelType == TOPDataChannelType.APPSFLYER) {
          channelString = "Appsflyer";
        } else if (channelType == TOPDataChannelType.FIREBASE) {
          channelString = "Firebase";
        } else if (channelType == TOPDataChannelType.ADJUST) {
          channelString = "Adjust";
        }
        return jsb.reflection.callStaticMethod(
          this.androidClass,
          "report",
          "(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V",
          eventName,
          paramsJsonStr,
          channelString
        );
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
    TopSDKEmitter.listeners
      .get(name)
      .push(new TopSDKObserver(callback, context));
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
  private callback: Function = () => {};
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

  public constructor() {}

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

  EmitPayQuerySuccessEvent(productInfo: string) {
    var productInfoDic = decodeURIComponent(productInfo);
    console.log("获取商品信息成功:" + productInfoDic);
    TopSDKEmitter.fire("EmitPayQuerySuccessEvent", productInfoDic);
  }

  EmitPayQueryFailedEvent(errorInfo: string) {
    var errorInfoDic = decodeURIComponent(errorInfo);
    console.log("获取商品信息失败:" + errorInfoDic);
    TopSDKEmitter.fire("EmitPayQueryFailedEvent", errorInfoDic);
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
