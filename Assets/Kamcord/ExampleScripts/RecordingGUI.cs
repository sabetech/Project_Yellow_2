using UnityEngine;
using System.Collections.Generic;

public class RecordingGUI : MonoBehaviour
{
    public Font buttonFont;

#if UNITY_IPHONE || (UNITY_ANDROID && ((!(UNITY_2_6 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_1))))
    private bool firstVideoRecorded;

    private Rect startRecordingButtonRect;
    private Rect stopRecordingButtonRect;
    private Rect showViewButtonRect;
    private Rect showWatchViewButtonRect;
    private System.DateTime recordingStartedAt;

    void Awake()
    {
        RegisterCallbacks();
    }

    void Start()
    {
        firstVideoRecorded = false;
        InitGUI();
    }

    private void InitGUI()
    {
        int w = 265; // button width
        int h = 100; // button height
        int m = 20;  // margin around buttons

        startRecordingButtonRect = new Rect(m, m, w, h);
        stopRecordingButtonRect = new Rect(m, 2*m+h, w, h);
        showViewButtonRect = new Rect(m, 3*m+2*h, w, h);
        showWatchViewButtonRect = new Rect(2*m+w, m, w, h);
    }

    private void RegisterCallbacks()
    {
        Kamcord.kamcordDidStartRecording += MyKamcordDidStartRecording;
        Kamcord.kamcordDidStopRecording += MyKamcordDidStopRecording;

        Kamcord.kamcordViewDidAppear += MyKamcordViewDidAppear;
        Kamcord.kamcordViewWillDisappear += MyKamcordViewWillDisappear;
        Kamcord.kamcordViewDidDisappear += MyKamcordViewDidDisappear;
        Kamcord.kamcordViewDidNotAppear += MyKamcordViewDidNotAppear;
        Kamcord.kamcordWatchViewDidAppear += MyKamcordWatchViewDidAppear;
        Kamcord.kamcordWatchViewWillDisappear += MyKamcordWatchViewWillDisappear;
        Kamcord.kamcordWatchViewDidDisappear += MyKamcordWatchViewDidDisappear;

        Kamcord.videoThumbnailReadyAtFilePath += MyVideoThumbnailReadyAtFilePath;
        Kamcord.shareButtonPressed += MyShareButtonPressed;

        Kamcord.videoWillBeginUploading += MyVideoWillBeginUploading;
        Kamcord.videoUploadProgressed += MyVideoUploadProgressed;
        Kamcord.videoFinishedUploading += MyVideoFinishedUploading;

        Kamcord.videoSharedToFacebook += MyVideoSharedToFacebook;
        Kamcord.videoSharedToTwitter += MyVideoSharedToTwitter;
        Kamcord.videoSharedToYoutube += MyVideoSharedToYoutube;

        Kamcord.snapshotReadyAtFilePath += MySnapshotReadyAtFilePath;
        Kamcord.pushNotifCallToActionButtonPressed += MyPushNotifCallToActionButtonPressed;

        Kamcord.adjustAndroidWhitelist += MyAdjustAndroidWhitelist;
        Kamcord.isEnabledChanged += MyIsEnabledChanged;
    }

    void OnDestroy()
    {
        UnregisterCallbacks();
    }

    private void UnregisterCallbacks()
    {
        Kamcord.kamcordDidStartRecording -= MyKamcordDidStartRecording;
        Kamcord.kamcordDidStopRecording -= MyKamcordDidStopRecording;

        Kamcord.kamcordViewDidAppear -= MyKamcordViewDidAppear;
        Kamcord.kamcordViewWillDisappear -= MyKamcordViewWillDisappear;
        Kamcord.kamcordViewDidDisappear -= MyKamcordViewDidDisappear;
        Kamcord.kamcordViewDidNotAppear -= MyKamcordViewDidNotAppear;

        Kamcord.kamcordWatchViewDidAppear -= MyKamcordWatchViewDidAppear;
        Kamcord.kamcordWatchViewWillDisappear -= MyKamcordWatchViewWillDisappear;
        Kamcord.kamcordWatchViewDidDisappear -= MyKamcordWatchViewDidDisappear;

        Kamcord.videoThumbnailReadyAtFilePath -= MyVideoThumbnailReadyAtFilePath;
        Kamcord.shareButtonPressed -= MyShareButtonPressed;

        Kamcord.videoWillBeginUploading -= MyVideoWillBeginUploading;
        Kamcord.videoUploadProgressed -= MyVideoUploadProgressed;
        Kamcord.videoFinishedUploading -= MyVideoFinishedUploading;

        Kamcord.videoSharedToFacebook -= MyVideoSharedToFacebook;
        Kamcord.videoSharedToTwitter -= MyVideoSharedToTwitter;
        Kamcord.videoSharedToYoutube -= MyVideoSharedToYoutube;

        Kamcord.snapshotReadyAtFilePath -= MySnapshotReadyAtFilePath;
        Kamcord.pushNotifCallToActionButtonPressed -= MyPushNotifCallToActionButtonPressed;

        Kamcord.adjustAndroidWhitelist -= MyAdjustAndroidWhitelist;
        Kamcord.isEnabledChanged -= MyIsEnabledChanged;
    }

    GUIStyle GetStyle()
    {
        return new GUIStyle(GUI.skin.button);
    }

    void OnGUI()
    {
        GUIStyle style = GetStyle();

        if ((Application.platform == RuntimePlatform.IPhonePlayer ||
             Application.platform == RuntimePlatform.Android))
        {
            if ( GUI.Button(showWatchViewButtonRect, "Show Watch View", style) )
            {
                Kamcord.ShowWatchView();
            }
        }

        if (!Kamcord.IsEnabled())
        {
            string reason = Kamcord.GetDisabledReason();
            GUI.Label(startRecordingButtonRect, "Kamcord Disabled:\n" + reason);
            return;
        }

        if (Kamcord.IsRecording() || Kamcord.IsPaused())
        {
            if (Kamcord.IsPaused())
            {
                if (GUI.Button(startRecordingButtonRect, "Resume", style))
                {
                    Kamcord.Resume();
                }
            }
            else
            {
                if (GUI.Button(startRecordingButtonRect, "Pause", style))
                {
                    Kamcord.Pause();
                }
            }

            if (GUI.Button(stopRecordingButtonRect, "Stop Recording", style))
            {
                Kamcord.StopRecording();

                // It is very important to set a descriptive video title for each video.
                // In addition to the video title, setting the level and score also makes
                // the watch experience significantly better.
                double gameplayDuration = (System.DateTime.Now - recordingStartedAt).TotalSeconds;
                Kamcord.SetVideoTitle("An Awesome Gameplay - " + gameplayDuration.ToString("F2") + " sec");

                Kamcord.SetDeveloperMetadata(Kamcord.MetadataType.level, "Level", "1");
                Kamcord.SetDeveloperMetadata(Kamcord.MetadataType.score, "Score", "1000");
                Kamcord.SetDeveloperMetadata(Kamcord.MetadataType.other, "Key", "Value");
                Kamcord.SetDeveloperMetadataWithNumericValue(Kamcord.MetadataType.other, "Key", "Value", 1000);

                firstVideoRecorded = true;
            }
        }
        else
        {
            if (GUI.Button(startRecordingButtonRect, "Start Recording", style))
            {
                Kamcord.StartRecording();

                recordingStartedAt = System.DateTime.Now;
            }

            if (firstVideoRecorded &&
                GUI.Button(showViewButtonRect, "Show Last Video", style))
            {
                Kamcord.ShowView();
            }
        }
    }

#if UNITY_IPHONE
    void Update()
    {
        foreach(LocalNotification notif in NotificationServices.localNotifications)
        {
            if (notif.userInfo.Contains("Kamcord"))
            {
                Kamcord.HandleKamcordNotification(notif);
            }
        }
        NotificationServices.ClearLocalNotifications();
    }
#endif

    // The Kamcord share view appeared and disappeared
    void MyKamcordDidStartRecording()
    {
        Debug.Log("Hello MyKamcordDidStartRecording");
    }

    void MyKamcordDidStopRecording()
    {
        Debug.Log("Hello MyKamcordDidStopRecording");
    }

    void MyKamcordViewDidAppear()
    {
        Debug.Log("Hello MyKamcordViewDidAppear");
    }

    void MyKamcordViewWillDisappear()
    {
        Debug.Log("Hello MyKamcordViewWillDisappear");
    }

    void MyKamcordViewDidDisappear()
    {
        Debug.Log("Hello MyKamcordViewDidDisappear");
    }

    void MyKamcordViewDidNotAppear()
    {
        Debug.Log("Hello MyKamcordViewDidNotAppear");
    }

    void MyKamcordWatchViewDidAppear()
    {
        Debug.Log("Hello MyKamcordWatchViewDidAppear");
    }

    void MyKamcordWatchViewWillDisappear()
    {
        Debug.Log("Hello MyKamcordWatchViewWillDisappear");
    }

    void MyKamcordWatchViewDidDisappear()
    {
        Debug.Log("Hello MyKamcordWatchViewDidDisappear");
    }

    // The thumbnail for the latest video is ready at
    // this absolute filepath.
    void MyVideoThumbnailReadyAtFilePath(string filepath)
    {
        Debug.Log("Thumbnail ready at: " + filepath);
    }

    // The user pressed the share button
    void MyShareButtonPressed()
    {
        Debug.Log("ShareButtonPressed.");
    }

    // When the video begins and finishes uploading
    void MyVideoWillBeginUploading(string videoID, string url)
    {
        Debug.Log("Video " + videoID + " will begin uploading: " + url);
    }

    void MyVideoUploadProgressed(string videoID, float progress)
    {
        Debug.Log("Video " + videoID + " upload progressed: " + progress);
    }

    void MyVideoFinishedUploading(string videoID, bool success)
    {
        Debug.Log("Video " + videoID + " finished uploading: " + success);
    }

    // When the video has finished sharing to Facebook, Twitter or Youtube
    void MyVideoSharedToFacebook(string videoID, bool success)
    {
        Debug.Log("VideoSharedToFacebook: " + videoID);
    }

    void MyVideoSharedToTwitter(string videoID, bool success)
    {
        Debug.Log("VideoSharedToTwitter: " + videoID);
    }

    void MyVideoSharedToYoutube(string videoID, bool success)
    {
        Debug.Log("VideoSharedToYoutube: " + videoID);
    }

    // When the snapshot you requested via Kamcord.Snapshot(...) is ready
    void MySnapshotReadyAtFilePath(string filepath)
    {
        Debug.Log("Snapshot ready at filepath: " + filepath);
    }

    // When the call to action button on the push notification view was pressed
    void MyPushNotifCallToActionButtonPressed()
    {
        Debug.Log("PushNotifCallToActionButtonPressed");
    }

    void MyAdjustAndroidWhitelist()
    {
        // Comment this in to enable Kamcord on all Android devices.
        // WARNING: Use this only in development environments!
        // Kamcord.WhitelistAll ();
    }

    void MyIsEnabledChanged(bool enabled)
    {
        Debug.Log("IsEnabledChanged: " + enabled);
    }

#endif
}
