﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestFirst.Net.Extensions.Moq
{
    [TestFixture]
    public class MockMethodVoidTest
    {
        [Test]
        public void InvokeCountOkPasses()
        {
            var mock = FluentMock<IMyTestInterface>.With().WhereMethod(x => x.Foo()).IsCalled(3).Times();
            var instance = mock.Instance;

            instance.Foo();
            instance.Foo();
            instance.Foo();

            mock.OnScenarioEnd();    
        }

        [Test]
        public void InvokeCountTooLowFails()
        {
            var mock = FluentMock<IMyTestInterface>.With().WhereMethod(x => x.Foo()).IsCalled(3).Times();
            var instance = mock.Instance;

            instance.Foo();
            instance.Foo();

            Exception error = null;
            try
            {
                mock.OnScenarioEnd();
            }
            catch (Exception e)
            {
                error = e;
            }
            Assert.NotNull(error, "expected failure on too few method calls");
            Assert.IsTrue(error.Message.Contains("unexpectedly performed 2 times"));
            Assert.IsTrue(error.Message.Contains("expected 3 times"));

        }

        [Test]
        public void InvokeCountTooHighFails()
        {
            var mock = FluentMock<IMyTestInterface>.With().WhereMethod(x => x.Foo()).IsCalled(3).Times();
            var instance = mock.Instance;

            instance.Foo();
            instance.Foo();
            instance.Foo();

            Exception error = null;
            try
            {
                instance.Foo();
            }
            catch (Exception e)
            {
                error = e;
            }
            Assert.NotNull(error, "expected failure on too many method calls");
            Assert.IsTrue(error.Message.Contains("unexpectedly performed 4 times"));
            Assert.IsTrue(error.Message.Contains("expected 3 times"));


        }

        public interface IMyTestInterface
        {
            void Foo();
        }
    }
}
