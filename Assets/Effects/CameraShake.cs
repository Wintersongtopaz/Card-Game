using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    static float time = 0f;
    static float strength = 0.1f;

    public static void Shake(float time = 0.1f, float strength = 0.1f)
    {
        CameraShake.time = time;
        CameraShake.strength = strength;
    }


    // Update is called once per frame
    void Update()
    {
        if (time <= 0f) return;

        time -= Time.deltaTime;
        Vector3 position = new Vector3(Random.Range(-strength, strength), Random.Range(-strength, strength), -10f);
        Camera.main.transform.position = position;

        if (time <= 0f)
        {
            Camera.main.transform.position = new Vector3(0f, 0f, -10f);
        }
    }
}
