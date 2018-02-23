using System.Drawing;

namespace etf.nim.km140088d {
    class Renderer {
        public Controller ctrl;

        public Renderer(Controller ctrl) {
            this.ctrl = ctrl;
        }

        public void RenderMainMenu(Graphics g) {
            g.DrawImage(ctrl.mainMenuImg, new Rectangle(0, 0, ctrl.mainMenuImg.Size.Width, ctrl.mainMenuImg.Size.Height));
        }

        public void RenderPlayerMenu(Graphics g) {
            g.DrawImage(ctrl.playerMenuImg, new Rectangle(0, 0, ctrl.playerMenuImg.Size.Width, ctrl.playerMenuImg.Size.Height));
        }

        public void RenderDifficultyMenu(Graphics g) {
            g.DrawImage(ctrl.difficultyMenuImg, new Rectangle(0, 0, ctrl.difficultyMenuImg.Size.Width, ctrl.difficultyMenuImg.Size.Height));

            string diffMessage = ctrl.playerSetting == Controller.H_VS_C ? "Computer difficulty?" : ctrl.firstComputerDiffSet ? "Second computer difficulty?" : "First Computer difficulty?";
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            g.DrawString(diffMessage, new Font("Ariel", 30, FontStyle.Bold), new SolidBrush(Color.FromArgb(255, 95, 95, 95)), new Rectangle(0, 0, 800, 100), format);
        }

        public void RenderAlgorithmMenu(Graphics g) {
            g.DrawImage(ctrl.algorithmMenuImg, new Rectangle(0, 0, ctrl.difficultyMenuImg.Size.Width, ctrl.difficultyMenuImg.Size.Height));

            string algMessage = ctrl.playerSetting == Controller.H_VS_C ? "Computer algorithm?" : ctrl.firstComputerAlgSet ? "Second computer algorithm?" : "First Computer algorithm?";
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            g.DrawString(algMessage, new Font("Ariel", 30, FontStyle.Bold), new SolidBrush(Color.FromArgb(255, 95, 95, 95)), new Rectangle(0, 0, 800, 100), format);
        }

        public void RenderPillarNum(Graphics g) {
            g.DrawImage(ctrl.pillarNumImg, new Rectangle(0, 0, ctrl.pillarNumImg.Size.Width, ctrl.pillarNumImg.Size.Height));

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            g.DrawString("Number of pillars?", new Font("Ariel", 30, FontStyle.Bold), new SolidBrush(Color.FromArgb(255, 95, 95, 95)), new Rectangle(0, 0, 800, 100), format);
        }

        public void RenderPebbleNum(Graphics g) {
            g.DrawImage(ctrl.pebbleNumImg, new Rectangle(0, 0, ctrl.pebbleNumImg.Size.Width, ctrl.pebbleNumImg.Size.Height));

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            int x = ctrl.currentPillar + 1;
            g.DrawString("Number of pebbles on the pillar " + x + '?', new Font("Ariel", 30, FontStyle.Bold), new SolidBrush(Color.FromArgb(255, 95, 95, 95)), new Rectangle(0, 0, 800, 100), format);
        }

        public void RenderPlay(Graphics g) {
            g.DrawImage(ctrl.backgroundImg, new Rectangle(0, 0, ctrl.backgroundImg.Size.Width, ctrl.backgroundImg.Size.Height));

            string playerMessage;
            switch (ctrl.playerSetting) {
                case Controller.H_VS_H:
                    playerMessage = ctrl.currentPlayer ? "First players turn." : "Second players turn.";
                    break;
                case Controller.H_VS_C:
                    playerMessage = ctrl.currentPlayer ? "Your turn." : "Computers turn.";
                    break;
                case Controller.C_VS_C:
                    playerMessage = ctrl.currentPlayer ? "First computers turn." : "Second computers turn.";
                    break;
                default:
                    playerMessage = "Error!";
                    break;
            }

            for (int i = 0; i < ctrl.pillarNum; ++i) {
                g.DrawImage(ctrl.pillarImg, new Rectangle(
                    (i + 1) * ctrl.windowWidth / (ctrl.pillarNum + 1) - ctrl.pillarImg.Size.Width / 2,
                    ctrl.windowHeight / 2 - ctrl.pillarImg.Size.Height / 2,
                    ctrl.pillarImg.Size.Width,
                    ctrl.pillarImg.Size.Height));
            }

            for (int i = 0; i < ctrl.pillarNum; ++i) {
                for (int j = 0; j < ctrl.pebbleArr[i]; ++j) {
                    g.DrawImage(ctrl.pebbleImg, new Rectangle(
                        (i + 1) * ctrl.windowWidth / (ctrl.pillarNum + 1) - ctrl.pebbleImg.Size.Width / 2,
                        ctrl.windowHeight / 2 + ctrl.pillarImg.Size.Height / 2 - ctrl.pebbleImg.Size.Height - j * ctrl.pebbleImg.Size.Height,
                        ctrl.pebbleImg.Size.Width,
                        ctrl.pebbleImg.Size.Height));
                }
            }

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            g.DrawString(playerMessage, new Font("Ariel", 30, FontStyle.Bold), new SolidBrush(Color.FromArgb(255, 95, 95, 95)), new Rectangle(10, 10, 800, 100), format);
        }

        public void RenderEnd(Graphics g) {
            g.DrawImage(ctrl.endGameImg, new Rectangle(0, 0, ctrl.difficultyMenuImg.Size.Width, ctrl.difficultyMenuImg.Size.Height));

            string victoryMessage;
            switch (ctrl.playerSetting) {
                case Controller.H_VS_H:
                    victoryMessage = !ctrl.currentPlayer ? "First player won." : "Second player won.";
                    break;
                case Controller.H_VS_C:
                    victoryMessage = !ctrl.currentPlayer ? "Your won." : "Computer won.";
                    break;
                case Controller.C_VS_C:
                    victoryMessage = !ctrl.currentPlayer ? "First computer won." : "Second computer won.";
                    break;
                default:
                    victoryMessage = "Error!";
                    break;
            }

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            g.DrawString(victoryMessage, new Font("Ariel", 30, FontStyle.Bold), new SolidBrush(Color.FromArgb(255, 95, 95, 95)), new Rectangle(0, 0, 800, 100), format);
        }
    }
}
