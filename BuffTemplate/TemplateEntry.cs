using BuffTemplate.Buffs;
using RandomBuff.Core.Buff;
using RandomBuff.Core.Entry;
using System.Security.Permissions;
using RandomBuff.Core.Game.Settings.Missions;
using BuffTemplate.Missions;

#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618

namespace BuffTemplate
{
    public class TemplateEntry : IBuffEntry
    {
        public void OnEnable()
        {
            BuffRegister.RegisterBuff<SimpleBuff, SimpleBuffData, SimpleBuffHook>(BuffEnums.SimpleBuff);
            BuffRegister.RegisterBuff<TriggerableBuff, TriggerableBuffData>(BuffEnums.TriggerableBuff);
            BuffRegister.RegisterBuff<CountDownBuff, CountDownBuffData, CountDownBuffHook>(BuffEnums.CountDownBuff);
            BuffRegister.RegisterBuff<StackableBuff, StackableBuffData, StackableBuffHook>(BuffEnums.StackableBuff);

        }

    }


    //务必保持ID的独特
    //各种功能可以组合使用
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