using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerHitSound;
    public static AudioClip birdSound;

    public static AudioClip endSound;
    static AudioSource audioSrc;
    //public static AudioClip birdHitSound;
    // Start is called before the first frame update
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("obstacle_metal");
        birdSound = Resources.Load<AudioClip>("obstacle_bird");
        endSound = Resources.Load<AudioClip>("success_drumroll_AndEating");
        audioSrc = GetComponent<AudioSource>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound (string clip){
        switch(clip){
            case "obstacle_metal":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "obstacle_bird":
                audioSrc.PlayOneShot(birdSound);
                break;
            case "success_drumroll_AndEating":
                audioSrc.PlayOneShot(endSound);
                break;                    
        }
    }
}
