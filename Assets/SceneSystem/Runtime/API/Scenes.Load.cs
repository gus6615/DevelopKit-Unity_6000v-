using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.SceneSystem.LoadSceneOperations;

namespace UnityEngine.SceneSystem
{
    public static partial class Scenes
    {
        private static List<LoadSceneOperationBase> operationListCache = new List<LoadSceneOperationBase>();

        public static ISceneHandler Loader { get; set; } = new SceneHandler();

        public static LoadSceneOperationHandle LoadSceneAsync(int sceneBuildIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(sceneBuildIndex);
            return Loader.LoadAsync(scenePath, loadSceneMode);
        }

        public static LoadSceneOperationHandle LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            return Loader.LoadAsync(sceneName, loadSceneMode);
        }

#if USE_SCENE_REFERENCE
        public static LoadSceneOperationHandle LoadSceneAsync(SceneReference sceneReference, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            return Loader.LoadAsync(sceneReference.Path, loadSceneMode);
        }
#endif

        public static LoadSceneOperationHandle LoadScenesAsync(params int[] sceneBuildIndexes)
        {
            return LoadScenesAsync(LoadMultiSceneMode.Parallel, sceneBuildIndexes);
        }

        public static LoadSceneOperationHandle LoadScenesAsync(LoadMultiSceneMode multiLoadSceneMode, params int[] sceneBuildIndexes)
        {
            if (sceneBuildIndexes.Length == 0)
            {
                ThrowHelper.Throw_ArgArraySizeZero_Exception();
                return default;
            }

            operationListCache.Clear();
            foreach (var index in sceneBuildIndexes)
            {
                var name = SceneUtility.GetScenePathByBuildIndex(index);
                operationListCache.Add(Loader.GetLoadSceneOperation(name, LoadSceneMode.Additive));
            }

            switch (multiLoadSceneMode)
            {
                default:
                case LoadMultiSceneMode.Parallel:
                    return new MergeLoadSceneOperation(operationListCache.ToArray()).Execute();
                case LoadMultiSceneMode.Sequential:
                    return new ConcatLoadSceneOperation(operationListCache.ToArray()).Execute();
            }
        }

        public static LoadSceneOperationHandle LoadScenesAsync(params string[] sceneNames)
        {
            return LoadScenesAsync(LoadMultiSceneMode.Parallel, sceneNames);
        }

        public static LoadSceneOperationHandle LoadScenesAsync(LoadMultiSceneMode multiLoadSceneMode, params string[] sceneNames)
        {
            if (sceneNames.Length == 0)
            {
                ThrowHelper.Throw_ArgArraySizeZero_Exception();
                return default;
            }

            operationListCache.Clear();
            foreach (var name in sceneNames)
            {
                operationListCache.Add(Loader.GetLoadSceneOperation(name, LoadSceneMode.Additive));
            }

            switch (multiLoadSceneMode)
            {
                default:
                case LoadMultiSceneMode.Parallel:
                    return new MergeLoadSceneOperation(operationListCache.ToArray()).Execute();
                case LoadMultiSceneMode.Sequential:
                    return new ConcatLoadSceneOperation(operationListCache.ToArray()).Execute();
            }
        }

#if USE_SCENE_REFERENCE
        public static LoadSceneOperationHandle LoadScenesAsync(params SceneReference[] sceneReferences)
        {
            return LoadScenesAsync(LoadMultiSceneMode.Parallel, sceneReferences);
        }

        public static LoadSceneOperationHandle LoadScenesAsync(LoadMultiSceneMode multiLoadSceneMode, params SceneReference[] sceneReferences)
        {
            if (sceneReferences.Length == 0)
            {
                ThrowHelper.Throw_ArgArraySizeZero_Exception();
                return default;
            }

            operationListCache.Clear();
            foreach (var reference in sceneReferences)
            {
                operationListCache.Add(Loader.GetLoadSceneOperation(reference.Path, LoadSceneMode.Additive));
            }

            switch (multiLoadSceneMode)
            {
                default:
                case LoadMultiSceneMode.Parallel:
                    return new MergeLoadSceneOperation(operationListCache.ToArray()).Execute();
                case LoadMultiSceneMode.Sequential:
                    return new ConcatLoadSceneOperation(operationListCache.ToArray()).Execute();
            }
        }
#endif

        public static LoadSceneOperationHandle UnloadSceneAsync(int sceneBuildIndex)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(sceneBuildIndex);
            return Loader.UnloadAsync(scenePath);
        }

        public static LoadSceneOperationHandle UnloadSceneAsync(string sceneName)
        {
            return Loader.UnloadAsync(sceneName);
        }

#if USE_SCENE_REFERENCE
        public static LoadSceneOperationHandle UnloadSceneAsync(SceneReference sceneReference)
        {
            return Loader.UnloadAsync(sceneReference.Path);
        }
#endif
        
        public static LoadSceneOperationHandle UnloadScenesAsync(params int[] sceneBuildIndexes)
        {
            if (sceneBuildIndexes.Length == 0)
            {
                ThrowHelper.Throw_ArgArraySizeZero_Exception();
                return default;
            }

            operationListCache.Clear();
            foreach (var index in sceneBuildIndexes)
            {
                var handle = UnloadSceneAsync(index);
                operationListCache.Add(handle.operation);
            }

            return new MergeLoadSceneOperation(operationListCache.ToArray()).Execute();
        }

        public static LoadSceneOperationHandle UnloadScenesAsync(params string[] sceneNames)
        {
            if (sceneNames.Length == 0)
            {
                ThrowHelper.Throw_ArgArraySizeZero_Exception();
                return default;
            }

            operationListCache.Clear();
            foreach (var name in sceneNames)
            {
                var handle = UnloadSceneAsync(name);
                operationListCache.Add(handle.operation);
            }

            return new MergeLoadSceneOperation(operationListCache.ToArray()).Execute();
        }

#if USE_SCENE_REFERENCE
        public static LoadSceneOperationHandle UnloadScenesAsync(params SceneReference[] sceneReferences)
        {
            if (sceneReferences.Length == 0)
            {
                ThrowHelper.Throw_ArgArraySizeZero_Exception();
                return default;
            }

            operationListCache.Clear();
            foreach (var reference in sceneReferences)
            {
                var handle = UnloadSceneAsync(reference);
                operationListCache.Add(handle.operation);
            }

            return new MergeLoadSceneOperation(operationListCache.ToArray()).Execute();
        }
#endif

        public static void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            Loader.Load(sceneName, loadSceneMode);
        }

#if USE_SCENE_REFERENCE
        public static void LoadScene(SceneReference sceneReference, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            Loader.Load(sceneReference.Path, loadSceneMode);
        }
#endif

        public static void LoadScene(int sceneBuildIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(sceneBuildIndex);
            Loader.Load(scenePath, loadSceneMode);
        }

        public static void UnloadScene(string sceneName)
        {
            Loader.Unload(sceneName);
        }

#if USE_SCENE_REFERENCE
        public static void UnloadScene(SceneReference sceneReference)
        {
            Loader.Unload(sceneReference.Path);
        }
#endif

        public static void UnloadScene(int sceneBuildIndex)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(sceneBuildIndex);
            Loader.Unload(scenePath);
        }

        public static void LoadScenes(params int[] sceneBuildIndexes)
        {
            foreach (var index in sceneBuildIndexes)
            {
                LoadScene(index, LoadSceneMode.Additive);
            }
        }

        public static void LoadScenes(params string[] sceneNames)
        {
            foreach (var sceneName in sceneNames)
            {
                LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }

#if USE_SCENE_REFERENCE
        public static void LoadScenes(params SceneReference[] sceneReferences)
        {
            foreach (var sceneReference in sceneReferences)
            {
                LoadScene(sceneReference, LoadSceneMode.Additive);
            }
        }
#endif

        public static void UnloadScenes(params int[] sceneBuildIndexes)
        {
            foreach (var index in sceneBuildIndexes)
            {
                UnloadScene(index);
            }
        }

        public static void UnloadScenes(params string[] sceneNames)
        {
            foreach (var sceneName in sceneNames)
            {
                UnloadScene(sceneName);
            }
        }

#if USE_SCENE_REFERENCE
        public static void UnloadScenes(params SceneReference[] sceneReferences)
        {
            foreach (var sceneReference in sceneReferences)
            {
                UnloadScene(sceneReference);
            }
        }
#endif
    }
}
