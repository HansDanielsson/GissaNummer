namespace GissaNummer;

public class GuessNumberGame
{
  /** Main class for the Guess the Number game.
   * Handles the game logic, including generating a random number, processing user guesses,
   * and managing the game loop for replaying.
   * Utilizes the Score class to track player scores and the HandleHighScore class to save and retrieve high scores.
   */
  private readonly Score gameScore;
  private readonly HandleHighScore saveScoreList;
  /** Constructor for the GuessNumberGame class, initializes the Score and HandleHighScore instances.
   */
  public GuessNumberGame()
  {
    gameScore = new();
    saveScoreList = new();
  }

  /** Starts and manages the game loop for playing the Guess the Number game.
   * Prompts the user to guess a number between 1 and 100, provides feedback on guesses,
   * and handles saving the player's score upon a correct guess.
   * After each game, prompts the user to decide whether to play again.
   * @param playAgainInput Initial input to determine if the user wants to play the game.
   */
  public void PlayGame(string playAgainInput)
  {
    // Ensure playAgainInput is not null
    playAgainInput ??= string.Empty;
    while (WillPlay(playAgainInput))
    {
      Console.Clear();
      Random random = new();
      int numberToGuess = random.Next(1, 101);
      int numberOfGuesses = 0;
      bool correctGuess = false;

      while (!correctGuess)
      {
        Console.Write("Gissa ett nummer mellan 1 och 100: ");
        string? userInput = Console.ReadLine();
        if (int.TryParse(userInput, out int userGuess))
        {
          numberOfGuesses++;
          if (userGuess < numberToGuess)
          {
            Console.WriteLine("Det var för lågt, gissa igen");
          }
          else if (userGuess > numberToGuess)
          {
            Console.WriteLine("Det var för högt, gissa igen");
          }
          else
          {
            correctGuess = true;
            Console.WriteLine($"Du gissade rätt: talet var {numberToGuess} och det tog dig {numberOfGuesses} försök.");
            Console.Write("Vad heter du? ");
            string? playerName = Console.ReadLine();
            gameScore.Name = playerName ?? string.Empty;
            gameScore.Guess = numberOfGuesses;
            var scores = saveScoreList.FetchHighScore();
            scores.Add(gameScore);
            if (saveScoreList.SaveHighScore(scores))
            {
              Console.WriteLine("Din poäng har sparats i highscore-listan.");
            }
            else
            {
              Console.WriteLine("Ett fel uppstod vid sparande av din poäng.");
            }
          }
        }
        else
        {
          Console.WriteLine("Ogiltig inmatning. Vänligen ange ett nummer.");
        }
      }
      Console.Write("Vill du spela igen? (J/N): ");
      playAgainInput = Console.ReadLine() ?? string.Empty;
    }
    Console.WriteLine("Tack för ditt spel, välkommen igen en annan gång");
  }

  /** Determines if the user wants to play the game based on their input.
   * Accepts any input starting with 'j' or 'J' as a positive response to play.
   * @param parameter The user's input string.
   * @return True if the user wants to play, false otherwise.
   */
  private static bool WillPlay(string parameter) => parameter.ToLower()[0] == 'j';
}