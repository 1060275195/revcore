﻿using Data.Structures.Creature;
using Data.Structures.Player;
using Global.Logic;

namespace WorldServer.SkillEngine.Effects
{
    class EfPreciseness : EfDefault
    {
        public override void Init()
        {
            CreatureLogic.UpdateCreatureStats(Creature);
        }

        public override void SetImpact(CreatureEffectsImpact impact)
        {
            if (IsUpdateStats)
                return;

            Player Player = Creature as Player;
            CreatureBaseStats baseStats = Global.Global.StatsService.GetBaseStats(Player);

            double increase = 0.0;

            if (Ability.level < 1)
                return;

            increase = Ability.FirstLevel;

            for (int i = 1; i < Ability.level; i++)
            {
                increase += Ability.Step;
            }

            double percent = 1 + (increase / 100);

            impact.ChangeOfAccuracy = (int)(baseStats.Accuracy * percent);
            IsUpdateStats = true;
        }

        public override void Release()
        {
            CreatureLogic.UpdateCreatureStats(Creature);

            base.Release();
        }
    }
}
