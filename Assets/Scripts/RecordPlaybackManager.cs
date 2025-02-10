using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlaybackManager : MonoBehaviour
{
    [System.Serializable]
    public class ActionRecord
    {
        public string actionType;
        public float time; // Relative time of this action
    }

    public List<ActionRecord> recordedActions = new List<ActionRecord>();
    private bool isRecording = false;
    private bool isPlaying = false;
    private float startTime; // Start time of the first action
    private float lastActionTime = 0f; // Time of the last recorded action
    private int currentActionIndex = 0;
    private string lastRecordedAction = "";
    private float actionCooldown = 0.2f; // Minimum interval between repeated actions
    private float lastActionRecordedTime = 0f;

    public PlayerController player;
    public ButtonManager buttonManager; // Reference to ButtonManager

    public void StartRecording()
    {
        if (isPlaying) return;

        isRecording = true;
        recordedActions.Clear();
        startTime = 0f; // Reset startTime
        lastActionTime = 0f; // Reset lastActionTime

        player.SetControl(false); // Disable movement during recording
        buttonManager.OnRecordingPress(); // Update button states
    }

    public void StopRecording()
    {
        if (!isRecording) return;

        isRecording = false;

        // Adjust the timing of the last action to remove idle delay
        if (recordedActions.Count > 0)
        {
            recordedActions[recordedActions.Count - 1].time = Time.time - startTime;
        }

        player.SetControl(false); // Still disable movement
        buttonManager.OnStopPress(); // Update button states
    }

    public void StartPlayback()
    {
        if (isRecording || recordedActions.Count == 0) return;

        isPlaying = true;
        currentActionIndex = 0;
        player.SetControl(false); // Prevent manual input
        buttonManager.OnPlaybackPress(); // Update button states
        StartCoroutine(PlaybackRoutine());
    }

    private IEnumerator PlaybackRoutine()
    {
        while (currentActionIndex < recordedActions.Count)
        {
            var action = recordedActions[currentActionIndex];

            // Wait for the relative timing between actions
            if (currentActionIndex == 0)
            {
                yield return new WaitForSeconds(action.time);
            }
            else
            {
                float waitTime = action.time - recordedActions[currentActionIndex - 1].time;
                yield return new WaitForSeconds(waitTime);
            }

            // Execute the recorded action
            ExecuteAction(action.actionType);

            currentActionIndex++;
        }

        isPlaying = false;
        player.SetControl(false); // Disable movement after playback finishes

        // Reset button states after playback ends
        buttonManager.OnStopPress(); // Simulate stop button state to re-enable recording
    }

    private void RecordActionOnce(string actionType)
    {
        float currentTime = Time.time;

        // Prevent rapid duplicate recordings
        if (actionType != lastRecordedAction || currentTime - lastActionRecordedTime > actionCooldown)
        {
            RecordAction(actionType);
            lastRecordedAction = actionType;
            lastActionRecordedTime = currentTime;
        }
    }

    private void Update()
    {
        if (!isRecording && !isPlaying)
        {
            player.SetControl(false);
        }

        if (isRecording)
        {
            // Ghi nhận di chuyển liên tục khi giữ phím
            if (Input.GetKey(KeyCode.LeftArrow))
                RecordActionOnce("MoveLeft");

            if (Input.GetKey(KeyCode.RightArrow))
                RecordActionOnce("MoveRight");

            if (Input.GetKeyUp(KeyCode.LeftArrow))
                RecordActionOnce("StopMove");
            if (Input.GetKeyUp(KeyCode.RightArrow))
                RecordActionOnce("StopMove");


            // Ghi nhận hành động nhảy (chỉ ghi 1 lần khi nhấn)
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                    RecordActionOnce("JumpLeft");
                else if (Input.GetKey(KeyCode.RightArrow))
                    RecordActionOnce("JumpRight");
                else
                    RecordActionOnce("JumpUp");
            }
        }
    }


    private void RecordAction(string actionType)
    {
        float currentTime = Time.time;

        // Set the startTime to the time of the first action
        if (startTime == 0f)
        {
            startTime = currentTime;
        }

        // Record the action relative to the first action
        recordedActions.Add(new ActionRecord
        {
            actionType = actionType,
            time = currentTime - startTime // Time since the first action
        });

        lastActionTime = currentTime; // Update the last action time
    }

    private void ExecuteAction(string actionType)
    {
        switch (actionType)
        {
            case "MoveLeft":
                player.Move(-1);  // Di chuyển trái liên tục
                break;
            case "MoveRight":
                player.Move(1);   // Di chuyển phải liên tục
                break;
            case "JumpLeft":
                player.ResetHorizontalVelocity();  // Reset vận tốc ngang
                player.JumpAndMove(-1);           // Nhảy sang trái
                break;
            case "JumpRight":
                player.ResetHorizontalVelocity();  // Reset vận tốc ngang
                player.JumpAndMove(1);           // Nhảy sang phải
                break;
            case "JumpUp":
                player.ResetHorizontalVelocity();  // Reset vận tốc ngang
                player.JumpAndMove(0);           // Nhảy thẳng
                break;
            case "StopMove":
                player.ResetHorizontalVelocity();  // Reset vận tốc ngang
                break;

        }
    }
}

