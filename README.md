# [Horror Maze] : [11]
# Members
Project Manager: Bayla Patin (BaylaPatin)\
Communications Lead: Mia Miranda (Astarosa-MM)\
Git Master: Brent Bolden (BrentBolden)\
Design Lead: Michael Vasquez (Mvasquez1)\
Quality Assurance Tester: Eyzeya Douglas (edoug-the-19th)

# About Our Software
Our project is a first person horror game that takes place in a maze. The goal is to get to the door at the end of the level and progress through multiple mazes.
## Platforms Tested on
- Windows
# Important Links
Kanban Board: https://github.com/orgs/CSC-3380-Spring-2025/projects/24 \
Designs: [link]\
Styles Guide(s): https://docs.google.com/document/d/1_ZzTzE_Z5fh7o3Nl8PJiodl_-QYDmxRJcALpyWDgjjQ/edit?tab=t.0

# How to Run Dev and Test Environment
To run the project you first need to install Godot 4.3 .NET which can be downloaded at https://godotengine.org/download/archive/4.4-stable/. Then extract the zip file to any location on your PC. 
Afterwards you need to download the .NET SDK at https://dotnet.microsoft.com/en-us/download. After downloading the installation file go the file location on your PC and run it and follow it's installation instrucitons. Both of these are all you need to run the project however VSCode has certain export variabes that only appear once VSCode is used to build the porject at least once and it's also just reccommended in general since Godot's editor is barebones. 
To download VSCode go to https://code.visualstudio.com/ and press on the download for Windows button or click on the other platforms link at the bottom right of the download button. After installing run the file and follow it's installation instructions. After it's finished installing you will also need the C# extension for VSCode at https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp. Press the install button and it will take your VSCode installation. There, you just need to click install.

Now, to run the project, first open Godot and then click the Import button. Navigate to the folder where the project is located and click the project.godot file and then click Import & Edit. Wait until the assets are reimported. If the progress bar hangs or the project closes simply open Godot again click on the project in the file list and click the edit button and the assets should be reimported. To run the project click the play button on the top right of the screen. This is the minimum needed to run the project. However to access some of the export variables and to have a better coding enviroment we need to set up VSCode.

To start go the Editor in the menu bar and select the Editor Settings menu. Then search for the Dotnet tab and select Editor under it. Set the External Editor to Visual Studio Code. Now go the file location where the project is located and create a folder called .vscode. Create two files launch.json and tasks.json.

Copy this code into the launch.json file. Note that for the launch configuration to work you need to either set up a GODOT4 environment varaible that contains the path to the godot excecutable or replace the "program" parameter with the path to the GODOT executable.

Example of launch.json with GODOT4:
```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Play",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${env:GODOT4}",
            "args": [],
            "cwd": "${workspaceFolder}",
            "stopAtEntry": false,
        }
    ]
}
```
Example of launch.json with the path directly:

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Play",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "C:/User/Path/To/Godot.exe",
            "args": [],
            "cwd": "${workspaceFolder}",
            "stopAtEntry": false,
        }
    ]
}
```

For the task.json file:
```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "build",
            "command": "dotnet",
            "type": "process",
            "args": [
                "build"
            ],
            "problemMatcher": "$msCompile"
        }
    ]
}
```
These steps are also located at https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_basics.html under the Visual Studio Code section.

All you need to do now is to go to File -> Open Folder to find the folder and then start the debugger in VSCode and the Godot project will run. Run the project at least once in VSCode to get the Export variables created in the files to show up in the editor.

## Dependencies
- Godot .NET 4.3 
- .NET SDK
- VSCode
- VSCode C# extension
### Downloading Dependencies
Godot 4.3:\
https://godotengine.org/download/archive/4.3-stable/
Make sure to download the .NET version of Godot 4.3 

.NET SDK:\
https://dotnet.microsoft.com/en-us/download
Make sure to download the 64-bit version if using the 64-bit version of Godot.

VSCode: \
https://code.visualstudio.com 

VSCode C# extension: \
https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp
## Commands

To clone the project run git clone:
```sh
git clone https://github.com/CSC-3380-Spring-2025/Team-11.git
```
To stage changes:
```sh
git add .
```
To check the status of your tracked files:
```sh
git status
```
To commit changes:
```sh
git commit -m "message"
```
To push changes to remote:
```sh
git push origin target-branch
```
To get changes from another repo:
```sh
git pull
```
To update changes from remote:
```sh
git fetch
```

