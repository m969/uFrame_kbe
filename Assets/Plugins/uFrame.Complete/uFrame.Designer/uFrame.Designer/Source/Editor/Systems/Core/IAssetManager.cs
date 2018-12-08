using System;
using System.Collections.Generic;

namespace uFrame.Editor.Core
{
    public interface IAssetManager
    {
        object CreateAsset(Type type);
        object LoadAssetAtPath(string path, Type repositoryFor);

        IEnumerable<object> GetAssets(Type type);
    }
}