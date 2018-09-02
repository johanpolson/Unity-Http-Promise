namespace JohanPolosn.UnityHttpPromise.Mocking
{
    using System;
    using System.Collections;
    using UnityEngine;

    public class HttpPromiseMockMockRunner : MonoBehaviour
    {
        public void RunCoroutine(float seconds, Action callback)
        {
            StartCoroutine(Dely(seconds, callback));
        }

        private IEnumerator Dely(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            try
            {
                callback();
            }
            finally
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }

}
