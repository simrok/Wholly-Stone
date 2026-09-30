# Skeleton Guardian — Unity — 1.0.1

A stylized armored skeleton with a separate sword and shield, 110 animations, and per-motion equipment loadouts.

Native engine package: free or pay what you want. Editable source files are available in the optional Source archive.

## Install and preview

Requires Unity Editor 6000.6.2f1, installed through Unity Hub (tested). Materials target the Built-in Render Pipeline; URP/HDRP may require conversion.

1. Extract the ZIP and add the extracted project folder in Unity Hub.
2. Open it with the matching Editor and allow package resolution and asset import to finish.
3. Open `Assets/SkeletonGuardian/Scenes/Preview.unity` and press Play. Choose animations in the viewer; equipment switches with each motion.
4. Alternatively import `SkeletonGuardian.unitypackage` through Assets > Import Package > Custom Package in a compatible project.
5. Use `Assets/SkeletonGuardian/Prefabs/SkeletonGuardian.prefab` in your own scenes. Retain its native meshes, avatar, animations, materials, textures, and equipment dependencies.

Raw FBX is not required for native playback. Add your own gameplay controller and root-motion handling.

## Included documentation

- `Documentation/INTEGRATION.md`: compatibility and known limitations.
- `Documentation/ANIMATIONS.csv`: readable animation index.
- `Documentation/animation-catalog.json`: exact motion IDs, duration, loadout, and movement information.
- `Documentation/USAGE-NOTES.md` and `LICENSE.txt`: usage terms.

Support and releases: https://g4g-assets.itch.io
Website: https://www.g4gassets.com


Privacy update: editor source-file histories have been removed. When reimporting edited source assets, select the source files on your own computer.
