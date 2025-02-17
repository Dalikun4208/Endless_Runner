using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneryManager : Singleton<SceneryManager>
{
    [SerializeField] Image screenImage;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(AsyncLoad(1));
        }
    }

    public IEnumerator FadeIn()
    {
        screenImage.gameObject.SetActive(true);

        Color color = screenImage.color;

        color.a = 1f;

        while(color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;

            screenImage.color = color;

            yield return null;
        }

        screenImage.gameObject.SetActive(false);
    }

    public IEnumerator AsyncLoad(int index)
    {
        screenImage.gameObject.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);

        // { allowSceneActivation } 
        // 장면이 준비된 즉시 장면이 활성화되는 것을 허용하는 변수입니다.
        asyncOperation.allowSceneActivation = false;

        Color color = screenImage.color;

        color.a = 0;

        // { isDone }
        // 해당 동작이 완료되었는지 나타내는 변수입니다. (읽기 전용)
        while (asyncOperation.isDone == false)
        {
            color.a += Time.deltaTime;

            screenImage.color = color;

            // { progress } 
            // 작업의 진행 상태를 나타내는 변수입니다. (읽기 전용)
            if (asyncOperation.progress >= 0.9f)
            {
                color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime);

                screenImage.color = color;

                if(color.a >= 1.0f)
                {
                    asyncOperation.allowSceneActivation = true;

                    yield break;
                }
            }

            yield return null;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        StartCoroutine(FadeIn());
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
