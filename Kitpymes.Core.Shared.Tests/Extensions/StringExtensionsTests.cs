using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kitpymes.Core.Shared.Extensions.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        #region ToRemove

        [DataTestMethod]
        [DataRow(" *", " ", "*")]
        [DataRow("* ", " ", "*")]
        [DataRow(" * ", " ", "*")]
        [DataRow("hello hello", " ", "hellohello")]
        [DataRow("hello hello ", " ", "hellohello")]
        [DataRow(" hello hello ", " ", "hellohello")]
        [DataRow("aeiou", "a", "eiou")]
        [DataRow("aeiou", "ae", "iou")]
        [DataRow("aeiou", "aei", "ou")]
        [DataRow("aeiou", "aeio", "u")]
        [DataRow("aeiou", "aeiou", "")]
        [DataRow("aeiou", "e", "aiou")]
        [DataRow("aeiou", "ei", "aou")]
        public void ToRemove_Passing_ValidValue_Returns_ValueWithoutRemovedChars(string value, string remove, string valueExpected)
        {
            var valueActual = value.ToRemove(remove);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToRemove

        #region ToReplace

        [DataTestMethod]
        [DataRow("aeiou", "*", 0, 1, "*eiou")]
        [DataRow("aeiou", "*", 0, 2, "**iou")]
        [DataRow("aeiou", "*", 0, 3, "***ou")]
        [DataRow("aeiou", "*", 0, 4, "****u")]
        [DataRow("aeiou", "*", 0, 5, "*****")]
        [DataRow("aeiou", "*", 2, 1, "ae*ou")]
        [DataRow("aeiou", "*", 2, 2, "ae**u")]
        public void ToReplace_Passing_ValidValue_Returns_ReplacedValue(
            string value,
            string replace,
            int start,
            int count,
            string valueExpected)
        {
            var valueActual = value.ToReplace(replace, start, count);

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("àáäâãøçħ", "aaaaaoch")]
        [DataRow(" àáäâ ãøçħ ", " aaaa aoch ")]
        [DataRow("*¨^_:-?¿;", "")]
        [DataRow(" * ¨ ^ _ : - ? ¿ ; ", "          ")]
        [DataRow("*¨^_:-?¿;", "?¿", "?¿")]
        public void ToReplaceSpecialChars_Passing_ValidValue_Returns_ReplacedValueWithoutIgnoreSpecialChars(string value, string valueExpected, params string[] ignoreSpecialChars)
        {
            var valueActual = value.ToReplaceSpecialChars(ignoreSpecialChars);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToReplace

        #region ToNormalize

        [DataTestMethod]
        [DataRow("àáäâãøçħ", "aaaaaoch")]
        [DataRow(" àáäâ ãøçħ ", "aaaa aoch")]
        [DataRow("*¨^_:-?¿;", "")]
        [DataRow(" * ¨ ^ _ : - ? ¿ ; ", "")]
        public void ToNormalize_Passing_ValidValue_Returns_NormalizedValue(string value, string valueExpected)
        {
            var valueActual = value.ToNormalize();

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToNormalize

        #region ToEnum

        enum FakeEnum { A, B, C, c };

        [TestMethod]
        public void ToEnum_Passing_ValidValue_String_Returns_EnumValue()
        {
            var valueExpected = FakeEnum.B;

            var valueActual = "B".ToEnum<FakeEnum>();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToEnum_Passing_InvalidValueString_Returns_EnumDefaultValue()
        {
            var valueExpected = FakeEnum.A;

            var valueActual = "b".ToEnum<FakeEnum>();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToEnum_Passing_InvalidValueString_Returns_EnumCustomValue()
        {
            var valueExpected = FakeEnum.c;

            var valueActual = "b".ToEnum(FakeEnum.c);

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToEnum_Passing_ValidValueInt_Returns_EnumValue()
        {
            var valueExpected = FakeEnum.B;

            var valueActual = 1.ToEnum<FakeEnum>();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToEnum_Passing_InvalidValueInt_Returns_EnumDefaultValue()
        {
            var valueExpected = FakeEnum.A;

            var valueActual = 9.ToEnum<FakeEnum>();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [TestMethod]
        public void ToEnum_Passing_InvalidValueInt_Returns_EnumCustomValue()
        {
            var valueExpected = FakeEnum.c;

            var valueActual = 9.ToEnum(FakeEnum.c);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToEnum

        #region ToFirstLetter

        [DataTestMethod]
        [DataRow("aaaaaa", "Aaaaaa")]
        public void ToFirstLetterUpper_Passing_ValidValue_Returns_StringWithFirstLetterUpper(string value, string valueExpected)
        {
            var valueActual = value.ToFirstLetterUpper();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("AAAAAA", "aAAAAA")]
        public void ToFirstLetterLower_Passing_ValidValue_Returns_StringWithFirstLetterLower(string value, string valueExpected)
        {
            var valueActual = value.ToFirstLetterLower();

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToFirstLetter

        #region ToEmailMask

        [DataTestMethod]
        [DataRow("miemail@gmail.com", "m*****l@gmail.com")]
        public void ToEmailMaskUserName_Passing_ValidEmail_Returns_EmailWithMask(string value, string valueExpected)
        {
            var valueActual = value.ToEmailMaskUserName();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("miemail@gmail.com", "m*****l@g***l.com")]
        public void ToEmailMaskUserNameAndDomain_Passing_ValidEmail_Returns_EmailWithMask(string value, string valueExpected)
        {
            var valueActual = value.ToEmailMaskUserNameAndDomain();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("miemail@gmail.com", "m*****l@g***l.c*m")]
        public void ToEmailMaskUserNameAndDomainAndExtension_Passing_ValidEmail_Returns_EmailWithMask(string value, string valueExpected)
        {
            var valueActual = value.ToEmailMaskUserNameAndDomainAndExtension();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("miemail@gmail.com", "m-----l@g---l.c-m", @"(?<=(?:^|@)[^.]*)\B.\B|(?<=[\w]{1})[\w-\+%]*(?=[\w]{1})", '-')]
        public void ToEmailMask_Passing_ValidEmail_Returns_EmailWithMask(string value, string valueExpected, string pattern, char replace)
        {
            var valueActual = value.ToEmailMask(pattern, replace);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToEmailMask

        #region ToUri

        [DataTestMethod]
        [DataRow("http://foo.com/blah_blah")]
        [DataRow("http://foo.com/blah_blah/")]
        [DataRow("http://foo.com/blah_blah/p=5")]
        [DataRow("http://foo.com/blah_blah?p=5")]
        [DataRow("http://foo.com/blah_blah?p=5&j=fe")]
        [DataRow("https://foo.com/blah_blah")]
        [DataRow("https://foo.com/blah_blah/")]
        [DataRow("https://foo.com/blah_blah/p=5")]
        [DataRow("https://foo.com/blah_blah?p=5")]
        [DataRow("https://foo.com/blah_blah?p=5&j=fe")]
        [DataRow("//foo.com/blah_blah")]
        [DataRow("//foo.com/blah_blah/")]
        [DataRow("//foo.com/blah_blah/p=5")]
        [DataRow("//foo.com/blah_blah?p=5")]
        [DataRow("//foo.com/blah_blah?p=5&j=fe")]
        [DataRow("http://foo.com/blah_blah_(wikipedia)")]
        [DataRow("http://foo.com/blah_blah_(wikipedia)_(again)")]
        [DataRow("http://www.example.com/wpstyle/?p=364")]
        [DataRow("https://www.example.com/foo/?bar=baz&inga=42&quux")]
        [DataRow("http://✪df.ws/123")]
        [DataRow("http://userid:password@example.com:8080")]
        [DataRow("http://userid:password@example.com:8080/")]
        [DataRow("http://userid@example.com")]
        [DataRow("http://userid@example.com/")]
        [DataRow("http://userid@example.com:8080")]
        [DataRow("http://userid@example.com:8080/")]
        [DataRow("http://userid:password@example.com")]
        [DataRow("http://userid:password@example.com/")]
        [DataRow("http://142.42.1.1/")]
        [DataRow("http://142.42.1.1:8080/")]
        [DataRow("http://➡.ws/䨹")]
        [DataRow("http://⌘.ws")]
        [DataRow("http://⌘.ws/")]
        [DataRow("http://foo.com/blah_(wikipedia)#cite-1")]
        [DataRow("http://foo.com/blah_(wikipedia)_blah#cite-1")]
        [DataRow("http://foo.com/unicode_(✪)_in_parens")]
        [DataRow("http://foo.com/(something)?after=parens")]
        [DataRow("http://☺.damowmow.com/")]
        [DataRow("http://code.google.com/events/#&product=browser")]
        [DataRow("http://j.mp")]
        [DataRow("http://foo.bar/?q=Test%20URL-encoded%20stuff")]
        [DataRow("http://مثال.إختبار")]
        [DataRow("http://例子.测试")]
        [DataRow("http://उदाहरण.परीक्ष")]
        [DataRow("http://-.~_!$&'()*+,;=:%40:80%2f::::::@example.com")]
        [DataRow("http://1337.net")]
        [DataRow("http://a.b-c.de")]
        [DataRow("http://223.255.255.254")]
        public void ToUri_Passing_ValidValueString_Returns_Uri(string valueExpected)
        {
            var result = valueExpected.ToUri();

            Assert.AreEqual(valueExpected, result?.OriginalString);
        }

        [DataTestMethod]
        [DataRow("http://?")]
        [DataRow("http://")]
        [DataRow("http://.")]
        [DataRow("http://..")]
        [DataRow("http://../")]
        [DataRow("http://?")]
        [DataRow("http://??")]
        [DataRow("http://??/")]
        [DataRow("http://#")]
        [DataRow("http://##")]
        [DataRow("http://##/")]
        public void ToUri_Passing_InvalidValueString_Returns_Null(string value)
        {
            Uri? valueExpected = null;

            var valueActual = value.ToUri();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("http://?")]
        [DataRow("http://")]
        [DataRow("http://.")]
        [DataRow("http://..")]
        [DataRow("http://../")]
        [DataRow("http://?")]
        [DataRow("http://??")]
        [DataRow("http://??/")]
        [DataRow("http://#")]
        [DataRow("http://##")]
        [DataRow("http://##/")]
        public void ToUri_Passing_InvalidValueString_Returns_DefaultValue(string value)
        {
            var defaultValue = new Uri("https://google.com");

            var valueExpected = defaultValue;

            var valueActual = value.ToUri(defaultValue);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToUri

        #region ToHashAlgorithmName

        [DataTestMethod]
        [DataRow("MD5")]
        public void ToHashAlgorithmName_Passing_ValidValueString_Returns_HashAlgorithmName(string value)
        {
            var valueExpected = HashAlgorithmName.MD5;

            var valueActual = value.ToHashAlgorithmName();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("md5")]
        public void ToHashAlgorithmName_Passing_InvalidValueLowerCaseString_Returns_Null(string value)
        {
            HashAlgorithmName? valueExpected = null;

            var valueActual = value.ToHashAlgorithmName();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("md5")]
        public void ToHashAlgorithmName_Passing_InvalidValueLowerCaseStringWithDefaultValue_Returns_DefaultValue(string value)
        {
            var valueExpected = HashAlgorithmName.SHA1;

            var valueActual = value.ToHashAlgorithmName(HashAlgorithmName.SHA1);

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("hhhhhh")]
        public void ToHashAlgorithmName_Passing_InvalidValueString_Returns_Null(string value)
        {
            HashAlgorithmName? valueExpected = null;

            var valueActual = value.ToHashAlgorithmName();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("hhhhhh")]
        public void ToHashAlgorithmName_Passing_InvalidValueStringWithDefaultValue_Returns_DefaultValue(string value)
        {
            var valueExpected = HashAlgorithmName.SHA1;

            var valueActual = value.ToHashAlgorithmName(HashAlgorithmName.SHA1);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToHashAlgorithmName

        #region ToInt

        [DataTestMethod]
        [DataRow("1234567890")]
        public void ToInt_Passing_ValidValueString_Returns_Int(string value)
        {
            var valueExpected = 1234567890;

            var valueActual = value.ToInt();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("1.3333")]
        [DataRow("1,3333")]
        [DataRow("1.333,39")]
        [DataRow("999999439293294392492349329493294")]
        [DataRow("asdasdasdasd234324")]
        public void ToInt_Passing_InvalidValueString_Returns_Null(string value)
        {
            int? valueExpected = null;

            var valueActual = value.ToInt();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataTestMethod]
        [DataRow("1.3333")]
        [DataRow("1,3333")]
        [DataRow("1.333,39")]
        [DataRow("999999439293294392492349329493294")]
        [DataRow("asdasdasdasd234324")]
        public void ToInt_Passing_InvalidValueString_Returns_DefaultValue(string value)
        {
            var defaultValue = 1234567890;

            int valueExpected = defaultValue;

            var valueActual = value.ToInt(defaultValue);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToInt

        #region ToZip

#if DEBUG

        [TestMethod]
        public void ToZipCreate_Passing_ValidFiles_Returns_ZipFiles()
        {
            var proyectDirectoryPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var sourceDirectoryPath = proyectDirectoryPath + "\\Fakes";

            var fileZipPath = sourceDirectoryPath + ".zip";
            var destinationDirectoryPath = proyectDirectoryPath;

            sourceDirectoryPath.ToZipCreate(destinationDirectoryPath);

            Assert.IsTrue(File.Exists(fileZipPath));

            File.Delete(fileZipPath);
        }

        [TestMethod]
        public void ToZipCreate_Passing_ValidFilesWithCutomZipName_Returns_ZipFiles()
        {
            var proyectDirectoryPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var sourceDirectoryPath = proyectDirectoryPath + "\\Fakes";

            var customZipName = Guid.NewGuid().ToString();
            var fileZipPath = proyectDirectoryPath + $"\\{customZipName}.zip";
            var destinationDirectoryPath = proyectDirectoryPath;

            sourceDirectoryPath.ToZipCreate(destinationDirectoryPath, customZipName);

            Assert.IsTrue(File.Exists(fileZipPath));

            File.Delete(fileZipPath);
        }

        [TestMethod]
        public void ToZipExtract_Passing_ValidArchive_Returns_Files()
        {
            var proyectDirectoryPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var folderNameToZip = "\\Fakes";
            var sourceDirectoryPath = proyectDirectoryPath + folderNameToZip;

            var zipNamePath = sourceDirectoryPath + ".zip";
            var destinationDirectoryPath = proyectDirectoryPath;
            var destinationDirectoryPathToExtractFiles = "FakeFolderZip".ToDirectoryTemporary();
            int fileCountExpected = Directory.GetFiles(sourceDirectoryPath, "*.*").Length;

            sourceDirectoryPath.ToZipCreate(destinationDirectoryPath);

            zipNamePath.ToZipExtract(destinationDirectoryPathToExtractFiles);

            int fileCountActual = Directory.GetFiles(destinationDirectoryPathToExtractFiles, "*.*").Length;

            Assert.AreEqual(fileCountExpected, fileCountActual);

            File.Delete(zipNamePath);
            destinationDirectoryPathToExtractFiles.ToDirectoryDeleteFiles(true);
        }

#endif

        #endregion ToZip

        #region ToContentType

        [DataTestMethod]
        [DataRow("xxxx.pdf", MediaTypeNames.Application.Pdf)]
        [DataRow("xxxx.json", MediaTypeNames.Application.Json)]
        [DataRow("xxxx.gif", MediaTypeNames.Image.Gif)]
        [DataRow("xxxx.jpeg", MediaTypeNames.Image.Jpeg)]
        [DataRow("xxxx.html", MediaTypeNames.Text.Html)]
        [DataRow("xxxx.txt", MediaTypeNames.Text.Plain)]
        public void ToContentType_Passing_ValidValueString_Returns_ContentType(string value, string valueExpected)
        {
            var valueActual = value.ToContentType();

            Assert.AreEqual(valueExpected, valueActual);
        }

        [DataRow("xxxx.x", MediaTypeNames.Text.Plain, MediaTypeNames.Application.Pdf)]
        [DataRow("xxxx.x", MediaTypeNames.Text.Plain, MediaTypeNames.Application.Pdf)]
        [DataRow("xxxx.x", MediaTypeNames.Text.Plain, MediaTypeNames.Application.Pdf)]
        [DataRow("xxxx.x", MediaTypeNames.Text.Plain, MediaTypeNames.Application.Pdf)]
        [DataRow("xxxx.x", MediaTypeNames.Text.Plain, MediaTypeNames.Application.Pdf)]
        public void ToContentType_Passing_InvalidValueString_Returns_DefaultContentType(string value, string valueExpected, string defaultValue)
        {
            var valueActual = value.ToContentType(defaultValue);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToContentType

        #region ToDirectory

        #region ToDirectoryFindFilePath

#if DEBUG

        [TestMethod]
        public void ToDirectoryFindFilePath_Passing_ValidValue_Returns_FilePath()
        {
            var folderName = Guid.NewGuid().ToString();
            var destinationDirectoryPath = folderName.ToDirectoryTemporary();
            var fileName = Guid.NewGuid().ToString();
            var fileContent = Guid.NewGuid().ToString();
            File.WriteAllText(destinationDirectoryPath + $"\\{fileName}.txt", fileContent);

            var valueExpected = $"{destinationDirectoryPath}\\{fileName}.txt";

            var valueActual = destinationDirectoryPath.ToDirectoryFindFilePath(fileName);

            Assert.AreEqual(valueExpected, valueActual);

            destinationDirectoryPath.ToDirectoryDeleteFiles(true, true);
        }

        [TestMethod]
        public void ToDirectoryFindFilePath_Passing_ValidValueIntoSubDirectory_Returns_FilePath()
        {
            var folderName = Guid.NewGuid().ToString();
            var destinationDirectoryPath = folderName.ToDirectoryTemporary();
            var subDirectoryPath = Directory.CreateDirectory(destinationDirectoryPath + "\\SubDirectory");
            var fileName = Guid.NewGuid().ToString();
            var fileContent = Guid.NewGuid().ToString();
            File.WriteAllText(subDirectoryPath + $"\\{fileName}.txt", fileContent);

            var valueExpected = $"{subDirectoryPath}\\{fileName}.txt";

            var valueActual = destinationDirectoryPath.ToDirectoryFindFilePath(fileName);

            Assert.AreEqual(valueExpected, valueActual);

            destinationDirectoryPath.ToDirectoryDeleteFiles(true, true);
        }

#endif

        #endregion ToDirectoryFindFilePath

        #region ToDirectoryFileInfo

        [TestMethod]
        public void ToDirectoryFileInfo_Passing_ValidValue_Returns_FileInfo()
        {
#if DEBUG

            var folderName = Guid.NewGuid().ToString();
            var destinationDirectoryPath = folderName.ToDirectoryTemporary();
            var fileName = Guid.NewGuid().ToString();
            var fileContent = Guid.NewGuid().ToString();
            File.WriteAllText(destinationDirectoryPath + $"\\{fileName}.txt", fileContent);

            var valueExpected = $"{destinationDirectoryPath}\\{fileName}.txt";

            var valueActual = destinationDirectoryPath.ToDirectoryFileInfo(fileName);

            Assert.AreEqual(valueExpected, valueActual?.FullName);

            destinationDirectoryPath.ToDirectoryDeleteFiles(true, true);

#endif
        }

        #endregion ToDirectoryFileInfo

        #region ToDirectoryDeleteFiles

        [TestMethod]
        public void ToDirectoryDeleteFiles_Passing_ValidValue_Returns_DirectoryNoExists()
        {
            var folderName = Guid.NewGuid().ToString();
            var destinationDirectoryPath = folderName.ToDirectoryTemporary();
            var fileName = Guid.NewGuid().ToString();
            var fileContent = Guid.NewGuid().ToString();
            File.WriteAllText(destinationDirectoryPath + $"\\{fileName}.txt", fileContent);

            destinationDirectoryPath.ToDirectoryDeleteFiles(true, true);

            var valueActual = destinationDirectoryPath.ToIsDirectory();

            Assert.IsFalse(valueActual);
        }

        #endregion ToDirectoryDeleteFiles

        #region ToDirectorySaveFileAsync

#if DEBUG

        [TestMethod]
        public async Task ToDirectorySaveFileAsync_Passing_ValidValue_Returns_False()
        {
            var folderName = Guid.NewGuid().ToString();
            var destinationDirectoryPath = folderName.ToDirectoryTemporary();
            var fileName = Guid.NewGuid().ToString() + ".txt";
            var fileContent = Guid.NewGuid().ToString();
            var bytes = Encoding.UTF8.GetBytes(fileContent);

            await destinationDirectoryPath.ToDirectorySaveFileAsync(fileName, bytes);

            var valueActual = File.Exists(destinationDirectoryPath + $"\\{fileName}");

            Assert.IsTrue(valueActual);

            destinationDirectoryPath.ToDirectoryDeleteFiles(true, true);
        }

#endif

        #endregion ToDirectorySaveFileAsync

        #region ToDirectoryReadFileAsync

        [TestMethod]
        public async Task ToDirectoryReadFileAsync_Passing_ValidValue_Returns_False()
        {
            var folderName = Guid.NewGuid().ToString();
            var destinationDirectoryPath = folderName.ToDirectoryTemporary();
            var fileName = Guid.NewGuid().ToString();
            var fileContent = Guid.NewGuid().ToString();
            File.WriteAllText(destinationDirectoryPath + $"\\{fileName}.txt", fileContent);

            var valueExpected = $"{destinationDirectoryPath}\\{fileName}.txt";

            var result = await destinationDirectoryPath.ToDirectoryReadFileAsync(fileName);

            if (result != null)
            {
                Assert.AreEqual(valueExpected, result.Value.filePath);
            }

            destinationDirectoryPath.ToDirectoryDeleteFiles(true, true);
        }

        #endregion ToDirectoryReadFileAsync

        #region ToDirectoryTemporary

        [TestMethod]
        public void ToDirectoryTemporary_Passing_ValidValue_Returns_DirectoryExists()
        {
            var folderName = Guid.NewGuid().ToString();
            var destinationDirectoryPath = folderName.ToDirectoryTemporary();

            var valueActual = destinationDirectoryPath.ToIsDirectory();

            Assert.IsTrue(valueActual);

            destinationDirectoryPath.ToDirectoryDeleteFiles(true, true);
        }

        #endregion ToDirectoryTemporary

        #endregion ToDirectory

        #region ToFormat

        [TestMethod]
        public void ToFormat_Passing_ValidValueFormatWithArguments_Returns_ValueWithFormat()
        {
            var valueFormat = "{0}-{1}-{2}";
            var value0 = "A";
            var value1 = "B";
            var value2 = "C";
            var valueExpected = $"{value0}-{value1}-{value2}";
           
            var valueActual = valueFormat.ToFormat(value0, value1, value2);

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToFormat

        #region ToAssembly

        [TestMethod]
        public void ToAssembly_Passing_ValidValueAssemblyString_Returns_Assembly()
        {
            var valueExpected = typeof(StringExtensionsTests).Assembly;
            var stringAssembly = "Kitpymes.Core.Shared.Tests";

            var valueActual = stringAssembly.ToAssembly();

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToAssembly
    }
}
