// See https://aka.ms/new-console-template for more information
using GissaNummer;

UI ui = new();
GuessNumberGame game = new();

game.PlayGame(ui.DrawUI());
