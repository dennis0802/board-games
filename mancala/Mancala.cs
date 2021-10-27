using System;

namespace Mancala{

    public class Mancala{
        
        // Print game status (board and score)
        public static string PrintStatus(int[] arr1, int[] arr2, int p1, int p2){
            string output = "Current State of Board: \n";
            for(int i = 0; i < arr1.Length; i++){
                output += arr1[i];
                if(i != 5){
                    output += ", ";
                }
            }
            output += "\n";
            for(int i = 0; i < arr2.Length; i++){
                output += arr2[i];
                if(i != 5){
                    output += ", ";
                }
            }
            output += "\n\n";
            output = output + "S C O R E\nP1: " + p1 + "\nP2: " + p2 + "\n";
            return output;
        }

        public static void SetBoard(int[] arr1, int[] arr2){
            for(int i = 0; i < arr1.Length; i++){
                arr1[i] = arr2[i] = 4;
            }
        }

        public static int SumArray(int[] arr){
            int sum = 0;
            for(int i = 0; i < arr.Length; i++){
                sum += arr[i];
            }
            return sum;
        }

        public static void MoveLeft(){

        }

        public static void MoveRight(){
            
        }

        public static void Main(string[] args){
            int[] rowP1 = new int[6];
            int[] rowP2 = new int[6];
            int p1score = 0, p2score = 0, endPos = 0;
            bool playAgain = true;
            string input = "";

            SetBoard(rowP1, rowP2);
            Console.WriteLine(PrintStatus(rowP1, rowP2, p1score, p2score));
            Console.WriteLine(SumArray(rowP1));

            while(playAgain){
                while(true){
                    // Prompt P1 to pick a pocket
                    Console.WriteLine("P1: Pick a pocket not containing 0 to move");
                    int startPocket = Convert.ToInt32(Console.ReadLine());

                    // Pick the pocket again if it contained 0 or invalid number
                    while(startPocket >= 7){
                        Console.WriteLine("P1: Pick a different pocket. That pocket contains 0 or is invalid");
                        startPocket = Convert.ToInt32(Console.ReadLine());                    
                    }
                    
                    startPocket--;
                    int moves = rowP1[startPocket];
                    rowP1[startPocket] = 0;
                    bool belongtoRow = true;

                    // Row 1 movements - start at user's selection and move left
                    for(int i = startPocket - 1; moves != 0; i--){
                        // First encounter with out of bounds - score and add to p2's row if applicable
                        if(i < 0){
                            moves--;
                            belongtoRow = false;
                            p1score++;
                            if(moves != 0){
                                // Row 2 movements - start at the beginning and move right
                                for(int j = 0; moves != 0; j++){
                                    moves--;
                                    // Second encounter with out of bounds - repeat row 1 movements if applicable
                                    if(j >= rowP2.Length){
                                        belongtoRow = true;
                                        for(int k = rowP1.Length-1; moves != 0; k--){
                                            moves--;
                                            rowP1[k]++;
                                            endPos = k;
                                        }
                                        break;
                                    }
                                    rowP2[j]++;
                                }
                            }
                            break;
                        }
                        moves--;
                        rowP1[i]++;
                        endPos = i;
                    }
                    
                    if((rowP1[endPos] - 1 == 0) && rowP2[endPos] > 0 && belongtoRow){
                        Console.WriteLine("Captured P2's stones!\n");
                        rowP1[endPos] += rowP2[endPos];
                        rowP2[endPos] = 0;
                    }

                    Console.WriteLine(PrintStatus(rowP1, rowP2, p1score, p2score));

                    // Prompt P2 to pick a pocket
                    Console.WriteLine("P2: Pick a pocket not containing 0 to move");
                    startPocket = Convert.ToInt32(Console.ReadLine());

                    // Pick the pocket again if it contained 0
                    while(rowP2[startPocket - 1] == 0 || startPocket >= 7){
                        Console.WriteLine("P2: Pick a different pocket. That pocket contains 0");
                        startPocket = Convert.ToInt32(Console.ReadLine());                    
                    }
                    startPocket--;
                    moves = rowP2[startPocket];
                    rowP2[startPocket] = 0;
                    belongtoRow = true;

                    // Row 2 movements - start at user's selection and move right
                    for(int i = startPocket + 1; moves != 0; i++){
                        // First encounter with out of bounds - score and add to p1's row if applicable
                        if(i >= rowP2.Length){
                            moves--;
                            belongtoRow = false;
                            p2score++;
                            if(moves != 0){
                                // Row 1 movements - start at the end and move left
                                for(int j = rowP1.Length-1; moves != 0; j--){
                                    moves--;
                                    // Second encounter with out of bounds - repeat row 2 movements if applicable
                                    if(j < 0){
                                        belongtoRow = true;
                                        for(int k = 0; moves != 0; k++){
                                            moves--;
                                            rowP2[k]++;
                                            endPos = k;
                                        }
                                        break;
                                    }
                                    rowP1[j]++;
                                }
                            }
                            break;
                        }
                        moves--;
                        rowP2[i]++;
                        endPos = i;
                    }

                    if((rowP2[endPos] - 1 == 0) && rowP1[endPos] > 0 && belongtoRow){
                        Console.WriteLine("Captured P1's stones!\n");
                        rowP2[endPos] += rowP1[endPos];
                        rowP1[endPos] = 0;
                    }
                    Console.WriteLine(PrintStatus(rowP1, rowP2, p1score, p2score));

                    // Gameplay ends when one player's row is empty
                    // Remaining 'stones' in remaining player's row go to their score
                    if(SumArray(rowP1) == 0){
                        for(int i = 0; i < rowP2.Length; i++){
                            p2score += rowP2[i];
                            rowP2[i] = 0;
                        }
                        break;
                    }
                    else if(SumArray(rowP2) == 0){
                        for(int i = 0; i < rowP1.Length; i++){
                            p1score += rowP1[i];
                            rowP1[i] = 0;
                        }
                        break;
                    }
                }
                Console.WriteLine("Game over!");
                if(p1score > p2score){
                    Console.WriteLine("Player 1 wins!");
                }
                else if(p2score > p1score){
                    Console.WriteLine("Player 2 wins!");
                }
                else{
                    Console.WriteLine("Draw!");
                }
                Console.WriteLine("Play again? (Y/N)");
                input = Console.ReadLine().ToUpper();

                while(!(string.Equals(input, "Y") && !(string.Equals(input, "N")))){
                    Console.WriteLine("Play again? (Y/N)");
                    input = Console.ReadLine().ToUpper();
                }

                if(string.Equals(input, "Y")){
                    continue;
                }
                else{
                    playAgain = false;
                    break;
                }
            }
        }
    }
}
