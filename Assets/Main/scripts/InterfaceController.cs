
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class InterfaceController : MonoBehaviour
{
    [Header("Game Mode")]
    [SerializeField] GameObject classModeManager;

    RegularImageTracking regularImageTracking;

    [Header("UI")]
    [SerializeField] GameObject hintBox;
    [SerializeField] GameObject chooseGameMode;
    [SerializeField] GameObject backButtonBox;
    [SerializeField] GameObject closeButtonBox;
    [SerializeField] Image volumeIcon;

    [SerializeField] Sprite volumeMute;
    [SerializeField] Sprite volumeUnmute;

    [Header("Credit")]
    [SerializeField] GameObject CreditScreen;

    public TextMeshProUGUI hint;
    Slider volumeController;

    AudioSource music;

    

    void Awake() 
    {
        music = FindObjectOfType<AudioSource>();
        volumeController = GetComponentInChildren<Slider>();
        regularImageTracking = FindObjectOfType<RegularImageTracking>();
        hint = hintBox.GetComponentInChildren<TextMeshProUGUI>();
        backButtonBox.SetActive(false);
        closeButtonBox.SetActive(true);
        hintBox.SetActive(false);
        music.gameObject.SetActive(true);
    }
    public void ChooseClassMode()
    {
        chooseGameMode.SetActive(false);
        classModeManager.SetActive(true);
        hintBox.SetActive(true);
        hint.text = "[X]  Arahkan kamera pada ruangan atau wilayah yang luas. Kemudian ketuk pada bidang titik putih";
        backButtonBox.SetActive(true);
        closeButtonBox.SetActive(false);
    }

    public void ChooseRegularMode()
    {
        chooseGameMode.SetActive(false);
        regularImageTracking.enabled = true;
        hintBox.SetActive(true);
        hint.text = "[X]  Dekatkan QR Code pada kamera dan pastikan gambar terlihat jelas";
        backButtonBox.SetActive(true);
        closeButtonBox.SetActive(false);
    }

    public void ChooseCreditScreen()
    {
        chooseGameMode.SetActive(false);
        CreditScreen.SetActive(true);
        backButtonBox.SetActive(true);
        closeButtonBox.SetActive(false);
    }

    public void BackToChooseMode()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    
    public void SliderValueChaned()
    {
        volumeController.onValueChanged.AddListener((value) =>  music.volume = value);
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
