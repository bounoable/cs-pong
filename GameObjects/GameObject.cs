namespace Pong.GameObjects
{
    class GameObject: IGameObject
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public GameObject(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }
    }
}