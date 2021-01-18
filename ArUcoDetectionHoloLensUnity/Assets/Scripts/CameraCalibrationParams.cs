using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCalibrationParams : MonoBehaviour
{
    // Calibration parameters from opencv, compute once for each hololens 2 device
    //{"camera_matrix": [[677.8968352717175, 0.0, 439.2388714449508], [0.0, 677.1775976226464, 231.50848952714483], [0.0, 0.0, 1.0]], 
    //"dist_coeff": [[-0.002602963842533594, -0.008751170499511022, -0.0022398259556777236, -5.941804169976817e-05, 0.0]], 
    //"height": 504, "width": 896}
    //677.8968352717175f, 677.1775976226464f, // focal length (0,0) & (1,1)
    //439.2388714449508f, 231.50848952714483f, // principal point (0,2) & (2,2)
    //-0.002602963842533594f, -0.008751170499511022f, 0.0f, // radial distortion (0,0) & (0,1) & (0,4)
    //-0.0022398259556777236f, -5.941804169976817e-05f, // tangential distortion (0,2) & (0,3)
    //504, 896); // image width and height

    public Vector2 focalLength;
    public Vector2 principalPoint;
    public Vector3 radialDistortion;
    public Vector2 tangentialDistortion;
    public int imageWidth;
    public int imageHeight;
}
