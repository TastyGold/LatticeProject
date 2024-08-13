using LatticeProject.Utility;
using Raylib_cs;
using System.Numerics;

namespace LatticeProject.Game
{
    public class LatticeCamera
    {
        public Camera2D camera = new Camera2D()
        {
            Target = Vector2.Zero,
            Zoom = 1,
            Rotation = 0,
            Offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2),
        };

        public Vector2 Target
        {
            get
            {
                return camera.Target;
            }
            set
            {
                camera.Target = value;
            }
        }
        public float Zoom
        {
            get
            {
                return camera.Zoom;
            }
            set
            {
                camera.Zoom = value;
            }
        }
        public float Rotation
        {
            get
            {
                return camera.Rotation;
            }
            set
            {
                camera.Rotation = value;
            }
        }
        public Vector2 Offset
        {
            get
            {
                return camera.Offset;
            }
            set
            {
                camera.Offset = value;
            }
        }

        private float targetZoom = 1;
        private Vector2 targetPosition = Vector2.Zero;

        public float cameraPanSpeed = 1200;
        public float cameraZoomLerpSpeed = 40f;
        public float cameraZoomFactor = 1.3f;

        public float maxZoom = 2;
        public float minZoom = 0.2f;

        public void UpdateCamera()
        {
            HandleCameraZooming();
            HandleCameraMovement();
        }

        private void HandleCameraZooming()
        {
            if (!Raylib.IsKeyDown(KeyboardKey.LeftShift))
            {
                if (Raylib.GetMouseWheelMove() > 0 && targetZoom < maxZoom)
                {
                    targetZoom *= cameraZoomFactor;
                }
                if (Raylib.GetMouseWheelMove() < 0 && targetZoom > minZoom)
                {
                    targetZoom /= cameraZoomFactor;
                }
            }
            camera.Zoom = LatticeMath.Lerp(camera.Zoom, targetZoom, Raylib.GetFrameTime() * cameraZoomLerpSpeed);
        }

        private void HandleCameraMovement()
        {
            Vector2 inputVector = Vector2.Zero;

            if (Raylib.IsKeyDown(KeyboardKey.A)) inputVector.X--;
            if (Raylib.IsKeyDown(KeyboardKey.D)) inputVector.X++;
            if (Raylib.IsKeyDown(KeyboardKey.W)) inputVector.Y--;
            if (Raylib.IsKeyDown(KeyboardKey.S)) inputVector.Y++;

            if (inputVector.Y != 0 && Raylib.IsMouseButtonDown(0)) inputVector.X *= 0.577f;
            if (inputVector != Vector2.Zero) inputVector = Vector2.Normalize(inputVector);

            targetPosition += inputVector * cameraPanSpeed * Raylib.GetFrameTime() * (Raylib.IsKeyDown(KeyboardKey.LeftShift) ? 2 : 1) / camera.Zoom;

            camera.Target = Vector2.Lerp(camera.Target, targetPosition, Raylib.GetFrameTime() * 20f);
        }

        public LatticeCamera(Vector2 target, float zoom, float rotation, Vector2 offset)
        {
            camera = new Camera2D()
            {
                Target = target,
                Zoom = zoom,
                Rotation = rotation,
                Offset = offset,
            };
        }
    }
}