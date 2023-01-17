using System.Windows.Forms;
using FishGame;

var menuForm = new MenuForm();
var keepOpen = true;
while (keepOpen)
{
    using var game = new FishGame.FishGame();
    var result = menuForm.ShowDialog();
    if (result == DialogResult.OK)
    {
        game.FishAmount = menuForm.FishAmount;
        game.HiddenNeuronAmount = menuForm.HiddenNeuronAmount;
        game.SensorsAmount = menuForm.SensorsAmount;
        game.RunMode = menuForm.RunMode;
        game.Run();
    }
    if (result == DialogResult.Cancel)
        keepOpen = false;
}