# Compatibility and integration notes

Skeleton Guardian contains 110 distinct animations: 50 shared selections and 60 guardian-oriented selections. Different file formats do not add to this count. Native tracks preserve authored Hips travel; some clips are in place and others move. Root-motion extraction, looping, blending, transitions, and character control require integration in your game.

## Character and equipment

- Character: approximately 1.85 m tall in rest pose, 8,324 triangles, and 24 source joints.
- Separate sword: 792 triangles. Separate shield with rear straps: 1,902 triangles.
- Sword attaches to RightHand; shield attaches to LeftForeArm. Per-motion defaults select sword/shield, sword-only, or empty hands.
- No separate finger joints; fingers retain an open pose.
- Breastplate follows the torso rigidly. Belt, buckle, and upper hip plates follow the pelvis rigidly. Surrounding collar, shoulder fittings, helmet, and cloth retain deformation. This is not a fully articulated rigid armor system.
- Cloth is skinned to hips/legs; no physical cloth simulation is included.

## Motion limitations

Recovery clips Stand Up 2, 4, 5, 8, and 9 can place hands or feet below a reference floor. Adapt ground contact/IK and transition timing for your game. Several motions include stylized extremes. Preview repetition does not imply a seamless loop.

Equipment clearance was sampled at 30 fps across 24 equipped motions against hand/forearm surfaces. This does not certify continuous or whole-body collision clearance. Other prop/body intersections and cloth deformation may occur.

No gameplay AI, ragdoll setup, movement controller, combat system, or engine-to-engine retargeter is included.

## Compatibility

Tested on macOS with Godot 4.7.2, Unity 6000.6.2f1, Unreal Engine 5.8.2, and Blender 5.2.1. Other versions/platforms need your own import/build validation. Unity uses native assets and the Built-in Render Pipeline. Unreal uses a custom skeleton; Epic mannequin/MetaHuman retargeting compatibility is not claimed.

Textures provide base color with ordinary lit rough materials. Engine packages contain native assets; original Blender/GLB/FBX files are in the optional Source archive.

Dungeon scenery, trailer choreography, branding overlays, music, and visual effects are presentation material and are not included as game assets.

Created using an AI-assisted workflow, followed by local assembly, equipment work, inspection, and engine testing. See INSTALL.md, ANIMATIONS.csv, and animation-catalog.json for installation and motion details.


Privacy update: editor source-file histories have been removed. When reimporting edited source assets, select the source files on your own computer.
