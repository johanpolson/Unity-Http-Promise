namespace JohanPolosn.UnityHttpPromise.Mocking
{
    using System;
    using UnityEngine;

    public class HttpPromiseMock<TResult, TError>
        : HttpPromise<TResult, TError>
    {
        private float progress = 0f;

        public override ulong DownloadedBytes { get { return 0; } }

        public override float DownloadProgress { get { return this.progress; } }

        public override ulong UploadedBytes { get { return 0; } }

        public override float UploadProgress { get { return this.progress; } }
        
        private HttpPromiseMock() { }

        private static void StartHttpTestMockRunner(float seconds, Action callback)
        {
            var gameObject = new GameObject("HttpTestMockGameObject")
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            var runner = gameObject.AddComponent<HttpPromiseMockMockRunner>();
            runner.RunCoroutine(seconds, callback);
        }

        public static HttpPromiseMock<TResult, TError> OfSucceeded(long responseCode, TResult result)
        {
            return OfSucceeded(responseCode, result, 0F);
        }

        public static HttpPromiseMock<TResult, TError> OfSucceeded(long responseCode, TResult result, float seconds)
        {
            var mock = new HttpPromiseMock<TResult, TError>();
            StartHttpTestMockRunner(seconds, () =>
            {
                mock.ResponseCode = responseCode;
                mock.Result = result;
                mock.progress = 1f;
                mock.notCompleted = false;
                mock.InvokeCompleted();
            });
            return mock;
        }

        public static HttpPromiseMock<TResult, TError> OfError(long responseCode, TError error)
        {
            return OfError(responseCode, error, 0F);
        }

        public static HttpPromiseMock<TResult, TError> OfError(long responseCode, TError error, float seconds)
        {
            var mock = new HttpPromiseMock<TResult, TError>();
            StartHttpTestMockRunner(seconds, () =>
            {
                mock.ResponseCode = responseCode;
                mock.HasError = true;
                mock.Error = error;
                mock.progress = 1f;
                mock.notCompleted = false;
                mock.InvokeCompleted();
            });
            return mock;
        }

    }

}
