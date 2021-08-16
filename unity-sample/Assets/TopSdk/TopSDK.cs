
public class TopSDK :
// Choose base class based on target platform...
#if UNITY_ANDROID
    TopSDKAndroid
#else
    TopSDKiOS
#endif
{
    private static string _sdkName;

    public static string SdkName
    {
        get { return _sdkName ?? (_sdkName = GetSDKVersion().Replace("+unity", "")); }
    }
}
