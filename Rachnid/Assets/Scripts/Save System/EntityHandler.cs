using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> loadableEntities = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnedEntities = new List<GameObject>();
    private void Awake()
    {
        SaveManager.OnSave += SaveManager_OnSave;
        SaveManager.OnLoad += SaveManager_OnLoad;
        InstantiateEntity(loadableEntities[0], transform.position, Quaternion.identity);
    }
    private void OnDestroy()
    {
        SaveManager.OnSave -= SaveManager_OnSave;
        SaveManager.OnLoad -= SaveManager_OnLoad;
    }

    private void SaveManager_OnSave()
    {
        SaveData.current.entityData.entities.Clear();
        for (int i = 0; i < spawnedEntities.Count; i++)
        {
            GameObject thisSpawnedEntity = spawnedEntities[i];
            if(thisSpawnedEntity != null)
            {
                Entity entity = new Entity();
                EnemyAI ai = thisSpawnedEntity.GetComponent<EnemyAI>();

                entity.id = GetEntityId(thisSpawnedEntity);
                entity.position = ai.transform.position;
                entity.rotation = ai.transform.rotation;
                entity.health = ai.health;
                entity.state = ai.state;
                SaveData.current.entityData.entities.Add(entity);
            }
        }
    }

    private void SaveManager_OnLoad()
    {
        List<Entity> entities = SaveData.current.entityData.entities;
        for (int i = 0; i < entities.Count; i++)
        {
            Entity thisEntity = entities[i];
            EnemyAI ai = InstantiateEntity(GetEntityObj(thisEntity.id), thisEntity.position, thisEntity.rotation).GetComponent<EnemyAI>();
            ai.health = thisEntity.health;
            ai.state = thisEntity.state;
        }
    }

    public GameObject InstantiateEntity(GameObject obj, Vector3 position, Quaternion rotation)
    {
        GameObject newEntity = Instantiate(obj, position, rotation);
        spawnedEntities.Add(newEntity);
        return newEntity;
    }
    int GetEntityId(GameObject obj)
    {
        for (int i = 0; i < loadableEntities.Count; i++)
        {
            if (obj == loadableEntities[i])
                return i;
        }
        return 0;
    }
    GameObject GetEntityObj(int id)
    {
        for (int i = 0; i < loadableEntities.Count; i++)
        {
            if (id == i)
                return loadableEntities[i];
        }
        return null;
    }
}