using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneChangingScript : MonoBehaviour
{
    public static SceneChangingScript instance;
    public GameObject loadingScreen;
    public Image percentageBar;
    public TextMeshProUGUI percentageText;

    private int _loadPercentage = 0;
    private float _totalSceneProgress = 0;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(loadingScreen);
    }
    
    public void LoadScene(SceneIndex scene)
    {
        loadingScreen.SetActive(true);

        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single));

        StartCoroutine(nameof(GetLoadSceneProgress));
    }

    private IEnumerator GetLoadSceneProgress()
    {
        for (int i = 0; i < _scenesLoading.Count; i++)
        {
            while (!_scenesLoading[i].isDone)
            {
                _totalSceneProgress = 0;

                foreach (AsyncOperation op in _scenesLoading)
                {
                    _totalSceneProgress += op.progress;
                }

                _totalSceneProgress = (_totalSceneProgress / _scenesLoading.Count) * 100f;
                _loadPercentage = Mathf.RoundToInt(_totalSceneProgress);
                percentageText.text = _loadPercentage.ToString() + "%";
                percentageBar.fillAmount = _totalSceneProgress / _scenesLoading.Count;

                yield return null;
            }
        }

        loadingScreen.SetActive(false);
    }
}

public enum SceneIndex
{
    MainMenu = 0,
    GameDeliver = 1,
    GameWater = 2,
    Scene3 = 3
}
