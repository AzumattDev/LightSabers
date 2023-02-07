# Description

Lightsabers requested by OdinPlus Community `client and server (if you want to enforce config values)`

`Version checks with itself. If installed on the server, it will kick clients who do not have it installed.`

`This mod uses ServerSync, if installed on the server, it will sync all configs to client`

`This mod uses a file watcher. If the configuration file is not changed with BepInEx Configuration manager, but changed in the file directly on the server, upon file save, it will sync the changes to all clients.`



<details><summary>Sabers</summary>

`Prefab name(s): LightSaber_{ColorName}`

`Crafting Table: Forge (Level 3)`

#### Requirements (Configurable!):

`SaberCrystal_{ColorName} (20)`

`Silver (40)`

`Iron (20)`

### Colors

```
Red, Green, Blue, Pink, Purple, Dark, Orange
```

</details>

<details><summary>Saber Crystals</summary>

`Prefab name(s): SaberCrystal_{ColorName}`

`Crafting Table: Forge (Level 3)`

#### Requirements (Configurable!):

`Crystal (50)`
</details>

# Manual Install

> Download the latest copy of Bepinex

> Place the LightSaber DLL inside of the "Bepinex\plugins\" folder. (You can embed it inside a folder if you want
> organization)

For Questions or Comments, find me in the Odin Plus Team Discord or in mine:

[![https://i.imgur.com/XXP6HCU.png](https://i.imgur.com/XXP6HCU.png)](https://discord.gg/Pb6bVMnFb2)
<a href="https://discord.gg/pdHgy6Bsng"><img src="https://i.imgur.com/Xlcbmm9.png" href="https://discord.gg/pdHgy6Bsng" width="175" height="175"></a>

# Changelog

| `Version` | `Update Notes`                                                                                                                                                                                                                                                                                                                                                                                                                       |
|-----------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 1.2.4     | - Remove Key Manager. Not sure why it was in here to start with. Maybe I used this for testing it? Hmmm..oh well.                                                                                                                                                                                                                                                                                                                    |
| 1.2.3     | - Mistlands update                                                                                                                                                                                                                                                                                                                                                                                                                   |
| 1.2.2     | - Fix for the newest version of Valheim                                                                                                                                                                                                                                                                                                                                                                                              |
| 1.2.1     | - Add back the ability to change the base damage. Blaxxun's version of the ItemManager didn't have it. Sorry about that.                                                                                                                                                                                                                                                                                                             |
| 1.2.0     | - Update ServerSync internally. Push this update a bit early by request</br> - The few of you that know I'm working on other things for this mod...those are still in the works. Hopefully I can get them out soon.                                                                                                                                                                                                                  |
| 1.1.0     | - Upload under my own name to make it easier to find for users that use my mods. OdinPlus version is deprecated. There isn't a difference other than it's now under my name and the config file is `Azumatt.Lightsabers.cfg` instead<br/>- Update to my edited version of ItemManager (to allow for damage value configuration). This update was requested by Seneo#1307 in Discord.<br/>- Update some internal code and references. |                                                                                                                                                                                                                                                                                                                                                                                                                                    |
| 1.0.4     | - Remove set up for config values. Causes issues, will test more                                                                                                                                                                                                                                                                                                                                                                     |
| 1.0.3     | - Fix README.md                                                                                                                                                                                                                                                                                                                                                                                                                      |
| 1.0.2     | - Update README.md file<br/>- Begin setup for config values                                                                                                                                                                                                                                                                                                                                                                          |
| 1.0.1     | - Fix issue with some mod compatibility                                                                                                                                                                                                                                                                                                                                                                                              |
| 1.0.0     | - Initial Release                                                                                                                                                                                                                                                                                                                                                                                                                    |