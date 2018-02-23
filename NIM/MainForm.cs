using System;
using System.Drawing;
using System.Windows.Forms;

namespace etf.nim.km140088d {
    public partial class MainForm : Form {

        Controller ctrl = new Controller();
        Renderer rnd;

        const int MAIN_MENU_STATE = 0;
        const int PLAYER_MENU_STATE = 1;
        const int DIFFICULTY_MENU_STATE = 2;
        const int ALGORITHM_MENU_STATE = 3;
        const int PILLARS_NUM_STATE = 4;
        const int PEBBLES_NUM_STATE = 5;
        const int PLAY_STATE = 6;
        const int END_GAME_STATE = 7;

        private int state = MAIN_MENU_STATE;

        public MainForm() {
            rnd = new Renderer(ctrl);
            Size = new Size(ctrl.windowWidth, ctrl.windowHeight);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            DoubleBuffered = true;
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);

            switch (state) {
                case MAIN_MENU_STATE:
                    ControlMainMenu(e.X, e.Y);
                    break;
                case PLAYER_MENU_STATE:
                    ControlPlayerMenu(e.X, e.Y);
                    break;
                case DIFFICULTY_MENU_STATE:
                    ControlDifficultyMenu(e.X, e.Y);
                    break;
                case ALGORITHM_MENU_STATE:
                    ControlAlgorithmMenu(e.X, e.Y);
                    break;
                case PILLARS_NUM_STATE:
                    ControlPillarNum(e.X, e.Y);
                    break;
                case PEBBLES_NUM_STATE:
                    ControlPebbleNum(e.X, e.Y);
                    break;
                case PLAY_STATE:
                    ControlHuman(e.X, e.Y);
                    break;
                case END_GAME_STATE:
                    ControlEnd(e.X, e.Y);
                    break;
                default:
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            switch (state) {
                case MAIN_MENU_STATE:
                    rnd.RenderMainMenu(g);
                    break;
                case PLAYER_MENU_STATE:
                    rnd.RenderPlayerMenu(g);
                    break;
                case DIFFICULTY_MENU_STATE:
                    rnd.RenderDifficultyMenu(g);
                    break;
                case ALGORITHM_MENU_STATE:
                    rnd.RenderAlgorithmMenu(g);
                    break;
                case PILLARS_NUM_STATE:
                    rnd.RenderPillarNum(g);
                    break;
                case PEBBLES_NUM_STATE:
                    rnd.RenderPebbleNum(g);
                    break;
                case PLAY_STATE:
                    rnd.RenderPlay(g);
                    break;
                case END_GAME_STATE:
                    rnd.RenderEnd(g);
                    break;
                default:
                    break;
            }
        }

        void ControlMainMenu(int eX, int eY) {
            if (eX > ctrl.windowWidth / 2 - ctrl.buttonWidth / 2 && eX < ctrl.windowWidth / 2 + ctrl.buttonWidth / 2) {
                if (eY > ctrl.windowHeight / 2 - 5 * ctrl.buttonHeight / 2 && eY < ctrl.windowHeight / 2 - 3 * ctrl.buttonHeight / 2) {
                    Console.WriteLine("start");
                    state = ctrl.playerSetting == Controller.H_VS_H ? PILLARS_NUM_STATE : DIFFICULTY_MENU_STATE;
                    Refresh();
                }
                else if (eY > ctrl.windowHeight / 2 - ctrl.buttonHeight / 2 && eY < ctrl.windowHeight / 2 + ctrl.buttonHeight / 2) {
                    Console.WriteLine("player");
                    state = PLAYER_MENU_STATE;
                    Refresh();
                }
                else if (eY < ctrl.windowHeight / 2 + 5 * ctrl.buttonHeight / 2 && eY > ctrl.windowHeight / 2 + 3 * ctrl.buttonHeight / 2) {
                    Console.WriteLine("quit");
                    Application.Exit();
                    Refresh();
                }
            }
        }

        void ControlPlayerMenu(int eX, int eY) {
            int chosenPlayerSetting = Controller.H_VS_C;
            if (eX > ctrl.windowWidth / 2 - ctrl.buttonWidth / 2 && eX < ctrl.windowWidth / 2 + ctrl.buttonWidth / 2) {
                if (eY > ctrl.windowHeight / 2 - 7 * ctrl.buttonHeight / 2 && eY < ctrl.windowHeight / 2 - 5 * ctrl.buttonHeight / 2) {
                    Console.WriteLine("hh");
                    chosenPlayerSetting = Controller.H_VS_H;
                }
                else if (eY > ctrl.windowHeight / 2 - 3 * ctrl.buttonHeight / 2 && eY < ctrl.windowHeight / 2 - ctrl.buttonHeight / 2) {
                    Console.WriteLine("hc");
                    chosenPlayerSetting = Controller.H_VS_C;
                }
                else if (eY < ctrl.windowHeight / 2 + 3 * ctrl.buttonHeight / 2 && eY > ctrl.windowHeight / 2 + ctrl.buttonHeight / 2) {
                    Console.WriteLine("cc");
                    chosenPlayerSetting = Controller.C_VS_C;
                }
                else if (eY < ctrl.windowHeight / 2 + 7 * ctrl.buttonHeight / 2 && eY > ctrl.windowHeight / 2 + 5 * ctrl.buttonHeight / 2) {
                    Console.WriteLine("back");
                } else {
                    return;
                }

                ctrl.playerSetting = chosenPlayerSetting;
                state = MAIN_MENU_STATE;
                Refresh();
            }
        }

        void ControlDifficultyMenu(int eX, int eY) {
            int chosenDifficultySetting = Controller.NORMAL_DIFFICULTY;
            if (eX > ctrl.windowWidth / 2 - ctrl.buttonWidth / 2 && eX < ctrl.windowWidth / 2 + ctrl.buttonWidth / 2) {
                if (eY > ctrl.windowHeight / 2 - 7 * ctrl.buttonHeight / 2 && eY < ctrl.windowHeight / 2 - 5 * ctrl.buttonHeight / 2) {
                    Console.WriteLine("easy");
                    chosenDifficultySetting = Controller.EASY_DIFFICULTY;
                }
                else if (eY > ctrl.windowHeight / 2 - 3 * ctrl.buttonHeight / 2 && eY < ctrl.windowHeight / 2 - ctrl.buttonHeight / 2) {
                    Console.WriteLine("normal");
                    chosenDifficultySetting = Controller.NORMAL_DIFFICULTY;
                }
                else if (eY < ctrl.windowHeight / 2 + 3 * ctrl.buttonHeight / 2 && eY > ctrl.windowHeight / 2 + ctrl.buttonHeight / 2) {
                    Console.WriteLine("hard");
                    chosenDifficultySetting = Controller.HARD_DIFFICULTY;
                }
                else if (eY < ctrl.windowHeight / 2 + 7 * ctrl.buttonHeight / 2 && eY > ctrl.windowHeight / 2 + 5 * ctrl.buttonHeight / 2) {
                    Console.WriteLine("back");
                    state = MAIN_MENU_STATE;
                    Refresh();
                    return;
                } else {
                    return;
                }

                if (!ctrl.firstComputerDiffSet) {
                    ctrl.difficultyFirstComputer = chosenDifficultySetting;
                    ctrl.firstComputerDiffSet = true;
                    if (ctrl.playerSetting == Controller.C_VS_C)
                        state = DIFFICULTY_MENU_STATE;
                    else
                        state = ALGORITHM_MENU_STATE;
                }
                else {
                    ctrl.difficultySecondComputer = chosenDifficultySetting;
                    state = ALGORITHM_MENU_STATE;
                }

                Refresh();
            }
        }

        void ControlAlgorithmMenu(int eX, int eY) {
            int chosenAlgorithm = Controller.MIN_MAX;
            if (eX > ctrl.windowWidth / 2 - ctrl.buttonWidth / 2 && eX < ctrl.windowWidth / 2 + ctrl.buttonWidth / 2) {
                if (eY > ctrl.windowHeight / 2 - 7 * ctrl.buttonHeight / 2 && eY < ctrl.windowHeight / 2 - 5 * ctrl.buttonHeight / 2) {
                    Console.WriteLine("minmax");
                    chosenAlgorithm = Controller.MIN_MAX;
                }
                else if (eY > ctrl.windowHeight / 2 - 3 * ctrl.buttonHeight / 2 && eY < ctrl.windowHeight / 2 - ctrl.buttonHeight / 2) {
                    Console.WriteLine("alphabeta");
                    chosenAlgorithm = Controller.ALPHA_BETA;
                }
                else if (eY < ctrl.windowHeight / 2 + 3 * ctrl.buttonHeight / 2 && eY > ctrl.windowHeight / 2 + ctrl.buttonHeight / 2) {
                    Console.WriteLine("alphabeta++");
                    chosenAlgorithm = Controller.ALPHA_BETA_PLUS;
                }
                else if (eY < ctrl.windowHeight / 2 + 7 * ctrl.buttonHeight / 2 && eY > ctrl.windowHeight / 2 + 5 * ctrl.buttonHeight / 2) {
                    Console.WriteLine("back");
                    state = MAIN_MENU_STATE;
                    Refresh();
                    return;
                }
                else {
                    return;
                }

                if (!ctrl.firstComputerAlgSet) {
                    ctrl.algorithmFirstComputer = chosenAlgorithm;
                    ctrl.firstComputerAlgSet = true;
                    if (ctrl.playerSetting == Controller.C_VS_C)
                        state = ALGORITHM_MENU_STATE;
                    else
                        state = PILLARS_NUM_STATE;
                }
                else {
                    ctrl.algorithmSecondComputer = chosenAlgorithm;
                    state = PILLARS_NUM_STATE;
                }

                Refresh();
            }
        }

        void ControlPillarNum(int eX, int eY) {
            if (eY > ctrl.windowHeight / 2 - ctrl.smallButtonSide / 2 && eY < ctrl.windowHeight / 2 + ctrl.smallButtonSide / 2)
                if (eX > ctrl.windowWidth / 2 - 29 * ctrl.smallButtonSide / 4 && eX < ctrl.windowWidth / 2 + 29 * ctrl.smallButtonSide / 4)
                    if (((eX - (ctrl.windowWidth / 2 - 29 * ctrl.smallButtonSide / 4)) % (3 * ctrl.smallButtonSide / 2) < ctrl.smallButtonSide)) {
                        ctrl.pillarNum = (eX - (ctrl.windowWidth / 2 - 29 * ctrl.smallButtonSide / 4)) / (3 * ctrl.smallButtonSide / 2) + 1;
                        Console.WriteLine(ctrl.pillarNum);
                        state = PEBBLES_NUM_STATE;
                        Refresh();
                    }
        }

        void ControlPebbleNum(int eX, int eY) {
            if (eY > ctrl.windowHeight / 2 - ctrl.smallButtonSide / 2 && eY < ctrl.windowHeight / 2 + ctrl.smallButtonSide / 2)
                if (eX > ctrl.windowWidth / 2 - 32 * ctrl.smallButtonSide / 4 && eX < ctrl.windowWidth / 2 + 32 * ctrl.smallButtonSide / 4)
                    if (((eX - (ctrl.windowWidth / 2 - 32 * ctrl.smallButtonSide / 4)) % (3 * ctrl.smallButtonSide / 2) < ctrl.smallButtonSide)) {
                        int currPebbleNum = (eX - (ctrl.windowWidth / 2 - 32 * ctrl.smallButtonSide / 4)) / (3 * ctrl.smallButtonSide / 2);

                        for (int i = 0; i < ctrl.currentPillar; ++i)
                            if (ctrl.pebbleArr[i] == currPebbleNum && currPebbleNum != 0)
                                return;

                        ctrl.pebbleArr.Add(currPebbleNum);
                        Console.WriteLine(ctrl.pebbleArr[ctrl.currentPillar++]);
                        if (ctrl.currentPillar >= ctrl.pillarNum) {
                            state = PLAY_STATE;
                            Refresh();
                            if (ctrl.playerSetting == Controller.C_VS_C)
                                ControlComputer();
                        }

                        Refresh();
                    }
        }

        void ControlHuman(int eX, int eY) {
            if ((ctrl.playerSetting == Controller.H_VS_C && ctrl.currentPlayer) || ctrl.playerSetting == Controller.H_VS_H) { // check for current player
                int slice = ctrl.windowWidth / (ctrl.pillarNum + 1);
                int col = (slice / 2 + eX) / slice;
                int inCol = (slice / 2 + eX) % slice;
                int newPebbleNum = (ctrl.windowHeight / 2 + ctrl.pillarImg.Size.Height / 2 - eY) / ctrl.pebbleImg.Size.Height;

                if (inCol > (slice - ctrl.pebbleImg.Size.Width) / 2 && inCol < slice - (slice - ctrl.pebbleImg.Size.Width) / 2) {
                    if (eY < ctrl.windowHeight / 2 + ctrl.pillarImg.Size.Height / 2 && col != 0 && col != ctrl.pillarNum + 1) {
                        if (ctrl.pebbleArr[col - 1] - newPebbleNum > 0) {

                            for (int i = 0; i < ctrl.pillarNum; ++i)
                                if (ctrl.pebbleArr[i] == newPebbleNum && newPebbleNum != 0) {
                                    Refresh();
                                    return;
                                }

                            int currentPebbleTake = ctrl.pebbleArr[col - 1] - newPebbleNum;
                            if (currentPebbleTake > ctrl.lastPebbleNum * 2) {
                                Refresh();
                                return;
                            }

                            ctrl.lastPebbleNum = currentPebbleTake;
                            ctrl.pebbleArr[col - 1] = newPebbleNum;
                            ctrl.currentPlayer = !ctrl.currentPlayer;

                            bool endGame = true;
                            for (int i = 0; i < ctrl.pillarNum; ++i)
                                if (ctrl.pebbleArr[i] != 0)
                                    endGame = false;

                            if (!endGame) {
                                endGame = true;
                                for (int i = 0; i < ctrl.pillarNum; ++i) {
                                    for (int j = 1; j <= ctrl.pebbleArr[i]; ++j) {
                                        if (j <= ctrl.lastPebbleNum * 2) {
                                            bool skip = false;
                                            if (ctrl.pebbleArr[i] - j == 0)
                                                skip = true;

                                            bool same = false;
                                            if (!skip)
                                                for (int k = 0; k < ctrl.pillarNum; ++k)
                                                    if (ctrl.pebbleArr[k] == ctrl.pebbleArr[i] - j)
                                                        same = true;

                                            if (!same) {
                                                endGame = false;
                                                goto asd;
                                            }
                                        }
                                    }
                                }
                            }
                            asd:
                            
                            if (endGame)
                                state = END_GAME_STATE;

                            Console.WriteLine("In Col: " + col + '.' + " Peb Num: " + ctrl.pebbleArr[col - 1]);

                            Refresh();

                            if (ctrl.playerSetting == Controller.H_VS_C && !endGame)
                                ControlComputer();
                        }
                    }
                }
            }
        }

        void ControlComputer() {
            // u zavisnosti od toga ko trenutno igra, koji igraci igraju i koja je tezina, odradi ovo
            System.Threading.Thread.Sleep(1700);

            int chosenAlg = -1;
            int chosenDiff = -1;
            if (ctrl.playerSetting == Controller.H_VS_C) {
                chosenAlg = ctrl.algorithmFirstComputer;
                chosenDiff = ctrl.difficultyFirstComputer;
            } else if (ctrl.playerSetting == Controller.C_VS_C) {
                if (ctrl.currentPlayer) { // first cumputer
                    chosenAlg = ctrl.algorithmFirstComputer;
                    chosenDiff = ctrl.difficultyFirstComputer;
                } else { // second computer
                    chosenAlg = ctrl.algorithmSecondComputer;
                    chosenDiff = ctrl.difficultySecondComputer;
                }
            }
            
            GameTree gt = new GameTree(ctrl);
            GameTree.Node nd = gt.CreateTree(chosenDiff, chosenAlg);

            ctrl.pebbleArr = nd.chosenState;
            ctrl.currentPlayer = !ctrl.currentPlayer;

            if (nd.childrenArr.Count == 0) {
                state = END_GAME_STATE;
                Refresh();
                return;
            }

            bool endGame = true;
            for (int i = 0; i < ctrl.pebbleArr.Count; ++i)
                if (ctrl.pebbleArr[i] != 0)
                    endGame = false;

            if (endGame) {
                state = END_GAME_STATE;
                Refresh();
                return;
            }


            Console.WriteLine("prosla 1,7 sekunda");
            Refresh();

            if (ctrl.playerSetting == Controller.C_VS_C)
                ControlComputer();
        }

        void ControlEnd(int eX, int eY) {
            if (eX > ctrl.windowWidth / 2 - ctrl.buttonWidth / 2 && eX < ctrl.windowWidth / 2 + ctrl.buttonWidth / 2)
                if (eY > ctrl.windowHeight / 2 - ctrl.buttonHeight / 2 && eY < ctrl.windowHeight / 2 + ctrl.buttonHeight / 2) {
                    Console.WriteLine("main menu");
                    state = MAIN_MENU_STATE;
                    ctrl = new Controller();
                    rnd.ctrl = ctrl;
                    Refresh();
                }
        }
    }
}
