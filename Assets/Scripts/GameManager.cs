////using UnityEngine;
////using TMPro;
////using System.Collections;
////using UnityEngine.SceneManagement;

////public class GameManager : MonoBehaviour
////{
////    public static GameManager instance;


////    [SerializeField] private TMP_Text coinText;

////    [SerializeField] private PlayerController playerController;

////    private int coinCount = 0;
////    private int gemCount = 0;
////    private bool isGameOver = false;
////    private Vector3 playerPosition;

////    //Level Complete

////    [SerializeField] GameObject levelCompletePanel;
////    [SerializeField] TMP_Text leveCompletePanelTitle;
////    [SerializeField] TMP_Text levelCompleteCoins;





////    private int totalCoins = 0;




////    private void Awake()
////    {
////        instance = this;
////        Application.targetFrameRate = 60;
////    }

////    private void Start()
////    {
////        UpdateGUI();
////        UIManager.instance.fadeFromBlack = true;
////        playerPosition = playerController.transform.position;

////        FindTotalPickups();
////    }

////    public void IncrementCoinCount()
////    {
////        coinCount++;
////        UpdateGUI();
////    }
////    public void IncrementGemCount()
////    {
////        gemCount++;
////        UpdateGUI();
////    }

////    private void UpdateGUI()
////    {
////        coinText.text = coinCount.ToString();

////    }

////    public void Death()
////    {
////        if (!isGameOver)
////        {
////            // Disable Mobile Controls
////            UIManager.instance.DisableMobileControls();
////            // Initiate screen fade
////            UIManager.instance.fadeToBlack = true;

////            // Disable the player object
////            playerController.gameObject.SetActive(false);

////            // Start death coroutine to wait and then respawn the player
////            StartCoroutine(DeathCoroutine());

////            // Update game state
////            isGameOver = true;

////            // Log death message
////            Debug.Log("Died");
////        }
////    }

////    public void FindTotalPickups()
////    {

////        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();

////        foreach (pickup pickupObject in pickups)
////        {
////            if (pickupObject.pt == pickup.pickupType.coin)
////            {
////                totalCoins += 1;
////            }

////        }



////    }
////    public void LevelComplete()
////    {



////        levelCompletePanel.SetActive(true);
////        leveCompletePanelTitle.text = "LEVEL COMPLETE";



////        levelCompleteCoins.text = "COINS COLLECTED: "+ coinCount.ToString() +" / " + totalCoins.ToString();

////    }

////    public IEnumerator DeathCoroutine()
////    {
////        yield return new WaitForSeconds(1f);
////        playerController.transform.position = playerPosition;

////        // Wait for 2 seconds
////        yield return new WaitForSeconds(1f);

////        // Check if the game is still over (in case player respawns earlier)
////        if (isGameOver)
////        {
////            SceneManager.LoadScene(1);


////        }
////    }

////}


//using UnityEngine;
//using TMPro;
//using System.Collections;
//using UnityEngine.SceneManagement;

//public class GameManager : MonoBehaviour
//{
//    public static GameManager instance;

//    [SerializeField] private TMP_Text coinText;
//    [SerializeField] private PlayerController playerController;
//    [SerializeField] private GameObject[] heartIcons; // 3 images de cœur (rouge ou noir)
//    [SerializeField] private GameObject levelCompletePanel;
//    [SerializeField] private TMP_Text leveCompletePanelTitle;
//    [SerializeField] private TMP_Text levelCompleteCoins;

//    private int coinCount = 0;
//    private int gemCount = 0;
//    private int totalCoins = 0;
//    private int lives = 3; // ❤️❤️❤️
//    private bool isGameOver = false;
//    private Vector3 playerStartPosition;

//    private void Awake()
//    {
//        instance = this;
//        Application.targetFrameRate = 60;
//    }

//    private void Start()
//    {
//        UIManager.instance.fadeFromBlack = true;
//        playerStartPosition = playerController.transform.position;
//        UpdateGUI();
//        FindTotalPickups();
//    }

//    // -----------------------
//    // 💰 COINS & GUI
//    // -----------------------
//    public void IncrementCoinCount()
//    {
//        coinCount++;
//        UpdateGUI();
//    }

//    public void IncrementGemCount()
//    {
//        gemCount++;
//        UpdateGUI();
//    }

//    private void UpdateGUI()
//    {
//        coinText.text = coinCount.ToString();
//    }

//    // -----------------------
//    // 💀 MORT DU JOUEUR
//    // -----------------------
//    public void Death()
//    {
//        if (!isGameOver)
//        {
//            StartCoroutine(HandleDeath());
//        }
//    }

//    private IEnumerator HandleDeath()
//    {
//        // Désactive le joueur pendant 1 seconde
//        UIManager.instance.DisableMobileControls();
//        UIManager.instance.fadeToBlack = true;
//        playerController.gameObject.SetActive(false);

//        yield return new WaitForSeconds(1f);

//        // Retire une vie
//        lives--;
//        UpdateHearts();

//        // Si encore des vies, respawn
//        if (lives > 0)
//        {
//            RespawnPlayer();
//        }
//        else
//        {
//            StartCoroutine(GameOver());
//        }
//    }

//    private void RespawnPlayer()
//    {
//        playerController.transform.position = playerStartPosition;
//        playerController.gameObject.SetActive(true);
//        UIManager.instance.fadeToBlack = false;
//        UIManager.instance.fadeFromBlack = true;
//    }

//    private IEnumerator GameOver()
//    {
//        Debug.Log("GAME OVER");
//        yield return new WaitForSeconds(1.5f);
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // redémarre le niveau
//    }

//    // -----------------------
//    // 💖 COEURS
//    // -----------------------
//    private void UpdateHearts()
//    {
//        for (int i = 0; i < heartIcons.Length; i++)
//        {
//            if (i < lives)
//                heartIcons[i].SetActive(true); // cœur rouge
//            else
//                heartIcons[i].SetActive(false); // cœur noir
//        }
//    }

//    // -----------------------
//    // 🧩 PICKUPS & FIN DE NIVEAU
//    // -----------------------
//    public void FindTotalPickups()
//    {
//        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();
//        foreach (pickup pickupObject in pickups)
//        {
//            if (pickupObject.pt == pickup.pickupType.coin)
//                totalCoins++;
//        }
//    }

//    public void LevelComplete()
//    {
//        levelCompletePanel.SetActive(true);
//        leveCompletePanelTitle.text = "LEVEL COMPLETE";
//        levelCompleteCoins.text = "COINS COLLECTED: " + coinCount + " / " + totalCoins;

//        StartCoroutine(LoadNextLevel());
//    }

//    private IEnumerator LoadNextLevel()
//    {
//        yield return new WaitForSeconds(2f);
//        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

//        // Si tu n’as que deux levels :
//        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
//            SceneManager.LoadScene(0); // retourne au premier niveau
//        else
//            SceneManager.LoadScene(nextIndex);
//    }
//}


//using UnityEngine;
//using TMPro;
//using System.Collections;
//using UnityEngine.SceneManagement;

//public class GameManager : MonoBehaviour
//{
//    public static GameManager instance;

//    [Header("UI Elements")]
//    [SerializeField] private TMP_Text coinText;
//    [SerializeField] private GameObject[] hearts;
//    [SerializeField] private GameObject levelCompletePanel;
//    [SerializeField] private TMP_Text levelCompleteTitle;
//    [SerializeField] private TMP_Text levelCompleteCoins;
//    [SerializeField] private UnityEngine.UI.Image deathFlashOverlay; // 🔴 Image rouge semi-transparente dans le Canvas

//    [Header("Player Reference")]
//    [SerializeField] private PlayerController playerController;

//    [Header("Audio")]
//    [SerializeField] private AudioSource audioSource;
//    [SerializeField] private AudioClip coinSound;
//    [SerializeField] private AudioClip gemSound;
//    [SerializeField] private AudioClip deathSound;
//    [SerializeField] private AudioClip levelCompleteSound;
//    [SerializeField] private AudioClip gameOverSound;

//    [Header("Hearts Sprites")]
//    [SerializeField] private Sprite fullHeart;
//    [SerializeField] private Sprite emptyHeart;

//    private int coinCount = 0;
//    private int gemCount = 0;
//    private int totalCoins = 0;
//    private int lives = 3;
//    private bool isGameOver = false;
//    private Vector3 startPosition;

//    private void Awake()
//    {
//        instance = this;
//        Application.targetFrameRate = 60;
//    }

//    private void Start()
//    {
//        UIManager.instance.fadeFromBlack = true;
//        startPosition = playerController.transform.position;
//        FindTotalPickups();
//        UpdateGUI();

//        if (deathFlashOverlay != null)
//            deathFlashOverlay.color = new Color(1, 0, 0, 0);
//    }

//    // ---------- AUDIO ----------
//    private void PlaySound(AudioClip clip)
//    {
//        if (audioSource != null && clip != null)
//            audioSource.PlayOneShot(clip);
//    }

//    // ---------- PICKUPS ----------
//    public void IncrementCoinCount()
//    {
//        coinCount++;
//        PlaySound(coinSound);
//        UpdateGUI();
//    }

//    public void IncrementGemCount()
//    {
//        gemCount++;
//        PlaySound(gemSound);
//        UpdateGUI();
//    }

//    private void UpdateGUI()
//    {
//        if (coinText != null)
//            coinText.text = coinCount.ToString();
//    }

//    // ---------- DEATH / RESPAWN ----------
//    public void Death()
//    {
//        if (isGameOver) return;

//        PlaySound(deathSound);
//        UIManager.instance.DisableMobileControls();
//        UIManager.instance.fadeToBlack = true;

//        StartCoroutine(FlashRed());
//        playerController.gameObject.SetActive(false);
//        StartCoroutine(DeathCoroutine());
//        isGameOver = true;

//        Debug.Log("Player died.");
//    }

//    private IEnumerator FlashRed()
//    {
//        if (deathFlashOverlay == null)
//            yield break;

//        // Monte le rouge rapidement
//        for (float a = 0; a < 0.5f; a += Time.deltaTime * 3f)
//        {
//            deathFlashOverlay.color = new Color(1, 0, 0, a);
//            yield return null;
//        }

//        // Puis redescend
//        for (float a = 0.5f; a > 0; a -= Time.deltaTime * 2f)
//        {
//            deathFlashOverlay.color = new Color(1, 0, 0, a);
//            yield return null;
//        }
//    }

//    private IEnumerator DeathCoroutine()
//    {
//        yield return new WaitForSeconds(1.5f);

//        lives--;
//        UpdateHearts();

//        if (lives <= 0)
//        {
//            PlaySound(gameOverSound);
//            Debug.Log("GAME OVER");
//            yield return new WaitForSeconds(1f);
//            SceneManager.LoadScene(1);
//        }
//        else
//        {
//            playerController.transform.position = startPosition;
//            playerController.gameObject.SetActive(true);
//            UIManager.instance.fadeToBlack = false;
//            UIManager.instance.fadeFromBlack = true;
//            UIManager.instance.EnableMobileControls();
//            isGameOver = false;
//        }
//    }

//    private void UpdateHearts()
//    {
//        for (int i = 0; i < hearts.Length; i++)
//        {
//            var image = hearts[i].GetComponent<UnityEngine.UI.Image>();
//            if (image != null)
//                image.sprite = i < lives ? fullHeart : emptyHeart;
//        }
//    }

//    // ---------- PICKUP SYSTEM ----------
//    public void FindTotalPickups()
//    {
//        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();
//        totalCoins = 0;
//        foreach (pickup p in pickups)
//        {
//            if (p.pt == pickup.pickupType.coin)
//                totalCoins++;
//        }
//    }

//    // ---------- LEVEL COMPLETE ----------
//    public void LevelComplete()
//    {
//        if (playerController != null)
//            playerController.gameObject.SetActive(false);

//        UIManager.instance.DisableMobileControls();
//        UIManager.instance.fadeToBlack = true;
//        PlaySound(levelCompleteSound);

//        StartCoroutine(ShowLevelCompletePanel());
//    }

//    private IEnumerator ShowLevelCompletePanel()
//    {
//        // 🔒 Désactive tout sauf UI, caméra et GameManager
//        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
//        {
//            if (go == UIManager.instance.gameObject) continue;
//            if (go == GameManager.instance.gameObject) continue;
//            if (go.layer == LayerMask.NameToLayer("UI")) continue;
//            if (go.GetComponent<Camera>() != null) continue;
//            go.SetActive(false);
//        }

//        yield return new WaitForSeconds(1f); // petit délai post-fondu

//        // 🎉 Affiche le panneau avec animation
//        levelCompletePanel.SetActive(true);
//        levelCompletePanel.transform.localScale = Vector3.zero;
//        LeanTween.scale(levelCompletePanel, Vector3.one, 0.6f).setEaseOutBack();

//        // Compte à rebours fluide
//        float countdown = 5f;
//        while (countdown > 0f)
//        {
//            countdown -= Time.deltaTime;
//            levelCompleteTitle.text = $"LEVEL COMPLETE — Wait {countdown:F1}s";
//            levelCompleteCoins.text = $"COINS COLLECTED: {coinCount} / {totalCoins}";
//            yield return null;
//        }

//        // Charge la scène suivante
//        int currentScene = SceneManager.GetActiveScene().buildIndex;
//        int nextSceneIndex = (currentScene == 0) ? 2 : 1;
//        SceneManager.LoadSceneAsync(nextSceneIndex);
//    }
//}



using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private TMP_Text levelCompleteTitle;
    [SerializeField] private TMP_Text levelCompleteCoins;
    [SerializeField] private UnityEngine.UI.Image deathFlashOverlay;

    [Header("Player Reference")]
    [SerializeField] private PlayerController playerController;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource; // Pour les effets ponctuels (coin, death, etc.)
    [SerializeField] private AudioSource gameplayMusic; // 🎵 Nouveau : musique d’ambiance en fond
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip levelCompleteSound;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Hearts Sprites")]
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private int coinCount = 0;
    private int gemCount = 0;
    private int totalCoins = 0;
    private int lives = 3;
    private bool isGameOver = false;
    private Vector3 startPosition;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UIManager.instance.fadeFromBlack = true;
        startPosition = playerController.transform.position;
        FindTotalPickups();
        UpdateGUI();

        if (deathFlashOverlay != null)
            deathFlashOverlay.color = new Color(1, 0, 0, 0);

        // 🎵 Démarre la musique d’ambiance dès le début du niveau
        if (gameplayMusic != null)
        {
            gameplayMusic.loop = true;
            gameplayMusic.Play();
        }
    }

    // ---------- AUDIO ----------
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }

    private void StopGameplayMusic()
    {
        if (gameplayMusic != null && gameplayMusic.isPlaying)
            gameplayMusic.Stop();
    }

    // ---------- PICKUPS ----------
    public void IncrementCoinCount()
    {
        coinCount++;
        PlaySound(coinSound);
        UpdateGUI();
    }

    public void IncrementGemCount()
    {
        gemCount++;
        PlaySound(gemSound);
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        if (coinText != null)
            coinText.text = coinCount.ToString();
    }

    // ---------- DEATH / RESPAWN ----------
    public void Death()
    {
        if (isGameOver) return;

        StopGameplayMusic(); // 🛑 coupe la musique d’ambiance
        PlaySound(deathSound);
        UIManager.instance.DisableMobileControls();
        UIManager.instance.fadeToBlack = true;

        StartCoroutine(FlashRed());
        playerController.gameObject.SetActive(false);
        StartCoroutine(DeathCoroutine());
        isGameOver = true;

        Debug.Log("Player died.");
    }

    private IEnumerator FlashRed()
    {
        if (deathFlashOverlay == null)
            yield break;

        for (float a = 0; a < 0.5f; a += Time.deltaTime * 3f)
        {
            deathFlashOverlay.color = new Color(1, 0, 0, a);
            yield return null;
        }

        for (float a = 0.5f; a > 0; a -= Time.deltaTime * 2f)
        {
            deathFlashOverlay.color = new Color(1, 0, 0, a);
            yield return null;
        }
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        lives--;
        UpdateHearts();

        if (lives <= 0)
        {
            StopGameplayMusic(); // 🛑 coupe la musique
            PlaySound(gameOverSound);
            Debug.Log("GAME OVER");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(1);
        }
        else
        {
            playerController.transform.position = startPosition;
            playerController.gameObject.SetActive(true);
            UIManager.instance.fadeToBlack = false;
            UIManager.instance.fadeFromBlack = true;
            UIManager.instance.EnableMobileControls();

            // 🔁 relance la musique d’ambiance après respawn
            if (gameplayMusic != null && !gameplayMusic.isPlaying)
                gameplayMusic.Play();

            isGameOver = false;
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            var image = hearts[i].GetComponent<UnityEngine.UI.Image>();
            if (image != null)
                image.sprite = i < lives ? fullHeart : emptyHeart;
        }
    }

    // ---------- PICKUP SYSTEM ----------
    public void FindTotalPickups()
    {
        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();
        totalCoins = 0;
        foreach (pickup p in pickups)
        {
            if (p.pt == pickup.pickupType.coin)
                totalCoins++;
        }
    }

    // ---------- LEVEL COMPLETE ----------
    public void LevelComplete()
    {
        StopGameplayMusic(); // 🛑 stoppe la musique d’ambiance
        PlaySound(levelCompleteSound);

        if (playerController != null)
            playerController.gameObject.SetActive(false);

        UIManager.instance.DisableMobileControls();
        UIManager.instance.fadeToBlack = true;

        StartCoroutine(ShowLevelCompletePanel());
    }

    private IEnumerator ShowLevelCompletePanel()
    {
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (go == UIManager.instance.gameObject) continue;
            if (go == GameManager.instance.gameObject) continue;
            if (go.layer == LayerMask.NameToLayer("UI")) continue;
            if (go.GetComponent<Camera>() != null) continue;
            go.SetActive(false);
        }

        yield return new WaitForSeconds(1f);

        levelCompletePanel.SetActive(true);
        levelCompletePanel.transform.localScale = Vector3.zero;
        LeanTween.scale(levelCompletePanel, Vector3.one, 0.6f).setEaseOutBack();

        float countdown = 5f;
        while (countdown > 0f)
        {
            countdown -= Time.deltaTime;
            levelCompleteTitle.text = $"LEVEL COMPLETE — Wait {countdown:F1}s";
            levelCompleteCoins.text = $"COINS COLLECTED: {coinCount} / {totalCoins}";
            yield return null;
        }

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentScene == 0) ? 2 : 1;
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }
}
