using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using KamcordJSON;

//////////////////////////////////////////////////////////////////
/// iOS Version: 1.8.6 (2014-10-24)
/// Android Version: 1.5.1 (2014-10-28)
//////////////////////////////////////////////////////////////////
public class Kamcord : MonoBehaviour
{
#pragma warning disable
    /*
     *
     * Kamcord callbacks.
     * To subscribe to these, in any C# class do:
     *
     *     Kamcord.kamcordViewDidAppear += myKamcordViewDidAppearFunction;
     *
     * where myKamcordViewDidAppearFunction exists in your C# class like so:
     *
     *     void myKamcordViewDidAppearFunction() {
     *         // ...
     *     }
     *
     */

    /*
     * Kamcord started and stopped recording. These are called immediately upon
     * Kamcord.StartRecording() and Kamcord.StopRecording().
     */
    public delegate void KamcordDidStartRecording ();
    public delegate void KamcordDidStopRecording ();

    // Corresponding events
    public static event KamcordDidStartRecording kamcordDidStartRecording;
    public static event KamcordDidStopRecording kamcordDidStopRecording;

    /*
     * The Kamcord share view appeared and disappeared
     */
    public delegate void KamcordViewDidAppear ();
    public delegate void KamcordViewWillDisappear ();
    public delegate void KamcordViewDidDisappear ();
    public delegate void KamcordViewDidNotAppear ();

    // Corresponding events
    public static event KamcordViewDidAppear kamcordViewDidAppear;
    public static event KamcordViewWillDisappear kamcordViewWillDisappear;
    public static event KamcordViewDidDisappear kamcordViewDidDisappear;
    public static event KamcordViewDidNotAppear kamcordViewDidNotAppear;


    /*
     * The Kamcord watch view appeared and disappeared
     */
    public delegate void KamcordWatchViewDidAppear ();
    public delegate void KamcordWatchViewWillDisappear ();
    public delegate void KamcordWatchViewDidDisappear ();

    // Corresponding events
    public static event KamcordWatchViewDidAppear kamcordWatchViewDidAppear;
    public static event KamcordWatchViewWillDisappear kamcordWatchViewWillDisappear;
    public static event KamcordWatchViewDidDisappear kamcordWatchViewDidDisappear;

    /*
     * Kamcord age gate status changed
     */
    public delegate void AgeStatusUpdated(AgeGateStatus status);
    // Corresponding events
    public static event AgeStatusUpdated ageStatusUpdated;

    /*
     * The thumbnail for the latest video is ready at this absolute filepath.
     */
    public delegate void VideoThumbnailReadyAtFilePath (string filepath);

    // Corresponding event
    public static event VideoThumbnailReadyAtFilePath videoThumbnailReadyAtFilePath;


    /*
     * The user pressed the share button
     */
    public delegate void ShareButtonPressed ();

    // Corresponding event
    public static event ShareButtonPressed shareButtonPressed;


    /*
     * When the video begins and finishes uploading
     */
    public delegate void VideoWillBeginUploading (string videoID,string URL);
    public delegate void VideoUploadProgressed (string videoID,float progress);
    public delegate void VideoFinishedUploading (string videoID,bool success);

    // Corresponding events
    public static event VideoWillBeginUploading videoWillBeginUploading;
    public static event VideoUploadProgressed videoUploadProgressed;
    public static event VideoFinishedUploading videoFinishedUploading;


    /*
     * When the video has finished sharing to Facebook and/or Twitter
     */
    public delegate void VideoSharedTo (string kamcordVideoID, string networkName, bool success);
    public delegate void VideoSharedToFacebook (string kamcordVideoID,bool success);
    public delegate void VideoSharedToTwitter (string kamcordVideoID,bool success);
    public delegate void VideoSharedToYoutube (string kamcordVideoID,bool success);

    // Corresponding events
    public static event VideoSharedTo videoSharedTo;
    public static event VideoSharedToFacebook videoSharedToFacebook;
    public static event VideoSharedToTwitter videoSharedToTwitter;
    public static event VideoSharedToYoutube videoSharedToYoutube;

    /*
     * When the snapshot you requested is ready
     */
    public delegate void SnapshotReadyAtFilePath (string filepath);

    // Corresponding event
    public static event SnapshotReadyAtFilePath snapshotReadyAtFilePath;

    /*
     * When the call to action button on the push notification view was pressed
     */
    public delegate void PushNotifCallToActionButtonPressed ();

    // Corresponding event
    public static event PushNotifCallToActionButtonPressed pushNotifCallToActionButtonPressed;

    /*
     * When this app is an install that's traced back to Kamcord.
     */
    public delegate void AttributedKamcordInstall ();

    // Corresponding event
    public static event AttributedKamcordInstall attributedKamcordInstall;

    /*
     * When the Android whitelist is ready to be adjusted/finalized.
     */
    public delegate void AdjustAndroidWhitelist ();

    // Corresponding event
    public static event AdjustAndroidWhitelist adjustAndroidWhitelist;

    /*
     * When the enabled status for recording on Android has changed.
     */
     public delegate void IsEnabledChanged (bool isEnabled);

     // Corresponding event
     public static event IsEnabledChanged isEnabledChanged;

    // Useful for unsubscribing from everything
    public static void UnsubscribeFromAllCallbacks ()
    {
        kamcordViewDidAppear = null;
        kamcordViewWillDisappear = null;
        kamcordViewDidDisappear = null;
        kamcordViewDidNotAppear = null;
        kamcordWatchViewDidAppear = null;
        kamcordWatchViewWillDisappear = null;
        kamcordWatchViewDidDisappear = null;
        ageStatusUpdated = null;
        videoThumbnailReadyAtFilePath = null;
        videoWillBeginUploading = null;
        videoUploadProgressed = null;
        videoFinishedUploading = null;
        videoSharedTo = null;
        videoSharedToFacebook = null;
        videoSharedToTwitter = null;
        videoSharedToYoutube = null;
        snapshotReadyAtFilePath = null;
        pushNotifCallToActionButtonPressed = null;
        adjustAndroidWhitelist = null;
        isEnabledChanged = null;

        Kamcord.ClearListeners();
    }


    /*
     *
     * The following block of methods are only used by Kamcord
     *
     */
    protected static void CallKamcordDidStartRecording()
    {
        if (kamcordDidStartRecording != null)
        {
            kamcordDidStartRecording ();
        }
    }

    protected static void CallKamcordDidStopRecording()
    {
        if (kamcordDidStopRecording != null)
        {
            kamcordDidStopRecording ();
        }
    }

    protected static void CallKamcordViewDidAppear()
    {
        if (kamcordViewDidAppear != null)
        {
            kamcordViewDidAppear ();
        }
    }

    protected static void CallKamcordViewDidDisappear()
    {
        if (kamcordViewDidDisappear != null)
        {
            kamcordViewDidDisappear ();
        }
    }

    protected static void CallKamcordViewWillDisappear()
    {
        if (kamcordViewWillDisappear != null)
        {
            kamcordViewWillDisappear ();
        }
    }

    protected static void CallKamcordViewDidNotAppear()
    {
        if (kamcordViewDidNotAppear != null)
        {
            kamcordViewDidNotAppear ();
        }
    }

    protected static void CallKamcordWatchViewDidAppear()
    {
        if (kamcordWatchViewDidAppear != null)
        {
            kamcordWatchViewDidAppear();
        }
    }

    protected static void CallKamcordWatchViewDidDisappear()
    {
        if (kamcordWatchViewDidDisappear != null)
        {
            kamcordWatchViewDidDisappear();
        }
    }

    protected static void CallKamcordWatchViewWillDisappear()
    {
        if (kamcordWatchViewWillDisappear != null)
        {
            kamcordWatchViewWillDisappear();
        }
    }

    protected static void CallKamcordAgeStatusUpdated(AgeGateStatus status)
    {
        if (ageStatusUpdated != null)
        {
            ageStatusUpdated(status);
        }
    }

    protected static void CallVideoWillBeginUploading(string videoID,string URL)
    {
        if (videoWillBeginUploading != null)
        {
            videoWillBeginUploading(videoID,URL);
        }
    }
    protected static void CallVideoUploadProgressed(string videoID,float progress)
    {
        if (videoUploadProgressed != null)
        {
            videoUploadProgressed(videoID,progress);
        }
    }
    protected static void CallVideoFinishedUploading(string videoID,bool success)
    {
        if (videoFinishedUploading != null)
        {
            videoFinishedUploading(videoID,success);
        }
    }

    protected static void CallVideoSharedTo(string videoID, string networkName, bool success)
    {
        if (videoSharedTo != null)
        {
            videoSharedTo(videoID,networkName,success);
        }
    }
    protected static void CallVideoSharedToFacebook(string videoID,bool success)
    {
        if (videoSharedToFacebook != null)
        {
            videoSharedToFacebook(videoID,success);
        }
    }
    protected static void CallVideoSharedToTwitter(string videoID,bool success)
    {
        if (videoSharedToTwitter != null)
        {
            videoSharedToTwitter(videoID,success);
        }
    }
    protected static void CallVideoSharedToYoutube(string videoID,bool success)
    {
        if (videoSharedToYoutube != null)
        {
            videoSharedToYoutube(videoID,success);
        }
    }

    protected static void CallShareButtonPressed()
    {
        if (shareButtonPressed != null)
        {
            shareButtonPressed();
        }
    }

    protected static void CallVideoThumbnailReadyAtFilePath(string url)
    {
        if (videoThumbnailReadyAtFilePath != null)
        {
            videoThumbnailReadyAtFilePath (url);
        }
    }

    protected static void CallPushNotifCallToActionButtonPressed()
    {
        if (pushNotifCallToActionButtonPressed != null)
        {
            pushNotifCallToActionButtonPressed();
        }
    }

    protected static void CallSnapshotReadyAtFilePath(string filepath)
    {
        if (snapshotReadyAtFilePath != null)
        {
            snapshotReadyAtFilePath(filepath);
        }
    }

    protected static void CallAttributedKamcordInstall()
    {
        if (attributedKamcordInstall != null)
        {
            attributedKamcordInstall();
        }
    }

    public static void CallAdjustAndroidWhitelist()
    {
        if (adjustAndroidWhitelist != null)
        {
            adjustAndroidWhitelist ();
        }
    }

    public static void CallIsEnabledChanged(bool isEnabled)
    {
        if (isEnabledChanged != null)
        {
            isEnabledChanged(isEnabled);
        }
    }

    // Possible values of videoQuality
    public enum VideoQuality
    {
        Standard = 0,
        Trailer    // Do not release your game with this setting!
    };

    // Possible values of the age gate flow
    public enum AgeGateStatus
    {
        Unknown = 0,
        Restricted,
        Unrestricted
    };

    // Possible metadataTypes
    public enum MetadataType
    {
        level = 0,        // For a level played in the video
        score,             // For a score for the video
        list,              // For a ',' delimited list of values to apply to a key, numerical value if given will apply to all
        other = 1000,     // For arbitrary key to value metadata
    };

    // Share targets for iOS
    public enum ShareTarget
    {
        Facebook = 0,
        Twitter = 1,
        YouTube = 2,
        Email = 3,
#if UNITY_IOS
        WeChat = 4, // WeChat is currently supported on iOS only.
#endif
        LINE = 5,
    };

    // Possible values of the YouTube video category
    public enum YouTubeVideoCategory
    {
        Comedy,
        Education,
        Entertainment,
        Games,
        Music
    };

    public bool enableIOS = true;
    public bool enableAndroid = false;
    public bool enableLogging = true;
    public string developerKey = "Kamcord developer key";
    public string developerSecret = "Kamcord developer secret";
    public string appName = "Application name";
    public VideoQuality videoQuality = VideoQuality.Standard;
    public bool enableVoiceOverlay = true;
    public bool useFastRender = true;

    // Can be used to disable Kamcord on certain devices
    [System.Serializable]
    public class KamcordBlacklist
    {
        public bool ipod4Gen = false;
        public bool ipod5Gen = false;
        public bool iphone3GS = false;
        public bool iphone4 = false;
        public bool iphone4S = false;
        public bool iphone5 = false;
        public bool iphone5c = false;
        public bool iphone5S = false;
        public bool ipad1 = false;
        public bool ipad2 = false;
        public bool ipadMini = false;
        public bool ipad3 = false;
        public bool ipad4 = false;
        public bool ipadAir = false;
    }

    public KamcordBlacklist deviceBlacklist;

    // The default empty implementation that's called when this game
    // is run on a platform that Kamcord doesn't support.
    public class Implementation
    {
        public virtual void SetLoggingEnabled(bool value)
        {
        }

        public virtual bool IsEnabled ()
        {
            return false;
        }

        public virtual String GetDisabledReason()
        {
            return "";
        }

        public virtual void WhitelistBoard(string boardName)
        {
        }

        public virtual void BlacklistBoard(string boardName)
        {
        }

        public virtual void WhitelistDevice(string deviceName)
        {
        }

        public virtual void BlacklistDevice(string deviceName)
        {
        }

        public virtual void WhitelistBoard(string boardName, int sdkVersion)
        {
        }

        public virtual void BlacklistBoard(string boardName, int sdkVersion)
        {
        }

        public virtual void WhitelistDevice(string deviceName, int sdkVersion)
        {
        }

        public virtual void BlacklistDevice(string deviceName, int sdkVersion)
        {
        }

        // Deprecated.  Use WhitelistAll() instead.
        public virtual void WhitelistAllBoards()
        {
        }

        // Deprecated.  Use BlacklistAll() instead.
        public virtual void BlacklistAllBoards()
        {
        }

        public virtual void WhitelistAll()
        {
        }

        public virtual void BlacklistAll()
        {
        }

        public virtual string GetBoard()
        {
            return "";
        }

        public virtual string GetDevice()
        {
            return "";
        }

        // Deprecated.  Use IsWhitelisted() with no arguments instead.
        public virtual bool IsWhitelisted(string boardName)
        {
            return false;
        }

        public virtual bool IsWhitelisted()
        {
            return false;
        }

        public virtual void DoneChangingWhitelist()
        {
        }

        public virtual void SetVideoTitle (string title)
        {
        }

        public virtual void SetYouTubeSettings (
            string description,
            string tags)
        {
        }

        public virtual void SetFacebookAppID (string facebookAppID)
        {
        }

        public virtual void SetFacebookAppIDAndShareAuth (string facebookAppID, bool useSharedAuth)
        {
        }

        public virtual void LogoutOfSharedFacebookAuth()
        {
        }

        public virtual void SetWeChatAppID (string weChatAppID)
        {
        }

        public virtual void SetFacebookDescription (string facebookDescription)
        {
        }

        public virtual void SetDefaultTweet (string tweet)
        {
        }

        public virtual void SetTwitterDescription (string twitterDescription)
        {
        }

        public virtual void SetDefaultEmailSubject (string subject)
        {
        }

        public virtual void SetDefaultEmailBody (string body)
        {
        }

        public virtual void SetShareTargets(
                ShareTarget target1,
                ShareTarget target2,
                ShareTarget target3,
                ShareTarget target4)
        {
        }

        public virtual void SetVideoMetadata (Dictionary <string, object> metadata)
        {
        }

        public virtual void SetMaxFreeDiskSpacePercentageUsage (double percentage)
        {
        }

        public virtual string Version()
        {
            return "";
        }

        public virtual void SetLevelAndScore (string level, double score)
        {
        }

        public virtual void SetDeveloperMetadata (
            MetadataType metadataType,
            string displayKey,
            string displayValue)
        {
        }

        public virtual void SetDeveloperMetadataWithNumericValue (
                MetadataType metadataType,
                string displayKey,
                string displayValue,
                double numericValue)
        {
        }

        //////////////////////////////////////////////////////////////////
        /// Video Watching
        ///

        public virtual bool VideoExistsWithMetadataConstraints(Dictionary <string, object> metadata)
        {
            return false;
        }

        public virtual void ShowVideoWithMetadataConstraints(Dictionary <string, object> metadata, string title)
        {
        }

        public virtual void ShowVideoWithVideoID(string videoID, string title)
        {
        }

        public virtual void BeginDraw ()
        {
        }

        public virtual void EndDraw ()
        {
        }

        public virtual void StartRecording ()
        {
        }

        public virtual void StopRecording ()
        {
        }

        public virtual void Pause ()
        {
        }

        public virtual void Resume ()
        {
        }

        public virtual bool IsRecording ()
        {
            return false;
        }

        public virtual bool IsPaused ()
        {
            return false;
        }

        public virtual bool IsViewShowing ()
        {
            return false;
        }

        public virtual void Snapshot (string filename)
        {
        }

        public virtual void SetVideoQuality (VideoQuality quality)
        {
        }

        public virtual void SetUseFastRender(bool useFastRender)
        {
        }

        public virtual void SetVoiceOverlayEnabled (bool enabled)
        {
        }

        public virtual bool VoiceOverlayEnabled ()
        {
            return false;
        }

        public virtual void ActivateVoiceOverlay (bool activate)
        {
        }

        public virtual bool VoiceOverlayActivated ()
        {
            return false;
        }

        public virtual void CaptureFrame ()
        {
        }

        public virtual void SetNotificationsEnabled (bool notificationsEnabled)
        {
        }

        public virtual void FireTestNotification ()
        {
        }

        public virtual void ShowView ()
        {
        }

        public virtual void ShowWatchView ()
        {
        }

        public virtual void SetMaximumVideoLength (uint seconds)
        {
        }

        public virtual uint MaximumVideoLength ()
        {
            return 0;
        }

        public virtual void SetVideoFPS (uint videoFPS)
        {
        }

        public virtual uint VideoFPS ()
        {
            return 0;
        }

        public virtual void SetShouldPauseGameEngine(bool shouldPause)
        {
        }

        public virtual bool ShouldPauseGameEngine ()
        {
            return false;
        }

        public virtual void UploadVideo (string title)
        {
        }

        public virtual void Disable ()
        {
        }

        public virtual void TurnOffAutomaticAudioRecording (bool state)
        {
        }

        public virtual void PlayBackgroundSound (string fileName, bool loop)
        {
        }

        public virtual void Init(
            string devKey,
            string devSecret,
            string appName,
            VideoQuality videoQuality)
        {
        }

        public virtual void SetDeviceBlacklist(
            bool disableiPod4G,
            bool disableiPod5G,
            bool disableiPhone3GS,
            bool disableiPhone4,
            bool disableiPhone4S,
            bool disableiPhone5,
            bool disableiPhone5C,
            bool disableiPhone5S,
            bool disableiPad1,
            bool disableiPad2,
            bool disableiPadMini,
            bool disableiPad3,
            bool disableiPad4,
            bool disableiPadAir)
        {
        }

        public virtual void SetDefaultTitle (string title)
        {
        }

        public virtual void SetYouTubeVideoCategory (YouTubeVideoCategory category)
        {
        }

        public virtual void SetFacebookSettings (string title, string caption, string description)
        {
        }

        public virtual void SetDefaultEmailSubjectAndBody (string subject, string body)
        {
        }

        public virtual void Awake(Kamcord kamcordInstance)
        {
        }

        public virtual void Start(Kamcord kamcordInstance)
        {
        }

        public virtual void SetCrossPromoIcon(string localFileImageURL)
        {
        }

        public virtual void SetMode(int mode)
        {
        }

        public virtual void SetAgeRestrictionEnabled(bool restricted)
        {

        }

        public virtual bool IsAgeRestrictionEnabled()
        {
            return false;
        }

        public virtual void SetVideoIncompleteWarningEnabled (bool enabled)
        {
        }

        public virtual bool IsVideoComplete()
        {
            return true;
        }

        public virtual void SetAudioSettings(int sampleRate, int numChannels)
        {
        }

        public virtual void WriteAudioData(float [] data, int length)
        {
        }

        public virtual void SetFlushOnCopy(bool flush)
        {
        }
    }

    public static bool iOSEnabled_ = true;
    public static bool loggingEnabled_ = true;
    public static bool androidEnabled_ = false;

    private static Implementation implementation_;

    private static Implementation implementation()
    {
        // iOSEnabled_ or androidEnabled_ must be set true before calling this function,
        // or it will return the empty implementation forever after.

        if (implementation_ == null)
        {
            if ((iOSEnabled_ || androidEnabled_) &&
                (Application.platform == RuntimePlatform.Android ||
                  Application.platform == RuntimePlatform.IPhonePlayer))
            {
#if UNITY_ANDROID && !(UNITY_2_6 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_1)
                if (androidEnabled_ && KamcordImplementationAndroid.getSDKVersion() >= 16)
                {
                    implementation_ = new KamcordImplementationAndroid();
                }
#elif UNITY_IPHONE
                if (iOSEnabled_)
                {
                    implementation_ = new KamcordImplementationIOS();
                }
#endif
            }
        }


        // If the implementation is still null by here, use the empty implementation.
        if (implementation_ == null)
        {
            implementation_ = new Implementation();
        }

        return implementation_;
    }

    /*
     *
     * This is the beginning of the Kamcord public API
     *
     */

    public static void SetLoggingEnabled(bool value)
    {
        implementation().SetLoggingEnabled(value);
    }

    public static bool IsEnabled()
    {
        return implementation().IsEnabled();
    }

    public static string GetDisabledReason()
    {
        return implementation().GetDisabledReason();
    }

    public static void WhitelistBoard(string boardName)
    {
        implementation().WhitelistBoard(boardName);
    }

    public static void BlacklistBoard(string boardName)
    {
        implementation().BlacklistBoard(boardName);
    }

    public static void WhitelistDevice(string deviceName)
    {
        implementation().WhitelistDevice(deviceName);
    }

    public static void BlacklistDevice(string deviceName)
    {
        implementation().BlacklistDevice(deviceName);
    }

    public static void WhitelistBoard(string boardName, int sdkVersion)
    {
        implementation().WhitelistBoard(boardName, sdkVersion);
    }

    public static void BlacklistBoard(string boardName, int sdkVersion)
    {
        implementation().BlacklistBoard(boardName, sdkVersion);
    }

    public static void WhitelistDevice(string deviceName, int sdkVersion)
    {
        implementation().WhitelistDevice(deviceName, sdkVersion);
    }

    public static void BlacklistDevice(string deviceName, int sdkVersion)
    {
        implementation().BlacklistDevice(deviceName, sdkVersion);
    }

    // Deprecated.  Use WhitelistAll() instead.
    public static void WhitelistAllBoards()
    {
        implementation().WhitelistAllBoards();
    }

    // Deprecated.  Use BlacklistAll() instead.
    public static void BlacklistAllBoards()
    {
        implementation().BlacklistAllBoards();
    }

    public static void WhitelistAll()
    {
        implementation().WhitelistAll();
    }

    public static void BlacklistAll()
    {
        implementation().BlacklistAll();
    }

    public static string GetBoard()
    {
        return implementation().GetBoard();
    }

    public static bool IsWhitelisted(string boardName)
    {
        return implementation().IsWhitelisted(boardName);
    }

    public static void DoneChangingWhitelist()
    {
        implementation().DoneChangingWhitelist();
    }

    public static void SetVideoTitle(string title)
    {
        implementation().SetVideoTitle(title);
    }

    public static void SetYouTubeSettings (string description, string tags)
    {
        implementation().SetYouTubeSettings (description, tags);
    }

    public static void SetFacebookAppID(string facebookAppID)
    {
        implementation().SetFacebookAppID(facebookAppID);
    }

    public static void SetFacebookAppIDAndShareAuth(string facebookAppID, bool useSharedAuth)
    {
        implementation().SetFacebookAppIDAndShareAuth(facebookAppID, useSharedAuth);
    }

    public static void LogoutOfSharedFacebookAuth()
    {
        implementation().LogoutOfSharedFacebookAuth();
    }

    public static void SetWeChatAppID(string weChatAppID)
    {
        implementation().SetWeChatAppID(weChatAppID);
    }

    public static void SetFacebookDescription(string facebookDescription)
    {
        implementation().SetFacebookDescription(facebookDescription);
    }

    public static void SetDefaultTweet(string tweet)
    {
        implementation().SetDefaultTweet(tweet);
    }

    public static void SetTwitterDescription(string twitterDescription)
    {
        implementation().SetTwitterDescription(twitterDescription);
    }

    public static void SetDefaultEmailSubject (string subject)
    {
        implementation().SetDefaultEmailSubject(subject);
    }

    public static void SetDefaultEmailBody (string body)
    {
        implementation().SetDefaultEmailBody(body);
    }

    public static void SetShareTargets(
            ShareTarget target1,
            ShareTarget target2,
            ShareTarget target3,
            ShareTarget target4)
    {
        implementation().SetShareTargets(target1, target2, target3, target4);
    }

    public static void SetVideoMetadata (Dictionary <string, object> metadata)
    {
        implementation().SetVideoMetadata(metadata);
    }

    public static void SetDeveloperMetadata (
            MetadataType metadataType,
            string displayKey,
            string displayValue)
    {
        implementation().SetDeveloperMetadata (metadataType, displayKey, displayValue);
    }

    public static void SetDeveloperMetadataWithNumericValue (
            MetadataType metadataType,
            string displayKey,
            string displayValue,
            double numericValue)
    {
        implementation().SetDeveloperMetadataWithNumericValue (metadataType, displayKey, displayValue, numericValue);
    }

    //////////////////////////////////////////////////////////////////
    /// Video Watching
    ///

    public static bool VideoExistsWithMetadataConstraints(Dictionary <string, object> metadata)
    {
        return implementation().VideoExistsWithMetadataConstraints (metadata);
    }

    public static void ShowVideoWithMetadataConstraints(Dictionary <string, object> metadata, string title)
    {
        implementation().ShowVideoWithMetadataConstraints (metadata, title);
    }

    public static void ShowVideoWithVideoID(string videoID, string title)
    {
        implementation().ShowVideoWithVideoID (videoID, title);
    }

    public static void SetMaxFreeDiskSpacePercentageUsage(double percentage)
    {
        implementation().SetMaxFreeDiskSpacePercentageUsage(percentage);
    }

    public static string Version()
    {
        return implementation().Version();
    }

    public static void SetLevelAndScore(string level, double score)
    {
        implementation().SetLevelAndScore(level, score);
    }

    public static void BeginDraw()
    {
        implementation().BeginDraw();
    }

    public static void EndDraw()
    {
        implementation().EndDraw();
    }

    public static void StartRecording()
    {
        implementation().StartRecording();
        Kamcord.CallKamcordDidStartRecording();
    }

    public static void StopRecording()
    {
        implementation().StopRecording();
        Kamcord.CallKamcordDidStopRecording();
    }

    public static void Pause()
    {
        implementation().Pause();
    }

    public static void Resume()
    {
        implementation().Resume();
    }

    public static bool IsRecording()
    {
        return implementation().IsRecording();
    }

    public static bool IsPaused()
    {
        return implementation().IsPaused();
    }

    public static bool IsViewShowing()
    {
        return implementation().IsViewShowing();
    }

    public static void Snapshot(string filename)
    {
        implementation().Snapshot(filename);
    }

    public static void SetVideoQuality (VideoQuality quality)
    {
        implementation().SetVideoQuality(quality);
    }

    private static bool developerSetVoiceOverlay = false;
    public static void SetVoiceOverlayEnabled (bool enabled)
    {
        implementation().SetVoiceOverlayEnabled(enabled);
        developerSetVoiceOverlay = true;
    }

    public static bool VoiceOverlayEnabled ()
    {
        return implementation().VoiceOverlayEnabled();
    }

    public static void ActivateVoiceOverlay(bool activate)
    {
        implementation().ActivateVoiceOverlay(activate);
    }

    public static bool VoiceOverlayActivated()
    {
        return implementation().VoiceOverlayActivated();
    }

    public static void CaptureFrame()
    {
        implementation().CaptureFrame();
    }

    public static void SetNotificationsEnabled(bool notificationsEnabled)
    {
        implementation().SetNotificationsEnabled(notificationsEnabled);
    }

    public static void FireTestNotification()
    {
        implementation().FireTestNotification();
    }

    public static void ShowView ()
    {
        implementation().ShowView();
    }

    public static void ShowWatchView ()
    {
        implementation().ShowWatchView();
    }

    public static void SetMaximumVideoLength (uint seconds)
    {
        implementation().SetMaximumVideoLength(seconds);
    }

    public static uint MaximumVideoLength ()
    {
        return implementation().MaximumVideoLength();
    }

    public static void SetVideoFPS (uint videoFPS)
    {
        implementation().SetVideoFPS(videoFPS);
    }

    public static uint VideoFPS()
    {
        return implementation().VideoFPS();
    }

    public static void SetShouldPauseGameEngine (bool shouldPause)
    {
        implementation().SetShouldPauseGameEngine(shouldPause);
    }

    public static bool ShouldPauseGameEngine()
    {
        return implementation().ShouldPauseGameEngine();
    }

    public static void SetAgeRestrictionEnabled (bool restricted)
    {
        implementation().SetAgeRestrictionEnabled(restricted);
    }

    public static bool IsAgeRestrictionEnabled()
    {
        return implementation().IsAgeRestrictionEnabled();
    }

    public static void SetVideoIncompleteWarningEnabled (bool enabled)
    {
        implementation().SetVideoIncompleteWarningEnabled(enabled);
    }

    public static bool IsVideoComplete()
    {
        return implementation().IsVideoComplete();
    }

    public static void Disable()
    {
        implementation().Disable();
    }

    //////////////////////////////////////////////////////////////////
    /// Miscellaneous audio utility methods (not for general use)
    ///

    // Do not use the following audio methods unless you programmatically create AudioListeners
    // (as opposed to having them already in the scene).
    public static void SetAudioListener(AudioListener audioListener)
    {
        GameObject targetGameObject = audioListener.gameObject;

        Boolean doAttach = true;
        Component[] audioListenerComponents = targetGameObject.GetComponents(typeof (MonoBehaviour));
        foreach (Component audioListenerComponent in audioListenerComponents)
        {
            if ((typeof (KamcordAudioRecorder)).Equals(audioListenerComponent.GetType()))
            {
                Debug.Log("Game Object already has KamcordAudioRecorder attached, not re-attaching for scene " + Application.loadedLevelName);
                doAttach = false;
            }
        }

        // Get the number of channels. For now, only support 1 or 2.
        int numChannels = (AudioSettings.speakerMode == AudioSpeakerMode.Mono ? 1 : 2);

        // Tell Kamcord about the audio settings
        implementation().SetAudioSettings(AudioSettings.outputSampleRate, numChannels);

        if( doAttach )
        {
            // Capture bytes from this audio recorder
            Debug.Log("Programmatically adding KamcordAudioRecorder for scene " + Application.loadedLevelName);
            audioListener.enabled = false;
            targetGameObject.AddComponent("KamcordAudioRecorder");
            audioListener.enabled = true;
        }
    }

    public static void WriteAudioData(float [] data, int numSamples)
    {
        implementation().WriteAudioData(data, numSamples);
    }

    public static void Init (
            string devKey,
            string devSecret,
            string appName,
            VideoQuality videoQuality)
    {
        implementation().Init(
            devKey,
            devSecret,
            appName,
            videoQuality);
    }

    public static void SetDeviceBlacklist (
            bool disableiPod4G,
            bool disableiPod5G,
            bool disableiPhone3GS,
            bool disableiPhone4,
            bool disableiPhone4S,
            bool disableiPhone5,
            bool disableiPhone5C,
            bool disableiPhone5S,
            bool disableiPad1,
            bool disableiPad2,
            bool disableiPadMini,
            bool disableiPad3,
            bool disableiPad4,
            bool disableiPadAir)
    {
        implementation().SetDeviceBlacklist (
            disableiPod4G,
            disableiPod5G,
            disableiPhone3GS,
            disableiPhone4,
            disableiPhone4S,
            disableiPhone5,
            disableiPhone5C,
            disableiPhone5S,
            disableiPad1,
            disableiPad2,
            disableiPadMini,
            disableiPad3,
            disableiPad4,
            disableiPadAir);
    }

    public static void SetDefaultTitle (string title)
    {
        implementation().SetDefaultTitle(title);
    }

    public static void SetYouTubeVideoCategory (YouTubeVideoCategory category)
    {
        implementation().SetYouTubeVideoCategory(category);
    }

    public static void SetFacebookSettings (string title, string caption, string description)
    {
        implementation().SetFacebookSettings(title, caption, description);
    }

    public static void SetDefaultEmailSubjectAndBody(string subject, string body)
    {
        implementation().SetDefaultEmailSubjectAndBody(subject, body);
    }

    public static void SetCrossPromoIcon(string localImageFileURL)
    {
        implementation().SetCrossPromoIcon(localImageFileURL);
    }

    public static void TurnOffAutomaticAudioRecording (bool status)
    {
        implementation().TurnOffAutomaticAudioRecording(status);
    }

    // Private API: do not use.
    public static void SetMode(int mode)
    {
        implementation().SetMode(mode);
    }

    public static void SetFlushOnCopy(bool flush)
    {
        implementation().SetFlushOnCopy(flush);
    }

    public static Kamcord instance;

    void Awake()
    {
        // Prevent multiple instances of this Prefab from existing
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        this.gameObject.name = "KamcordPrefab";
        DontDestroyOnLoad(this);
        instance = this;

        // Set iOS / Android enable state.
        iOSEnabled_ = enableIOS;
        androidEnabled_ = enableAndroid;
        loggingEnabled_ = enableLogging;

#if UNITY_IPHONE
        implementation().SetUseFastRender(useFastRender);

        implementation().SetDeviceBlacklist(
            deviceBlacklist.ipod4Gen,
            deviceBlacklist.ipod5Gen,
            deviceBlacklist.iphone3GS,
            deviceBlacklist.iphone4,
            deviceBlacklist.iphone4S,
            deviceBlacklist.iphone5,
            deviceBlacklist.iphone5c,
            deviceBlacklist.iphone5S,
            deviceBlacklist.ipad1,
            deviceBlacklist.ipad2,
            deviceBlacklist.ipadMini,
            deviceBlacklist.ipad3,
            deviceBlacklist.ipad4,
            deviceBlacklist.ipadAir);
#endif

        implementation().Awake(this);
        Kamcord.SetMode(0);
    }

    void Start()
    {
        implementation().Start(this);
        if( !developerSetVoiceOverlay )
        {
            implementation().SetVoiceOverlayEnabled(enableVoiceOverlay);
        }
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Kamcord.Pause();
        }

        /*!
         *
         * Your game may not want to resume as soon as the application resumes!
         * If, for instance, you have a pause menu, you'll want to call Kamcord.Resume()
         * when the user resume the game from that pause menu. In that case, comment out
         * this else case below.
         *
         */
        else
        {
            Kamcord.Resume();
        }
    }

    //////////////////////////////////////////////////////////////////
    /// Subscribe to KamcordCallbackInterface callback
    ///

    // The listseners list
    protected static List<KamcordCallbackInterface> listeners = new List<KamcordCallbackInterface> ();

    // Call this override method to have your object receive all of the
    // KamcordCallbackInterface callbacks.
    public static void AddListener (KamcordCallbackInterface listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public static void RemoveListener (KamcordCallbackInterface listener)
    {
        listeners.Remove(listener);
    }

    public static void ClearListeners ()
    {
        listeners.Clear();
    }

#if UNITY_IPHONE

    //////////////////////////////////////////////////////////////////
    /// Handling callbacks from Objective-C
    ///

    // The Kamcord share view appeared
    private void _KamcordViewDidAppear(string empty)
    {
        Kamcord.CallKamcordViewDidAppear();

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.KamcordViewDidAppear();
        }
    }

    private void _KamcordViewWillDisappear(string empty)
    {
        Kamcord.CallKamcordViewWillDisappear();

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.KamcordViewWillDisappear();
        }
    }

    // The Kamcord share view disappeared
    private void _KamcordViewDidDisappear(string empty)
    {
        Kamcord.CallKamcordViewDidDisappear();

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.KamcordViewDidDisappear();
        }
    }

    // The Kamcord watch view appeared
    private void _KamcordWatchViewDidAppear(string empty)
    {
        Kamcord.CallKamcordWatchViewDidAppear();

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.KamcordWatchViewDidAppear();
        }
    }

    private void _KamcordWatchViewWillDisappear(string empty)
    {
        Kamcord.CallKamcordWatchViewWillDisappear();

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.KamcordWatchViewWillDisappear();
        }
    }

    // The Kamcord watch view disappeared
    private void _KamcordWatchViewDidDisappear(string empty)
    {
        Kamcord.CallKamcordWatchViewDidDisappear();

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.KamcordWatchViewDidDisappear();
        }
    }


    private void _KamcordAgeStatusUpdated(string status)
    {
        // Parse the string to our enum
        AgeGateStatus ageStatus = (AgeGateStatus)Convert.ToInt32(status);

        // Pass the integer value as an enum to registered callbacks
        Kamcord.CallKamcordAgeStatusUpdated(ageStatus);
        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.AgeStatusUpdated(ageStatus);
        }
    }

    // The share button was pressed
    private void _ShareButtonPressed(string empty)
    {
        Kamcord.CallShareButtonPressed();

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.ShareButtonPressed();
        }
    }

    // The thumbnail for the latest video is ready at this
    // absolute file path
    private void _VideoThumbnailReadyAtFilePath(string filepath)
    {
        Kamcord.CallVideoThumbnailReadyAtFilePath(filepath);

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.VideoThumbnailReadyAtFilePath(filepath);
        }
    }

    // When the video begins and finishes uploading
    private void _VideoWillUploadToURL(string serializedString)
    {
        string[] stringDelimiters = new string[] { "??" };
        string[] values = serializedString.Split(stringDelimiters, StringSplitOptions.None);

        string videoID = values[0];
        string url = values[1];

        Kamcord.CallVideoWillBeginUploading(videoID, url);

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.VideoWillBeginUploading(videoID, url);
        }
    }

    private void _VideoUploadProgressed(string serializedString)
    {
        string[] stringDelimiters = new string[] { ":" };
        string[] values = serializedString.Split(stringDelimiters, StringSplitOptions.None);

        string videoID = values[0];
        float progress = float.Parse(values[1]);

        Kamcord.CallVideoUploadProgressed(videoID, progress);

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.VideoUploadProgressed(videoID, progress);
        }
    }

    private void _VideoUploadedWithSuccess(string serializedString)
    {
        string[] stringDelimiters = new string[] { ":" };
        string[] values = serializedString.Split(stringDelimiters, StringSplitOptions.None);

        string videoID = values[0];
        bool truthValue = (values[1] == "true" ? true : false);

        Kamcord.CallVideoFinishedUploading(videoID, truthValue);

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.VideoFinishedUploading(videoID, truthValue);
        }
    }

    private void _VideoSharedToFacebook(string serializedString)
    {
        string[] stringDelimiters = new string[] { ":" };
        string[] values = serializedString.Split(stringDelimiters, StringSplitOptions.None);
        bool truthValue = (values[1] == "true" ? true : false);

        Kamcord.CallVideoSharedToFacebook(values[0], truthValue);

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.VideoSharedToFacebook(values[0], truthValue);
        }
    }

    private void _VideoSharedToTwitter(string serializedString)
    {
        string[] stringDelimiters = new string[] { ":" };
        string[] values = serializedString.Split(stringDelimiters, StringSplitOptions.None);
        bool truthValue = (values[1] == "true" ? true : false);

        Kamcord.CallVideoSharedToTwitter(values[0], truthValue);

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.VideoSharedToTwitter(values[0], truthValue);
        }
    }

    private void _VideoWillBeSharedToYouTube(string videoID)
    {
        Kamcord.CallVideoSharedToYoutube(videoID, true);
    }

    private void _KamcordAttributedKamcordInstall(string empty)
    {
        Kamcord.CallAttributedKamcordInstall();
    }

    private void _VideoSharedTo(string serializedString)
    {
        string[] stringDelimiters = new string[] { ":" };
        string[] values = serializedString.Split(stringDelimiters, StringSplitOptions.None);
        bool truthValue = (values[2] == "true" ? true : false);

        Kamcord.CallVideoSharedTo(values[0], values[1], truthValue);
    }

    // When the call to action button on the notification view was pressed
    private void _PushNotifCallToActionButtonPressed()
    {
        Kamcord.CallPushNotifCallToActionButtonPressed();

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.PushNotifCallToActionButtonPressed();
        }
    }

    // When the snapshot you requested is ready
    private void _SnapshotReadyAtFilePath(string filepath)
    {
        Kamcord.CallSnapshotReadyAtFilePath(filepath);

        foreach (KamcordCallbackInterface listener in listeners)
        {
            listener.SnapshotReadyAtFilePath(filepath);
        }
    }

    [DllImport ("__Internal")]
    private static extern void Kamcord_ShowPushNotifView();

    public static void HandleKamcordNotification (LocalNotification notification)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (notification.userInfo.Contains("Kamcord"))
            {
                Kamcord_ShowPushNotifView();
            }
        }
    }

#elif UNITY_ANDROID && ((!(UNITY_2_6 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_1)))

    private static float timeScale;

    private void _KamcordViewDidAppear(string empty)
    {
        timeScale = Time.timeScale;
        Time.timeScale = 0;

        Kamcord.CallKamcordViewDidAppear();
    }

    private void _KamcordViewDidDisappear(string empty)
    {
        Time.timeScale = timeScale;
        Kamcord.CallKamcordViewDidDisappear ();
    }

    private void _KamcordViewDidNotAppear(string empty)
    {
        Kamcord.CallKamcordViewDidNotAppear ();
    }

    private void _KamcordVideoWillBeginUploading(string jsonString)
    {
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        Kamcord.CallVideoWillBeginUploading((string)dict["videoID"],(string)dict["URL"]);
    }

    private void _KamcordVideoUploadProgressed(string jsonString)
    {
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        Kamcord.CallVideoUploadProgressed((string)dict["videoID"],Convert.ToSingle(dict["progress"]));
    }

    private void _KamcordVideoFinishedUploading(string jsonString)
    {
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        Kamcord.CallVideoFinishedUploading((string)dict["videoID"],(bool)dict["success"]);
    }

    private void _KamcordVideoSharedTo(string jsonString)
    {
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        Kamcord.CallVideoSharedTo((string)dict["videoID"],(string)dict["networkName"],(bool)dict["success"]);
    }

    private void _KamcordVideoSharedToFacebook(string jsonString)
    {
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        Kamcord.CallVideoSharedToFacebook((string)dict["videoID"],(bool)dict["success"]);
    }

    private void _KamcordVideoSharedToTwitter(string jsonString)
    {
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        Kamcord.CallVideoSharedToTwitter((string)dict["videoID"],(bool)dict["success"]);
    }

    private void _KamcordVideoSharedToYoutube(string jsonString)
    {
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        Kamcord.CallVideoSharedToYoutube((string)dict["videoID"],(bool)dict["success"]);
    }

    private void _KamcordVideoThumbnailReadyAtFilePath(string jsonString)
    {
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        Kamcord.CallVideoThumbnailReadyAtFilePath((string)dict["url"]);
    }

    private void _KamcordIsEnabledChanged(string jsonString)
    {
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        Kamcord.CallIsEnabledChanged((bool)dict["isEnabled"]);
    }

#endif

}

