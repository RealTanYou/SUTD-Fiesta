using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeInstruction : MonoBehaviour
{
    public RenderTexture rt; 
    public VideoPlayer videoPlayer;
    public Text instructionText, controlsMoveText, controlsInteractText;
    public VideoClip[] videoClips = new VideoClip[3];
    public TextAsset[] instructions = new TextAsset[3], controlsMove = new TextAsset[3], controlsInteract = new TextAsset[3];
    private int nextclip = 0;
    public bool change_clip = false;
    private float starttime;
    public ChangeInstruction instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        videoPlayer.clip = videoClips[nextclip];
        instructionText.text = instructions[nextclip].ToString();
        controlsMoveText.text = controlsMove[nextclip].ToString();
        controlsInteractText.text = controlsInteract[nextclip].ToString();
        nextclip++;
        videoPlayer.Play();
        starttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - starttime > 5.0f && nextclip < videoClips.Length){
            videoPlayer.clip = videoClips[nextclip];
            instructionText.text = instructions[nextclip].ToString();
            controlsMoveText.text = controlsMove[nextclip].ToString();
            controlsInteractText.text = controlsInteract[nextclip].ToString();
            nextclip++;
            videoPlayer.Play();
            starttime = Time.time;
        }
        if(nextclip >= videoClips.Length){
            nextclip = 0;
        }
    }
}
