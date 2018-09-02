namespace JohanPolosn.UnityHttpPromise
{
    using System;
    using UnityEngine;
    using UnityEngine.Networking;

    public partial class HttpPromise<TResult, TError>
    {
        // Get
        public static HttpPromise<TResult, TError> Get(string url, OnCompletedDelegate onCompleted)
        {
            return new HttpPromise<TResult, TError>(UnityWebRequest.Get(url), onCompleted);
        }

        public static HttpPromise<TResult, TError> Get(Uri uri, OnCompletedDelegate onCompleted)
        {
            return new HttpPromise<TResult, TError>(UnityWebRequest.Get(uri), onCompleted);
        }

        // Delete
        public static HttpPromise<TResult, TError> Delete(string url, OnCompletedDelegate onCompleted)
        {
            return new HttpPromise<TResult, TError>(UnityWebRequest.Delete(url), onCompleted);
        }

        public static HttpPromise<TResult, TError> Delete(Uri uri, OnCompletedDelegate onCompleted)
        {
            return new HttpPromise<TResult, TError>(UnityWebRequest.Delete(uri), onCompleted);
        }

        // Put
        public static HttpPromise<TResult, TError> Put(string url, byte[] bodyData, OnCompletedDelegate onCompleted)
        {
            return new HttpPromise<TResult, TError>(UnityWebRequest.Put(url, bodyData), onCompleted);
        }

        public static HttpPromise<TResult, TError> Put(string url, string bodyData, OnCompletedDelegate onCompleted)
        {
            return new HttpPromise<TResult, TError>(UnityWebRequest.Put(url, bodyData), onCompleted);
        }

        public static HttpPromise<TResult, TError> Put(Uri uri, byte[] bodyData, OnCompletedDelegate onCompleted)
        {
            return new HttpPromise<TResult, TError>(UnityWebRequest.Put(uri, bodyData), onCompleted);
        }

        public static HttpPromise<TResult, TError> Put(Uri uri, string bodyData, OnCompletedDelegate onCompleted)
        {
            return new HttpPromise<TResult, TError>(UnityWebRequest.Put(uri, bodyData), onCompleted);
        }

        // JsonPut
        public static HttpPromise<TResult, TError> JsonPut(string url, byte[] bodyData, OnCompletedDelegate onCompleted)
        {
            var request = UnityWebRequest.Put(url, bodyData);
            request.SetRequestHeader("Content-Type", "application/json");
            return new HttpPromise<TResult, TError>(request, onCompleted);
        }

        public static HttpPromise<TResult, TError> JsonPut(string url, string bodyData, OnCompletedDelegate onCompleted)
        {
            var request = UnityWebRequest.Put(url, bodyData);
            request.SetRequestHeader("Content-Type", "application/json");
            return new HttpPromise<TResult, TError>(request, onCompleted);
        }

        public static HttpPromise<TResult, TError> JsonPut(string url, object bodyObj, OnCompletedDelegate onCompleted)
        {
            var json = JsonUtility.ToJson(bodyObj);
            var request = UnityWebRequest.Put(url, json);
            request.SetRequestHeader("Content-Type", "application/json");
            return new HttpPromise<TResult, TError>(request, onCompleted);
        }

        public static HttpPromise<TResult, TError> JsonPut(Uri uri, byte[] bodyData, OnCompletedDelegate onCompleted)
        {
            var request = UnityWebRequest.Put(uri, bodyData);
            request.SetRequestHeader("Content-Type", "application/json");
            return new HttpPromise<TResult, TError>(request, onCompleted);
        }

        public static HttpPromise<TResult, TError> JsonPut(Uri uri, string bodyData, OnCompletedDelegate onCompleted)
        {
            var request = UnityWebRequest.Put(uri, bodyData);
            request.SetRequestHeader("Content-Type", "application/json");
            return new HttpPromise<TResult, TError>(request, onCompleted);
        }

        public static HttpPromise<TResult, TError> JsonPut(Uri uri, object bodyObj, OnCompletedDelegate onCompleted)
        {
            var json = JsonUtility.ToJson(bodyObj);
            var request = UnityWebRequest.Put(uri, json);
            request.SetRequestHeader("Content-Type", "application/json");
            return new HttpPromise<TResult, TError>(request, onCompleted);
        }

    }
}
