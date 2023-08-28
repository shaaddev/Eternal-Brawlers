using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject slimePrefab;
    // [SerializeField] private GameObject ;

    [SerializeField] private float slimeInterval = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(slimeInterval, slimePrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy){
        yield return new WaitForSeconds(interval);

        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
