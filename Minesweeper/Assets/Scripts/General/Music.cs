using UnityEngine;

namespace Minesweeper.General
{
    public class Music : MonoBehaviour
    {
        protected AudioSource audio;

        private void Awake()
        {
            audio = GetComponent<AudioSource>();
        }

        public void ChangeVolume(int newVolume)
        {
            ChangeVolume(Mathf.Clamp01(newVolume / 100f));
        }

        public void ChangeVolume(float newVolume)
        {
            audio.volume = newVolume;
        }
    }
}