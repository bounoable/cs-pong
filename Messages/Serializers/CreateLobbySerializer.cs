using GameNet.Messages;

namespace Pong.Messages.Serializers
{
    class CreateLobbySerializer: ObjectSerializer<CreateLobby>
    {
        override public byte[] GetBytes(CreateLobby message)
            => Build().String(message.Name).String(message.Secret).String(message.Password).Data;

        override public CreateLobby GetObject(byte[] data)
            => new CreateLobby(PullString(ref data), PullString(ref data), PullString(ref data));
    }
}