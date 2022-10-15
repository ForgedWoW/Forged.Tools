using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Constants;
using Game.Entities;

namespace Game.Scripting
{
    public class TopicRouter : Singleton<TopicRouter>
    {
        Dictionary<ForgeTopic, List<IForgeTopicHandler>> _topicHandlers = new Dictionary<ForgeTopic, List<IForgeTopicHandler>>();

        public void AddHandler(IForgeTopicHandler handler)
        {
            if (!_topicHandlers.TryGetValue(handler.Topic, out var forgeTopicHandlers))
            {
                forgeTopicHandlers = new List<IForgeTopicHandler>();
                _topicHandlers.Add(handler.Topic, forgeTopicHandlers);
            }

            forgeTopicHandlers.Add(handler);
        }

        public void Route(Player player, string message)
        {
            int splitIndex = message.IndexOf(':');

            if (splitIndex == -1) 
                return;

            var msgTypeStr = message.Substring(0, splitIndex);

            if (!int.TryParse(msgTypeStr, out var msgType))
                return;

            splitIndex++;

            ForgeAddonMessage msg = new ForgeAddonMessage();
            msg.Player = player;
            msg.Message = message.Substring(splitIndex);
            msg.Topic = (ForgeTopic)msgType;
            Route(msg);
        }

        public void Route(ForgeAddonMessage msg)
        {
            if (_topicHandlers.TryGetValue(msg.Topic, out var forgeTopicHandlers))
                foreach (var handler in forgeTopicHandlers)
                    handler.HandleMessage(msg);
        }
    }
}
