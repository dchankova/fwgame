namespace FruitWars
{
    public abstract class Animal
    {
        protected int speed;
        protected int power;
        
        public int Speed
        {
            get => speed;
            set
            {
                speed += 1;
            }
        }

        public int Power
        {
            get => power;
            set
            {
                power += 1;
            }
        }

        public void Eat(Fruits fruit)
        {
            switch (fruit)
            {
                case Fruits.Apple:
                    var power = Power;
                    break;
                case Fruits.Pear:
                    var speed = Speed;
                    break;
                default:
                    break;
            }
        }
    }
}
