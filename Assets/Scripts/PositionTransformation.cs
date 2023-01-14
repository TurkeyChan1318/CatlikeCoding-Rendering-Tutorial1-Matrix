using UnityEngine;

public class PositionTransformation : Transformation {

	public Vector3 position;

	//重载位移矩阵
    public override Matrix4x4 Matrix {
		//位移矩阵，SetRow是指用行来写入
		get {
			Matrix4x4 matrix = new Matrix4x4();
			matrix.SetRow(0, new Vector4(1f, 0f, 0f, position.x));
			matrix.SetRow(1, new Vector4(0f, 1f, 0f, position.y));
			matrix.SetRow(2, new Vector4(0f, 0f, 1f, position.z));
			matrix.SetRow(3, new Vector4(0f, 0f, 0f, 1f));
			return matrix;
		}
	}
}