using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace etf.nim.km140088d {
    public class Controller {
        // stage
        public int windowWidth = 800;
        public int windowHeight = 600;

        public int buttonWidth = 176;
        public int buttonHeight = 38;
        public int smallButtonSide = 40;

        // bitmaps
        public Bitmap mainMenuImg =         new Bitmap(Directory.GetCurrentDirectory() + @"\NIM_main_menu.bmp");
        public Bitmap playerMenuImg =       new Bitmap(Directory.GetCurrentDirectory() + @"\NIM_player_menu.bmp");
        public Bitmap difficultyMenuImg =   new Bitmap(Directory.GetCurrentDirectory() + @"\NIM_difficulty_menu.bmp");
        public Bitmap algorithmMenuImg =    new Bitmap(Directory.GetCurrentDirectory() + @"\NIM_algorithm_menu.bmp");
        public Bitmap pillarNumImg =        new Bitmap(Directory.GetCurrentDirectory() + @"\NIM_pillar_num.bmp");
        public Bitmap pebbleNumImg =        new Bitmap(Directory.GetCurrentDirectory() + @"\NIM_pebble_num.bmp");
        public Bitmap backgroundImg =       new Bitmap(Directory.GetCurrentDirectory() + @"\NIM_background.bmp");
        public Bitmap pillarImg =           new Bitmap(Directory.GetCurrentDirectory() + @"\NIM_pillar.bmp");
        public Bitmap pebbleImg =           new Bitmap(Directory.GetCurrentDirectory() + @"\NIM_pebble.bmp");
        public Bitmap endGameImg =          new Bitmap(Directory.GetCurrentDirectory() + @"\NIM_end_game.bmp");

        // pillars
        public const int PILLAR_MAX = 10;
        public int pillarNum = 5;
        public int currentPillar = 0; // reset every time

        // player
        public const bool FIRST_PLAYER_TURN = true;
        public const bool SECOND_PLAYER_TURN = false;
        public bool currentPlayer = true;

        // pebbles
        public const int PEBBLE_MAX = 10;
        public List<int> pebbleArr = new List<int>();
        public int lastPebbleNum = PEBBLE_MAX;

        // difficulty settings
        public const int EASY_DIFFICULTY = 0;
        public const int NORMAL_DIFFICULTY = 1;
        public const int HARD_DIFFICULTY = 2;
        public int difficultyFirstComputer = NORMAL_DIFFICULTY;
        public int difficultySecondComputer = NORMAL_DIFFICULTY;
        public bool firstComputerDiffSet = false;

        // algorithm settings
        public const int MIN_MAX = 0;
        public const int ALPHA_BETA = 1;
        public const int ALPHA_BETA_PLUS = 2;
        public int algorithmFirstComputer = MIN_MAX;
        public int algorithmSecondComputer = MIN_MAX;
        public bool firstComputerAlgSet = false;

        // player settings
        public const int H_VS_H = 0;
        public const int H_VS_C = 1;
        public const int C_VS_C = 2;
        public int playerSetting = H_VS_C;

        public Controller() {
            pebbleImg.MakeTransparent();
        }
    }
}
