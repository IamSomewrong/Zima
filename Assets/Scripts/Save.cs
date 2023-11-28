using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public string name;
    public Vector2Int[] offs;
    public List<Vector2> chunkPositions = new List<Vector2>();
    public List<ChunkGOPos> gameObjectPositions = new List<ChunkGOPos>();
    public List<ChunkGOType> gameObjectTypes = new List<ChunkGOType>();
    public List<ChunkGOAct> gameObjectActivity = new List<ChunkGOAct>();
    public List<SaveItem> inventory = new List<SaveItem>();
    public Vector3 playerPos = new Vector3();
}

[System.Serializable]
public class ChunkGOPos
{
    public List<Vector3> positions = new List<Vector3>();
}

[System.Serializable]
public class ChunkGOType
{
    public List<int> types = new List<int>();
}

[System.Serializable]
public class ChunkGOAct
{
    public List<bool> activity = new List<bool>();
}

[System.Serializable]
public class WorldList
{
    public List<string> names = new List<string>();
}

[System.Serializable]
public class SaveItem
{
    public int itemId = -1;
    public int collCount = -1;
    public int usableDurNow = -1;
    public SaveItemType saveItemType = SaveItemType.None;
}

[System.Serializable]
public enum SaveItemType
{
    None,
    Coll,
    OneTime,
    Placeble,
    Usable
}