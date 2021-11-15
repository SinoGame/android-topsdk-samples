package com.sino.customerservice;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.Process;
import android.view.View;

import androidx.appcompat.app.AppCompatActivity;

import com.sino.basic.AnalyticsManager;
import com.sino.customerservice.databinding.ActivityMainBinding;
import com.sino.topsdk.customer.service.CustomerServiceManager;
import com.sino.topsdk.customer.service.bean.TOPCustomerServiceInfo;

public class MainActivity extends AppCompatActivity implements View.OnClickListener {
    private ActivityMainBinding binding;
    private SharedPreferences sp;
    private String appId, roleId, roleName, extraJson;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());
        binding.btnCv.setOnClickListener(this);
        binding.btnCvNullInfo.setOnClickListener(this);
        initSdk();
    }

    private void initSdk() {
        sp = getSharedPreferences("cs_", Context.MODE_PRIVATE);
        appId = sp.getString("appId", "132168550394630144");
        roleId = sp.getString("roleId", "1121541");
        roleName = sp.getString("roleName", "角色名");
        extraJson = sp.getString("extraJson", "{\"level\":\"1000\"}");
        binding.etAppId.setText(appId);
        binding.etRoleId.setText(roleId);
        binding.etRoleName.setText(roleName);
        binding.etExtra.setText(extraJson);
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.btn_cv:
                appId = binding.etAppId.getText().toString().trim();
                roleId = binding.etRoleId.getText().toString().trim();
                roleName = binding.etRoleName.getText().toString().trim();
                extraJson = binding.etExtra.getText().toString().trim();
                SharedPreferences.Editor editor = sp.edit();
                editor.putString("appId", appId);
                editor.putString("roleId", roleId);
                editor.putString("roleName", roleName);
                editor.putString("extraJson", extraJson);
                editor.apply();
                CustomerServiceManager.init(this, appId);
                AnalyticsManager.getInstance().enableNetworkLogging(true);
                TOPCustomerServiceInfo info = new TOPCustomerServiceInfo();
                info.setRoleId(roleId);
                info.setRoleName(roleName);
                info.setExtraJson(extraJson);
                CustomerServiceManager.enter(this, info);
                break;
            case R.id.btn_cv_null_info:
                appId = binding.etAppId.getText().toString().trim();
                CustomerServiceManager.init(this, appId);
                AnalyticsManager.getInstance().enableNetworkLogging(true);
                CustomerServiceManager.enter(this, null);
                break;
            default:
                break;
        }
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        Process.killProcess(Process.myPid());
    }
}