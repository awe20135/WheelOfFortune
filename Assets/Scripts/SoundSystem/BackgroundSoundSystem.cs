using System.Collections;
using UnityEngine;
using WheelOfFortune.Events;

namespace WheelOfFortune.SoundSystem
{
    public class BackgroundSoundSystem : MonoBehaviour
    {
        [SerializeField] private AudioClip _defaultBackground;
        [SerializeField] private AudioClip _spinBackground;

        private float _backgroundTrackPausedTime = 0;
        private bool _isTrackCurrentlySwap;

        private AudioSource _audio;

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            if (!_audio)
                Debug.LogError($"Undefined AudioSource component on {this}");

            SwapBackgroundTrack(_defaultBackground);
        }

        private void PlayOnStartSpinAudio()
        {
            _backgroundTrackPausedTime = _audio.time;
            StartCoroutine(FadeSwapTrack(_spinBackground));
        }

        private void PlayOnEndSpinAudio()
        {
            StartCoroutine(FadeSwapTrack(_defaultBackground, _backgroundTrackPausedTime));
        }

        private void SwapBackgroundTrack(AudioClip clip, float startTime = 0)
        {
            _audio.clip = clip;
            _audio.time = startTime;
            _audio.Play();
        }

        private void OnEnable()
        {
            EventManager.Instance.OnStartSpin += PlayOnStartSpinAudio;
            EventManager.Instance.OnEndSpin += PlayOnEndSpinAudio;
        }

        private void OnDisable()
        {
            EventManager.Instance.OnStartSpin -= PlayOnStartSpinAudio;
            EventManager.Instance.OnEndSpin -= PlayOnEndSpinAudio;
        }

        private IEnumerator FadeSwapTrack(AudioClip clip, float startTime = 0, float fadeSecond = .5f)
        {
            while (_isTrackCurrentlySwap) { yield return new WaitForEndOfFrame(); }
            _isTrackCurrentlySwap = true;

            float cachedVolume = _audio.volume;

            float secondDelayByChangeStep = .1f;
            float volumeChangeByStep = cachedVolume / ((fadeSecond/2f) / secondDelayByChangeStep);

            while(_audio.volume !=0)
            {
                float nextVolumeValue = volumeChangeByStep - _audio.volume;
                _audio.volume = nextVolumeValue < 0 ? 0 : nextVolumeValue;
                yield return new WaitForSeconds(secondDelayByChangeStep);
            }

            SwapBackgroundTrack(clip, startTime);

            while (_audio.volume != cachedVolume)
            {
                float nextVolumeValue = volumeChangeByStep + _audio.volume;
                _audio.volume = nextVolumeValue > cachedVolume ? cachedVolume : nextVolumeValue;
                yield return new WaitForSeconds(secondDelayByChangeStep);
            }

            _isTrackCurrentlySwap = false;
        }
    }
}
