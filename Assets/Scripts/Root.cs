using UnityEngine;
using UnityEngine.SceneManagement;

public class Root : MonoBehaviour
{
    [SerializeField]
    private CoroutineHelper m_coroutineHelper;
    public CoroutineHelper CoroutineHelper { get { return m_coroutineHelper; } }

    // Singleton
    private static Root m_instance = null;
    public static Root Instance
    {
        get { return m_instance; }
    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    /// <summary>
    /// Initializes the root.
    /// </summary>
    private void Initialize()
    {
        SceneManager.LoadScene("Dungeon1", LoadSceneMode.Additive);
    }
}
