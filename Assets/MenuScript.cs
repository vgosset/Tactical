// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;
// using Resources.resx;
//
// public class MenuScript
// {
//   [MenuItem("Tools/AssignTileMat")]
//   public void AssignTileMat()
//   {
//     GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
//
//     Material material = Recources.Load<Material>("Tile");
//
//     foreach (GameObject go in tiles)
//     {
//       go.GetComponent<Renderer>().material = material;
//     }
//   }
//
// }
