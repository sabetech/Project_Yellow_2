#define KAMCORD_ANDROID

#if UNITY_ANDROID && !(UNITY_2_6 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_1)

using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using KamcordJSON;


public class KamcordImplementationAndroid : Kamcord.Implementation
{
    private AndroidJavaClass kamcordJavaClass_;

    private AndroidJavaClass kamcordJavaClass()
    {
        if ( kamcordJavaClass_ == null )
            kamcordJavaClass_ = new AndroidJavaClass ("com.kamcord.android.Kamcord");

        if ( kamcordJavaClass_ == null )
        {
            Debug.Log("Kamcord: Unable to find Kamcord java class.");
        }

        return kamcordJavaClass_;
    }

    public override void SetLoggingEnabled(bool value)
    {
        if (kamcordJavaClass() != null)
            kamcordJavaClass().CallStatic("setLoggingEnabled", value);
    }

    public override bool IsEnabled()
    {
        if (kamcordJavaClass() != null)
            return kamcordJavaClass().CallStatic<bool>("isEnabled");

        return false;
    }

    public override String GetDisabledReason()
    {
        if (kamcordJavaClass() != null)
            return kamcordJavaClass().CallStatic<string>("getDisabledReason");

        return "Kamcord java class object not accessible from Unity script.";
    }

    public override void WhitelistBoard(string boardName)
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: WhitelistBoard called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("whitelistBoard", boardName);
    }

    public override void BlacklistBoard(string boardName)
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: BlacklistBoard called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("blacklistBoard", boardName);
    }

    public override void WhitelistDevice(string deviceName)
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: WhitelistDevice called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("whitelistDevice", deviceName);
    }

    public override void BlacklistDevice(string deviceName)
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: BlacklistDevice called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("blacklistDevice", deviceName);
    }

    public override void WhitelistBoard(string boardName, int sdkVersion)
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: WhitelistBoard called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("whitelistBoard", boardName, sdkVersion);
    }

    public override void BlacklistBoard(string boardName, int sdkVersion)
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: BlacklistBoard called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("blacklistBoard", boardName, sdkVersion);
    }

    public override void WhitelistDevice(string deviceName, int sdkVersion)
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: WhitelistDevice called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("whitelistDevice", deviceName, sdkVersion);
    }

    public override void BlacklistDevice(string deviceName, int sdkVersion)
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: BlacklistDevice called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("blacklistDevice", deviceName, sdkVersion);
    }

    public override void WhitelistAllBoards()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: WhitelistAllBoards called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("whitelistAllBoards");
    }

    public override void BlacklistAllBoards()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: BlacklistAllBoards called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("blacklistAllBoards");
    }

    public override void WhitelistAll()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: WhitelistAll called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("whitelistAll");
    }

    public override void BlacklistAll()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: BlacklistAll called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("blacklistAll");
    }

    public override string GetBoard()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: GetBoard called with no kamcordJavaClass");
            return "";
        }

        return kamcordJavaClass().CallStatic<string>("getBoard");
    }

    public override string GetDevice()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: GetDevice called with no kamcordJavaClass");
            return "";
        }

        return kamcordJavaClass().CallStatic<string>("getDevice");
    }

    // Deprecated.
    public override bool IsWhitelisted(string boardName)
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: IsWhitelisted called with no kamcordJavaClass");
            return false;
        }

        return kamcordJavaClass().CallStatic<bool>("isWhitelisted", boardName);
    }

    public override bool IsWhitelisted()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: IsWhitelisted called with no kamcordJavaClass");
            return false;
        }

        return kamcordJavaClass().CallStatic<bool>("isWhitelisted");
    }

    public override void DoneChangingWhitelist()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: DoneChangingWhitelist called with no kamcordJavaClass");
            return;
        }

        kamcordJavaClass().CallStatic("doneChangingWhitelist");
    }

    //////////////////////////////////////////////////////////////////
    /// Share settings
    ///

    public override void SetVideoTitle(string title)
    {
        kamcordJavaClass().CallStatic("setDefaultVideoTitle", title);
    }

    public override void SetYouTubeSettings (string description, string tags)
    {
        kamcordJavaClass().CallStatic("setDefaultYoutubeDescription", description);
        kamcordJavaClass().CallStatic("setDefaultYoutubeKeywords", tags);
    }


    public override void SetFacebookDescription(string facebookDescription)
    {
        kamcordJavaClass().CallStatic("setDefaultFacebookDescription", facebookDescription);
    }

    public override void SetDefaultTweet(string tweet)
    {
        kamcordJavaClass().CallStatic("setDefaultTweet", tweet);
    }

    public override void SetTwitterDescription(string twitterDescription)
    {
        kamcordJavaClass().CallStatic("setDefaultTwitterDescription", twitterDescription);
    }

    public override void SetDefaultEmailSubject(string subject)
    {
        kamcordJavaClass().CallStatic("setDefaultEmailSubject", subject);
    }

    public override void SetDefaultEmailBody(string body)
    {
        kamcordJavaClass().CallStatic("setDefaultEmailBody", body);
    }

    public override void SetShareTargets(
            Kamcord.ShareTarget target1,
            Kamcord.ShareTarget target2,
            Kamcord.ShareTarget target3,
            Kamcord.ShareTarget target4)
    {
        kamcordJavaClass().CallStatic("setShareTargets", new int[4] {(int) target1, (int) target2, (int) target3, (int) target4});
    }

    public override void SetVideoMetadata(Dictionary <string, object> metadata)
    {
        if (metadata != null && metadata.Count > 0)
        {
            kamcordJavaClass().CallStatic("setVideoMetadata", Json.Serialize(metadata));
        }
    }

    public override string Version()
    {
        return kamcordJavaClass().CallStatic<string>("version");
    }

    public override void SetLevelAndScore (string level, double score)
    {
        kamcordJavaClass().CallStatic("setLevel", level);
        kamcordJavaClass().CallStatic("setScore", score);
    }

    public override void SetDeveloperMetadata (
            Kamcord.MetadataType metadataType,
            string displayKey,
            string displayValue)
    {
        kamcordJavaClass().CallStatic("setDeveloperMetadata", (int) metadataType, displayKey, displayValue);
    }

    public override void SetDeveloperMetadataWithNumericValue (
            Kamcord.MetadataType metadataType,
            string displayKey,
            string displayValue,
            double numericValue)
    {
        kamcordJavaClass().CallStatic("setDeveloperMetadataWithNumericValue", (int) metadataType, displayKey, displayValue, numericValue);
    }


    //////////////////////////////////////////////////////////////////
    /// Video Recording
    ///

    private static bool beginDrawErrorOnce = false;

    private bool frameCaptured;

    public override void BeginDraw ()
    {
        frameCaptured = false;

        if (kamcordJavaClass() == null)
        {
            if(!beginDrawErrorOnce)
            {
                Debug.Log ("Kamcord: BeginDraw called with no kamcordJavaClass().  This error only prints once even if it happens more.");
                beginDrawErrorOnce = true;
            }
            return;
        }

        kamcordJavaClass().CallStatic("beginDraw");
    }

    private static bool endDrawErrorOnce = false;

    public override void EndDraw ()
    {
        if (kamcordJavaClass() == null)
        {
            if(!endDrawErrorOnce)
            {
                Debug.Log ("Kamcord: EndDraw called with no kamcordJavaClass().  This error only prints once even if it happens more.");
                endDrawErrorOnce = true;
            }
            return;
        }

        if (frameCaptured) // If they called CaptureFrame, CaptureFrame calls EndDraw, no need to call it again.
            return;

        kamcordJavaClass().CallStatic("endDraw");
    }

    public override void StartRecording ()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: StartRecording called with no kamcordJavaClass().");
            return;
        }

        kamcordJavaClass().CallStatic("startRecording");
    }

    public override void StopRecording ()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: StopRecording called with no kamcordJavaClass().");
            return;
        }

        kamcordJavaClass().CallStatic("stopRecording");
    }

    public override void Pause ()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: Pause called with no kamcordJavaClass().");
            return;
        }

        kamcordJavaClass().CallStatic("pauseRecording");
    }

    public override void Resume ()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: Resume called with no kamcordJavaClass().");
            return;
        }

        kamcordJavaClass().CallStatic("resumeRecording");
    }

    public override bool IsRecording ()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: IsRecording called with no kamcordJavaClass().");
            return false;
        }

        return kamcordJavaClass().CallStatic<bool>("isRecording");
    }

    public override bool IsPaused ()
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: isPaused called with no kamcordJavaClass().");
            return false;
        }

        return kamcordJavaClass().CallStatic<bool>("isPaused");
    }

    public override void SetVideoQuality (Kamcord.VideoQuality quality)
    {
        kamcordJavaClass().CallStatic("setVideoQuality", quality);
    }

    public override bool VoiceOverlayEnabled ()
    {
        return kamcordJavaClass().CallStatic<bool>("voiceOverlayEnabled");
    }

    public override bool VoiceOverlayActivated ()
    {
        return kamcordJavaClass().CallStatic<bool>("voiceOverlayActivated");
    }

    public override void SetVoiceOverlayEnabled (bool enabled)
    {
        kamcordJavaClass().CallStatic("setVoiceOverlayEnabled", enabled);
    }

    public override void ActivateVoiceOverlay (bool activate)
    {
        kamcordJavaClass().CallStatic("setVoiceOverlayActivated", activate);
    }

    public override void SetVideoIncompleteWarningEnabled (bool enabled)
    {
        kamcordJavaClass().CallStatic("setVideoIncompleteWarningEnabled", enabled);
    }

    public override void TurnOffAutomaticAudioRecording (bool state)
    {
        kamcordJavaClass().CallStatic("setAutomaticAudioRecording", !state);
    }

    public override bool IsVideoComplete()
    {
        return kamcordJavaClass().CallStatic<bool>("isVideoComplete");
    }

    public override void CaptureFrame ()
    {
        EndDraw(); // EndDraw() returns early if frameCaptured is true.
        frameCaptured = true;
    }

    public override void ShowView ()
    {
        kamcordJavaClass().CallStatic("showView");
    }

    public override void ShowWatchView ()
    {
        kamcordJavaClass().CallStatic("showWatchView");
    }

    public override void Init (
        string devKey,
        string devSecret,
        string appName,
        Kamcord.VideoQuality videoQuality)
    {
        if (kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: Init called with no kamcordJavaClass().");
            return;
        }

        int n = 1;
        switch(videoQuality)
        {
            default:
            case Kamcord.VideoQuality.Standard:
                n = 0;
                break;
            case Kamcord.VideoQuality.Trailer:
                n = 1;
                break;
        }

        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activityJavaObject = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        kamcordJavaClass().CallStatic("initActivity", activityJavaObject);
        kamcordJavaClass().CallStatic("initKeyAndSecret", devKey, devSecret, appName, n);
    }

    public override void Awake(Kamcord kamcordInstance)
    {
        if(kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: Java class not accessible from C#.");
        }
        else
        {
            InitializeRenderCamera("Pre");
            InitializeRenderCamera("Post");
        }
    }

    public override void Start(Kamcord kamcordInstance)
    {
        Kamcord.CallAdjustAndroidWhitelist ();

        SetLoggingEnabled(Kamcord.loggingEnabled_);

        if(kamcordJavaClass() == null)
        {
            Debug.Log ("Kamcord: Java class not accessible from C#.");
        }
        else
        {
            Init(
                kamcordInstance.developerKey,
                kamcordInstance.developerSecret,
                kamcordInstance.appName,
                kamcordInstance.videoQuality);
        }
    }

    public override void SetAudioSettings(int sampleRate, int numChannels)
    {
        kamcordJavaClass().CallStatic("setAudioSettings", sampleRate, numChannels);
    }

    public override void WriteAudioData(float [] data, int length)
    {
        kamcordJavaClass().CallStatic("writeAudioData", data, length);
    }

    private void InitializeRenderCamera(string type)
    {
        if( type.Equals("Pre") || type.Equals("Post") )
        {
            // Set up the render camera, if it doesn't already exist.
            if( GameObject.Find("kamcord"+type+"Camera") == null )
            {
                GameObject cameraObject = new GameObject();
                Camera camera = (Camera) cameraObject.AddComponent<Camera>();
                camera.name = "kamcord"+type+"Camera";
                camera.clearFlags = CameraClearFlags.Nothing;
                camera.cullingMask = 0;

                if( type.Equals("Pre") )
                {
                    camera.depth = Single.MinValue;
                    camera.gameObject.AddComponent<KamcordAndroidPreRender>();
                }
                else // Post
                {
                    camera.depth = Single.MaxValue;
                    camera.gameObject.AddComponent<KamcordAndroidPostRender>();
                }

                // Keep this fella around.
                cameraObject.SetActive(true);
                UnityEngine.Object.DontDestroyOnLoad(cameraObject);
            }
        }
    }

    public static void SetRenderCameraEnabled(string type, bool flag)
    {
        if( type.Equals("Pre") || type.Equals("Post") )
        {
            GameObject renderCamera = GameObject.Find ("kamcord"+type+"Camera");
            if( renderCamera != null )
            {
                renderCamera.SetActive(flag);
            }
        }
    }

    public static int getSDKVersion()
    {
        AndroidJavaClass vJavaClass = new AndroidJavaClass ("android.os.Build$VERSION");
        return vJavaClass.GetStatic<int>("SDK_INT");
    }
}

#endif
