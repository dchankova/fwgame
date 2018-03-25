namespace FruitWars
{
    public static class Constants
    {
        public const char EMPTY_CELL = '\0';
        public const char DEFAULT_CHARACTER = '-';
        public const char APPLE = 'A';
        public const char PEAR = 'P';
        public const int PLAYER_COUNT = 2;
        public const string FIRST_PLAYER = "1";
        public const string SECOND_PLAYER = "2";

        public const int APPLES_COUNT = 4;
        public const int PEARS_COUNT = 3;
        public const int FRUITS_DISTANCE = 2;
        public const int PLAYER_DISTANCE = 3;
        public const int MATRIX_SIZE = 8;

        public const string CHOOSE_WARRIOR_MSG = "Insert 1 for turtle / 2 for monkey / 3 for pigeon.";
        public const string START_NEW_GAME_MSG = "Do you want to start the rematch? (y/n)";
        public const string VALIDATION_KEY_INPUT_MSG = "Select one key from left, right, up, down.";
        public const string YES_NO_REGEX = "^[yn]$";
        public const string DRAW_GAME_MSG = "Draw game.";
        public const string NO_ANSWER = "n";
    }
}
