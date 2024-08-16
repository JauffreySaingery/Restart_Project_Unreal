# Restart_Project_Unreal
Add the option when rigth clicking a .uproject to rebuild the entire project.

![image](https://github.com/user-attachments/assets/4b55fd5f-c35c-45b7-a1d7-f5334187c1ed)

the plugin does the following :

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
Then lanch the project and opens it.
