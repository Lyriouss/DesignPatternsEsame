using UnityEngine;

//always runs outside of play
[ExecuteAlways]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] private float cameraDistance = 15f;

    private void Update()
    {
        //if there is no player transform, skip update
        if (player == null)
            return;
        
        //tracks the position of player and always maintains a distance equal to cameraDistance
        transform.position = new Vector3(player.position.x, player.position.y + cameraDistance, player.position.z);
    }
}
