using OCR.dtos;
using System.Text;
using Tesseract;

namespace Program;

public class Program
{
    public static void Main(string[] args)
    {
        string imagePath = Path.Combine(AppContext.BaseDirectory, "address.jpg");

        string tessDataPath = Path.Combine(AppContext.BaseDirectory, "tessdata");

        using var engine = new TesseractEngine(tessDataPath, "tha+eng", EngineMode.Default);

        using var image = Pix.LoadFromFile(imagePath);
        using var page = engine.Process(image);

        string text = page.GetText();
        string[] textLines = text.Split('\n');

        List<KeyValueDto> keyValues = new List<KeyValueDto>();
        foreach (string line in textLines)
        {
            string[] datas = line.Split(':');

            if (datas.Length == 2)
            {
                string title = datas[0];
                string value = datas[1];

                keyValues.Add(new KeyValueDto()
                {
                    Title = title,
                    Value = new StringBuilder(value)
                });
            }
            else
            {
                if (keyValues.Count > 0)
                {
                    keyValues[keyValues.Count - 1].Value.Append($" {line}");
                }
            }
        }

        foreach (var keyValue in keyValues)
        {
            Console.WriteLine($"Title: {keyValue.Title}, Value: {keyValue.Value.ToString()}");
        }
    }
}