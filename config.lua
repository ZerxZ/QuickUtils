---@class Mod
local Mod = {
  Config = {}
}
---初始化
---@return table
function Mod.Init()
  return Mod.Config;
end

---@alias Config {Title:string,Version:number,Author:string,Description:string,FileId:number|nil,Cover:string|nil,HasArchive?:boolean|nil}

--- Settings设置
---@param config Config
---@return Mod
function Mod.Setting(config)
  for key, value in pairs(config) do
    Mod["Config"][key] = value
  end
  return Mod
end

---@alias Plugins {FrontendPlugins?:table<string>|nil,BackendPlugins?:table<string>|nil,BackendPatches?:table<string>|nil,FrontendPatches?:table<string>|nil,EventPackages?:table<string>|nil,}
--- Plugins设置
---@param plugins Plugins
---@return Mod
function Mod.SetPlugins(plugins)
  for key, value in pairs(plugins) do
    Mod["Config"][key] = value
  end
  return Mod
end

---@alias SettingToggle {Key:string,DisplayName:string,SettingType:"Toggle",Description:string,DefaultValue:boolean}
---@alias SettingToggleGroup {Key:string,DisplayName:string,SettingType:"ToggleGroup",Description:string,DefaultValue:integer,Toggles:table<string>}
---@alias SettingInputField {Key:string,DisplayName:string,SettingType:"InputField",Description:string,DefaultValue:string}
---@alias SettingSlider {Key:string,DisplayName:string,SettingType:"Slider",Description:string,DefaultValue:integer,MinValue:number,MaxValue:number,StepSize:number}
---@alias SettingDropdown {Key:string,DisplayName:string,SettingType:"Dropdown",Description:string,DefaultValue:string,Options:table<string>}
--- SetDefaultSettings设置
---@param setting SettingToggle|SettingToggleGroup|SettingInputField|SettingSlider|SettingDropdown
---@return Mod
function Mod.SetDefaultSettings(setting)
  if Mod["Config"]["DefaultSettings"] == nil then
    Mod["Config"]["DefaultSettings"] = {}
  end
  table.insert(Mod["Config"]["DefaultSettings"], setting)
  return Mod
end

---SettingToggle
---@param setting SettingToggle
---@return Mod
function Mod.SetSettingToggle(setting)
  return Mod.SetDefaultSettings(setting)
end

---SettingToggleGroup
---@param setting SettingToggleGroup
---@return Mod
function Mod.SetSettingToggleGroup(setting)
  return Mod.SetDefaultSettings(setting)
end

---SettingInputField
---@param setting SettingInputField
---@return Mod
function Mod.SetSettingInputField(setting)
  return Mod.SetDefaultSettings(setting)
end

---SettingSlider
---@param setting SettingSlider
---@return Mod
function Mod.SetSettingSlider(setting)
  return Mod.SetDefaultSettings(setting)
end

---SettingDropdown
---@param setting SettingDropdown
---@return Mod
function Mod.SettingDropdown(setting)
  return Mod.SetDefaultSettings(setting)
end

return Mod
    .Setting({ Title = "QuickUtils", Version = 12, Author = "Zerxz", Description = "" })
    .SetPlugins({
      FrontendPlugins = { "QuickUtils.dll" },
      BackendPlugins = { "QuickUtilsBackend.dll" },
    })
    .SetSettingInputField({
      Key = "Key_QuickWarehourseKey",
      DisplayName = "远程仓库快捷键",
      SettingType = "InputField",
      Description = "在游戏中，按下该快捷键，可以远程仓库",
      DefaultValue = "Q"
    })
    .SetSettingInputField({
      Key = "Key_QuickBagKey",
      DisplayName = "查看玩家快捷键",
      SettingType = "InputField",
      Description = "在游戏中，按下该快捷键，可以查看角色",
      DefaultValue = "E"
    })
    .SetSettingInputField({
      Key = "Key_QuickAutoCombatKey",

      DisplayName = "自动战斗快捷键",
      SettingType = "InputField",
      Description = "在游戏战斗中，按下该快捷键，可以切换自动战斗状态",
      DefaultValue = "Z"
    })
    .SetSettingToggle({
      Key = "Key_DefalutAutoCombatAll",
      DisplayName = "全局的自动战斗",
      SettingType = "Toggle",
      Description = "是否开启开局自动战斗(PS:关掉这个战斗速度会一样被关掉)",
      DefaultValue = true
    })
    .SetSettingToggle({
      Key = "Key_DefalutAutoCombatBoss",
      DisplayName = "Boss局的自动战斗",
      SettingType = "Toggle",
      Description = "是否开启Boss战的自动战斗",
      DefaultValue = true
    })
    .SetSettingToggle({
      Key = "Key_DefalutAutoCombatAnimal",
      DisplayName = "动物局的自动战斗",
      SettingType = "Toggle",
      Description = "是否开启动物战的自动战斗",
      DefaultValue = true
    })
    .SetSettingToggle({
      Key = "Key_DefalutAutoCombat",
      DisplayName = "其他局的自动战斗",
      SettingType = "Toggle",
      Description = "是否开启其他战的自动战斗",
      DefaultValue = true
    })
    .SettingDropdown({
      Key = "Key_DefalutSpeed",
      DisplayName = "其他局的战斗速度",
      SettingType = "Dropdown",
      Options = { "二倍", "默认", "半倍" },
      Description = "",
      DefaultValue = "二倍"
    })
    .SettingDropdown({
      Key = "Key_DefalutSpeedBoss",
      DisplayName = "Boss局的战斗速度",
      SettingType = "Dropdown",
      Options = { "二倍", "默认", "半倍" },
      Description = "",
      DefaultValue = "二倍"

    })
    .SettingDropdown({
      Key = "Key_DefalutSpeedAnimal",
      DisplayName = "动物局的战斗速度",
      SettingType = "Dropdown",
      Options = { "二倍", "默认", "半倍" },
      Description = "",
      DefaultValue = "二倍"
    })
    .SetSettingToggle({
      Key = "Key_SkillOneCost",
      DisplayName = "全技能一格消耗",
      SettingType = "Toggle",
      Description = "",
      DefaultValue = true
    }).Init()
