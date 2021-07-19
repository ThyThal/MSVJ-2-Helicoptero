using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MainMenuTweened : MonoBehaviour
{
    public List<GameObject> buttons = new List<GameObject>();
    public float tweenTime = .25f;
    public float tweenOffset = .1f;
    private List<float> buttonsX = new List<float>();

    private void Start()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttonsX.Add(buttons[i].transform.position.x);
            buttons[i].transform.position += Vector3.right * buttons[i].transform.position.x;
        }

        StartCoroutine(nameof(IntroAnimation));
    }
    
    private IEnumerator IntroAnimation()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            LeanTween.moveX(buttons[i], buttonsX[i], tweenTime).setEaseOutBack();
            yield return new WaitForSecondsRealtime(tweenOffset);
        }
    }

    public void ChangeScene(int index)
    {
        SceneChangingScript.instance.LoadScene((SceneIndex)index);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
