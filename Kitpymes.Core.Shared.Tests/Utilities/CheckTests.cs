using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class CheckTests
    {
        #region IsNullOrEmpty

        [TestMethod]
        public void IsNullOrEmpty_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsNullOrEmpty
            (
                FakeTypes.ValueTypes.SimpleTypes.SByte_Null,
                FakeTypes.ValueTypes.SimpleTypes.Short_Null,
                FakeTypes.ValueTypes.SimpleTypes.Int_Null,
                FakeTypes.ValueTypes.SimpleTypes.Long_Null,
                FakeTypes.ValueTypes.SimpleTypes.Byte_Null,
                FakeTypes.ValueTypes.SimpleTypes.UShort_Null,
                FakeTypes.ValueTypes.SimpleTypes.UInt_Null,
                FakeTypes.ValueTypes.SimpleTypes.ULong_Null,
                FakeTypes.ValueTypes.SimpleTypes.Char_Null,
                FakeTypes.ValueTypes.SimpleTypes.Float_Null,
                 FakeTypes.ValueTypes.SimpleTypes.Double_Null,
                FakeTypes.ValueTypes.SimpleTypes.Decimal_Null,
                FakeTypes.ValueTypes.SimpleTypes.Bool_Null,
                FakeTypes.ValueTypes.EnumerationTypes.ExampleEnumeration.a,
                new FakeTypes.ValueTypes.StructureTypes.ExampleStructure(),
                FakeTypes.ValueTypes.StructureTypes.Guid_Empty,
                FakeTypes.ValueTypes.StructureTypes.Guid_Null,

                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default,
                FakeTypes.ReferenceTypes.ClassTypes.Class_Null,
                FakeTypes.ReferenceTypes.ClassTypes.Class_Default,
                FakeTypes.ReferenceTypes.ClassTypes.Object_Null,
                FakeTypes.ReferenceTypes.ClassTypes.Object_Default,
                FakeTypes.ReferenceTypes.MatrizTypes.Array_Null,
                FakeTypes.ReferenceTypes.MatrizTypes.Array_Default,

                FakeTypes.ColecctionsTypes.Enumerable_Null,
                FakeTypes.ColecctionsTypes.Enumerable_Default,
                FakeTypes.ColecctionsTypes.List_Null,
                FakeTypes.ColecctionsTypes.List_Default,
                FakeTypes.ColecctionsTypes.HashSet_Null,
                FakeTypes.ColecctionsTypes.HashSet_Default,
                FakeTypes.ColecctionsTypes.Collection_Null,
                FakeTypes.ColecctionsTypes.Collection_Default,
                FakeTypes.ColecctionsTypes.Queue_Null,
                FakeTypes.ColecctionsTypes.Queue_Default,
                FakeTypes.ColecctionsTypes.SortedSet_Null,
                FakeTypes.ColecctionsTypes.SortedSet_Default,
                FakeTypes.ColecctionsTypes.Stack_Null,
                FakeTypes.ColecctionsTypes.Stack_Default,
                FakeTypes.ColecctionsTypes.ArrayClass_Null,
                FakeTypes.ColecctionsTypes.ArrayClass_Default,
                FakeTypes.ColecctionsTypes.ArrayList_Null,
                FakeTypes.ColecctionsTypes.ArrayList_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 41);
        }

        #endregion IsNullOrEmpty

        #region IsNullOrAny

        [TestMethod]
        public void IsNullOrAny_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsNullOrAny
            (
                // Se pueden enviar valores de tipo String a un parámetro de tipo IEnumerable
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default,

                FakeTypes.ReferenceTypes.MatrizTypes.Array_Null,
                FakeTypes.ReferenceTypes.MatrizTypes.Array_Default,
                FakeTypes.ReferenceTypes.MatrizTypes.Array_CountZero,

                FakeTypes.ColecctionsTypes.Enumerable_Null,
                FakeTypes.ColecctionsTypes.Enumerable_Default,
                FakeTypes.ColecctionsTypes.Enumerable_CountZero,

                FakeTypes.ColecctionsTypes.List_Null,
                FakeTypes.ColecctionsTypes.List_Default,
                FakeTypes.ColecctionsTypes.List_CountZero,

                FakeTypes.ColecctionsTypes.HashSet_Null,
                FakeTypes.ColecctionsTypes.HashSet_Default,
                FakeTypes.ColecctionsTypes.HashSet_CountZero,

                FakeTypes.ColecctionsTypes.Collection_Null,
                FakeTypes.ColecctionsTypes.Collection_Default,
                FakeTypes.ColecctionsTypes.Collection_CountZero,

                FakeTypes.ColecctionsTypes.Queue_Null,
                FakeTypes.ColecctionsTypes.Queue_Default,
                FakeTypes.ColecctionsTypes.Queue_CountZero,

                FakeTypes.ColecctionsTypes.SortedSet_Null,
                FakeTypes.ColecctionsTypes.SortedSet_Default,
                FakeTypes.ColecctionsTypes.SortedSet_CountZero,

                FakeTypes.ColecctionsTypes.Stack_Null,
                FakeTypes.ColecctionsTypes.Stack_Default,
                FakeTypes.ColecctionsTypes.Stack_CountZero,

                FakeTypes.ColecctionsTypes.ArrayClass_Null,
                FakeTypes.ColecctionsTypes.ArrayClass_Default,
                FakeTypes.ColecctionsTypes.ArrayClass_CountZero,

                FakeTypes.ColecctionsTypes.ArrayList_Null,
                FakeTypes.ColecctionsTypes.ArrayList_Default,
                FakeTypes.ColecctionsTypes.ArrayList_CountZero
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 33);
        }

        #endregion IsNullOrAny

        #region IsGreater

        [TestMethod]
        public void IsGreater_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var (HasErrors, Count) = Util.Check.IsGreater
            (
                max,

                FakeTypes.ValueTypes.SimpleTypes.Byte_New(),
                FakeTypes.ValueTypes.SimpleTypes.UShort_New(),
                FakeTypes.ValueTypes.SimpleTypes.UInt_New(),
                FakeTypes.ValueTypes.SimpleTypes.ULong_New(),
                FakeTypes.ValueTypes.SimpleTypes.Char_New(),
                FakeTypes.ValueTypes.EnumerationTypes.ExampleEnumeration.a,

                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.MatrizTypes.Array_New(),

                FakeTypes.ColecctionsTypes.Enumerable_New(),
                FakeTypes.ColecctionsTypes.List_New(),
                FakeTypes.ColecctionsTypes.HashSet_New(),
                FakeTypes.ColecctionsTypes.Collection_New(),
                FakeTypes.ColecctionsTypes.Queue_New(),
                FakeTypes.ColecctionsTypes.SortedSet_New(),
                FakeTypes.ColecctionsTypes.Stack_New(),
                FakeTypes.ColecctionsTypes.ArrayClass_New(),
                FakeTypes.ColecctionsTypes.ArrayList_New()
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 17);
        }

        #endregion IsGreater

        #region IsLess

        [TestMethod]
        public void IsLess_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;

            var (HasErrors, Count) = Util.Check.IsLess
            (
                min,

                FakeTypes.ValueTypes.SimpleTypes.Char_New(),
                FakeTypes.ValueTypes.EnumerationTypes.ExampleEnumeration.e,

                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.MatrizTypes.Array_New(),

                FakeTypes.ColecctionsTypes.Enumerable_New(),
                FakeTypes.ColecctionsTypes.List_New(),
                FakeTypes.ColecctionsTypes.HashSet_New(),
                FakeTypes.ColecctionsTypes.Collection_New(),
                FakeTypes.ColecctionsTypes.Queue_New(),
                FakeTypes.ColecctionsTypes.SortedSet_New(),
                FakeTypes.ColecctionsTypes.Stack_New(),
                FakeTypes.ColecctionsTypes.ArrayClass_New(),
                FakeTypes.ColecctionsTypes.ArrayList_New()
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 13);
        }

        #endregion IsLess

        #region IsEqual

        [TestMethod]
        public void IsEqual_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsEqual
            (
                value: FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_New(value: '-')
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        #endregion IsEqual

        #region IsRange

        [TestMethod]
        public void IsRange_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var min = FakeTypes.ValueTypes.SimpleTypes.Int_Max;
            var max = FakeTypes.ValueTypes.SimpleTypes.Int_Min;

            var (HasErrors, Count) = Util.Check.IsRange
            (
               min, max,

                FakeTypes.ValueTypes.SimpleTypes.Byte_New(),
                FakeTypes.ValueTypes.SimpleTypes.UShort_New(),
                FakeTypes.ValueTypes.SimpleTypes.UInt_New(),
                FakeTypes.ValueTypes.SimpleTypes.ULong_New(),
                FakeTypes.ValueTypes.SimpleTypes.Char_New(),
                FakeTypes.ValueTypes.EnumerationTypes.ExampleEnumeration.a,

                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.MatrizTypes.Array_New(),

                FakeTypes.ColecctionsTypes.Enumerable_New(),
                FakeTypes.ColecctionsTypes.List_New(),
                FakeTypes.ColecctionsTypes.HashSet_New(),
                FakeTypes.ColecctionsTypes.Collection_New(),
                FakeTypes.ColecctionsTypes.Queue_New(),
                FakeTypes.ColecctionsTypes.SortedSet_New(),
                FakeTypes.ColecctionsTypes.Stack_New(),
                FakeTypes.ColecctionsTypes.ArrayClass_New(),
                FakeTypes.ColecctionsTypes.ArrayList_New()
           );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 17);
        }

        #endregion IsRange

        #region IsRegexMatch

        [TestMethod]
        public void IsRegexMatch_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsRegexMatch
            (
                Util.Regexp.ForEmail,
                FakeTypes.ReferenceTypes.ClassTypes.String_New(), 
                FakeTypes.ReferenceTypes.ClassTypes.String_New('-')
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 2);
        }

        #endregion IsRegexMatch

        #region IsCustom

        [TestMethod]
        public void IsCustom_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsCustom
            (
                () => FakeTypes.ValueTypes.SimpleTypes.Int_Null is null,
                () => FakeTypes.ValueTypes.StructureTypes.Guid_Null is null,
                () => FakeTypes.ReferenceTypes.ClassTypes.String_Default is null,
                () => FakeTypes.ColecctionsTypes.ArrayList_Default is null
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        #endregion IsCustom

        #region IsName

        [TestMethod]
        public void IsName_Passing_ValidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsName
            (
                 "España", "Españá", "Españà", "Españä", "Españâ", "Esp añâ"
            );

            Assert.IsFalse(HasErrors);
            Assert.IsTrue(Count == 0);
        }

        [TestMethod]
        public void IsName_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsName
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        #endregion IsName

        #region IsEmail

        [TestMethod]
        public void IsEmail_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsEmail
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        #endregion IsEmail

        #region IsDirectory

        [TestMethod]
        public void IsDirectory_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsDirectory
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        #endregion IsDirectory

        #region IsFile

        [TestMethod]
        public void IsFile_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsFile
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        #endregion IsFile

        #region IsFileExtension

        [TestMethod]
        public void IsFileExtension_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsFileExtension
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        #endregion IsFileExtension

        #region IsSubdomain

        [TestMethod]
        public void IsSubdomain_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsSubdomain
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsSubdomain_Passing_ValidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsSubdomain
            (
                "subdomian",
                "123",
                "subdomian123",
                "subdomian.123",
                "subdomian-123",
                "subdomian_123"
            );

            Assert.IsFalse(HasErrors);
            Assert.IsTrue(Count == 0);
        }

        #endregion IsSubdomain

        #region IsDomain

        [TestMethod]
        public void IsDomain_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsDomain
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default,
                "33.domain.com",
                "host..com",
                "a-.com",
                "helloworld.c"
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 8);
        }

        [TestMethod]
        public void IsDomain_Passing_ValidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsDomain
            (
                "domain.com",
                "domain.it",
                "domain.net",
                "host.domain.com",
                "_host.domain.com",
                "1host-2._ldap.domain.com"
            );

            Assert.IsFalse(HasErrors);
            Assert.IsTrue(Count == 0);
        }

        #endregion IsDomain

        #region IsHostname

        [TestMethod]
        public void IsHostname_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsHostname
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsHostname_Passing_ValidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsHostname
            (
                "hostname.domain.com",
                "www.pointtoserver.com",
                "122.pointtoserver.it",
                "aeiou.pointtoserver.net"
            );

            Assert.IsFalse(HasErrors);
            Assert.IsTrue(Count == 0);
        }

        #endregion IsHostname

        #region IsDigit

        [TestMethod]
        public void IsDigit_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsDigit
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsDigit_Passing_ValidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsDigit
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(value: '3'),
                "asc4dfsfsd",
                "43242343"
            );

            Assert.IsFalse(HasErrors);
            Assert.IsTrue(Count == 0);
        }

        #endregion IsDigit

        #region IsUniqueChars

        [TestMethod]
        public void IsUniqueChars_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsUniqueChars
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsUniqueChars_Passing_ValidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsUniqueChars
            (
                "aeiou",
                "abcdefghi",
                "abc123",
                "1v2n3m4e5l6E"
            );

            Assert.IsFalse(HasErrors);
            Assert.IsTrue(Count == 0);
        }

        #endregion IsUniqueChars

        #region IsEspecialChars

        [TestMethod]
        public void IsEspecialChars_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsEspecialChars
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(value: '2'),
                FakeTypes.ReferenceTypes.ClassTypes.String_New(value: 'a'),
                FakeTypes.ReferenceTypes.ClassTypes.String_New(value: 'B'),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 6);
        }

        [TestMethod]
        public void IsEspecialChars_Passing_ValidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsEspecialChars
            (
                "aeiou!",
                "#abcdefghi",
                "abc123@",
                "+1v2n3m4e5l6E"
            );

            Assert.IsFalse(HasErrors);
            Assert.IsTrue(Count == 0);
        }

        #endregion IsEspecialChars

        #region IsLowercase

        [TestMethod]
        public void IsLowercase_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsLowercase
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(value: 'F'),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsLowercase_Passing_ValidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsLowercase
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(value: 'a'),
                "AEiOU",
                "ASDLFJDSOLFJLDSKFy",
                "12p345"
            );

            Assert.IsFalse(HasErrors);
            Assert.IsTrue(Count == 0);
        }

        #endregion IsLowercase

        #region IsUppercase

        [TestMethod]
        public void IsUppercase_Passing_InvalidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsUppercase
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(value: 'f'),
                FakeTypes.ReferenceTypes.ClassTypes.String_Empty,
                FakeTypes.ReferenceTypes.ClassTypes.String_Null,
                FakeTypes.ReferenceTypes.ClassTypes.String_Default
            );

            Assert.IsTrue(HasErrors);
            Assert.IsTrue(Count == 4);
        }

        [TestMethod]
        public void IsUppercase_Passing_ValidArguments_Returns_HasErrorsAndCount()
        {
            var (HasErrors, Count) = Util.Check.IsUppercase
            (
                FakeTypes.ReferenceTypes.ClassTypes.String_New(value: 'K'),
                "aeIou",
                "asasasasasasaR",
                "12P345"
            );

            Assert.IsFalse(HasErrors);
            Assert.IsTrue(Count == 0);
        }

        #endregion IsUppercase
    }
}
