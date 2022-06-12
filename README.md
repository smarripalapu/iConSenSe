# iConSenSe
Extracting IFC properties

### Prerequisites:
Install Python (3.8.8 version is used in this project) https://www.python.org/downloads/release/python-388/ <br>
Install ifcConvert in the server from http://ifcopenshell.org/ifcconvert <br>
Install ifcOpenShell-python from http://ifcopenshell.org/python <br>


#### ifcConvertPipeline script version for linux OS
This script can be used to convert ifc files to .dae files/xml files and import them directly to. new unity project. <br>
To execute, <br>
python ifcConvertPipelineLinux.py

#### ifcConvertPipeline script version for windows OS
This script can be used to convert ifc files to .dae files/xml files and import them directly to. new unity project. <br>
To execute,  <br>
py ifcConvertPipelineWin.py


## ICONSENSE
### IMMERSIVE HOLOGRAPHIC SPACE

#### Introduction
New Augmented Reality (AR) technology used through holograms can enhance and transform the decision-making experiences of 2D and 3D visualizations of engineering designs, by bringing to the physical environment the sense of interactivity for the recognition and analysis of design components and by communicating the engineers’ interaction with design components physically. 
Holographic technology allows translating virtual components of the engineering design to lock and move them in the physical space, which gives the user a sense of the scale of proportion, sense of form from geometries, and awareness of the component functionality in the engineering system.
Development

#### Step 1: Converting ifc file into dae format, create unity project.
Prerequisites:
Install Python (3.8.8 version is used in this project) https://www.python.org/downloads/release/python-388/
Install ifcConvert in the server from http://ifcopenshell.org/ifcconvert
Install ifcOpenShell-python from http://ifcopenshell.org/python

ifcConvertPipeline script version for linux OS :
This script can be used to convert ifc files to .dae files/xml files and import them directly to. new unity project. Github link for scripts: https://github.com/smarripalapu/iConSenSe
To execute,
python ifcConvertPipelineLinux.py
ifcConvertPipeline script version for windows OS:
This script can be used to convert ifc files to .dae files/xml files and import them directly to. new unity project. Github link for scripts: https://github.com/smarripalapu/iConSenSe
To execute,
py ifcConvertPipelineWin.py

#### Step 2: Open the unity project created and set up player settings for the universal windows application.
Disable static batching in player settings, windows tab.
Enable capabilities like access to WebCam, removable storage, Microphone…


In supported device families, use Holographic option.

For more information on setting up, deploying and building Universal Windows applications in unity, check https://docs.unity3d.com/Manual/class-PlayerSettingsWSA.html

#### Step 3: Installing the Windows Mixed Reality XR Plugin
To install the Windows Mixed Reality XR Plugin, do the following:
Install the XR Management package from Package Manager 
Follow the instructions for Installing an XR Plugin using XR Management in the End User Documentation section.
Note that the XR settings tab now has a dropdown for "Windows Mixed Reality". Navigate to the XR Plugin Management -> Windows Mixed Reality settings window in Project Settings to create a Windows MR XR Plugin specific settings asset. This asset is editable from the Windows Mixed Reality window and can toggle settings such as Shared Depth buffer support.

#### Step 4: Using the Mixed Reality Feature Tool
If MRTK is not installed in the system, as described in Welcome to the Mixed Reality Feature Tool you can download the tool using this link.

#### Step 5: Integrating unity with MRTK
Starting with version 2.5.0, using the Mixed Reality Feature Tool, the Microsoft Mixed Reality Toolkit integrates with the Unity Package Manager (UPM) when using Unity 2019.4 and newer.
Use this link for steps to add MRTK to the unity project. Link

#### Step 6: Setting up MRTK profiles for inputs
The Mixed Reality Toolkit centralizes as much of the configuration required to manage the toolkit as possible (except for true runtime "things").

This guide is a simple walkthrough for each of the configuration profile screens currently available for the toolkit.
https://docs.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/configuration/mixed-reality-configuration-guide?view=mrtkunity-2021-05
Speech commands :
https://docs.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/configuration/mixed-reality-configuration-guide?view=mrtkunity-2021-05#speech-commands
Eye gaze profile setup:
https://docs.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/features/input/eye-tracking/eye-tracking-basic-setup?view=mrtkunity-2021-05
#### Step 7: Set up project objects.
Drag and drop the dae model imported to unity Assets  to the scene.
Assign main camera in Mixed reality playspace. 
Select all child objects of .dae model and add ObjectManipulator, BoxCollider, NearInteractableGrababble and ToolTipSpawner script. 
Make changes to tooltipspawner script as in https://github.com/smarripalapu/iConSenSe/tree/main/Assets
Add the tag “changecolor” to the objects which should change color onclick of changecolor button.
All the remaining child objects in dae file in the scene  should be assigned a tag “others”.



#### Step 8: Building and Deploying application to hololens
https://docs.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/supported-devices/wmr-mrtk?view=mrtkunity-2021-05



If you face any issue with configurations or versions, raise a question on https://forums.hololens.com/categories/questions-and-answers

