using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanteenInstructionScript : MonoBehaviour
{
   public RenderTexture rt; 
    public VideoPlayer videoPlayer;
    public Text instructionText, controlsMoveText, controlsInteractText;
    public VideoClip videoClip;
    public TextAsset[] instructions = new TextAsset[1], controlsMove = new TextAsset[1], controlsInteract = new TextAsset[1];
    private float starttime;
    public ChangeInstruction instance;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.clip = videoClip;
        instructionText.text = instructions[0].ToString();
        controlsMoveText.text = controlsMove[0].ToString();
        controlsInteractText.text = controlsInteract[0].ToString();
        videoPlayer.Play();
        starttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
