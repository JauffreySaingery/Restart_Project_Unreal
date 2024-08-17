# Restart_Project_Unreal
Add the option when rigth clicking a .uproject to rebuild the entire project.

![image](https://github.com/user-attachments/assets/4b55fd5f-c35c-45b7-a1d7-f5334187c1ed)

### /!\ Warrning once installed if you open epic games, it will ask to "fix your project" do not accept this because it will unintall this plugin.

## Installation
You can only have this plugin as a pocket version.
Extract and put the folder somewhere on your computer. double click RPU_Installer.exe select what you want and done.
Afterwords because it is using "Restart_Project_Unreal.exe" you can't delete the folder where you put it in.

## What's RPU doing :

First delete the files :
- .vs
- Binaries
- Build
- DerivedDataCache
- Intermediate
- "yourproject".sln

![image](https://github.com/user-attachments/assets/5823e82d-3d7c-4f12-be9c-b8f26617731d)

be aware if it can't do it you will not show you a error in version 1.0 !!!
If when click the option restart project option didn't do anything, it's probably because you didn't close unreal or visual studio (it happens when the files that wants to be delete are still used by an other application)


Secondly :
It build the solution.
the afterword launch the build on the project (you will need to answer the question if you want to build the modules).

![image](https://github.com/user-attachments/assets/93eec34c-2c78-471b-9d2b-d8d5a476e7ae)

Then lanch the project and opens it.
