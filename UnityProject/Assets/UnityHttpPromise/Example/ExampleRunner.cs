namespace JohanPolosn.UnityHttpPromise.Example
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using System.Linq;

    using JohanPolosn.UnityHttpPromise;

    public class ExampleRunner : MonoBehaviour
    {
        private JSONPlaceholderApi api = new JSONPlaceholderApi();

        IEnumerator Start()
        {
            var postPromise = api.GetPost(1);
            yield return postPromise;

            if (postPromise)
            {
                Debug.Log(postPromise.Result);
            }
            else
            {
                Debug.LogError("error on post1:" + postPromise.Error);
            }

            api.GetPosts().completed += postsPromise =>
            {
                if (postsPromise)
                {
                    Debug.Log("-GetPosts-");
                    foreach (var post in postsPromise.Result.Take(3))
                    {
                        Debug.Log("- " + post);
                    }
                }
                else
                {
                    Debug.LogError("error GetPosts:" + postsPromise.Error);
                }
            };

            api.GetPost(3);
        }
    }

    public class JSONPlaceholderApi
    {
        [Serializable]
        public class Post
        {
            public long id;
            public string title;

            public override string ToString()
            {
                return "id:" + id + ", title:" + title.Substring(0, Math.Min(title.Length, 20));
            }
        }

        [Serializable]
        private class PostList
        {
            public List<Post> list;
        }

        public string baseUrl = "https://jsonplaceholder.typicode.com/";

        public HttpPromise<Post, string> GetPost(long id)
        {
            return HttpPromise<Post, string>.Get(
                this.baseUrl + "posts/" + id,
                (request, promise) =>
                {
                    if (request.isNetworkError || request.isHttpError)
                    {
                        promise.Error = request.error;
                    }
                    else
                    {
                        promise.Result = JsonUtility.FromJson<Post>(request.downloadHandler.text);
                    }
                });
        }

        public HttpPromise<List<Post>, string> GetPosts()
        {
            return HttpPromise<List<Post>, string>.Get(
                this.baseUrl + "posts",
                (request, promise) =>
                {
                    if (request.isNetworkError || request.isHttpError)
                    {
                        promise.Error = request.error;
                    }
                    else
                    {
                        var josn = "{\"list\":" + request.downloadHandler.text + "}";
                        promise.Result = JsonUtility.FromJson<PostList>(josn).list;
                    }
                });
        }

    }

}