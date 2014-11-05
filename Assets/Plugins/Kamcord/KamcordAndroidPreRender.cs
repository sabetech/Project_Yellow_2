using UnityEngine;
using System.Collections;

public class KamcordAndroidPreRender : MonoBehaviour {
#if UNITY_ANDROID && ((!(UNITY_2_6 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_1)))
    void OnPreRender() {
        Kamcord.BeginDraw();
    }
#endif
}
