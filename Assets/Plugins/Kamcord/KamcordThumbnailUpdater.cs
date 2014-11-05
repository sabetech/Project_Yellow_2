using UnityEngine;
using System.Collections;

public class KamcordThumbnailUpdater : MonoBehaviour
{
    // ------------------------------------------------------------------
    // Public member variables
    // ------------------------------------------------------------------

    // The play button texture object
    public Texture2D playButtonTexture;

    // The X,Y location of the thumbnail, where
    // (0,0) is the bottom left of the screen and
    // (1,1) is the top right of the screen.
    public float thumbnailRelativeX = 0.25f;
    public float thumbnailRelativeY = 0.25f;

    // The ratio of the thumbnail to the screen resolution.
    // A ratio of 1.0 will make the thumbnail dimensions
    // the exact same as the full screen dimensions.
    // A ratio of 0.5 will make the thumbnail width and height
    // equal to half of the full screen dimensions.
    // The min ratio is 0.2.
    public float thumbnailToScreenRatio = 0.4f;

#if UNITY_IPHONE || (UNITY_ANDROID && ((!(UNITY_2_6 || UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_1))))

    // ------------------------------------------------------------------
    // Public methods
    // ------------------------------------------------------------------
    public void EnableThumbnail(bool enable)
    {
        if (this.theGuiTexture != null)
        {
            this.theGuiTexture.enabled = enable;
        }
    }

    // ------------------------------------------------------------------
    // Private member variables
    // ------------------------------------------------------------------

    // The GUITexture object the
    private GUITexture theGuiTexture;

    // How large the play button is relative to the thumbnail
    private float playButtonToThumbnailRatio = 0.5f;
    private Rect playButtonLocationAndSize;

    void Start ()
    {
        gameObject.AddComponent("GUITexture");
        GUITexture[] guiTextures = gameObject.GetComponents<GUITexture>();
        if (guiTextures.Length == 0)
        {
            throw new System.Exception("Kamcord script " + this.name + " needs to have at least one GUITexture component on the attached game object named: " + this.gameObject.name);
        }

        this.theGuiTexture = guiTextures[0];

        Kamcord.videoThumbnailReadyAtFilePath += VideoThumbnailReadyAtFilePath;
        EnableThumbnail(false);
    }

    void OnDestroy()
    {
        Kamcord.videoThumbnailReadyAtFilePath -= VideoThumbnailReadyAtFilePath;
    }

    // Detect touch events
    void Update()
    {
        if (this.theGuiTexture != null)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began && this.theGuiTexture.HitTest(touch.position))
                {
                    Kamcord.ShowView();
                    break;
                }
            }
        }
    }

    // Display a play button if the thumbnail is visible
    void OnGUI()
    {
        if (this.theGuiTexture != null && this.theGuiTexture.enabled)
        {
            GUI.Label(playButtonLocationAndSize, playButtonTexture);
        }
    }

    // The thumbnail for the latest video is ready at this absolute filepath.
    public void VideoThumbnailReadyAtFilePath(string filepath)
    {
        Debug.Log("Thumbnail exists at " + filepath);
        if (System.IO.File.Exists(filepath))
        {
            SetThumbnailTextureToFilepath(filepath);
        }
    }

    // The async call to load the texture and then set the thumbnail
    // to the loaded texture.
    private IEnumerator WaitForLoadToFinishAndThenSetThumbnail(WWW loader)
    {
        yield return loader;

        if (loader.error == null)
        {
            // Min ratio is 0.25f
            if (this.thumbnailToScreenRatio < 0.2f)
            {
                this.thumbnailToScreenRatio = 0.2f;
            }

            // First set the thumbnail location and size
            float absoluteX = this.thumbnailRelativeX * Screen.width;
            float absoluteY = this.thumbnailRelativeY * Screen.height;
            float absoluteWidth  = this.thumbnailToScreenRatio * Screen.width;
            float absoluteHeight = this.thumbnailToScreenRatio * Screen.height;

            // Then center the play button on the thumbnail
            float playButtonWidth  = Mathf.Min(playButtonTexture.width, this.playButtonToThumbnailRatio * absoluteWidth);
            float playButtonHeight = playButtonWidth;
            float playButtonAbsoluteX = absoluteX + 0.5f * (absoluteWidth  - playButtonWidth);
            // GUI.Label screen coords origin is top left, unlike thumbnail texture :(
            float playButtonAbsoluteY = (Screen.height - absoluteY) - 0.5f * (absoluteHeight + playButtonHeight);

            // Save it for later use in OnGUI
            playButtonLocationAndSize = new Rect(playButtonAbsoluteX, playButtonAbsoluteY, playButtonWidth, playButtonHeight);

            transform.position = Vector3.zero;
            transform.localScale = Vector3.zero;

            this.theGuiTexture.pixelInset = new Rect(absoluteX, absoluteY, absoluteWidth, absoluteHeight);
            this.theGuiTexture.texture = loader.texture;
            EnableThumbnail(true);
        }
    }

    // Method to actually load and set the thumbnail from the file URL
    private void SetThumbnailTextureToFilepath(string filepath)
    {
        WWW loader = new WWW("file://" + filepath);
        StartCoroutine(WaitForLoadToFinishAndThenSetThumbnail(loader));
    }
#endif
}
