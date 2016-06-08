using UnityEngine;
using System.Collections;
using System;

public class plugin : MonoBehaviour
{
    float RValue;
    // Use this for initialization
    void Start()
    {
        AndroidJNI.AttachCurrentThread();
    }

    void Update()
    {
        using (AndroidJavaClass javaClass = new AndroidJavaClass("com.example.jurek.hearthratelib.MainActivity"))
        {
            using (AndroidJavaObject activity = javaClass.GetStatic<AndroidJavaObject>("mContext"))
            {
                RValue = activity.CallStatic<float>("getRate");
            }
        }

        Debug.Log("rate " + RValue.ToString());
    }

    private GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontStyle = FontStyle.Italic;
        GUI.depth = 0;
        GUI.Label(new Rect(Screen.width * 0.37f, Screen.height * 0.08f, Screen.width * 0.5f, 50), "Your Heart Rate is = " + RValue.ToString(), guiStyle);
    }
}