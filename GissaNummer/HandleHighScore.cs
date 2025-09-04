namespace GissaNummer;

public class HandleHighScore
{
  /** Class to handle saving and fetching high scores from a text file.
   * Each score entry consists of the number of guesses and the player's name, stored in the format "guesses:name".
   * Provides methods to save a list of scores to a file and to retrieve the list of scores from the file.
   */
  private FileStream? fStream;
  private StreamWriter? sWriter;
  private StreamReader? sReader;

  public HandleHighScore()
  {
  }

  /** Saves a list of scores to a text file named "highscore.txt".
   * Each score is written in the format "guesses:name", one per line.
   * @param scores The list of Score objects to be saved.
   * @return True if the scores were successfully saved, false otherwise.
   */
  public bool SaveHighScore(List<Score> scores)
  {
    var path = @"highscore.txt";
    try
    {
      using (fStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
      using (sWriter = new StreamWriter(fStream))
        foreach (var score in scores)
        {
          sWriter.WriteLine($"{score.Guess}:{score.Name}");
          sWriter.Flush();
        }
      return true;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Ett fel uppstod vid sparande av highscore: {ex.Message}");
    }
    return false;
  }

  /** Fetches the list of scores from the "highscore.txt" file.
   * Reads each line in the file, expecting the format "guesses:name", and constructs a list of Score objects.
   * @return A list of Score objects representing the high scores. Returns an empty list if the file does not exist or an error occurs.
   */
  public List<Score> FetchHighScore()
  {
    var path = @"highscore.txt";
    try
    {
      if (!File.Exists(path))
      {
        return [];
      }
      List<Score> scores = [];
      using (fStream = new FileStream(path, FileMode.Open, FileAccess.Read))
      using (sReader = new StreamReader(fStream))
      {
        string? line;
        while ((line = sReader.ReadLine()) != null)
        {
          var parts = line.Split(':');
          if (parts.Length == 2 && int.TryParse(parts[0], out int guess))
          {
            scores.Add(new Score { Guess = guess, Name = parts[1] });
          }
        }
      }
      return scores;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Ett fel uppstod vid hämtning av highscore: {ex.Message}");
    }
    return [];
  }
}