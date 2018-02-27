using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrintPosition {
    [MenuItem("GameObject/Print/PrintAllChildrenWorldPosition", false, 10)]
    private static void PrintAllChildrenWorldPosition()
    {
        string str=null;
        if (Selection.transforms.Length <= 1)
        {
            Transform select = Selection.transforms[0];
            Transform[] trans=select.GetComponentsInChildren<Transform>();
            for (int i = 1; i <trans.Length; i++)
            {
                str+=trans[i].position.x+","+trans[i].position.y+","+trans[i].position.z;
                if (i != trans.Length - 1)
                {
                    str += ",";
                }
            }
            Debug.Log(str);
        }
        else
        {
            Debug.LogError("禁止多选");
        }
    }

    [MenuItem("GameObject/Print/PrintAllChildrenLocalPosition", false, 10)]
    private static void PrintAllChildrenLocalPosition()
    {
        string str = null;
        if (Selection.transforms.Length <= 1)
        {
            Transform select = Selection.transforms[0];
            Transform[] trans = select.GetComponentsInChildren<Transform>();
            for (int i = 1; i < trans.Length; i++)
            {
                str += trans[i].position.x + "," + trans[i].position.y + "," + trans[i].position.z;
                if (i != trans.Length - 1)
                {
                    str += ",";
                }
            }
            Debug.Log(str);
        }
        else
        {
            Debug.LogError("禁止多选");
        }

    }

    [MenuItem("GameObject/Print/PrintGameObjectWorldPosition", false, 10)]
    private static void PrintGameObjectPosition()
    {
        string str = null;
        if (Selection.transforms.Length <= 1)
        {
            Transform select = Selection.transforms[0];
            Transform trans = select.GetComponent<Transform>();
            str += trans.position;
            Debug.Log(str);
        }
        else
        {
            Debug.LogError("禁止多选");
        }
    }

    [MenuItem("GameObject/Print/PrintGameObjectWorldRotation", false, 10)]
    private static void PrintGameObjectRotation()
    {
        string str = null;
        if (Selection.transforms.Length <= 1)
        {
            Transform select = Selection.transforms[0];
            Transform trans = select.GetComponent<Transform>();
            str += trans.eulerAngles;
            Debug.Log(str);
        }
        else
        {
            Debug.LogError("禁止多选");
        }
    }


}
