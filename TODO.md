# SkateGame TODO

## 1st Phase
[x] Character Sprite (Skater)
[x] Grounded Push Kick (REWORKED)
[x] Push Kick Cooldown
[x] Friction for Feet and Sides (REWORKED)
[x] Auto Scroll Camera 
[x] Simple Ground Tiles
[x] Tile Map (Collider and player movement test)
[X] Basic Ramp Sprite + 2D Polygon/Edge Collider
[x] Death box + restart mechanic
[x] Jump Mechanics (One-Click hold to charge, button release jump)

## 2nd Phase
[x] Mouse Cursor Visible / Cursor Lock
[x] Mouse rotation controls character spin during jump charge
[x] Update Sprite art for event states
- Rebuild death state: Ragdoll on headbonk, reset on map fall. 
- Build physics ragoll gameobjects for each skater part
- Sprites for ragdoll parts
- Magnetic landing helper (Click and hold to land/grind, single click for tricks)
- Skatable Level Loop
- Basic UI
- Trick scoring system (Trick name popups, screenshake/freeze frame, ding sound, escalating combo sounds)
- Skater velocity tied to score
- Core loop established
- Simple Parallax Background
- Finite State Machine | Refactor code using Enum
- FIXME: Bug in crouch and crouch left/right flicker

## 3rd Phase
- Obstacle prefabs (box, rails)
- Grinds (Needs FSM)
- Manual nose/tail grind/slide (Needs FSM)
- Finalize / Tune Scoring after FSM
- Simple Background scenery
- Simple foreground scenery
- Procedural single Level / difficulty scales with time 



