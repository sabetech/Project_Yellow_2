using UnityEngine;
using System.Collections;

public class CaptureFrame : MonoBehaviour
{
    // Attach this script onto your HUD camera to enable HUD-less recording.
#if UNITY_IPHONE || (UNITY_ANDROID && ((!(UNITY_2_6 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_1))))
    void OnPreRender()
    {
        Kamcord.CaptureFrame();
    }
#endif
}
