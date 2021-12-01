using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class VideoScreen : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] Image volumeIcon;

    [SerializeField] Sprite volumeMute;
    [SerializeField] Sprite volumeUnmute;

    [SerializeField] GameObject musicVolumeControl;

    [Header("Play-Pause")]
    [SerializeField] Image playIcon;
    [SerializeField] Sprite playVideo;

    [SerializeField] Sprite pauseVideo;

    Slider volumeController;

    Slider videoSlider;

    [Header("videos")] 
    [SerializeField] VideoPlayer[] videos;

    int currentPlayerIndex = 0;

    VideoPlayer currentPlayer;
    AudioSource music;

    bool isDragged;

    void Awake() 
    {
        music = FindObjectOfType<AudioSource>();
        music.Pause();
        musicVolumeControl.SetActive(false);
        InitializingSlider();
        videos[currentPlayerIndex].gameObject.SetActive(true);
        videos[currentPlayerIndex].enabled = true;
        videos[currentPlayerIndex].Play();
        videoSlider.maxValue = ((float)videos[currentPlayerIndex].length);
        videoSlider.minValue = 0f;   
        
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
        if (!currentPlayer.isPlaying)
        {
            currentPlayer.Play();
            playIcon.sprite = pauseVideo;
        }
        else
        {
            currentPlayer.Pause();
            playIcon.sprite = playVideo;
        }
    }   

    private void Update() {
        currentPlayer = videos[currentPlayerIndex];
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

    public void PlayForward()
    {
        if (currentPlayerIndex < videos.Length)
        {
            currentPlayer.Stop();
            currentPlayer.enabled = false;
            currentPlayer.gameObject.SetActive(false);
            currentPlayerIndex++;
            Debug.Log(currentPlayerIndex);
            videos[currentPlayerIndex].gameObject.SetActive(true);
            videos[currentPlayerIndex].enabled = true;
            videos[currentPlayerIndex].Play();
        }
    }

    public void PlayBackWard()
    {
        if (currentPlayerIndex > 0)
        {
            currentPlayer.Stop();
            currentPlayer.enabled = false;
            currentPlayer.gameObject.SetActive(false);
            currentPlayerIndex--;
            Debug.Log(currentPlayerIndex);
            videos[currentPlayerIndex].gameObject.SetActive(true);
            videos[currentPlayerIndex].enabled = true;            
            videos[currentPlayerIndex].Play();
        }
    }
    public void VideoVolumeChanegd()
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

    private void OnDisable() {
        currentPlayer.Pause();
        music.Play();
    }
}
