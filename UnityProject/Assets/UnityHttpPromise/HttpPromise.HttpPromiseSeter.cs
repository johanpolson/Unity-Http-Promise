namespace JohanPolosn.UnityHttpPromise
{
    using System;

    public partial class HttpPromise<TResult, TError>
    {
        public class HttpPromiseSeter
        {
            private HttpPromise<TResult, TError> promise;

            public HttpPromiseSeter(HttpPromise<TResult, TError> promise)
            {
                this.promise = promise;
            }

            public bool HasError
            {
                get { return this.promise.HasError; }
                set { promise.HasError = value; }
            }

            public TResult Result
            {
                get { return this.promise.Result; }
                set
                {
                    if (this.promise.HasError)
                    {
                        throw new Exception("HasError Is set");
                    }

                    this.promise.Result = value;
                }
            }

            public TError Error
            {
                get { return this.promise.Error; }
                set
                {
                    this.promise.HasError = true;
                    this.promise.Error = value;
                }
            }
        }

    }
}
