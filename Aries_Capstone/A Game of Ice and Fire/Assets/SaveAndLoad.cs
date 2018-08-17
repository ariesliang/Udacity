using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
// using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveAndLoad : MonoBehaviour {

    public static PlayerStats localCopyOfData;
    public static bool isSceneBeingLoaded = false;

    public Text levelText;
    public GameObject win;
    public GameObject lose;
    public GameObject fire;
    private AudioSource winAudio;
    private AudioSource loseAudio;
    private AudioSource fireAudio; 

    // Game Menu
    public GameObject menuCanvas;
    public Button saveButton;
    public Button loadButton;
    public Button[] buttons;
    private int currentSelectedButtonIndex;
    private bool tabOpen;

    // Oculus controller 
    private OVRInput.Controller thisController;
    private float menuStickY;
    private bool menuIsSwipable;

    private void Awake()
    {
        thisController = OVRInput.Controller.LTouch ; // oculus
       // buttons = menuCanvas.GetComponentsInChildren<Button>();
        currentSelectedButtonIndex = 0;
        OpenTab();
        if (System.IO.File.Exists("Saves/save.binary")) // check if there are saved data, if yes then enable Load button, else disable it
        {
            Debug.Log("File Exists");
            loadButton.interactable = true;
        }
        else
        {
            Debug.Log("File Not Exists");
            loadButton.interactable = false;
        }
        saveButton.interactable = false; // disable Save button for now 
        winAudio = win.GetComponent<AudioSource>();
        loseAudio = lose.GetComponent<AudioSource>();
        fireAudio = fire.GetComponent<AudioSource>();
    }  
    
    public void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Four) && !tabOpen) 
        { 
            OpenTab(); 
        } 
        else if (OVRInput.Get(OVRInput.Button.Four) && tabOpen)
        {
            CloseTab();
        }  
        if (tabOpen)
        {
            if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
            {
                menuStickY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, thisController).y;
                // Debug.Log("MenuStickY: " + menuStickY);
                if (menuStickY < 0.45f && menuStickY > -0.45f)
                {
                    menuIsSwipable = true;
                }
                if (menuIsSwipable)
                {
                    // Debug.Log("Menu is swipable");
                    if (menuStickY >= 0.45f)
                    { 
                        menuIsSwipable = false;
                        ScrollMenu(false);
                    }
                    else if (menuStickY <= -0.45f)
                    { 
                        menuIsSwipable = false;
                        ScrollMenu(true);
                    }
                } 
            }
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            { 
                string name = EventSystem.current.currentSelectedGameObject.name;
                Debug.Log("name" + name);
                foreach (Button b in buttons)
                {
                    if (name.Equals(b.gameObject.name))
                    {
                        b.onClick.Invoke();
                    }
                } 
                CloseTab();
            }
        }
    }  
    
    public void StartNewGame ()
    {
        Debug.Log("New game called");
        GameLogic.instance.StartNewGame();
        saveButton.interactable = true;
        CloseTab();
    }

    public void Save ()
    {
        Debug.Log("Save called");
        SaveData();
        CloseTab();
    }

    public void Load ()
    {
        Debug.Log("Load called");
        LoadData();
        isSceneBeingLoaded = true;
        GameLogic.instance.LoadLastGame();
        saveButton.interactable = true;
        CloseTab();
    }

    public void ExitGame ()
    {
        Debug.Log("Exit called");
       // if (Application.isEditor) UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void GameOver(bool win)
    { 
        if (isSceneBeingLoaded) // delete saved data if currently using it 
        {
            System.IO.File.Delete("Saves/save.binary");
        } 
        fireAudio.Stop();
        if (win)
        { 
            levelText.text = "Congrats!";
            StartCoroutine(playSound(winAudio)); 
        }
        else
        {
            levelText.text = "Game Over!";
            StartCoroutine(playSound(loseAudio));
        };
        Awake();
        OpenTab();
    } 

    IEnumerator playSound(AudioSource audio)
    { 
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length + 2); 
    } 
    
    private void SaveData()
    {
        if (!Directory.Exists("Saves")) Directory.CreateDirectory("Saves");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");
        localCopyOfData = GameLogic.instance.playerStats;

        formatter.Serialize(saveFile, localCopyOfData);
        saveFile.Close();
    }

    private void LoadData()
    {
        Debug.Log("Load data is called");
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
        localCopyOfData = (PlayerStats)formatter.Deserialize(saveFile);
        saveFile.Close();
    }

    private void OpenTab()
    {
        tabOpen = true;
        menuCanvas.SetActive(true);
        Time.timeScale = 0.0f; 
        buttons[0].Select();
        buttons[0].OnSelect(null);
    } 

    private void CloseTab ()
    {
        tabOpen = false;
        menuCanvas.SetActive(false);
        Time.timeScale = 1.0f;  
    }
    
    private void ScrollMenu(bool up)
    {
        Debug.Log("Scroll Menu Called");
        /*
        if (up)
        {
            currentSelectedButtonIndex--;
            while ((currentSelectedButtonIndex) >= 0 && !buttons[currentSelectedButtonIndex].interactable)
            {
                currentSelectedButtonIndex--;
            }
        }
        else
        {
            currentSelectedButtonIndex++;
            while (currentSelectedButtonIndex < buttons.Length && !buttons[currentSelectedButtonIndex].interactable)
            {
                currentSelectedButtonIndex++;
            }
        }
        if (currentSelectedButtonIndex < buttons.Length && currentSelectedButtonIndex >= 0)
        {
            buttons[currentSelectedButtonIndex].Select();
            buttons[currentSelectedButtonIndex].OnSelect(null);
        }*/
    } 
}
