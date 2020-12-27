using UnityEngine;

public class NativeToast
{
    private string _toastString;
    AndroidJavaObject _currentActivity;

    public void MyShowToastMethod(string message)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            ShowToastOnUiThread(message);
        }
    }

    private void ShowToastOnUiThread(string toastString)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        _currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        _toastString = toastString;

        _currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(ShowToast));
    }

    private void ShowToast()
    {
        Debug.Log("Running on UI thread");
        AndroidJavaObject context = _currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaClass toastWidget = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", _toastString);
        AndroidJavaObject toast = toastWidget.CallStatic<AndroidJavaObject>("makeText", context, javaString,
            toastWidget.GetStatic<int>("LENGTH_SHORT"));
        toast.Call("show");
    }
}