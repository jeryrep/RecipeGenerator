using System.IO;
using System.Text.Json.Nodes;
using System.Windows;
using Microsoft.Win32;

namespace RecipeGenerator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow() => InitializeComponent();

    private void AlloyingGenerate_Click(object sender, RoutedEventArgs e)
    {
        var checkInputA = int.TryParse(InputACount.Text, out var inputACount);
        var checkInputB = int.TryParse(InputBCount.Text, out var inputBCount);
        var checkInputC = int.TryParse(InputCCount.Text, out var inputCCount);
        var checkOutput = int.TryParse(AlloyingOutputCount.Text, out var outputCount);
        if (!checkInputA || !checkInputB || !checkInputC || !checkOutput || InputAMod.Text == "" ||
            InputAItem.Text == "" || InputBMod.Text == "" || InputBItem.Text == "" || InputCMod.Text == "" ||
            InputCItem.Text == "" || AlloyingOutputMod.Text == "" || AlloyingOutputItem.Text == "")
        {
            MessageBox.Show("Not every field is filled in or contains errors.", "Error");
            return;
        }
        
        var recipe = new JsonObject
        {
            ["inputA"] = new JsonObject
            {
                [InputAType.Text.ToLower()] = $"{InputAMod.Text}:{InputAItem.Text}",
                ["count"] = inputACount
            },
            ["inputB"] = new JsonObject
            {
                [InputBType.Text.ToLower()] = $"{InputBMod.Text}:{InputBItem.Text}",
                ["count"] = inputBCount
            },
            ["inputC"] = new JsonObject
            {
                [InputCType.Text.ToLower()] = $"{InputCMod.Text}:{InputCItem.Text}",
                ["count"] = inputCCount
            },
            ["output"] = new JsonObject
            {
                ["item"] = $"{AlloyingOutputMod.Text}:{AlloyingOutputItem.Text}",
                ["count"] = outputCount
            }
        };
        var saveFile = new SaveFileDialog
        {
            DefaultExt = "json",
            FileName =
                $"{AlloyingOutputItem.Text}_from_alloying_{InputAItem.Text}_and_{InputBItem.Text}_and_{InputCItem.Text}"
        };
        if (saveFile.ShowDialog() == true)
            File.WriteAllText(saveFile.FileName, recipe.ToString());
    }

    private void CommonGenerate_Click(object sender, RoutedEventArgs e)
    {
        var checkInput = int.TryParse(CommonInputCount.Text, out var inputCount);
        var checkOutput = int.TryParse(CommonOutputCount.Text, out var outputCount);
        if (!checkInput || !checkOutput || CommonInputMod.Text == "" || CommonInputItem.Text == "" ||
            CommonOutputItem.Text == "" || CommonOutputMod.Text == "")
        {
            MessageBox.Show("Not every field is filled in or contains errors.", "Error");
            return;
        }

        var recipe = new JsonObject
        {
            ["input"] = new JsonObject
            {
                [CommonInputType.Text.ToLower()] = $"{CommonInputMod.Text}:{CommonInputItem.Text}",
                ["count"] = inputCount
            },
            ["output"] = new JsonObject
            {
                ["item"] = $"{CommonOutputMod.Text}:{CommonOutputItem.Text}",
                ["count"] = outputCount
            }
        };
        var saveFile = new SaveFileDialog
        {
            FileName = $"{CommonOutputItem.Text}_from_{CommonRecipeType.Text.ToLower()}_{CommonInputItem.Text}",
            DefaultExt = "json"
        };
        if (saveFile.ShowDialog() == true)
            File.WriteAllText(saveFile.FileName, recipe.ToString());
    }
}