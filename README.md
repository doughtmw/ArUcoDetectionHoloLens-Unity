# ArUcoDetectionHoloLens-Unity
ArUco marker tracking made available for use in Unity through [IL2CPP Windows Runtime support](https://docs.unity3d.com/2018.4/Documentation/Manual/IL2CPP-WindowsRuntimeSupport.html) for the HoloLens 1 (x86) and HoloLens 2 (ARM). 

Incorporates:
- [HoloLensForCV](https://github.com/microsoft/HoloLensForCV) sample from Microsoft, at personal [branch](https://github.com/doughtmw/HoloLensForCV/)
- [ArUco marker detection](https://docs.opencv.org/4.1.1/d5/dae/tutorial_aruco_detection.html) in OpenCV

![aruco tracking](https://github.com/doughtmw/ArUcoDetectionHoloLens-Unity/blob/master/single-aruco-marker.jpg)

## Requirements
- Tested with [Unity 2018.4 LTS and Unity 2019.4 LTS](https://unity3d.com/unity/qa/lts-releases
)
- [Visual Studio 2017/2019](https://visualstudio.microsoft.com/downloads/)
- Minimum [RS4](https://docs.microsoft.com/en-us/windows/mixed-reality/release-notes-april-2018), tested with [OS Build 17763.678](https://support.microsoft.com/en-ca/help/4511553/windows-10-update-kb4511553)
- Printed (or on screen) ArUco markers. Markers can be readily generated using the following [tool](http://chev.me/arucogen/)

## ArUco Detection Sample
1. Open HoloLensForCV sample in VS2017 and install included [OpenCV.HoloLens.3411](https://github.com/doughtmw/NuGet-Package-Creation/blob/master/OpenCV.HoloLens.3411.0.0.nupkg) NuGet package to HoloLensForCV project
2. Build the HoloLensForCV project (x86 OR ARM, Debug or Release) 
3. Copy all output files from HoloLensForCV output path (dlls and HoloLensForCV.winmd) to the Assets->Plugins->x86/ARM folder of the ArUcoDetectionHoloLensUnity project
4. Open ArUcoDetectionHoloLensUnity Unity project and build using IL2CPP, ensure that unsafe code is allowed under Unity Player Settings->Other Settings
5. Open VS solution, build then deploy to device
6. When looking at an aruco marker **from the correct ArUco dictionary**: this sample uses the ```6x6 (50, 100, 250, 500) dictionary``` by default, a virtual cube, which is scaled to the size of the ArUco marker, should appear on top of the marker

![aruco tracking view through pv camera](https://github.com/doughtmw/ArUcoDetectionHoloLens-Unity/blob/master/single-aruco-marker-pv-cam.jpg)

Figure is taken through the Microsoft Mixed Reality Capture interface, leading to a discrepancy in the position of the virtual cube. The [first figure](https://github.com/doughtmw/ArUcoDetectionHoloLens-Unity/blob/master/single-aruco-marker.jpg) was recorded directly through the HoloLens optical display and is a better representation of how tracking looks in the point-of-view of a user. 

## Using other research mode sensor streams - HoloLens 1 only...
To enable the use of [HoloLens research mode streams](https://docs.microsoft.com/en-us/windows/mixed-reality/research-mode) in Unity, additional permissions are required for the project. Navigate to Unity project build folder and modify the Package.appxmanifest file to include: 
- Restricted capabilities package:
```xml 
<Package 
  xmlns:mp="http://schemas.microsoft.com/appx/2014/phone/manifest" 
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10" 
  xmlns:uap2="http://schemas.microsoft.com/appx/manifest/uap/windows10/2" 
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3" 
  xmlns:uap4="http://schemas.microsoft.com/appx/manifest/uap/windows10/4" 
  xmlns:iot="http://schemas.microsoft.com/appx/manifest/iot/windows10" 
  xmlns:mobile="http://schemas.microsoft.com/appx/manifest/mobile/windows10" 
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities" 
  IgnorableNamespaces="uap uap2 uap3 uap4 mp mobile iot rescap" 
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"> 
```
- Modified capabilities with with new package:
```xml
  <Capabilities>
    <rescap:Capability Name="perceptionSensorsExperimental" />
    <Capability Name="internetClient" />
    <Capability Name="internetClientServer" />
    <Capability Name="privateNetworkClientServer" />
    <uap2:Capability Name="spatialPerception" />
    <DeviceCapability Name="webcam" />
  </Capabilities>
```

Now, modifying the selected sensor for streaming in Unity (for depth and infrared streams, more processing of input frames will likely be required to visualize) different streams can be selected.

![research mode sensor enum](https://github.com/doughtmw/ArUcoDetectionHoloLens-Unity/blob/master/research-mode-sensors.PNG)
