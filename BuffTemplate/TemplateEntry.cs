using BuffTemplate.Buffs;
using RandomBuff.Core.Buff;
using RandomBuff.Core.Entry;
using System.Security.Permissions;
using RandomBuff.Core.Game.Settings.Missions;
using BuffTemplate.Missions;

#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618

// mod文件夹内有完整的mod结构
// The mod folder contains the complete mod structure.

namespace BuffTemplate
{
    public class TemplateEntry : IBuffEntry
    {
        // 请将生成的dll/pdb文件放置在buffplugins内
        // Please place the generated DLL/PDB files in the buffplugins directory.
        public void OnEnable()
        {
            // 注册Buff
            // 注意: 注册的buff必须存在对应的json文件才能正常使用
            // Register Buff
            // Note: The registered Buff must have a corresponding JSON file to function properly.
            BuffRegister.RegisterBuff<SimpleBuff, SimpleBuffData, SimpleBuffHook>(BuffEnums.SimpleBuff);
            BuffRegister.RegisterBuff<TriggerableBuff, TriggerableBuffData>(BuffEnums.TriggerableBuff);
            BuffRegister.RegisterBuff<CountDownBuff, CountDownBuffData, CountDownBuffHook>(BuffEnums.CountDownBuff);
            BuffRegister.RegisterBuff<StackableBuff, StackableBuffData, StackableBuffHook>(BuffEnums.StackableBuff);

        }

    }


    internal static class BuffEnums
    {

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