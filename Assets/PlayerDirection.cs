using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    private Transform m_transform;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
    }

    private void LookAt()
    {
        Vector2 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition) - m_transform.position;
        float angle = math.atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        /*Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        m_transform.rotation = rotation;*/

        Vector2 direction = ((mousePosition) - (Vector2) transform.position).normalized;

        transform.up = direction;

    }
}
