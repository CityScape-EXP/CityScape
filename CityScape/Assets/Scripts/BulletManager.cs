using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // 프리펩 10~ 20 은 Bullet을 저장함

    float time;

    private void Update()
    {
        time += Time.deltaTime;
    }
}
