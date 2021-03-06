﻿// ---------------------------------------------------------
// UnitTestBase.cs
// 
// Created on:      09/01/2015 at 1:17 PM
// Last Modified:   09/01/2015 at 1:22 PM
// 
// Last Modified by: Matt Eland
// ---------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using MattEland.Common.Annotations;

using MattEland.Common.Providers;

using NUnit.Framework;

namespace MattEland.Common.Testing
{
    /// <summary>
    ///     A <see langword="base" /> class for all unit tests.
    /// </summary>
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    [SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
    public abstract class UnitTestBase : IHasContainer<IObjectContainer>
    {
        /// <summary>
        /// The container
        /// </summary>
        private IObjectContainer _container;

        /// <summary>
        ///     Gets or sets the random number generator.
        /// </summary>
        /// <remarks>
        ///     The random number generator is re-used between tests and set up at test fixture setup to
        ///     avoid the same number being generated repetitively.
        /// </remarks>
        /// <value>
        ///     The randomizer.
        /// </value>
        [NotNull]
        public Random Randomizer { get; set; }

        /// <summary>
        ///     Builds the container.
        /// </summary>
        /// <returns>
        ///     An IObjectContainer.
        /// </returns>
        [NotNull]
        protected virtual IObjectContainer BuildContainer()
        {
            return new CommonContainer();
        }

        /// <summary>
        ///     Gets or sets the <see cref="IObjectContainer" /> used by the test.
        /// </summary>
        /// <value>
        ///     The container.
        /// </value>
        [NotNull]
        public IObjectContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = BuildContainer();
                }

                // Ensure the name is accurate
                _container.Name = CurrentTestName;

                return _container;

            }
            set { _container = value; }
        }

        /// <summary>
        ///     Gets the current test's name.
        /// </summary>
        /// <value>
        ///     The name of the current test's name.
        /// </value>
        [NotNull]
        public string CurrentTestName
        {
            get
            {
                var currentContext = TestContext.CurrentContext;

                // Sanity check in an uncertain land
                currentContext.ShouldNotBeNull();
                currentContext.Test.ShouldNotBeNull();

                return $"Con_{currentContext.Test.Name.NonNull()}";
            }
        }

        /// <summary>
        ///     Sets up the test fixture.
        /// </summary>
        [TestFixtureSetUp]
        public virtual void SetUpFixture() { Randomizer = new Random(); }

        /// <summary>
        ///     Sets up the environment for each test.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            Some = new Some(Randomizer);
        }

        /// <summary>
        ///     Gets the <see cref="Some"/> instance used by the tests.
        /// </summary>
        /// <value>
        ///     The Some instance.
        /// </value>
        public Some Some
        {
            get; protected set;
        }

        /// <summary>
        ///     Declares that the executing test has not been completely implemented.
        /// </summary>
        /// <param name="isInconclusive">
        ///     if set to <c>true</c> this will result in an inconclusive result instead of a failure
        ///     result.
        /// </param>
        protected void TestIsNotImplemented(bool isInconclusive = false)
        {
            var message = string.Format("{0} has not been completely implemented.", CurrentTestName);
            if (isInconclusive)
            {
                Assert.Inconclusive(message);
            }
            else
            {
                Assert.Fail(message);
            }
        }
    }
}