namespace GissaNummer;

public class Score : IComparable<Score>
{
  /** Class to hold player score information, including their name and the number of guesses taken.
   * Implements IComparable to allow sorting by number of guesses and by name if guesses are equal.
   */
  public string? Name;
  public int Guess;
  /** Compares this Score instance with another Score instance.
   * Primary comparison is by number of guesses (Guess), with higer numbers being better.
   * If the number of guesses is the same, it compares by Name in order.
   * @param other The other Score instance to compare against.
   * @return A negative number if this instance is less than other, zero if they are equal, and a positive number if this instance is greater than other.
   */
  public int CompareTo(Score? other)
  {
    if (other is null)
    {
      return 1;
    }
    if (Guess == other.Guess)
    {
      // Handle possible nulls for Name and other.Name
      if (Name is null && other.Name is null)
        return 0;
      if (Name is null)
        return -1;
      if (other.Name is null)
        return 1;
      return Name.CompareTo(other.Name);
    }
    return Guess.CompareTo(other.Guess);
  }
}