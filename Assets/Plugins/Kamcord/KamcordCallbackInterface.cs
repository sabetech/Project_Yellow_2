// ---------------------------------------------------------------------------
// This is the interface that KamcordCallbackProcessor implements. These
// are all the possible callbacks that the Kamcord Objective-C framework
// will make back into Unity.
// ---------------------------------------------------------------------------

public interface KamcordCallbackInterface
{
#if UNITY_IPHONE
    // The Kamcord share view appeared and disappeared
    void KamcordViewDidAppear();
    void KamcordViewWillDisappear();
    void KamcordViewDidDisappear();

    // The Kamcord watch view appeared and disappeared
    void KamcordWatchViewDidAppear();
    void KamcordWatchViewWillDisappear();
    void KamcordWatchViewDidDisappear();

    // The thumbnail for the latest video is ready at
    // this absolute filepath.
    void VideoThumbnailReadyAtFilePath(string filepath);
    void AgeStatusUpdated(Kamcord.AgeGateStatus status);

    // The user pressed the share button
    void ShareButtonPressed();

    // When the video begins and finishes uploading
    void VideoWillBeginUploading(string videoID, string url);
    void VideoUploadProgressed(string videoID, float progress);
    void VideoFinishedUploading(string videoID, bool success);

    // When the video has finished sharing to Facebook and/or Twitter
    void VideoSharedToFacebook(string videoID, bool success);
    void VideoSharedToTwitter(string videoID, bool success);

    // When the snapshot you requested is ready
    void SnapshotReadyAtFilePath(string filepath);

    // When the call to action button on the push notification view was pressed
    void PushNotifCallToActionButtonPressed();
#endif
}
