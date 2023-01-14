using UnityEngine;

public class RotationTransformation : Transformation {

	public Vector3 rotation;

	//重载旋转矩阵
	//注：unity的旋转顺序是ZXY
	public override Matrix4x4 Matrix {
		//旋转矩阵，SetColumn是指用列来写入
		get{
			//把参数转化为弧度
			float radX = rotation.x * Mathf.Deg2Rad;
			float radY = rotation.y * Mathf.Deg2Rad;
			float radZ = rotation.z * Mathf.Deg2Rad;
			//利用弧度分别算出每个分量的sin和cos值
			float sinX = Mathf.Sin(radX);
			float cosX = Mathf.Cos(radX);
			float sinY = Mathf.Sin(radY);
			float cosY = Mathf.Cos(radY);
			float sinZ = Mathf.Sin(radZ);
			float cosZ = Mathf.Cos(radZ);

			

			/*
				xAxis.x  yAxis.x  zAxis.x     x
			   [xAxis.y  yAxis.y  zAxis.y] * [y]
				xAxis.z  yAxis.z  zAxis.z     z
			以下计算过程如上方写的旋转3x3矩阵与对象坐标相乘，得到变换后的坐标
			具体推导过程看https://catlikecoding.com/unity/tutorials/rendering/part-1/

			以下注释的是原计算
			Vector3 xAxis = new Vector3(
			cosY * cosZ,
			cosX * sinZ + sinX * sinY * cosZ,
			sinX * sinZ - cosX * sinY * cosZ
			);
			Vector3 yAxis = new Vector3(
			-cosY * sinZ,
			cosX * cosZ - sinX * sinY * sinZ,
			sinX * cosZ + cosX * sinY * sinZ
			);
			Vector3 zAxis = new Vector3(
			sinY,
			-sinX * cosY,
			cosX * cosY
			);

			return xAxis * point.x + yAxis * point.y + zAxis * point.z;
			*/


			//现使用重载矩阵，目的是把三个变换矩阵合成一个矩阵使用
			Matrix4x4 matrix = new Matrix4x4();
			matrix.SetColumn(0, new Vector4(
				cosY * cosZ,
				cosX * sinZ + sinX * sinY * cosZ,
				sinX * sinZ - cosX * sinY * cosZ,
				0f
			));
			matrix.SetColumn(1, new Vector4(
				-cosY * sinZ,
				cosX * cosZ - sinX * sinY * sinZ,
				sinX * cosZ + cosX * sinY * sinZ,
				0f
			));
			matrix.SetColumn(2, new Vector4(
				sinY,
				-sinX * cosY,
				cosX * cosY,
				0f
			));
			matrix.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
			return matrix;
			}
	}	
}