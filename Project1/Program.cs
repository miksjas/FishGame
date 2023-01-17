
using System.Windows.Forms;
using FishGame;

using var game = new FishGame.FishGame();

var menuForm = new MenuForm();

if (menuForm.ShowDialog() == DialogResult.OK)
{
    game.FishAmount = menuForm.FishAmount;
    game.HiddenNeuronAmount = menuForm.HiddenNeuronAmount;
    game.SensorsAmount = menuForm.SensorsAmount;
    game.RunMode = menuForm.RunMode;
    game.Run();
}