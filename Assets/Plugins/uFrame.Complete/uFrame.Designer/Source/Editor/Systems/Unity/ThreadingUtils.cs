﻿using System;
using UnityEditor;

namespace uFrame.Editor.Unity
{
    public class ThreadingUtils
    {

        public static DispatcherResult DispatchOnMainThread(Action x)
        {
            var d = new DispatcherResult();

            EditorApplication.delayCall += () =>
            {
                x();
                d.Done = true;
            };

            return d;
        }

        public static DispatcherResult WaitOnMainThread(Func<bool> selector)
        {
            var d = new DispatcherResult();

            EditorApplication.CallbackFunction callbackFunction = null;
            callbackFunction = () =>
            {
                if (selector())
                {
                    d.Done = true;
                    EditorApplication.update -= callbackFunction;
                }
            };
            EditorApplication.update += callbackFunction;

            return d;
        }



        public class DispatcherResult
        {
            public bool Done { get; set; }
        }
    }
}
