using Game.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forged.Tools.Shared.Spells
{
    public class Curve
    {
        /// <summary>
        /// TraitDefinitionEffectPointsRecord parent record
        /// </summary>
        public TraitDefinitionRecord TraitDefinition { get; set; }
        public TraitDefinitionEffectPointsRecord TraitDefinitionEffectPoints { get; set; }
        public CurveRecord CurveRecord { get; set; }
        public List<CurvePointRecord> CurvePoints { get; set; }
    }
}
