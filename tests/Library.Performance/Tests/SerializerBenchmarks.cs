using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Library.Models.Books;
using Soenneker.Utils.AutoBogus;

namespace Library.Performance.Tests;

[MemoryDiagnoser]
[HtmlExporter]
public class SerializerBenchmarks
{
    private readonly AutoFaker<BookViewModel> _bookGenerator = new();

    private BookViewModel _book;

    [GlobalSetup]
    public void Setup()
    {
        _book = _bookGenerator.Generate();
    }

    [Benchmark(Baseline = true)]
    public void NewtonsoftSerializer()
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(_book);
        var deserializedBook = Newtonsoft.Json.JsonConvert.DeserializeObject<BookViewModel>(json);
    }

    [Benchmark]
    public void SystemTextSerializer()
    {
        var json = JsonSerializer.Serialize(_book);
        var deserializedBook = JsonSerializer.Deserialize<BookViewModel>(json);
    }

}