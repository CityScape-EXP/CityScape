using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Collider2D coll;
    WaitForFixedUpdate waitf = new WaitForFixedUpdate();

    public float moveSpeed;
    
    private float cooltime = 0.3f;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public void TemporaryDisable()
    {
        StartCoroutine(Cooltime());
    }

    IEnumerator Cooltime()
    {
        float time = 0;
        gameObject.layer = LayerMask.NameToLayer("DownPlatform");
        coll.isTrigger = true;
        while(time < cooltime)
        {
            time += Time.deltaTime;
            yield return waitf;
        }
        gameObject.layer = LayerMask.NameToLayer("Platform");
        coll.isTrigger = false;
    }
}
