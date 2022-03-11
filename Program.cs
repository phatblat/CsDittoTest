using DittoSDK;
using dotenv.net;

//Console.WriteLine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
Console.WriteLine(System.Environment.CurrentDirectory);
Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
Console.WriteLine(Environment.CurrentDirectory);

//var pwd = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
//Console.WriteLine(pwd);

//DotEnv.Load(options: new DotEnvOptions(
//    ignoreExceptions: false,
//    envFilePaths: new[] { "/Users/phatblat/dev/dotnet/CsDittoTest/.env" }
//));
var env = DotEnv.Fluent()
    .WithEnvFiles()
    .WithDefaultEncoding()
    .WithExceptions()
    .WithTrimValues()
    // exe pwd is in bin/Debug/net6.0
    .WithProbeForEnv(probeLevelsToSearch: 4)
    .Read();

var license = env["DITTO_LICENSE_TOKEN"];
Console.WriteLine(license);

var ditto = new Ditto(identity: DittoIdentity.OfflinePlayground("CsDittoTest"));

ditto.SetOfflineOnlyLicenseToken(license);
ditto.TryStartSync();

var insertedId = ditto.Store.Collection("cars").Insert(new Dictionary<string, object>
{
    { "make", "honda" },
    { "color", "red" },
});

// See https://aka.ms/new-console-template for more information
Console.WriteLine($"inserted car: {insertedId}");
