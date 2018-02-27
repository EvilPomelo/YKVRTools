using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Gaze : MonoBehaviour
{
    private RectTransform rect;
    private AsyncOperation mAsyncOperation;
    private RectTransform aim;
    //private LineRenderer line;
    RaycastHit hit;
    // 初始化数据
    void Start()
    {
        rect = GameObject.Find("UIRoot/Canvas/RayTarget/ProgressBar").GetComponent<RectTransform>();
        aim = GameObject.Find("UIRoot/Canvas/Aim").GetComponent<RectTransform>();
        //line = transform.GetComponent<LineRenderer>();
        //line.SetPosition(0, transform.position);
        mAsyncOperation = SceneManager.LoadSceneAsync("Scene1Day");
        mAsyncOperation.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(mAsyncOperation.progress);
        //编辑器模式下画出该射线
        Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
        //向前1000米发出射线
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000))
        {
            //line.SetPosition(1, hit.point);

            //设置准星坐标为射线碰撞点
            aim.position = hit.point;
            //射线射到图片上则进度条增加
            if (hit.transform.name == "RayTarget")
            {
                rect.sizeDelta += new Vector2(50 * Time.deltaTime, 0);
            }
            //进度条满了的话
            if (rect.sizeDelta.x >= 200)
            {
                mAsyncOperation.allowSceneActivation = true;
            }
            //射线如果碰撞的是背景
            if (hit.transform.name == "BackGround")
            {
                rect.sizeDelta = new Vector2(0, rect.sizeDelta.y);
            }
        }
    }
}
