using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Video;
using TMPro;


public class RegularImageTracking : MonoBehaviour
{
    [Header("AR")]
    [SerializeField] GameObject[] placeAblePrefabs;
    [SerializeField] XRReferenceImageLibrary imageLibrary;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    AudioSource music;

    [Header("Quiz")]
    [SerializeField] GameObject Quiz;

    [Header("Music Volume Control")]
    
    [SerializeField] GameObject MusicVolumeControl;

    [Header("Petunjuk")]
    [SerializeField] GameObject ContainerPetunjuk;
    TextMeshProUGUI petunjuk;

    string prevPrefName = "";

    [SerializeField] GameObject VideoScreenAR;
    public VideoPlayer currentVideoPlayer;


    private void Awake() {

        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        trackedImageManager.enabled = true;
        music = FindObjectOfType<AudioSource>();
        petunjuk = ContainerPetunjuk.GetComponentInChildren<TextMeshProUGUI>();
        petunjuk.text = "[X]  Arahkan kamera QR Code yang disediakan";
        music.Play();

        foreach (GameObject prefab in placeAblePrefabs)
        {
            
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.SetActive(false);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);

        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        
        Vector3 position = trackedImage.transform.position;
        // Vector3 position = trackedImage.transform.position;
        var pref = spawnedPrefabs[imageName];
       
       
       if (trackedImage.trackingState == TrackingState.Tracking )
       {
            //     foreach (GameObject go in spawnedPrefabs.Values)
            // {
            //     if (go.name != name)
            //     {
            //         go.SetActive(false);
            //     } else {
            //         go.transform.position = position;
            //         go.SetActive(true);
            //     }
            // }
            ContainerPetunjuk.SetActive(false);
            music.Pause();

            if (prevPrefName != "" )
            {
                if (prevPrefName == imageName)
                {                  
                    if (imageName.Contains("video"))
                    {
                        
                        MusicVolumeControl.SetActive(false);
                        var video = pref.GetComponentInChildren<VideoPlayer>();
                        currentVideoPlayer = video;
                        VideoScreenAR.SetActive(true);
                        video.transform.position = trackedImage.transform.position;
                        video.transform.localScale = new Vector3(0.16f,0.09f,1.0f);
                        video.transform.rotation = Quaternion.identity;
                        video.gameObject.SetActive(true);

                        currentVideoPlayer.Play();
                    }
                    else if (imageName.Contains("quiz"))
                    {
                        // SceneManager.LoadScene("Quiz");
                        Quiz.SetActive(true);
                    }
                    else if (imageName.Contains("text"))
                    {
                        Application.OpenURL("https://pasak-book.web.app/");
                    }
                    
                } else if (prevPrefName != imageName)
                {
                    if (imageName.Contains("video"))
                    {
                        VideoScreenAR.SetActive(false);
                        spawnedPrefabs[prevPrefName].SetActive(false);
                        currentVideoPlayer = null;
                    }
                    else if (imageName.Contains("quiz"))
                    {
                        // SceneManager.LoadScene("Main");
                        if (Quiz.activeInHierarchy)
                        {
                            Quiz.SetActive(false);
                        }
                    }
                    else if (imageName.Contains("text"))
                    {
                        return;
                    }
                   
                }
                
            } 
           
            
        } else if (trackedImage.trackingState == TrackingState.Limited){
            ContainerPetunjuk.SetActive(true);
            music.Play();
            if (pref.name == imageName)
            {
                return;
            } else {
                if (imageName.Contains("video"))
                {
                    VideoScreenAR.SetActive(false);
                    var video = pref.GetComponentInChildren<VideoPlayer>();
                    currentVideoPlayer.Pause();
                    pref.SetActive(false);
                    currentVideoPlayer = null;
                }
                else if (imageName.Contains("quiz"))
                {
                    // SceneManager.LoadScene("Main");
                    if (Quiz.activeInHierarchy)
                    {
                        Quiz.SetActive(false);
                    }
                }
                else if (imageName.Contains("text"))
                {
                    return;
                }
            }
        }
        prevPrefName = imageName;
     
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
           
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
           // spawnedPrefabs[trackedImage.name].SetActive(false);
        }
    }

    private void OnEnable() {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable() {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }
}
