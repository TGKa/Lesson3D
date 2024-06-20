using System.Collections;
using UnityEngine;

namespace AlarmSystem
{
    public class Alarm : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;
        [SerializeField] private float _speed;

        private Coroutine _job;

        private void Start() =>
            _audio.volume = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Thief>(out Thief thief))
                EnableSiren(1f);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Thief>(out Thief thief))
                EnableSiren(0f);
        }

        private void EnableSiren(float endValue)
        {
            if (_job != null)
                StopCoroutine(_job);

            _job = StartCoroutine(ChangeVolume(endValue));
        }

        private IEnumerator ChangeVolume(float endValue)
        {
            _audio.Play();

            while (_audio.volume != endValue)
            {
                _audio.volume = Mathf.MoveTowards(_audio.volume, endValue, _speed * Time.deltaTime);

                yield return null;
            }

            if (_audio.volume == 0)
                _audio.Stop();
        }
    }
}