// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.SlowCheetah.VS.Tests
{
    using System.Collections.Generic;
    using Xunit;

    /// <summary>
    /// Test class for <see cref="PackageUtilities"/>
    /// </summary>
    public class PackageUtilitiesTest
    {
        /// <summary>
        /// Tests <see cref="PackageUtilities.IsFileTransform(string, string, IEnumerable{string})"/> returns on arguments that are null or empty strings
        /// </summary>
        /// <param name="docName">Document name</param>
        /// <param name="trnName">Tranform file name</param>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("App.config", null)]
        [InlineData("App.config", "")]
        [InlineData(null, "App.Debug.config")]
        [InlineData("", "App.Debug.config")]
        public void IsFileTransfromWithNullArguments(string docName, string trnName)
        {
            Assert.False(PackageUtilities.IsFileTransform(docName, trnName));
        }

        /// <summary>
        /// Tests <see cref="PackageUtilities.IsFileTransform(string, string, IEnumerable{string})"/> with valid arguments normally found in projects.
        /// </summary>
        /// <param name="docName">Document name</param>
        /// <param name="trnName">Tranform file name</param>
        [Theory]
        [InlineData("App.config", "App.Debug.config")]
        [InlineData("App.config", "app.release.config")]
        [InlineData("APP.config", "App.Debug.config")]
        [InlineData("App.Test.config", "App.Test.Debug.config")]
        public void IsFileTransfromWithValidArguments(string docName, string trnName)
        {
            Assert.True(PackageUtilities.IsFileTransform(docName, trnName));
        }

        /// <summary>
        /// Tests <see cref="PackageUtilities.IsFileTransform(string, string, IEnumerable{string})"/> with project configurations containing dots
        /// and file names with similar structures. Tests valid names
        /// </summary>
        /// <param name="docName">Document name</param>
        /// <param name="trnName">Tranform file name</param>
        [Theory]
        [InlineData("App.config", "App.Debug.Test.config")]
        [InlineData("App.System.config", "App.System.Debug.Test.config")]
        [InlineData("App.config", "App.Test.Release.config")]
        [InlineData("App.Test.config", "App.Test.Release.config")]
        [InlineData("App.Test.config", "App.Test.Test.Release.config")]
        [InlineData("App.config", "App.Test.Rel.config")]
        public void IsFileTransformWithDottedConfigsAndValidNames(string docName, string trnName)
        {
            Assert.True(PackageUtilities.IsFileTransform(docName, trnName));
        }
    }
}
