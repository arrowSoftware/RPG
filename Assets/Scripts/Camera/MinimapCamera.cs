using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    // TODO better minimap https://www.youtube.com/watch?v=kWhOMJMihC0

    public Transform player;
    public bool rotateWithPlayer = false;

    private void LateUpdate() {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        if (rotateWithPlayer) {
            transform.rotation = Quaternion.Euler(90.0f, player.eulerAngles.y, 0.0f);
        }
    }
}
