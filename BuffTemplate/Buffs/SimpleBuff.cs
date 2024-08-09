using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoMod.RuntimeDetour;
using RandomBuff.Core.Buff;
using RandomBuffUtils;

namespace BuffTemplate.Buffs
{
    internal class SimpleBuffData : BuffData
    {
        public override BuffID ID => BuffEnums.SimpleBuff;
    }

    internal class SimpleBuff : Buff<SimpleBuff,SimpleBuffData>
    {
        public override BuffID ID => BuffEnums.SimpleBuff;

        public SimpleBuff()
        {
            if (BuffCustom.TryGetGame(out var game))
            {
                foreach (var player in game.Players)
                    player.HypothermiaImmune = true;
            }
        }
    }

    internal class SimpleBuffHook
    {

        // 在HookOn内应用的Hook只会在Buff存在时生效
        // the hooks applied will only take effect when Buff is enabled in HookOn function
        public static void HookOn()
        {
            On.AbstractCreature.ctor += AbstractCreature_ctor;
        }

        private static void AbstractCreature_ctor(On.AbstractCreature.orig_ctor orig, AbstractCreature self, World world, CreatureTemplate creatureTemplate, Creature realizedCreature, WorldCoordinate pos, EntityID ID)
        {
            orig(self,world, creatureTemplate, realizedCreature, pos, ID);
            if (creatureTemplate.type == CreatureTemplate.Type.Slugcat)
                self.HypothermiaImmune = true;
        }
    }
        

}
