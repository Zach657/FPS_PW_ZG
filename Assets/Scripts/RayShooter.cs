using UnityEngine;
using System.Collections;

/*
 * @author James Greenwell
*/

public class RayShooter : MonoBehaviour
{

    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 origin = new Vector3(camera.pixelWidth / 2,
                                 camera.pixelHeight / 2,
                                 0);
            Ray ray = camera.ScreenPointToRay(origin);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                StartCoroutine(Shoot(hit.point));
            }
        }
    }

    private IEnumerator Shoot(Vector3 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;
        sphere.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
}