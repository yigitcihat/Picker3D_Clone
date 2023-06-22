﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Picker3D_.Scripts.Managers;
using Sirenix.OdinInspector;
using UnityEngine;


[System.Serializable]
public class SourceObjects
{

    public GameObject SourcePrefab;

    //If 0 will use the global object count
    public int MinNumberOfObject = 0;
    public bool AllowGrow = true;
    public bool AutoDestroy = true;

    [ReadOnly] public List<GameObject> clones;
}

public class PoolingSystem : Singleton<PoolingSystem>
{
    public List<SourceObjects> SourceObjects = new List<SourceObjects>();

    private List<AudioSource> pooledAudioSources = new List<AudioSource>();


    public int DefaultCount = 10;

    private void Start()
    {
        InitilizePool();
    }

    public void InitilizePool()
    {
        InitilizeGameObjects();
        InitilizeAudioSources();
    }

    private void InitilizeGameObjects()
    {
        foreach (var t in SourceObjects)
        {
            var copyNumber = DefaultCount;
            if (t.MinNumberOfObject != 0)
                copyNumber = t.MinNumberOfObject;

            for (var j = 0; j < copyNumber; j++)
            {
                var go = Instantiate(t.SourcePrefab, transform);
                go.SetActive(false);
                if (t.AutoDestroy)
                    go.AddComponent<PoolObject>();

                t.clones.Add(go);
            }
        }
    }

    private void InitilizeAudioSources()
    {
        var audioHolder = new GameObject();
        audioHolder.name = "AudioHolder";
        audioHolder.transform.SetParent(transform);
        audioHolder.transform.position = Vector3.zero;

        for (var i = 0; i < 20; i++)
        {
            var go = new GameObject();
            go.name = "PooledSource";
            go.transform.position = Vector3.zero;
            go.transform.SetParent(audioHolder.transform);
            var audioSource = go.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            pooledAudioSources.Add(audioSource);
        }
    }

    public GameObject InstantiateAPS(string Id)
    {
       
        foreach (var t in SourceObjects.Where(t => string.Equals(t.SourcePrefab.name, Id)))
        { 
            foreach (var t1 in t.clones.Where(t1 => !t1.activeInHierarchy))
            {
                t1.SetActive(true);
                //ForEach e al
                var poolable = t1.GetComponent<IPoolable>();
                if (poolable != null)
                    poolable.Initilize();

                return t1;
            }

            if (!t.AllowGrow) continue;
            {
                GameObject go = Instantiate(t.SourcePrefab, transform);
                t.clones.Add(go);
                IPoolable poolable = go.GetComponent<IPoolable>();
                if (poolable != null)
                    poolable.Initilize();

                if (t.AutoDestroy)
                    go.AddComponent<PoolObject>();
                return go;
            }
        }

        return null;
    }

    public GameObject InstantiateAPS(string iD, Vector3 position)
    {
        var go = InstantiateAPS(iD);
        if (go)
        {
            go.transform.position = position;
            return go;
        }
        else
            return null;
    }

    public GameObject InstantiateAPS(string iD, Vector3 position, Quaternion rotation)
    {
        var go = InstantiateAPS(iD);
        if (go)
        {
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
        }
        else
            return null;
    }

    public GameObject InstantiateAPS(GameObject sourcePrefab)
    {
        foreach (var t in SourceObjects.Where(t => ReferenceEquals(t.SourcePrefab, sourcePrefab)))
        {
            for (int j = 0; j < t.clones.Count; j++)
            {
                if (!t.clones[j].activeInHierarchy)
                {
                    t.clones[j].SetActive(true);
                    return t.clones[j];
                }
            }

            if (!t.AllowGrow) continue;
            var go = Instantiate(t.SourcePrefab, transform);
            t.clones.Add(go);
            return go;
        }

        return null;
    }

    public GameObject InstantiateAPS(GameObject sourcePrefab, Vector3 position)
    {
        GameObject go = InstantiateAPS(sourcePrefab);
        if (go)
        {
            go.transform.position = position;
            return go;
        }
        else
            return null;
    }

    public AudioSource GetAudioSource()
    {
        foreach (var t in pooledAudioSources.Where(t => !t.isPlaying))
        {
            return t;
        }

        var audioHolder = transform.Find("AudioHolder");
        var go = new GameObject();
        go.name = "PooledSource";
        go.transform.position = Vector3.zero;
        go.transform.SetParent(audioHolder);
        var audioSource = go.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        pooledAudioSources.Add(audioSource);
        return audioSource;
    }

    public void DestroyAPS(GameObject clone)
    {
        

        //ForEach e al
        IPoolable poolable = clone.GetComponent<IPoolable>();
        if (poolable != null)
            poolable.Dispose();
        clone.SetActive(false);
    }

    public void DestroyAPS(GameObject clone, float waitTime)
    {
        StartCoroutine(DestroyAPSCo(clone, waitTime));
    }

    IEnumerator DestroyAPSCo(GameObject clone, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DestroyAPS(clone);
    }
}