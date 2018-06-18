using System;
using Promises;
using UnityEngine;

namespace Entitas._tests
{
    public class PromiseTest : MonoBehaviour
    {
        private Deferred<int> promise;
        private Deferred<int> thenPromise;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var testPromise = StartPromise().Then(NextPromise());
                testPromise.OnFulfilled += OnFulfilled;
                testPromise.OnFailed += OnFailed;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                FulfillPromise();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                FailPromise();
            }
        }

        private void OnFailed(Exception error)
        {
            Debug.Log(error.Message);
        }

        private Deferred<int> StartPromise()
        {
            promise = new Deferred<int>();
            Debug.Log("Start promise");
            return promise;
        }

        private Deferred<int> NextPromise()
        {
            thenPromise = new Deferred<int>();
            Debug.Log("Next promise");
            return thenPromise;
        }

        private void FulfillPromise()
        {
            promise.Fulfill(5);
        }

        private void FailPromise()
        {
            promise.Fail(new Exception("Failed start promise"));
        }

        private void OnFulfilled(int result)
        {
            Debug.Log("Start Fulfilled: " + result);
        }
    }
}