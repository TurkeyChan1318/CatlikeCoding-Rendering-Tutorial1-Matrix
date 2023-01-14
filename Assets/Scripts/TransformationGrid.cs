using UnityEngine;
using System.Collections.Generic;

public class TransformationGrid : MonoBehaviour{
    public Transform prefab;//创建预制体

	Matrix4x4 transformation;//创建变换矩阵

	List<Transformation>transformations;//创建变换列表

    public int gridResolution = 10;//网格分辨率

    Transform[] grid;//创建网格数组

    void Awake(){
        grid = new Transform[gridResolution * gridResolution * gridResolution];
		//循环绘制网格点
        for (int i = 0, z = 0; z < gridResolution; z++) {
			for (int y = 0; y < gridResolution; y++) {
				for (int x = 0; x < gridResolution; x++, i++) {
					grid[i] = CreateGridPoint(x, y, z);
                }
            }
        }

		transformations = new List<Transformation>();
    }

	void Update () {
		UpdateTransformation();
		for (int i = 0, z = 0; z < gridResolution; z++) {
			for (int y = 0; y < gridResolution; y++) {
				for (int x = 0; x < gridResolution; x++, i++) {
					grid[i].localPosition = TransformPoint(x, y, z);
				}
			}
		}
	}

	//该方法用于更新变换
	void UpdateTransformation () {
		GetComponents<Transformation>(transformations);
		if (transformations.Count > 0) {
			transformation = transformations[0].Matrix;
			for (int i = 1; i < transformations.Count; i++) {
				transformation = transformations[i].Matrix * transformation;
			}
		}
	}

	//该方法用于获取变换后的坐标点
	Vector3 TransformPoint (int x, int y, int z) {
		Vector3 coordinates = GetCoordinates(x, y, z);
		return transformation.MultiplyPoint(coordinates);
	}

	//该方法用于绘制网格点
    Transform CreateGridPoint (int x, int y, int z){
        Transform point = Instantiate<Transform>(prefab);//实例化预制体
		point.localPosition = GetCoordinates(x, y, z);
		point.GetComponent<MeshRenderer>().material.color = new Color(
			(float)x / gridResolution,
			(float)y / gridResolution,
			(float)z / gridResolution
		);//给不同的点绘制不同的颜色
		return point;
    }

	//该方法用于获取网格点坐标
    Vector3 GetCoordinates (int x, int y, int z) {
		return new Vector3(
			x - (gridResolution - 1) * 0.5f,
			y - (gridResolution - 1) * 0.5f,
			z - (gridResolution - 1) * 0.5f
		);
	}
}