using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersManager : MonoBehaviour
{
    public List<MonsterBehaviour> MonstersPrefabs;
    List<MonsterBehaviour> monstersOnScreen = new List<MonsterBehaviour>();

    public float MinDelayGeneration = 0.5f;
    public float MaxDelayGeneration = 2.5f;
    int maxCountMonstersOnScreen = 10;

    void Start ()
    {
        StartCoroutine(GenerateNewMonsters());

        GameManager.instance.OnDestroyedMonster += OnDestroyedMonster;
        GameManager.instance.OnGameOver += OnGameOver;
        GameManager.instance.OnRestartGame += OnRestartGame;
    }

    void OnRestartGame()
    {
        GameManager.instance.OnDestroyedMonster += OnDestroyedMonster;
        StartCoroutine(GenerateNewMonsters());
    }

    void OnGameOver()
    {
        GameManager.instance.OnDestroyedMonster -= OnDestroyedMonster;

        StopAllCoroutines();
        foreach (MonsterBehaviour item in monstersOnScreen)
        {
            item.HideMonster();
        }
        monstersOnScreen.Clear();
    }

    void OnDestroyedMonster(MonsterBehaviour value)
    {
        monstersOnScreen.Remove(value);
    }

    IEnumerator GenerateNewMonsters()
    {
        while (true)
        {
            if (monstersOnScreen.Count <= maxCountMonstersOnScreen)
            {
                Vector3 newPosition = GetRandomPosiionOutsideCamera();
                MonsterBehaviour monster = MonsterPoolManager.GetObject();
                if (monster != null)
                {
                    monster.transform.position = newPosition;
                    monster.gameObject.SetActive(true);
                }
                else
                {
                    MonsterBehaviour prefab = MonstersPrefabs[Random.Range(0, MonstersPrefabs.Count)];
                    monster = Instantiate(prefab, newPosition, new Quaternion(0, 0, 0, 0), this.transform);
                }
                monstersOnScreen.Add(monster);
            }
            yield return new WaitForSeconds(Random.Range(MinDelayGeneration, MaxDelayGeneration));
        }
    }

    Vector3 GetRandomPosiionOutsideCamera()
    {
        float height = Camera.main.orthographicSize + 1;
        float width = Camera.main.orthographicSize * Camera.main.aspect + 1;

        float x = 0;
        float y = 0;
        switch (Random.Range(1, 4))
        {
            case 1:
                // from top
                x = Camera.main.transform.position.x + Random.Range(-width, width);
                y = Camera.main.transform.position.y + height;
                break;
            case 2:
                // from bottom
                x = Camera.main.transform.position.x + Random.Range(-width, width);
                y = Camera.main.transform.position.y - height; 
                break;
            case 3:
                // from left
                x = Camera.main.transform.position.x - width;
                y = Camera.main.transform.position.y + Random.Range(-height, height);
                break;
            case 4:
                // from right
                x = Camera.main.transform.position.x + width;
                y = Camera.main.transform.position.y + Random.Range(-height, height);
                break;
        }
        return new Vector3(x, y, -1f);
    }
}