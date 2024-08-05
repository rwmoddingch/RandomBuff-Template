using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomBuff.Core.Buff;
using RandomBuffUtils;
using RWCustom;
using UnityEngine;

namespace BuffTemplate.Buffs
{
    internal class StackableBuffData : BuffData
    {
        public override BuffID ID => BuffEnums.StackableBuff;
    }


    internal class StackableBuff : Buff<StackableBuff,StackableBuffData>
    {
        public override BuffID ID => BuffEnums.StackableBuff;

        public StackableBuff()
        {
            if (BuffCustom.TryGetGame(out var game))
            {
                foreach (var player in game.Players.Select(i => i.realizedCreature as Player))
                {
                    if(player == null || player.room == null)
                        continue;

                    player.room.AddObject(new SimpleShield(player.room, player, Data.StackLayer));
                }
            }
        }
    }

    internal class StackableBuffHook
    {
        public static void HookOn()
        {
            On.Player.NewRoom += Player_NewRoom;
        }

        private static void Player_NewRoom(On.Player.orig_NewRoom orig, Player self, Room newRoom)
        {
            orig(self, newRoom);
            newRoom.AddObject(new SimpleShield(newRoom, self, StackableBuff.Instance.Data.StackLayer));
        }
    }

    internal class SimpleShield : CosmeticSprite
    {
        public SimpleShield(Room room,Player player, int stackLayer)
        {
            this.room = room;
            this.player = player;
            this.stackLayer = stackLayer + 2;
        }


        public override void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
        {
            FContainer shieldContainer = new FContainer();
            for (int i = 0; i < stackLayer; i++)
            {
                FSprite sprite = new FSprite("Futile_White");
                sprite.scale = 0.5f;
                sprite.SetPosition(Custom.DegToVec(i * 360f / stackLayer) * 70);
                shieldContainer.AddChild(sprite);
            }

            sLeaser.sprites = Array.Empty<FSprite>();
            
            sLeaser.containers = new[] { shieldContainer };
            AddToContainer(sLeaser, rCam, null);
        }


        public override void AddToContainer(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, FContainer newContatiner)
        {
            if (newContatiner == null)
                newContatiner = rCam.ReturnFContainer("Foreground");
            newContatiner.AddChild(sLeaser.containers[0]);
        }

        public override void DrawSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
        {
            base.DrawSprites(sLeaser, rCam, timeStacker, camPos);
            sLeaser.containers[0].SetPosition(Vector2.Lerp(lastPos, pos, timeStacker) - camPos);
            sLeaser.containers[0].rotation = Mathf.Lerp(lastRotation, rotation, timeStacker);
        }

        public override void Update(bool eu)
        {
            base.Update(eu);
            pos = player.mainBodyChunk.pos;
            lastPos = player.mainBodyChunk.lastPos;

            lastRotation = rotation;
            rotation += 0.25f * 360 / 40;

            foreach (var crit in room.updateList.OfType<Creature>())
            {
                if(crit is Player) continue;
                for (int i = 0; i < stackLayer; i++)
                {
                    if (crit.bodyChunks.Any(chunk =>
                            Custom.DistLess(chunk.pos, pos + Custom.DegToVec((i * 360f / stackLayer) + rotation) * 70, 12)))
                    {
                        crit.Stun(50);
                        break;
                    }
                }
            }

            if(StackableBuff.Instance == null || player.room != room)
                Destroy();
        }

        private readonly Player player;
        private readonly int stackLayer;

        private float lastRotation;
        private float rotation;

    }
}
