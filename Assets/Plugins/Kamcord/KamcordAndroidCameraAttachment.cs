using UnityEngine;
using System.Collections;

public class KamcordAndroidCameraAttachment : MonoBehaviour {
#if UNITY_ANDROID && !(UNITY_2_6 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_1)
    void Awake()
    {
        // Disable the pre- and postrender camera.
        KamcordImplementationAndroid.SetRenderCameraEnabled("Pre", false);
        KamcordImplementationAndroid.SetRenderCameraEnabled("Post", false);
    }

    void OnDestroy()
    {
        // Re-enable the pre- and postrender camera.
        KamcordImplementationAndroid.SetRenderCameraEnabled("Pre", true);
        KamcordImplementationAndroid.SetRenderCameraEnabled("Post", true);
    }

    void OnPreRender() {
        Kamcord.BeginDraw();
    }

    void OnPostRender() {
        Kamcord.EndDraw();
    }
#endif
}

