using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Globe
{
    public static string nextSceneName;
}
public class AsycLoading : MonoBehaviour {

    public RectTransform ima;

    public Text loadingText;

    private float targetValue;

    private AsyncOperation operation;

    // Use this for initialization  
    void Start()
    {
        ima.sizeDelta=new Vector2(0,ima.sizeDelta.y);
            //启动协程  
            StartCoroutine(AsyncLoading());
    }

    IEnumerator AsyncLoading()
    {
        yield return new WaitForEndOfFrame();
        operation = SceneManager.LoadSceneAsync(Globe.nextSceneName);
        //阻止当加载完成自动切换  
        operation.allowSceneActivation = false;
        yield return operation;
    }

    // Update is called once per frame  
    void Update()
    {
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9  
            targetValue = 1.0f;
        }

        ima.sizeDelta = new Vector2(280 * targetValue, ima.sizeDelta.y);

        loadingText.text = ((int)(targetValue * 100)).ToString() + "%";

        if (targetValue >=1)
        {
            //允许异步加载完毕后自动切换场景  
            operation.allowSceneActivation = true;
        }
    }
}

