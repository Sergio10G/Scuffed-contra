using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : MonoBehaviour
{
    public GameObject goPrefab_;
    public int poolMaxSize_;
    List<GameObject> elementPoolList_;

    // Start is called before the first frame update
    void Start()
    {
        elementPoolList_ = new List<GameObject>();
        for (int i = 0; i < poolMaxSize_; i++)
        {
            GameObject obj = (GameObject)Instantiate(goPrefab_);
            obj.SetActive(false);
            elementPoolList_.Add(obj);
        }
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < elementPoolList_.Count; i++)
        {
            if (!elementPoolList_[i].activeInHierarchy)
            {
                return elementPoolList_[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(goPrefab_);
        elementPoolList_.Add(obj);
        return obj;
    }
}
