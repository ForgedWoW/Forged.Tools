using Framework.Constants;

namespace Game.Scripting
{
    public interface IForgeTopicHandler
    {
        ForgeTopic Topic { get; }

        void HandleMessage(ForgeAddonMessage msg);
    }
}