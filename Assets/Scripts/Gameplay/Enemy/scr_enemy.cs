using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_enemy : MonoBehaviour
{
    public float destroySpeed;

    scr_player player;

    void Start()
    {
        player = GameObject.FindObjectOfType<scr_player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            StartCoroutine(waitForDestroy());
        }
    }

    void updateHighScore(int add)
    {
        player.highScore += add;
    }

    IEnumerator waitForDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(destroySpeed);
            updateHighScore(10);
            GameObject.Destroy(this.gameObject);
        }
    }
}
