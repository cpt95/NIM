using System.Collections.Generic;

namespace etf.nim.km140088d {
    class GameTree {

        public Controller ctrl;
        public Node root;

        public class Node {
            public List<int> pebbleArr;
            public List<Node> childrenArr = new List<Node>();
            public int lastPebbleNum;
            public int depth;
            public int val = -1;
            public List<int> chosenState;

            public Node (List<int> pebbleArr, int lastPebbleNum, int depth) {
                this.pebbleArr = pebbleArr;
                //this.pebbleArrInverse = pebbleArrInverse;
                this.lastPebbleNum = lastPebbleNum;
                this.depth = depth;
            }
        }

        public GameTree(Controller ctrl) {
            this.ctrl = ctrl;
        }

        private void InitializeMinMaxTree(Node currNode, bool max) {
            if (currNode.childrenArr.Count == 0) { // these are leaves
                currNode.val = currNode.pebbleArr[0];
                for (int i = 1; i < currNode.pebbleArr.Count; currNode.val ^= currNode.pebbleArr[i++]);
                //currNode.chosenState = currNode.chosenState;
            }
            else { // these are not leaves
                for (int i = 0; i < currNode.childrenArr.Count; ++i)
                    InitializeMinMaxTree(currNode.childrenArr[i], !max);

                if (max) { // max node
                    currNode.val = int.MinValue;
                    for (int i = 0; i < currNode.childrenArr.Count; ++i)
                        if (currNode.childrenArr[i].val >= currNode.val) {
                            currNode.val = currNode.childrenArr[i].val;
                            currNode.chosenState = currNode.childrenArr[i].pebbleArr;
                        }
                }
                else { // min node
                    currNode.val = int.MaxValue;
                    for (int i = 0; i < currNode.childrenArr.Count; ++i)
                        if (currNode.childrenArr[i].val <= currNode.val) {
                            currNode.val = currNode.childrenArr[i].val;
                            currNode.chosenState = currNode.childrenArr[i].pebbleArr;
                        }
                }
            }
        }

        private void InitializeAlphaBetaTree(Node currNode, bool max, int alpha, int beta) {
            if (currNode.childrenArr.Count == 0) { // these are leaves
                currNode.val = currNode.pebbleArr[0];
                for (int i = 1; i < currNode.pebbleArr.Count; currNode.val ^= currNode.pebbleArr[i++]);
                //currNode.pebbleArr = currNode.chosenState;
            }
            else { // these are not leaves
                for (int i = 0; i < currNode.childrenArr.Count; ++i)
                    InitializeAlphaBetaTree(currNode.childrenArr[i], !max, alpha, beta);

                if (max) { // max node
                    currNode.val = int.MinValue;
                    for (int i = 0; i < currNode.childrenArr.Count; ++i)
                        if (currNode.childrenArr[i].val >= currNode.val) {
                            currNode.val = currNode.childrenArr[i].val;
                            currNode.chosenState = currNode.childrenArr[i].pebbleArr;
                            if (alpha < currNode.val)
                                alpha = currNode.val;
                            if (currNode.val > beta)
                                break;
                        }

                }
                else { // min node
                    currNode.val = int.MaxValue;
                    for (int i = 0; i < currNode.childrenArr.Count; ++i)
                        if (currNode.childrenArr[i].val <= currNode.val) {
                            currNode.val = currNode.childrenArr[i].val;
                            currNode.chosenState = currNode.childrenArr[i].pebbleArr;
                            if (beta > currNode.val)
                                beta = currNode.val;
                            if (currNode.val < alpha)
                                break;
                        }
                }
            }
        }

        private void InitializeAlphaBetaTreePlus(Node currNode, bool max, int alpha, int beta) {
            if (currNode.childrenArr.Count == 0) { // these are leaves
                bool endGame = true;
                for (int i = 0; i < ctrl.pillarNum; ++i) {
                    for (int j = 1; j <= currNode.pebbleArr[i]; ++j) {
                        if (j <= currNode.lastPebbleNum * 2) {
                            bool skip = false;
                            if (currNode.pebbleArr[i] - j == 0)
                                skip = true;

                            bool same = false;
                            if (!skip)
                                for (int k = 0; k < ctrl.pillarNum; ++k)
                                    if (currNode.pebbleArr[k] == currNode.pebbleArr[i] - j)
                                        same = true;

                            if (!same) {
                                endGame = false;
                                goto qwe;
                            }
                        }
                    }
                }

                qwe:
                if (endGame) {
                    currNode.val = int.MaxValue;
                } else {
                    currNode.val = currNode.pebbleArr[0];
                    for (int i = 1; i < currNode.pebbleArr.Count; currNode.val ^= currNode.pebbleArr[i++]) ;
                }
            }
            else { // these are not leaves
                for (int i = 0; i < currNode.childrenArr.Count; ++i)
                    InitializeAlphaBetaTree(currNode.childrenArr[i], !max, alpha, beta);

                if (max) { // max node
                    currNode.val = int.MinValue;
                    for (int i = 0; i < currNode.childrenArr.Count; ++i)
                        if (currNode.childrenArr[i].val >= currNode.val) {
                            currNode.val = currNode.childrenArr[i].val;
                            currNode.chosenState = currNode.childrenArr[i].pebbleArr;
                            if (alpha < currNode.val)
                                alpha = currNode.val;
                            if (currNode.val > beta)
                                break;
                        }

                }
                else { // min node
                    currNode.val = int.MaxValue;
                    for (int i = 0; i < currNode.childrenArr.Count; ++i)
                        if (currNode.childrenArr[i].val <= currNode.val) {
                            currNode.val = currNode.childrenArr[i].val;
                            currNode.chosenState = currNode.childrenArr[i].pebbleArr;
                            if (beta > currNode.val)
                                beta = currNode.val;
                            if (currNode.val < alpha)
                                break;
                        }
                }
            }
        }

        public Node CreateTree(int difficulty, int algorithm) {
            List<int> currentState = ctrl.pebbleArr;
            int lastPebbleNum = ctrl.lastPebbleNum;
            int difficultyDepth = difficulty + 1;

            root = new Node(currentState, lastPebbleNum, 0);
            Queue<Node> nodeQue = new Queue<Node>();
            nodeQue.Enqueue(root);
            while (nodeQue.Count != 0) { // wont work
                Node currNode = nodeQue.Dequeue();

                bool allZeroFlag = true;
                for (int j = 0; j < currNode.pebbleArr.Count; ++j)
                    if (currNode.pebbleArr[j] != 0)
                        allZeroFlag = false;

                if (allZeroFlag)
                    continue;

                for (int i = 0; i < currNode.pebbleArr.Count; ++i) {
                    for (int j = 1; j <= currNode.pebbleArr[i]; ++j) {
                        if (j <= currNode.lastPebbleNum * 2) {
                            bool skip = false;
                            if (currNode.pebbleArr[i] - j == 0)
                                skip = true;

                            bool same = false;
                            if (!skip)
                                for (int k = 0; k < currNode.pebbleArr.Count; ++k)
                                    if (currNode.pebbleArr[k] == currNode.pebbleArr[i] - j)
                                        same = true;

                            if (!same) {
                                List<int> newPebbleArr = new List<int>();
                                for (int k = 0; k < currNode.pebbleArr.Count; newPebbleArr.Add(currNode.pebbleArr[k++])) ; // puca
                                newPebbleArr[i] -= j;
                                Node newChild = new Node(newPebbleArr, j, currNode.depth + 1);
                                currNode.childrenArr.Add(newChild);
                                if (newChild.depth < difficultyDepth)
                                    nodeQue.Enqueue(newChild);
                            }
                        }
                    }
                }
            }

            /*Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Count != 0) {
                Node curr = q.Dequeue();
                Console.Write("(");
                foreach (int i in curr.pebbleArr) {
                    Console.Write(i + ", ");
                }
                Console.Write(")");
                foreach(Node i in curr.childrenArr) {
                    q.Enqueue(i);
                }
            }*/

            if (algorithm == Controller.MIN_MAX) {
                InitializeMinMaxTree(root, true);
            } else if (algorithm == Controller.ALPHA_BETA) {
                InitializeAlphaBetaTree(root, true, int.MinValue, int.MaxValue);
            } else if (algorithm == Controller.ALPHA_BETA_PLUS) {
                InitializeAlphaBetaTreePlus(root, true, int.MinValue, int.MaxValue);
            }

            return root;
        }
    }
}
