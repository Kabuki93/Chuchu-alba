using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarrierManager : MonoBehaviour
{
    public float animDuration = 0.5f;
    public List<BarrierModel> Barriers = new List<BarrierModel>();
    public static BarrierManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        for (int i = 0; i < Barriers.Count; i++)
        {
            var barrier = Barriers[i];
            if (barrier.parent != null)
            {
                barrier.barrier1 = barrier.parent.gameObject.transform.GetChild(0).Find("LV").gameObject;
                barrier.barrier2 = barrier.parent.gameObject.transform.GetChild(1).Find("LV").gameObject;
            }
        }
    }
    private IEnumerator RotateObject(BarrierModel barrier, Quaternion endRotation, float duration, bool open)
    {
        var barrier1 = barrier.barrier1;
        var barrier2 = barrier.barrier2;
        float elapsedTime = 0f;
        Quaternion startRotation1 = barrier1.transform.localRotation;
        Quaternion startRotation2 = barrier2.transform.localRotation;
        while (elapsedTime < duration)
        {
            if (barrier1 != null) barrier.barrier1.transform.localRotation = Quaternion.Lerp(startRotation1, endRotation, elapsedTime / duration);
            if (barrier2 != null) barrier.barrier2.transform.localRotation = Quaternion.Lerp(startRotation2, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
            barrier.isOpen = open;
        }
    }

    public void Open(GameObject gameObject)
    {
        var barrier = FindBarrier(gameObject);
        if (barrier.isOpen) return;
        Quaternion endRotation = Quaternion.Euler(-150f, -90, 90);
        StartCoroutine(RotateObject(barrier, endRotation, animDuration, true));
    }

    public void Close(GameObject gameObject)
    {
        var barrier = FindBarrier(gameObject);
        if (!barrier.isOpen) return;
        Quaternion endRotation = Quaternion.Euler(-90f, -90f, 90f);
        StartCoroutine(RotateObject(barrier, endRotation, animDuration, false));
    }

    public BarrierModel FindBarrier(GameObject gameObject)
    {
        return Barriers.Where(x => x.parent == gameObject).FirstOrDefault();
    }
}


[Serializable]
public class BarrierModel
{
    public GameObject parent;
    public bool isOpen = false;
    [HideInInspector] public GameObject barrier1;
    [HideInInspector] public GameObject barrier2;
}