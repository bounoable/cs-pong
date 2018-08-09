namespace Pong.GameObjects
{
    class Paddle: GameObject
    {
        public enum MoveDirection
        {
            Top,
            Bottom
        }

        public Paddle(Vector2 position, Vector2 size): base(position, size)
        {}

        public void Move(MoveDirection direction, int distance)
        {
            switch (direction) {
                case MoveDirection.Top:
                    Position = Position.Top(distance);
                    break;
                case MoveDirection.Bottom:
                    Position = Position.Bottom(distance);
                    break;
            }
        }

        public void MoveTop(int distance)
            => Move(MoveDirection.Top, distance);
        
        public void MoveBottom(int distance)
            => Move(MoveDirection.Bottom, distance);
    }
}