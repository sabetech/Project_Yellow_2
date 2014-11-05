using UnityEngine;

/*
 *
 * You only need to attach this class to your AudioListener's game object
 * if you programmatically create an AudioListener. Othewise, Kamcord
 * will automatically bind to FMOD and record the audio byte stream.
 *
 */
public class KamcordAudioRecorder : MonoBehaviour
{
    #if UNITY_IPHONE || (UNITY_ANDROID && ((!(UNITY_2_6 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_1))))
    void OnAudioFilterRead(float [] data, int numChannels)
    {
        if (Kamcord.IsRecording())
        {
            Kamcord.WriteAudioData(data, data.Length / numChannels);
        }
    }
    #endif
}
