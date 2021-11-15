package com.sino.PowerRaid;


import android.annotation.SuppressLint;
import android.app.Activity;
import android.graphics.Paint;
import android.os.Bundle;
import android.os.Process;
import android.view.View;

import androidx.appcompat.app.AlertDialog;

import com.sino.PowerRaid.databinding.ActivityGameBinding;
import com.sino.topsdk.api.bean.TOPBindData;
import com.sino.topsdk.api.bean.TOPUserInfo;
import com.sino.topsdk.api.listener.TOPBindCallback;
import com.sino.topsdk.core.bean.TOPError;
import com.sino.topsdk.core.bean.TOPPayParameters;
import com.sino.topsdk.core.bean.TOPPaymentData;
import com.sino.topsdk.core.bean.TOPRoleInfo;
import com.sino.topsdk.core.enums.PlatformTypeEnum;
import com.sino.topsdk.core.listener.TOPCallback;
import com.sino.topsdk.sdk.TOPSdkManager;


public class GameActivity extends Activity implements View.OnClickListener {

    private TOPSdkManager gameSDKManager;
    private ActivityGameBinding binding;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityGameBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());
        binding.btnLogin.setOnClickListener(this);
        binding.btnUserCenter.setOnClickListener(this);
        binding.btnInappOne.setOnClickListener(this);
        binding.btnInappTwo.setOnClickListener(this);
        binding.btnBindGoogle.setOnClickListener(this);
        binding.btnLogout.setOnClickListener(this);
        initSdk();
    }


    private void initSdk() {
        gameSDKManager = TOPSdkManager.getInstance();
        binding.tvVersion.setText(String.format("SDK v%s 演示DEMO", gameSDKManager.getSDKVersion()));
        String channel = gameSDKManager.getChannel();
        binding.tvChannel.setText("渠道：" + channel);
        binding.tvChannel.getPaint().setFlags(Paint.UNDERLINE_TEXT_FLAG);//下划线
        binding.tvChannel.getPaint().setAntiAlias(true);//抗锯齿
        gameSDKManager.registerLoginCallback(loginCallback);
        gameSDKManager.registerLogoutCallback(logoutCallback);
        gameSDKManager.registerBindStatusCallback(bindStatusCallback);
    }

    private TOPBindCallback bindStatusCallback = new TOPBindCallback() {
        @Override
        public void onSuccess(PlatformTypeEnum platformType, TOPBindData bindData) {
            //PlatformType:平台类型
            //bindStatus 0:未绑定/解绑 1:已绑定/绑定
            int bindStatus = bindData.getBindStatus();
            //平台信息
            String platform = bindData.getPlatform();
            showDialog("绑定成功", "绑定类型：" + platformType);
        }

        @Override
        public void onFailed(PlatformTypeEnum platformType, TOPError topError) {
            //PlatformType:平台类型
            //失败 ErrorResults 错误信息
            showDialog("绑定失败", "绑定类型：" + platformType + "\ncode：" + topError.getCode() + "\nmessage：" + topError.getMessage());
        }

    };

    private TOPCallback<TOPUserInfo> loginCallback = new TOPCallback<TOPUserInfo>() {


        @Override
        public void onSuccess(TOPUserInfo userData) {
            //用户id
            String uid = userData.getId();
            //用户名
            String userName = userData.getName();
            //用户token
            String token = userData.getToken();
            binding.tvMsg.setText("登录状态：已登录");
            showDialog("登录成功", "用户名：" + userName + "\n用户id：" + uid);
        }

        @Override
        public void onFailed(TOPError topError) {
            //登录失败 ErrorResults 错误信息
            binding.tvMsg.setText("登录状态：登录失败");
            showDialog("登录失败", "code：" + topError.getCode() + "\nmessage：" + topError.getMessage());
        }
    };

    private TOPCallback<Boolean> logoutCallback = new TOPCallback<Boolean>() {
        @Override
        public void onSuccess(Boolean success) {
            //退出登录成功
            binding.tvMsg.setText("登录状态：未登录");
            showDialog("退出登录", "退出登录成功");
        }

        @Override
        public void onFailed(TOPError topError) {
            //退出登录失败 ErrorResults 错误信息
            showDialog("退出登录失败", "code：" + topError.getCode() + "\nmessage：" + topError.getMessage());
        }
    };

    private void login() {
        gameSDKManager.login(this);
    }

    private TOPCallback<TOPPaymentData> gpCallback = new TOPCallback<TOPPaymentData>() {

        @Override
        public void onSuccess(TOPPaymentData paymentData) {
            showDialog("支付成功", "商品ID：" + paymentData.getProductId() + "\nSDK订单号：" + paymentData.getOrderNo() + "\n三方订单号：" + paymentData.getPayPlatformOrderNo());
        }

        @Override
        public void onFailed(TOPError topError) {
            showDialog("支付失败", "code：" + topError.getCode() + "\nmessage：" + topError.getMessage());
        }
    };

    @SuppressLint("NonConstantResourceId")
    @Override
    public void onClick(View v) {
        TOPRoleInfo roleInfo = new TOPRoleInfo();
        roleInfo.setRoleId("角色ID");
        roleInfo.setRoleName("角色名称");
        roleInfo.setRoleLevel("角色等级");
        roleInfo.setServerName("服务器名称");
        roleInfo.setVipLevel("vip等级");
        switch (v.getId()) {
            case R.id.btn_login:
                login();
                break;
            case R.id.btn_user_center:
                gameSDKManager.enterUserCenter(this);
                break;
            case R.id.btn_inapp_one:
                TOPPayParameters productInfo = new TOPPayParameters("item_01", "王者荣耀", 0.9999);
                gameSDKManager.pay(this, productInfo, roleInfo, gpCallback);
                break;
            case R.id.btn_inapp_two:
                TOPPayParameters productInfo1 = new TOPPayParameters("item_02", "王者荣耀", 0.99);
                gameSDKManager.pay(this, productInfo1, roleInfo, gpCallback);
                break;
            case R.id.btn_bind_google:
                gameSDKManager.bindPlatform(this, PlatformTypeEnum.GOOGLE);
                break;
            case R.id.btn_logout:
                gameSDKManager.logout(this);
                break;
            default:
                break;
        }

    }

    @Override
    protected void onDestroy() {
        if (gameSDKManager != null) {
            gameSDKManager.unregisterLoginCallback();
            gameSDKManager.unregisterLogoutCallback();
            gameSDKManager.unregisterBindStatusCallback();
            gameSDKManager.onDestroy(this);
        }
        super.onDestroy();
        Process.killProcess(Process.myPid());
    }

    @Override
    protected void onPause() {
        super.onPause();
    }

    @Override
    protected void onStop() {
        super.onStop();
    }

    @Override
    protected void onResume() {
        super.onResume();
    }

    @Override
    protected void onRestart() {
        super.onRestart();
    }

    private void showDialog(String title, String message) {
        new AlertDialog.Builder(this)
                .setTitle(title)
                .setMessage(message)
                .setNegativeButton("确定", null).show();
    }


}
