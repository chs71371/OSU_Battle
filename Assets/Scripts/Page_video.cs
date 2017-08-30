using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Video;
 
public class Page_video : MonoBehaviour
{
   
    public Image Img_plane = null;
    public VideoPlayer player;


    public void Awake()
    {
        Img_plane = gameObject.transform.Find("Img_plane").GetComponent<Image>();
        player = gameObject.transform.Find("Img_plane").GetComponent<VideoPlayer>();
        //mmt = gameObject.AddComponent<MobileMovieTexture>();
        //Material[] MovieMaterial = new Material[1];
        //MovieMaterial[0] = Img_plane.material;
        //mmt.m_movieMaterials = MovieMaterial;
        //mmt.PlayAutomatically = false;
        //mmt.LoopCount = 0;

        player.isLooping = true;
        player.Prepare();

    }

    public void Start()
    {
        play();
    }


    public void play()
    {
        player.Play();
    }


 
 
}
