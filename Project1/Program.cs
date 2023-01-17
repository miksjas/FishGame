
using FishGame;
using Microsoft.Xna.Framework;
using System.Windows.Forms;

using var game = new FishGame.FishGame();


var form = new Form1();

if (form.ShowDialog() == DialogResult.OK)
{
    game.Run();
}