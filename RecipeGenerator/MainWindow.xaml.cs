using System.IO;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace RecipeGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

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

            dynamic inputA = new JObject();
            inputA[InputAType.Text.ToLower()] = $"{InputAMod.Text}:{InputAItem.Text}";
            inputA.count = inputACount;
            dynamic inputB = new JObject();
            inputB[InputBType.Text.ToLower()] = $"{InputBMod.Text}:{InputBItem.Text}";
            inputB.count = inputBCount;
            dynamic inputC = new JObject();
            inputC[InputCType.Text.ToLower()] = $"{InputCMod.Text}:{InputCItem.Text}";
            inputC.count = inputCCount;
            dynamic output = new JObject();
            output.item = $"{AlloyingOutputMod.Text}:{AlloyingOutputItem.Text}";
            output.count = outputCount;
            dynamic recipe = new JObject();
            recipe.inputA = inputA;
            recipe.inputB = inputB;
            recipe.inputC = inputC;
            recipe.output = output;
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

            dynamic input = new JObject();
            input[CommonInputType.Text.ToLower()] = $"{CommonInputMod.Text}:{CommonInputItem.Text}";
            input.count = inputCount;
            dynamic output = new JObject();
            output.item = $"{CommonOutputMod.Text}:{CommonOutputItem.Text}";
            output.count = outputCount;
            dynamic recipe = new JObject();
            recipe.type = $"steamindustries:{CommonRecipeType.Text.ToLower()}";
            recipe.input = input;
            recipe.output = output;
            var saveFile = new SaveFileDialog
            {
                FileName = $"{CommonOutputItem.Text}_from_{CommonRecipeType.Text.ToLower()}_{CommonInputItem.Text}",
                DefaultExt = "json"
            };
            if (saveFile.ShowDialog() == true)
                File.WriteAllText(saveFile.FileName, recipe.ToString());
        }
    }
}