package com.sino.PowerRaid;

import android.widget.Toast;

import androidx.multidex.MultiDexApplication;

import com.sino.topsdk.core.bean.TOPError;
import com.sino.topsdk.core.listener.TOPCallback;
import com.sino.topsdk.sdk.TOPSdkManager;

/**
 * Package:        com.sino.gameplusdemo
 * ClassName:      App
 * Author:         Zorro
 * CreateDate:     2020/4/14 16:04
 * Description:    java类作用描述
 */
public class App extends MultiDexApplication {
    @Override
    public void onCreate() {
        super.onCreate();
        TOPSdkManager.getInstance().setDebugEnabled(true);
//        TOPSdkManager.getInstance().init(getApplicationContext(),  "173100807035879424",//onestore
        TOPSdkManager.getInstance().init(getApplicationContext(), "132168550394630144", //google
                new TOPCallback<Boolean>() {
            @Override
            public void onSuccess(Boolean aBoolean) {
                Toast.makeText(getApplicationContext(), "SDK初始化成功", Toast.LENGTH_SHORT).show();
            }

            @Override
            public void onFailed(TOPError topError) {
                Toast.makeText(getApplicationContext(), "SDK初始化失败", Toast.LENGTH_SHORT).show();
            }
        });
    }
}
