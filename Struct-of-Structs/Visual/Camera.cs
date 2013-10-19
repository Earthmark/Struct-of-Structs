using SharpDX;

namespace Struct_of_Structs.Visual
{
	public class Camera
	{
		private Vector3 position;
		private Vector3 rotation;

		public Matrix View { get; private set; }

		public Vector3 Position
		{
			get { return position; }
			set
			{
				position = value;
				CaluclateView();
			}
		}

		public Vector3 Rotation
		{
			get { return rotation; }
			set
			{
				rotation = value;
				CaluclateView();
			}
		}

		public Camera(Vector3 position, Vector3 rotation)
		{
			this.position = position;
			this.rotation = rotation;
			CaluclateView();
		}

		private void CaluclateView()
		{
			var up = Vector3.UnitY;
			var lookAt = Vector3.UnitZ;

			var smallRotation = rotation * 0.0174532925f;
			var rotationMatrix = Matrix.RotationYawPitchRoll(smallRotation.Y, smallRotation.X, smallRotation.Z);

			lookAt = Vector3.TransformCoordinate(lookAt, rotationMatrix);
			up = Vector3.TransformCoordinate(up, rotationMatrix);

			lookAt = position + lookAt;

			View = Matrix.LookAtLH(position, lookAt, up);
		}
	}
}
