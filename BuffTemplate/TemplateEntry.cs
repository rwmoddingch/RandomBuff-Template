using BuffTemplate.Buffs;
using RandomBuff.Core.Buff;
using RandomBuff.Core.Entry;
using System.Security.Permissions;
using RandomBuff.Core.Game.Settings.Missions;
using BuffTemplate.Missions;

#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618

// mod文件夹内有完整的Mod结构
// The mod folder contains the complete mod structure example.


// MOD STRUCTURE [Mod文件结构]
// YourMod
// |----buffassets/ (json file of buffs)
// |----buffplugins/ (buff extend DLL)
// |----plugins/ (your mod main DLL file) (optional)
// |----modinfo.json



// 请将生成的dll/pdb文件放置在buffplugins内
// 注意：在RandomBuff未启用时不会调用buffplugins下dll的内容，不影响mod的依赖关系，所以modinfo不一定需要把RandomBuff作为依赖项。
// Please place the generated DLL/PDB files in the buffplugins directory.
// Note: When RandomBuff is not enabled, the content of the DLLs in the buffplugins folder will not be called.
//       This does not affect the mod's dependencies, so it's not necessary for modinfo.json to list RandomBuff as a dependency.

namespace BuffTemplate
{
    // IBuffEntry是BuffPlugin的入口点，类似于 BaseUnityPlugin 类型。
    // 注意：IBuffEntry.OnEnable函数实际上会在RainWorld.OnModsInit中调用
    // IBuffEntry is the entry point for BuffPlugin, similar to the BaseUnityPlugin type.
    // Note: The IBuffEntry.OnEnable function is actually called within RainWorld.OnModsInit.
    public class TemplateEntry : IBuffEntry
    {
   
        public void OnEnable()
        {
            // 注册Buff
            // 注意: 注册的buff必须存在对应的json文件才能正常使用
            // Register Buff
            // Note: The registered Buff MUST have a corresponding JSON file to function properly.
            BuffRegister.RegisterBuff<SimpleBuff, SimpleBuffData, SimpleBuffHook>(BuffEnums.SimpleBuff);
            BuffRegister.RegisterBuff<TriggerableBuff, TriggerableBuffData>(BuffEnums.TriggerableBuff);
            BuffRegister.RegisterBuff<CountDownBuff, CountDownBuffData, CountDownBuffHook>(BuffEnums.CountDownBuff);
            BuffRegister.RegisterBuff<StackableBuff, StackableBuffData, StackableBuffHook>(BuffEnums.StackableBuff);

        }

    }


    internal static class BuffEnums
    {
        // 注意: 注册是必须的
        // NOTE: Register is necessary
        public static readonly BuffID SimpleBuff = new BuffID("Template.SimpleBuff", true);
        public static readonly BuffID TriggerableBuff = new BuffID("Template.TriggerableBuff", true);
        public static readonly BuffID CountDownBuff = new BuffID("Template.CountDownBuff", true);
        public static readonly BuffID StackableBuff = new BuffID("Template.StackableBuff", true);

        //WIP
        //public static readonly BuffID WitHudBuff = new BuffID("Template.WitHudBuff", true);
        //public static readonly BuffID WitTimerBuff = new BuffID("Template.WitHudBuff", true);

        public static readonly MissionID TemplateMission = new MissionID("Template.Mission", true);
    }
}