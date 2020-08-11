using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class playvideo : MonoBehaviour
{
    public VideoPlayer player;
    public RawImage image;
    RenderTexture text;
    // Start is called before the first frame update
    void Start()
    {
        text = new RenderTexture((int)player.clip.width, (int)player.clip.height, 0);
 
         player.targetTexture = text;
         image.texture = text;
 
         Vector3 scale = image.transform.localScale;
 
        // scale.y = player.clip.height / (float)player.clip.width * scale.y;
 
         image.transform.localScale = scale;
         player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
