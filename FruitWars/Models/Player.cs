namespace FruitWars.Models
{
    public class Player
    {
        public Player(string character)
        {
            Charecater = character;
        }

        public string Charecater { get; set; }

        public Animal PlayerType { get; set; }

        public Point Position { get; set; }

        public int RemainingMoves { get; set; }

        public void RefreshRemainingMoves()
        {
            RemainingMoves = PlayerType.Speed;
        }

        public int Battle(Player other)
        {
            return PlayerType.Power.CompareTo(other.PlayerType.Power);
        }
    }
}
