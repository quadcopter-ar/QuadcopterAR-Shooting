# ECS 189L Final Project: VR Quadcopter Game Dev

### Authors:
- Aziz Nassar, afnassar@ucdavis.edu
- Shikun Huang, kunhuang@ucdavis.edu

## Our project document is in the main folder of the repo under the name `ProjectDocument.md`. Please check that for an overview of our game.



# This is a template to build your own Quadcopter Game.
### Edits that you need to make to fix on your end
- edit the file at `./.git/config`
- add the code below to this file.
```
[merge]
        tool = unityyamlmerge

[mergetool "unityyamlmerge"]
        trustExitCode = false
        cmd = <PATH TO YOUR UNITY EDITOR INSTALL>\\2020.1.15f1\\Editor\\Data\\Tools\\UnityYAMLMerge.exe merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
```
