Requires Unity Editor 6000.6.2f1, installed through Unity Hub (tested). Materials target the Built-in Render Pipeline; URP/HDRP may require conversion.

1. Extract the ZIP and add the extracted project folder in Unity Hub.
2. Open it with the matching Editor and allow package resolution and asset import to finish.
3. Open `Assets/SkeletonGuardian/Scenes/Preview.unity` and press Play. Choose animations in the viewer; equipment switches with each motion.
4. Alternatively import `SkeletonGuardian.unitypackage` through Assets > Import Package > Custom Package in a compatible project.
5. Use `Assets/SkeletonGuardian/Prefabs/SkeletonGuardian.prefab` in your own scenes. Retain its native meshes, avatar, animations, materials, textures, and equipment dependencies.

Raw FBX is not required for native playback. Add your own gameplay controller and root-motion handling.
