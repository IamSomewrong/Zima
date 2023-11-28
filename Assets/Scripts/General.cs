using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class General : MonoBehaviour
{
    public List<GameObject> GOsToLoad;
    public List<Chunk> chunks = new List<Chunk>();
    public List<Vector2> exist_chunk = new List<Vector2>();
    LvLGen lvlGen;
    public PlayerMove Player;
    public string saveName = "";
    public string seed = "";
    public Vector2Int[] offs = new Vector2Int[3];
    public Vector3 playerPos;
    public ItemState[] inventory;
    public List<ItemState> startItems;
    public List<Item> itemBase;
    void Start()
    {
        DontDestroyOnLoad(this);
        BuiltProject.Builded += AddObjToChunk;
        Die.Dead += DeleteFromChunk;
        lvlGen = GetComponent<LvLGen>();
        SceneManager.sceneLoaded += LoadWorld;
        SceneManager.LoadScene(1);
    }

    public void NewChunk(Vector2 v2) // метод для подгрузки чанков игрового мира
    {
        if(!exist_chunk.Exists(x => { return x.x == v2.x && x.y == v2.y; }))
        {
            if (chunks.Exists(x => { return x.Pos == v2; }))
            {
                foreach (GameObject go in chunks.Find(x => { return x.Pos == v2; }).gameObjects) 
                {
                    go.SetActive(true); // если он уже сгенерирован и сохранен в памяти, включаем его
                    exist_chunk.Add(v2);
                }
            }
            else
            {
                chunks.Add(lvlGen.MapGen(v2)); // если его нет, то генерируем ноый чанк
                exist_chunk.Add(v2);
            }
        }
    }

    public void DeleteChunk(Vector2 v2) // метод для выгрузки чанков из мира
    {
        if(exist_chunk.Exists(x => { return x.x == v2.x && x.y == v2.y; })) 
        {
            Chunk c = chunks.Find(x => { return x.Pos == v2; });
            foreach (GameObject go in c.gameObjects)
            {
                go.SetActive(false);
                exist_chunk.Remove(v2);
            }
        }
    }

    public void AddObjToChunk(GameObject go, Vector2 v2)
    {
        Chunk c = chunks.Find(x => { return x.Pos == v2; });
        c.gameObjects.Add(go);
    }

    public void DeleteFromChunk(GameObject go, Vector2 v2)
    {
        Chunk c = chunks.Find(x => { return x.Pos == v2; });
        c.gameObjects.Remove(go);
    }
    public Save CreateSaveData()
    {
        Save data = new Save();
        data.name = saveName;
        data.offs = offs;
        for (int i = 0; i < chunks.Count; i++)
        {
            data.chunkPositions.Add(chunks[i].Pos);
            data.gameObjectPositions.Add(new ChunkGOPos());
            data.gameObjectTypes.Add(new ChunkGOType());
            data.gameObjectActivity.Add(new ChunkGOAct());
            for (int j = 0; j < chunks[i].gameObjects.Count; j++)
            {
                data.gameObjectPositions[i].positions.Add(chunks[i].gameObjects[j].transform.position);
                data.gameObjectTypes[i].types.Add(chunks[i].gameObjects[j].GetComponent<ObjectType>().type);
                data.gameObjectActivity[i].activity.Add(chunks[i].gameObjects[i].activeSelf);
            }
        }
        if(Player != null)
        {
            data.playerPos = Player.gameObject.transform.position;
        }
        else
        {
            data.playerPos = Vector3.zero;
        }
        if(Player != null)
        {
            TakeItems ti = Player.gameObject.GetComponent<TakeItems>();
            for (int i = 0; i < ti.inv_items.Length; i++)
            {
                data.inventory.Add(new SaveItem());
                if (ti.inv_items[i] is CollectableItemState cis)
                {
                    if (ti.inv_items[i] is PlacebleItemState)
                    {
                        data.inventory[i].saveItemType = SaveItemType.Placeble;
                    }
                    else
                    {
                        data.inventory[i].saveItemType = SaveItemType.Coll;

                    }
                    data.inventory[i].itemId = itemBase.FindIndex(x => x == cis.item);
                    data.inventory[i].collCount = cis.count;
                }
                else if (ti.inv_items[i] is UsableItemState uis)
                {
                    data.inventory[i].saveItemType = SaveItemType.Usable;
                    data.inventory[i].itemId = itemBase.FindIndex(x => x == uis.item);
                    data.inventory[i].usableDurNow = uis.Dur_now;
                }
                else
                {
                    if(ti.inv_items[i].item != null)
                    {
                        data.inventory[i].itemId = itemBase.FindIndex(x => x == startItems[i].item);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                data.inventory.Add(new SaveItem());
            }
            for (int i = 0; i < startItems.Count; i++)
            {
                if (startItems[i] is CollectableItemState cis)
                {
                    //if items[i] is OneTimeItemState
                    if (startItems[i] is PlacebleItemState)
                    {
                        data.inventory[i].saveItemType = SaveItemType.Placeble;
                    }
                    else
                    {
                        data.inventory[i].saveItemType = SaveItemType.Coll;
                        
                    }
                    data.inventory[i].itemId = itemBase.FindIndex(x => x == cis.item);
                    data.inventory[i].collCount = cis.count;
                }
                else if (startItems[i] is UsableItemState uis)
                {
                    data.inventory[i].saveItemType = SaveItemType.Usable;
                    data.inventory[i].itemId = itemBase.FindIndex(x => x == uis.item);
                    data.inventory[i].usableDurNow = uis.Dur_now;
                }
                else
                {
                    if (startItems[i].item != null)
                    {
                        data.inventory[i].itemId = itemBase.FindIndex(x => x == startItems[i].item);
                    }
                }
            }
        }
        
        return data;
    }

    public void SaveWorld()
    {
        Save save = CreateSaveData();
        string json = JsonUtility.ToJson(save);
        using (StreamWriter sr = new StreamWriter("saves/"+ save.name + ".sav"))
        {
            sr.Write(json);
        }
    }

    public void LoadWorld(Scene sc, LoadSceneMode md)
    {
        if(sc.buildIndex == 2)
        {
            chunks.Clear();
            inventory = new ItemState[9];
            string json;
            using (StreamReader sr = new StreamReader("saves/" + saveName + ".sav"))
            {
                json = sr.ReadToEnd();
            }
            Save save = JsonUtility.FromJson<Save>(json);
            offs = save.offs;
            this.saveName = save.name;
            for (int i = 0; i < save.chunkPositions.Count; i++)
            {
                Chunk chunk = new Chunk(save.chunkPositions[i]);
                for (int j = 0; j < save.gameObjectPositions[i].positions.Count; j++)
                {
                    GameObject go = Instantiate(GOsToLoad[save.gameObjectTypes[i].types[j]], save.gameObjectPositions[i].positions[j], Quaternion.identity);
                    chunk.gameObjects.Add(go);
                }
                chunks.Add(chunk);
            }
            playerPos = save.playerPos;
            Camera.main.transform.position = new Vector3(playerPos.x, playerPos.y, Camera.main.transform.position.z);
            for (int i = 0; i < save.inventory.Count; i++)
            {
                if (save.inventory[i].saveItemType == SaveItemType.Coll)
                {
                    inventory[i] = ScriptableObject.CreateInstance<CollectableItemState>();
                    inventory[i].item = itemBase[save.inventory[i].itemId];
                    ((CollectableItemState)inventory[i]).count = save.inventory[i].collCount;

                }
                else if (save.inventory[i].saveItemType == SaveItemType.Usable)
                {
                    inventory[i] = ScriptableObject.CreateInstance<UsableItemState>();
                    inventory[i].item = itemBase[save.inventory[i].itemId];
                    ((UsableItemState)inventory[i]).Dur_now = save.inventory[i].usableDurNow;
                }
                else if (save.inventory[i].saveItemType == SaveItemType.Placeble)
                {
                    inventory[i] = ScriptableObject.CreateInstance<PlacebleItemState>();
                    inventory[i].item = itemBase[save.inventory[i].itemId];
                    ((CollectableItemState)inventory[i]).count = save.inventory[i].collCount;
                }
                else
                {
                    if (save.inventory[i].itemId == -1)
                    {
                        inventory[i] = ScriptableObject.CreateInstance<ItemState>();
                    }
                    else
                    {
                        inventory[i] = ScriptableObject.CreateInstance<ItemState>();
                        inventory[i].item = itemBase[save.inventory[i].itemId];
                    }
                }
            }
        }
    }

    public void Seed(string s)
    {
        seed = s;
    }
    public void WorldName(string s)
    {
        saveName = s;
    }

    public void CreateWorld()
    {
        chunks.Clear();
        exist_chunk.Clear();
        if(seed != "")
        {
            int xd = 0;
            int yd = 0;
            int div = seed.Length / 2;
            for (int i = 0; i < div; i++)
            {
                xd += (int)seed[i];
            }
            for (int i = div; i < seed.Length; i++)
            {
                yd += (int)seed[i];
            }
            offs[0] = new Vector2Int(xd, yd);
            offs[1] = new Vector2Int((xd + 100) * 3, (yd + 100) * 3);
            offs[2] = new Vector2Int((xd - 100) * 2, (yd - 100) * 2);
        }
        SaveWorld();
        string json;
        WorldList wl;
        using (StreamReader sr = new StreamReader("Worlds" + ".wl"))
        {
            json = sr.ReadToEnd();
        }
        if(json != "")
        {
            wl = JsonUtility.FromJson<WorldList>(json);
            if (!wl.names.Exists(x => { return x == saveName; }))
            {
                wl.names.Add(saveName);
            }
        }
        else
        {
            wl = new WorldList();
            wl.names.Add(saveName);
        }
        
        json = JsonUtility.ToJson(wl);
        using (StreamWriter sr = new StreamWriter("Worlds" + ".wl"))
        {
            sr.Write(json);
        }
        SceneManager.LoadScene(2);
    }
}
