using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Events;

namespace WheelOfFortune.SoundSystem
{
    public class SoundEventPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _onStartSpinAudio;
        [SerializeField] private AudioClip _onEndSpinAudio;

        private AudioSource _audio;

        private float _spiningTime;
        private bool _isSpinStart;

        // Start is called before the first frame update
        void Start()
        {
            _audio = GetComponent<AudioSource>();
            if (!_audio)
                Debug.LogError($"Undefined AudioSource component on {this}");
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

        private void Update()
        {
            if (_isSpinStart)
            {
                _audio.pitch = 10 / Mathf.Exp(_spiningTime);
                _spiningTime += Time.deltaTime;
            }
        }

        private void PlayOnStartSpinAudio()
        {
            _audio.loop = true;
            _isSpinStart = true;
            PlaySound(_onStartSpinAudio);
        }
        
        private void PlayOnEndSpinAudio()
        {
            _audio.Stop();
            _isSpinStart = false;
            _audio.loop = false;

            _audio.pitch = 1;
            _spiningTime = 0;

            PlaySound(_onEndSpinAudio);
        }

        private void PlaySound(AudioClip clip)
        {
            _audio.clip = clip;
            _audio.Play();
        }
    }
}
