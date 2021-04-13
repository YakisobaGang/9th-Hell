using UnityEngine;

namespace ProjectD.Player
{
    public class SavePlayerPosition : MonoBehaviour
    {
        private string playerX = "PlayerPositionX";
        private string playerY = "PlayerPositionY";
        private string playerZ = "PlayerPositionZ";
        private void Awake()
        {
            if (PlayerPrefs.GetFloat(playerX) == 0) return;

            var lastPosition = new Vector3
            (
                PlayerPrefs.GetFloat(playerX),
                PlayerPrefs.GetFloat(playerY),
                PlayerPrefs.GetFloat(playerZ)
            );

            transform.position = lastPosition;
        }

        public void SavePlayerLastPosition()
        {
            var position = transform.position;

            var (x, y, z) = (position.x, position.y, position.z);
            Debug.Log($"{x:N2}, {y:N2}, {z:N2}");

            PlayerPrefs.SetFloat(playerX, x);
            PlayerPrefs.SetFloat(playerY, y);
            PlayerPrefs.SetFloat(playerZ, z);
        }
    }
}