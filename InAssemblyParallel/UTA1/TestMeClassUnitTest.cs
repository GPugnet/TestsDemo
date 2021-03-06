﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMe;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace InAssemblyParallelTests
{
    [TestClass]
    public class TestMeTests
    {
        // --------------------------------------------
        // regardless of parallelization mode, classinit/cleanup should be called only once.
        // Furthermore, classinit should have been called before classcleanup.
        // we use a static boolean to check that sequence of calls, and that it was called only once.
        static bool initialized = false;

        [ClassInitialize]
        public static void Initialize(TestContext tc)
        {
            if (initialized)
            {
                throw new Exception();
            }

            initialized = true;
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            // regardless of the parallelization mode, this should be called only once
            if (!initialized)
            {
                throw new Exception();
            }
        }
        // --------------------------------------------


        // --------------------------------------------
        // regardless of parallelization mode, testinit/cleanup should be called before/after every test in this class.
        // Furthermore, testinit should have been called before testcleanup.
        // we use a boolean to check that sequence of calls, and that it was called only once.
        bool testInitialized = false;
        [TestInitialize]
        public void TestInitialize()
        {
            if (testInitialized)
            {
                throw new Exception();
            }

            testInitialized = true;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (!testInitialized)
            {
                throw new Exception();
            }

            testInitialized = false; // get it ready fr the next call to testInit()
        }
        // --------------------------------------------


        [TestMethod]
        public void Test1()
        {
            var instance = new TestMeClass();
            instance.Delay(3);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test2()
        {
            var instance = new TestMeClass();
            instance.Delay(3);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test3()
        {
            var instance = new TestMeClass();
            instance.Delay(3);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test4()
        {
            var instance = new TestMeClass();
            instance.Delay(3);
            Assert.IsTrue(true);
        }
    }
}
