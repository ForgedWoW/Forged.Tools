using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Constants;

namespace Game.Scripting
{
    public abstract class ForgeTopicHandler : IForgeTopicHandler
    {
        public ForgeTopic Topic { get; }

        public ForgeTopicHandler(ForgeTopic topic)
        {
            Topic = topic;
        }

        public abstract void HandleMessage(ForgeAddonMessage msg);
    }
}
