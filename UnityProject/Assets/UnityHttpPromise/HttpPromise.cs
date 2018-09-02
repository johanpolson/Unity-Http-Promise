namespace JohanPolosn.UnityHttpPromise
{
    using System;
    using UnityEngine;
    using UnityEngine.Networking;

    public partial class HttpPromise<TResult, TError>
         : CustomYieldInstruction, IDisposable
    {
        public delegate void CompletedDelegate(HttpPromise<TResult, TError> promise);

        public delegate void OnCompletedDelegate(UnityWebRequest request, HttpPromiseSeter promise);

        public static implicit operator bool(HttpPromise<TResult, TError> obj)
        {
            return !obj.HasError;
        }

        private OnCompletedDelegate onCompleted;

        protected bool notCompleted = true;

        protected UnityWebRequest unityWebRequest;

        public event CompletedDelegate completed;

        public virtual ulong DownloadedBytes { get { return this.unityWebRequest.downloadedBytes; } }

        public virtual float DownloadProgress { get { return this.unityWebRequest.downloadProgress; } }

        public virtual ulong UploadedBytes { get { return this.unityWebRequest.uploadedBytes; } }

        public virtual float UploadProgress { get { return this.unityWebRequest.uploadProgress; } }

        public long ResponseCode { get; protected set; }

        public bool HasError { get; protected set; }

        public TResult Result { get; protected set; }

        public TError Error { get; protected set; }

        public override bool keepWaiting { get { return this.notCompleted; } }

        protected HttpPromise() { }

        public HttpPromise(UnityWebRequest unityWebRequest)
            : this(unityWebRequest, null)
        {
        }

        public HttpPromise(UnityWebRequest unityWebRequest, OnCompletedDelegate onCompleted)
        {
            if (unityWebRequest == null)
            {
                throw new ArgumentNullException("unityWebRequest");
            }

            this.unityWebRequest = unityWebRequest;
            this.onCompleted = onCompleted;
            var asyncOperation = unityWebRequest.SendWebRequest();
            asyncOperation.completed += _ =>
            {
                this.ResponseCode = this.unityWebRequest.responseCode;
                this.notCompleted = false;
                try
                {
                    if (this.onCompleted != null)
                    {
                        this.onCompleted(this.unityWebRequest, new HttpPromiseSeter(this));
                    }
                }
                catch (Exception)
                {
                    this.HasError = true;
                    throw;
                }
                finally
                {
                    this.InvokeCompleted();
                }
            };
        }

        public void Dispose()
        {
            if (this.unityWebRequest != null)
            {
                this.unityWebRequest.Dispose();
            }
        }

        protected void InvokeCompleted()
        {
            if (this.completed != null)
            {
                this.completed(this);
            }
        }

    }
}
