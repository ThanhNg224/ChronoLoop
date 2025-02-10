using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button recordingButton;
    public Button playbackButton;
    public Button stopButton;

    private Color highlightedColor = Color.green; // Highlighted color
    private Color darkColor = Color.gray;        // Dark/inactive color

    void Start()
    {
        // Initial button states
        HighlightButton(recordingButton); // Only recording button is highlighted at the start
        DarkenButton(playbackButton);
        DarkenButton(stopButton);

        // Disable button interactions initially
        playbackButton.interactable = false;
        stopButton.interactable = false;
    }

    public void OnRecordingPress()
    {
        // Highlight StopButton, disable RecordingButton
        HighlightButton(stopButton);
        DarkenButton(recordingButton);
        DarkenButton(playbackButton);

        // Enable StopButton, disable other buttons
        stopButton.interactable = true;
        recordingButton.interactable = false;
        playbackButton.interactable = false;
    }

    public void OnStopPress()
    {
        // Highlight RecordingButton and PlaybackButton, darken StopButton
        HighlightButton(recordingButton);
        HighlightButton(playbackButton);
        DarkenButton(stopButton);

        // Enable Recording and Playback buttons
        recordingButton.interactable = true;
        playbackButton.interactable = true;
        stopButton.interactable = false;
    }

    public void OnPlaybackPress()
    {
        // Highlight PlaybackButton, darken others
        HighlightButton(playbackButton);
        DarkenButton(recordingButton);
        DarkenButton(stopButton);

        // Disable all buttons during playback
        recordingButton.interactable = false;
        stopButton.interactable = false;
        playbackButton.interactable = false;
    }

    private void HighlightButton(Button button)
    {
        var colors = button.colors;
        colors.normalColor = highlightedColor;
        button.colors = colors;
    }

    private void DarkenButton(Button button)
    {
        var colors = button.colors;
        colors.normalColor = darkColor;
        button.colors = colors;
    }
}
