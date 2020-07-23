using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArUcoDetectionHoloLensUnity
{
    public abstract class CvUtils
    {
        // Enum for selection of device type
        public enum DeviceTypeUnity
        {
            HL1 = 0,
            HL2 = 1
        }

        // https://docs.opencv.org/trunk/dc/df7/dictionary_8hpp.html
        public enum ArUcoDictionaryName {
            DICT_4X4_50 = 0,
            DICT_4X4_100,
            DICT_4X4_250,
            DICT_4X4_1000,
            DICT_5X5_50,
            DICT_5X5_100,
            DICT_5X5_250,
            DICT_5X5_1000,
            DICT_6X6_50,
            DICT_6X6_100,
            DICT_6X6_250,
            DICT_6X6_1000,
            DICT_7X7_50,
            DICT_7X7_100,
            DICT_7X7_250,
            DICT_7X7_1000,
            DICT_ARUCO_ORIGINAL,
            DICT_APRILTAG_16h5,
            DICT_APRILTAG_25h9,
            DICT_APRILTAG_36h10,
            DICT_APRILTAG_36h11
        }

        // Sensor type enum for selection
        // of media frame source group.
        public enum SensorTypeUnity
        {
            Undefined = -1,
            PhotoVideo = 0,
            ShortThrowToFDepth = 1,
            ShortThrowToFReflectivity = 2,
            LongThrowToFDepth = 3,
            LongThrowToFReflectivity = 4,
            VisibleLightLeftLeft = 5,
            VisibleLightLeftFront = 6,
            VisibleLightRightFront = 7,
            VisibleLightRightRight = 8,
            NumberOfSensorTypes = 9
        }

        // Convert from system numerics to unity Vector 3
        public static Vector3 Vec3FromFloat3(System.Numerics.Vector3 v)
        {
            return new Vector3()
            {
                x = v.X,
                y = v.Y,
                z = v.Z
            };
        }

        public static Vector3 GetVectorFromMatrix(Matrix4x4 m)
        {
            return m.GetColumn(3);
        }

        public static Quaternion GetQuatFromMatrix(Matrix4x4 m)
        {
            return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
        }

        // CameraToWorld matrices assume a camera's front direction is in the negative z-direction
        // However, the values obtained from OpenCV assumes the camera's front direction is in the postiive z direction
        // Therefore, we negate the z components of our opencv camera transform
        public static Matrix4x4 TransformInUnitySpace(Vector3 v, Quaternion q)
        {
            var tOpenCV = Matrix4x4.TRS(v, q, Vector3.one);
            var t = tOpenCV;
            t.m20 *= -1.0f;
            t.m21 *= -1.0f;
            t.m22 *= -1.0f;
            t.m23 *= -1.0f;

            return t;
        }

        // Convert from system numerics to unity matrix 4x4
        public static Matrix4x4 Mat4x4FromFloat4x4(System.Numerics.Matrix4x4 m)
        {
            return new Matrix4x4()
            {
                m00 = m.M11,
                m10 = m.M21,
                m20 = m.M31,
                m30 = m.M41,

                m01 = m.M12,
                m11 = m.M22,
                m21 = m.M32,
                m31 = m.M42,

                m02 = m.M13,
                m12 = m.M23,
                m22 = m.M33,
                m32 = m.M43,

                m03 = m.M14,
                m13 = m.M24,
                m23 = m.M34,
                m33 = m.M44,
            };
        }

        // Get a rotation quaternion from rodrigues
        public static Quaternion RotationQuatFromRodrigues(Vector3 v)
        {
            var angle = Mathf.Rad2Deg * v.magnitude;
            var axis = v.normalized;
            Quaternion q = Quaternion.AngleAxis(angle, axis);

            // Ensure: 
            // Positive x axis is in the left direction of the observed marker
            // Positive y axis is in the upward direction of the observed marker
            // Positive z axis is facing outward from the observed marker
            // Convert from rodrigues to quaternion representation of angle
            q = Quaternion.Euler(
                -1.0f * q.eulerAngles.x,
                q.eulerAngles.y,
                -1.0f * q.eulerAngles.z) * Quaternion.Euler(0, 0, 180);

            return q;
        }
    }
}

