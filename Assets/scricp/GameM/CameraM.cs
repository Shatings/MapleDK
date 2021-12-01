using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraM : MonoBehaviour
{
    [SerializeField]
    private float fosx;
    [SerializeField]
    private float fosy;
    [SerializeField]
    private float fosz;
    public Player player;
    private Vector3 camerafos;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void LateUpdate()
    {
        camerafos.x = player.transform.position.x + fosx;
        camerafos.y = player.transform.position.y + fosy;
        camerafos.z = fosz;
        transform.position = camerafos;
    }
}
