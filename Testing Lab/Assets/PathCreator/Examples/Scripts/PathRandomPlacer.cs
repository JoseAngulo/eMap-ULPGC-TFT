using PathCreation;
using UnityEngine;

namespace PathCreation.Examples {

    [ExecuteInEditMode]
    public class PathRandomPlacer : PathSceneTool {

        public GameObject prefab;
        public GameObject holder;
        public int objectsToSpawn;
        public float spacing = 3;

        const float minSpacing = .1f;

        void Generate () {
            
            if (pathCreator != null && prefab != null && holder != null) {
                DestroyObjects ();

                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing);

                float randomPoint = Random.Range(0, path.length);

                while (objectsToSpawn > 0) {
                    Vector3 point = path.GetPointAtDistance (randomPoint);
                    Quaternion rot = path.GetRotationAtDistance (randomPoint);
                    Instantiate (prefab, point, rot, holder.transform);
                    randomPoint = Random.Range(0, path.length);
                    objectsToSpawn--;
                }
            }
        }

        void DestroyObjects () {
            int numChildren = holder.transform.childCount;
            for (int i = numChildren - 1; i >= 0; i--) {
                DestroyImmediate (holder.transform.GetChild (i).gameObject, false);
            }
        }

        protected override void PathUpdated () {
            if (pathCreator != null) {
                Generate ();
            }
        }
    }
}