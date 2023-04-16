// This file is part of CycloneDX Library for .NET
//
// Licensed under the Apache License, Version 2.0 (the “License”);
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an “AS IS” BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// SPDX-License-Identifier: Apache-2.0
// Copyright (c) OWASP Foundation. All Rights Reserved.

using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using CycloneDX.Json;

namespace CycloneDX.Core.Tests.Json.v1_4
{
    public class ValidationTests
    {
        [Theory]
        [InlineData("valid-assembly-1.4.json")]
        // [InlineData("valid-bom-1.4.json")]
        [InlineData("valid-component-hashes-1.4.json")]
        [InlineData("valid-component-ref-1.4.json")]
        [InlineData("valid-component-swid-1.4.json")]
        [InlineData("valid-component-swid-full-1.4.json")]
        [InlineData("valid-component-types-1.4.json")]
        [InlineData("valid-compositions-1.4.json")]
        [InlineData("valid-dependency-1.4.json")]
        [InlineData("valid-empty-components-1.4.json")]
        [InlineData("valid-evidence-1.4.json")]
        [InlineData("valid-external-reference-1.4.json")]
        [InlineData("valid-license-expression-1.4.json")]
        [InlineData("valid-license-id-1.4.json")]
        [InlineData("valid-license-name-1.4.json")]
        [InlineData("valid-metadata-author-1.4.json")]
        [InlineData("valid-metadata-license-1.4.json")]
        [InlineData("valid-metadata-manufacture-1.4.json")]
        [InlineData("valid-metadata-supplier-1.4.json")]
        [InlineData("valid-metadata-timestamp-1.4.json")]
        [InlineData("valid-metadata-tool-1.4.json")]
        [InlineData("valid-minimal-viable-1.4.json")]
        [InlineData("valid-patch-1.4.json")]
        [InlineData("valid-properties-1.4.json")]
        // [InlineData("valid-release-notes-1.4.json")]
        // [InlineData("valid-service-1.4.json")]
        [InlineData("valid-service-empty-objects-1.4.json")]
        [InlineData("valid-signatures-1.4.json")]
        [InlineData("valid-vulnerability-1.4.json")]
        public void ValidateJsonStringTest(string filename)
        {
            var resourceFilename = Path.Join("Resources", "v1.4", filename);
            var jsonString = File.ReadAllText(resourceFilename);

            var validationResult = Validator.Validate(jsonString, SpecificationVersion.v1_4);

            Assert.True(validationResult.Valid);
        }

        [Theory]
        [InlineData("valid-assembly-1.4.json")]
        // [InlineData("valid-bom-1.4.json")]
        [InlineData("valid-component-hashes-1.4.json")]
        [InlineData("valid-component-ref-1.4.json")]
        [InlineData("valid-component-swid-1.4.json")]
        [InlineData("valid-component-swid-full-1.4.json")]
        [InlineData("valid-component-types-1.4.json")]
        [InlineData("valid-compositions-1.4.json")]
        [InlineData("valid-dependency-1.4.json")]
        [InlineData("valid-empty-components-1.4.json")]
        [InlineData("valid-evidence-1.4.json")]
        [InlineData("valid-external-reference-1.4.json")]
        [InlineData("valid-license-expression-1.4.json")]
        [InlineData("valid-license-id-1.4.json")]
        [InlineData("valid-license-name-1.4.json")]
        [InlineData("valid-metadata-author-1.4.json")]
        [InlineData("valid-metadata-license-1.4.json")]
        [InlineData("valid-metadata-manufacture-1.4.json")]
        [InlineData("valid-metadata-supplier-1.4.json")]
        [InlineData("valid-metadata-timestamp-1.4.json")]
        [InlineData("valid-metadata-tool-1.4.json")]
        [InlineData("valid-minimal-viable-1.4.json")]
        [InlineData("valid-patch-1.4.json")]
        [InlineData("valid-properties-1.4.json")]
        // [InlineData("valid-release-notes-1.4.json")]
        // [InlineData("valid-service-1.4.json")]
        [InlineData("valid-service-empty-objects-1.4.json")]
        [InlineData("valid-signatures-1.4.json")]
        [InlineData("valid-vulnerability-1.4.json")]
        public async Task ValidateJsonStreamTest(string filename)
        {
            var resourceFilename = Path.Join("Resources", "v1.4", filename);
            using (var jsonStream = File.OpenRead(resourceFilename))
            {
                var validationResult = await Validator.ValidateAsync(jsonStream, SpecificationVersion.v1_4).ConfigureAwait(false);

                Assert.True(validationResult.Valid);
            }
        }

        [Theory]
        [InlineData("invalid-bomattribute-1.4.json")]
        [InlineData("invalid-component-type-1.4.json")]
        public void InvalidJsonTest(string filename)
        {
            var resourceFilename = Path.Join("Resources", "v1.4", filename);
            var xmlBom = File.ReadAllText(resourceFilename);

            var validationResult = Validator.Validate(xmlBom, SpecificationVersion.v1_4);

            Assert.False(validationResult.Valid);
        }
    }
}
