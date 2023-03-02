// Copyright (c) Forged WoW LLC <https://github.com/ForgedWoW/ForgedCore>
// Licensed under GPL-3.0 license. See <https://github.com/ForgedWoW/ForgedCore/blob/master/LICENSE> for full information.

using Forged.Tools.Shared.Utils;
using Game.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.Spells
{
    public class SpellCurve
    {
        /// <summary>
        /// TraitDefinitionEffectPointsRecord parent record
        /// </summary>
        public TraitDefinitionRecord TraitDefinition { get; set; }
        public TraitDefinitionEffectPointsRecord TraitDefinitionEffectPoints { get; set; }
        public CurveRecord CurveRecord { get; set; }
        public List<CurvePointRecord> CurvePoints { get; set; }

        public SpellCurve()
        {
            TraitDefinition = new();
            TraitDefinition.Id = 0;
            TraitDefinition.OverrideDescription = new LocalizedString();
            TraitDefinition.OverrideName = new LocalizedString();
            TraitDefinition.OverrideSubtext = new LocalizedString();
            TraitDefinition.OverrideIcon = 0;
            TraitDefinition.OverridesSpellID = 0;
            TraitDefinition.VisibleSpellID = 0;
            TraitDefinition.SpellID = 0;

            TraitDefinitionEffectPoints = new();
            TraitDefinitionEffectPoints.Id = 0;
            TraitDefinitionEffectPoints.EffectIndex = 0;
            TraitDefinitionEffectPoints.CurveID = 0;
            TraitDefinitionEffectPoints.TraitDefinitionID = 0;
            TraitDefinitionEffectPoints.OperationType = 0;

            CurveRecord = new();
            CurveRecord.Id = 0;
            CurveRecord.Flags = 0;
            CurveRecord.Type = 0;

            CurvePoints = new();
        }

        public SpellCurve DeepCopy()
        {
            SpellCurve curve = new SpellCurve();
            curve.TraitDefinition = new();
            curve.TraitDefinition.Id = TraitDefinition.Id;
            curve.TraitDefinition.OverrideDescription = TraitDefinition.OverrideDescription.DeepCopy();
            curve.TraitDefinition.OverrideName = TraitDefinition.OverrideName.DeepCopy();
            curve.TraitDefinition.OverrideSubtext = TraitDefinition.OverrideSubtext.DeepCopy();
            curve.TraitDefinition.OverrideIcon = TraitDefinition.OverrideIcon;
            curve.TraitDefinition.OverridesSpellID = TraitDefinition.OverridesSpellID;
            curve.TraitDefinition.VisibleSpellID = TraitDefinition.VisibleSpellID;
            curve.TraitDefinition.SpellID = TraitDefinition.SpellID;

            curve.TraitDefinitionEffectPoints = new();
            curve.TraitDefinitionEffectPoints.Id = TraitDefinitionEffectPoints.Id;
            curve.TraitDefinitionEffectPoints.EffectIndex = TraitDefinitionEffectPoints.EffectIndex;
            curve.TraitDefinitionEffectPoints.CurveID = TraitDefinitionEffectPoints.CurveID;
            curve.TraitDefinitionEffectPoints.TraitDefinitionID = TraitDefinitionEffectPoints.TraitDefinitionID;
            curve.TraitDefinitionEffectPoints.OperationType = TraitDefinitionEffectPoints.OperationType;

            curve.CurveRecord = new();
            curve.CurveRecord.Id = CurveRecord.Id;
            curve.CurveRecord.Flags = CurveRecord.Flags;
            curve.CurveRecord.Type = CurveRecord.Type;

            curve.CurvePoints = new();

            foreach (var cp in CurvePoints.OrderBy(a => a.OrderIndex))
            {
                CurvePointRecord newCp = new CurvePointRecord();
                newCp.Id = cp.Id;
                newCp.OrderIndex = cp.OrderIndex;
                newCp.CurveID = cp.CurveID;
                newCp.PreSLSquishPos = new System.Numerics.Vector2(cp.PreSLSquishPos.X, cp.PreSLSquishPos.Y);
                newCp.Pos = new System.Numerics.Vector2(cp.Pos.X, cp.Pos.Y);

                curve.CurvePoints.Add(newCp);
            }

            return curve;
        }
    }
}
