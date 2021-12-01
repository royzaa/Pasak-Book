using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class VideoScreenAR : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Volume")]
    [SerializeField] Image volumeIcon;

    [SerializeField] Sprite volumeMute;
    [SerializeField] Sprite volumeUnmute;

    [Header("Play-Pause")]
    [SerializeField] Image playIcon;
    [SerializeField] Sprite playVideo;

    [SerializeField] Sprite pauseVideo;

    Slider volumeController;

    Slider videoSlider;

    VideoPlayer currentPlayer;
    AudioSource music;

    bool isDragged;

    void Awake() 
    {
        currentPlayer = FindObjectOfType<RegularImageTracking>().currentVideoPlayer;
        if (currentPlayer != null)
        {
            music = FindObjectOfType<AudioSource>();
            music.Pause();
            InitializingSlider();
            videoSlider.maxValue = ((float)currentPlayer.length);
            videoSlider.minValue = 0f;
        }      
    }
 

    void InitializingSlider() 
    {
       Slider[] Slider = GetComponentsInChildren<Slider>();
       foreach (Slider control in Slider)
       {
           if (control.tag == "volume")
           {
               volumeController = control;
           }
           else if (control.tag == "videos")
           {
                videoSlider = control;
           }
       }
    }
    
    public void PlayVideo()
    {
        if (currentPlayer != null)
        {
            if (currentPlayer.isPaused)
            {
                currentPlayer.Play();
                playIcon.sprite = pauseVideo;
            }
            else if (currentPlayer.isPlaying)
            {
                currentPlayer.Pause();
                playIcon.sprite = playVideo;
            }
        }
    }   

    private void Update() {
        currentPlayer = FindObjectOfType<RegularImageTracking>().currentVideoPlayer;
        if (currentPlayer.isPlaying && !isDragged)
        {
            videoSlider.value = ((float)currentPlayer.time);
        }
    }
    public void OnDrag()
    {
        isDragged = true;
    }

    public void OnPointerUp()
    {
        isDragged = false;
    }

    public void VideoTrackChanged()
    {
        if (isDragged)
        {
            currentPlayer.time = videoSlider.value;
        }
    }

    public void VideoVolumeChanegd()
    {
        if (currentPlayer != null)
        {
            volumeController.onValueChanged.AddListener((value) =>  currentPlayer.SetDirectAudioVolume(0, value));
            if (music.volume == 0)
            {
                volumeIcon.sprite = volumeMute;
            }
            else
            {
                volumeIcon.sprite = volumeUnmute;
            }
        }
    }
}
