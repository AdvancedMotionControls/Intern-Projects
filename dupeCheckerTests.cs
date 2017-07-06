using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DuplicationChecker;

namespace DuplicationChecker.Tests
{
    [TestClass()]
    public class ProgramTests
    {

        /**
         *  DIRECTORY PROCESSING TEST METHODS
         **/

        [TestMethod()]
        public void ProcessDirectoryTest_breadth()
        {
            string filepath = "N:/Web Exchange/Kurt M/DupeChecker/";
            string output = Program.ProcessDirectory(filepath);

            if (!output.Contains("nodupes") || !output.Contains("single_dupe")
                || !output.Contains("topguy") || !output.Contains("double"))
                Assert.Fail();
        }

        [TestMethod()]
        public void ProcessDirectoryTest_depth()
        {
            string filepath = "N:/Web Exchange/Kurt M/DupeChecker/";
            string output = Program.ProcessDirectory(filepath);

            if (!output.Contains("otherDir") || !output.Contains("sampleDir"))
                Assert.Fail();
        }



        /**
         *  FILE PROCESSING TEST METHODS
         **/

        [TestMethod()]
        public void processFileTest_empty()
        {
            string filepath = "N:/Web Exchange/Kurt M/DupeChecker/empty.adf";
            string output = Program.processFile(filepath);

            if (!output.Contains("NO DUPLICATES FOUND")) Assert.Fail();
        }

        [TestMethod()]
        public void processFileTest_1dupe()
        {
            string filepath = "N:/Web Exchange/Kurt M/DupeChecker/single_dupe.adf";
            string output = Program.processFile(filepath);

            if (!output.Contains("Max_Switching_Freq")) Assert.Fail();
        }

        [TestMethod()]
        public void processFileTest_2dupes()
        {
            string filepath = "N:/Web Exchange/Kurt M/DupeChecker/double.adf";
            string output = Program.processFile(filepath);

            if (!output.Contains("Max_Switching_Freq") || !output.Contains("Shunt")) Assert.Fail();
        }

        [TestMethod()]
        public void processFileTest_badext()
        {
            string filepath = "N:/Web Exchange/Kurt M/DupeChecker/input.txt";
            string output = Program.processFile(filepath);

            if (output != null) Assert.Fail();
        }



        /**
         *  ARGUMENT CHECKING TEST METHODS
         **/

        [TestMethod()]
        public void validArgsTest_empty()
        {
            string[] noArgs = { };
            if (Program.validArguments(noArgs) == true) Assert.Fail();
        }

        [TestMethod()]
        public void validArgsTest_single()
        {
            string[] validArgs = { "input" };
            if (Program.validArguments(validArgs) == false) Assert.Fail();

        }

        [TestMethod()]
        public void validArgsTest_many()
        {
            string[] manyArgs = { "one", "two", "three" };
            string[] twoArgs = { "one", "two" };
            if (Program.validArguments(twoArgs) == true) Assert.Fail();
            if (Program.validArguments(manyArgs) == true) Assert.Fail();
        }



        /**
         *  EXTENSION CHECKING TEST METHODS
         **/

        [TestMethod()]
        public void validExtTest_empty()
        {
            if (Program.validExtension("") == true) Assert.Fail();
        }

        [TestMethod()]
        public void validExtTest_badfile()
        {
            if (Program.validExtension("N:/Web Exchange/Kurt M/DupeChecker/input.txt") == true) Assert.Fail();
        }

        [TestMethod()]
        public void validExtTest_goodfile()
        {
            if (Program.validExtension("N:/Web Exchange/Kurt M/DupeChecker/double.adf") == false) Assert.Fail();
        }


    }
}
