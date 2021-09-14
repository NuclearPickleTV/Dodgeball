using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    scr_player player;
    public BoxCollider boundCollider;

    public float speed;

    float destroySpeed;

    IEnumerator waitForSpawn()
    {
        while(true)
        {
            if (!player.isAlive) { yield return null; continue; }

            yield return new WaitForSeconds(speed);
            var enemy = Instantiate(enemyPrefab, GetRandomPointInsideBoundsCollider(), Quaternion.identity);

        } 
    }

    Vector3 GetRandomPointInsideBoundsCollider()
    {
        Vector3 randomPointinsideBounds = new Vector3(
            Random.Range(-boundCollider.bounds.extents.x, boundCollider.bounds.extents.x),
            Random.Range(-boundCollider.bounds.extents.y, boundCollider.bounds.extents.y),
            Random.Range(-boundCollider.bounds.extents.z, boundCollider.bounds.extents.z)
        );
        return boundCollider.bounds.center + randomPointinsideBounds;
    }

    private void Start()
    {
        player = GameObject.FindObjectOfType<scr_player>();
        player.isAlive = true;
        speed = 3.0f;
        StartCoroutine(waitForSpawn());
    }

    void Update()
    {
    }
}
