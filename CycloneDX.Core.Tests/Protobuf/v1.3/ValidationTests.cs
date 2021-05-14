using System;
using System.IO;
using Snapshooter;
using Snapshooter.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace CycloneDX.Tests.Protobuf.v1_3 
{
    public class ValidationTests
    {
        private readonly ITestOutputHelper output;
    
        public ValidationTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        // I can't be bothered setting up protoc in the github workflow for all platforms
        // if anyone wants to have a crack at it please go for it
        [Utils.LinuxOnlyForCITheory]
        [InlineData("valid-assembly-1.3.json")]
        [InlineData("valid-bom-1.3.json")]
        [InlineData("valid-component-hashes-1.3.json")]
        [InlineData("valid-component-ref-1.3.json")]
        [InlineData("valid-component-swid-1.3.json")]
        [InlineData("valid-component-swid-full-1.3.json")]
        [InlineData("valid-component-types-1.3.json")]
        [InlineData("valid-compositions-1.3.json")]
        [InlineData("valid-dependency-1.3.json")]
        [InlineData("valid-empty-components-1.3.json")]
        [InlineData("valid-evidence-1.3.json")]
        [InlineData("valid-external-reference-1.3.json")]
        [InlineData("valid-license-expression-1.3.json")]
        [InlineData("valid-license-id-1.3.json")]
        [InlineData("valid-license-name-1.3.json")]
        [InlineData("valid-metadata-author-1.3.json")]
        [InlineData("valid-metadata-license-1.3.json")]
        [InlineData("valid-metadata-manufacture-1.3.json")]
        [InlineData("valid-metadata-supplier-1.3.json")]
        [InlineData("valid-metadata-timestamp-1.3.json")]
        [InlineData("valid-metadata-tool-1.3.json")]
        [InlineData("valid-minimal-viable-1.3.json")]
        [InlineData("valid-patch-1.3.json")]
        [InlineData("valid-properties-1.3.json")]
        [InlineData("valid-service-1.3.json")]
        [InlineData("valid-service-empty-objects-1.3.json")]
        public void ValidProtobufTest(string filename)
        {
            using (var tempDir = new Utils.TempDirectoryWithProtoSchema())
            {
                var resourceFilename = Path.Join("Resources", "v1.3", filename);
                var jsonBom = File.ReadAllText(resourceFilename);
                var inputBom = CycloneDX.Json.Deserializer.Deserialize_v1_3(jsonBom);

                var stream = new MemoryStream();
                CycloneDX.Protobuf.Serializer.Serialize(stream, inputBom);

                var protoBom = stream.ToArray();

                var runner = new Utils.ProtocRunner();
                var result = runner.Run(tempDir.DirectoryPath, protoBom, new string[]
                {
                    "--proto_path=./",
                    "--decode=cyclonedx.v1_3.Bom",
                    "bom-1.3.proto"
                });

                if (result.ExitCode == 0)
                {
                    Snapshot.Match(result.Output, SnapshotNameExtension.Create(filename));
                }
                else
                {
                    output.WriteLine(result.Output);
                    output.WriteLine(result.Errors);
                    Assert.Equal(0, result.ExitCode);
                }
            }
        }
    }
}