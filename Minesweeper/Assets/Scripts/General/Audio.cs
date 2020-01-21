using UnityEngine;

namespace Minesweeper.General
{
    public class Audio : Music
    {
        public void Play(AudioClip clip)
        {
            audio.clip = clip;
            audio.Play();
        }
    }
}