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
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(loadingScreen);
        }
        else
            Destroy(this.gameObject);

    }
    
    public void LoadScene(SceneIndex scene)
    {
        if (loadingScreen != null) 
        { 
            loadingScreen.SetActive(true);

            _scenesLoading.Add(SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single));

            StartCoroutine(nameof(GetLoadSceneProgress));
        }
    }

    private IEnumerator GetLoadSceneProgress()
    {
        //loadingScreen.SetActive(true);
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
    LearnToFly = 1,
    CheckpointDash = 2,
    FireExtinguishing = 3,
    CargoHaul = 4
}
