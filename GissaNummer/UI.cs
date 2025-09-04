namespace GissaNummer;

public class UI
{
  private readonly HandleHighScore scoreList;
  /** Constructor for the UI class, initializes the HandleHighScore instance.
   * The variable scores is used in the DrawUI method to fetch and display the high score list.
   */
  public UI()
  {
    scoreList = new();
  }
  /** Draws the UI for the game, including displaying the high score list and prompting the user to play.
   * @return The user's response to whether they want to play the game or not.
   */
  public string DrawUI()
  {
    Console.WriteLine("Välkommen till Gissa Nummer-spelet");
    var scores = scoreList.FetchHighScore();
    if (scores != null && scores.Count > 0)
    {
      scores.Sort();
      Console.WriteLine("HighScore-lista:");
      foreach (var score in scores)
      {
        Console.WriteLine($"{score.Guess} : {score.Name}");
      }
    }
    else
    {
      Console.WriteLine("Ingen har ännu spelat - vill du bli den förste?");
    }
    Console.Write("Vill du spela? Svara med Ja/Nej ");
    return Console.ReadLine() ?? string.Empty;
  }
}