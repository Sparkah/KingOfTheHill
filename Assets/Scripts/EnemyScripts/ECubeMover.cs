using System.Collections;
using UnityEngine;

public class ECubeMover : MonoBehaviour
{
    private int cubeMoverPhases = 20;

    private void Start()
    {
        StartCoroutine(CubeMover());
        StartCoroutine(CubeDestroyer());//Удалим через время чтобы не занимал память 
    }

    IEnumerator CubeMover()
    {
        if (cubeMoverPhases == 0)
        {
            yield return new WaitForSeconds(1f);
            cubeMoverPhases = 20;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, (transform.rotation.y + 4.5f)* (20-cubeMoverPhases), (transform.rotation.z - 4.5f) * (20 - cubeMoverPhases)));

        if (cubeMoverPhases > 0 && cubeMoverPhases <= 10)
        {
            --cubeMoverPhases;
            yield return new WaitForSeconds(0.03f);
            transform.position += new Vector3(0, -0.12f, -0.05f);

            StartCoroutine(CubeMover());
        }

        if (cubeMoverPhases > 10)
        {
            --cubeMoverPhases;
            yield return new WaitForSeconds(0.015f);
            transform.position += new Vector3(0, 0.02f, -0.05f);

            StartCoroutine(CubeMover());
        }
    }

    IEnumerator CubeDestroyer()
    {
        yield return new WaitForSeconds(60f);
        Destroy(gameObject);
    }
}