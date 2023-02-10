using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

interface ILoader<T>
{
    List<T> MakeList();
}

interface ILoader<TKey, TValue>
{
    Dictionary<TKey, TValue> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.PlayerStat> PlayerStat { get; private set; } = new Dictionary<int, Data.PlayerStat>();

    public void Init()
    {
        PlayerStat = LoadJson<PlayerStatData, int, Data.PlayerStat>("PlayerStat").MakeDict();
    }

    Loader LoadSingleJson<Loader, T>(string path) where T : ILoader<T>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    Loader LoadJson<Loader, TKey, TValue>(string path) where Loader : ILoader<TKey, TValue>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
