using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*使用说明：
1、在init()方法中调用LoadPoolGameObject方法创建需要实例化的预制体与数量
2、在需要使用对象池对象的脚本中通过PoolManager._instace.GetGameObject2("BulletPlayer").transform方法获取暂未被使用的对象池对象
3、对象实例完成任务需销毁时通过调用 PoolManager._instace.PoolDestroy(gameObject)使其回到对象池
*/


public class PoolManager: MonoBehaviour
{
    //本类单例
    public static PoolManager _instace;
    //对象池的字典
    private Dictionary<string,List<GameObject>> dic;
    //用于存储prefab的游戏对象
    private GameObject go;
    //用于存储prefab实例化后的游戏对象
    private GameObject poolItem;
    //用于存储实例化后的游戏对象的List表
    private List<GameObject> array;
    void Awake()
    {
        if (_instace == null)
        {
            _instace = this;
        }
        dic = new Dictionary<string, List<GameObject>>();
        Init();
    }

    void Init()
    {
        //todo:初始化工作
        // LoadPoolGameObject("BulletPlayer", "BulletPlayer", 80);
        // LoadPoolGameObject("HitEffectLittle", "HitEffectLittle", 40);
        // LoadPoolGameObject("HitEffectBig", "HitEffectBig", 15);
        // LoadPoolGameObject("SkyBoom", "SkyBoom", 40);
    }

    void Start()
    {
    
    }

    //读取需要做成与对象池内容的预制物，存入字典中
    public void LoadPoolGameObject(string path,string name,int num) {
        array = null;
        go=Resources.Load(path) as GameObject;
        array = new List<GameObject>();
        for (int i = 0; i <num; i++)
        {
            poolItem=Instantiate(go);
            poolItem.transform.SetParent(transform);
            poolItem.SetActive(false);
            array.Add(poolItem);
        }
        dic.Add(name, array);
        array = null;
    }

    //得到需要的对象池对象
    public GameObject GetGameObject(string name) {
        if (dic.TryGetValue(name, out array))
        {
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i].activeInHierarchy == false)
                {
                        return array[i];
                }
            }
        }
        return null;
    }

    //根据传入对象将其将复位到对象池
    public void PoolDestroy(GameObject poolItem)
    {
        poolItem.transform.localPosition = Vector3.zero;
        poolItem.SetActive(false);
        poolItem.transform.SetParent(transform);
    }


    //由于子弹的拖尾效果原理，在对象池中遍历其数组寻找空第一个物体的的方式会使得刚销毁的物体立马出现在枪口，使得上一发子弹消失的位置到新子弹发射的位置出现拖尾效果
    //因此通过一个静态变量记录当前对象池中对象的位置，从对象池只实例化当前对象后面一个对象，直到最后一个物体也被实例化过，再从头寻找
    private static int count = 0;
    public GameObject GetGameObject2(string name)
    {
        if (dic.TryGetValue(name, out array))
        {
            for (int i = count; i < array.Count; i++)
            {
                count++;
                if (count == (array.Count))
                {
                    count = 0;
                }
                if (array[i].activeInHierarchy == false)
                {
                    return array[i];
                }
            }
        }
        return null;
    }
}
