namespace FruitWars
{
    public abstract class Animal 
    {
        protected Animal()
        {
            Speed = speed;
            Power = power;
        }

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

        public void Eat(Fruit fruit)
        {
            switch (fruit)
            {
                case Fruit.Apple:
                    Power += 1;
                    break;
                case Fruit.Pear:
                    Speed += 1;
                    break;
                default:
                    break;
            }
        }
    }
}
