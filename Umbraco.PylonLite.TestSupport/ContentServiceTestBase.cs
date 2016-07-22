﻿using Moq;
using System;
using System.Web.Routing;
using Umbraco.PylonLite.TestSupport.Mocks;

namespace Umbraco.PylonLite.TestSupport
{
    /// <summary>
    /// Base test file
    /// </summary>
    /// <typeparam name="TObjUt">The type of the object under test.</typeparam>
    /// <typeparam name="TContentRepo"></typeparam>
    public abstract class ContentServiceTestBase<TObjUt, TContentRepo> : ContentTestBase<TObjUt>
        where TContentRepo : class, IPublishedContentRepository
    {
        private MockHttpContext _mockContext;
        private Mock<TContentRepo> _mockRepo;
        private RouteData _routeData;

        #region | Setup and TearDown |

        /// <summary>
        /// Tears down.
        /// </summary>
        protected new void TearDown()
        {
            base.TearDown();

            RouteData = null;
            MockRepo = null;
            MockContext = null;
        }

        #endregion

        /// <summary>
        /// Gets or sets the mock HTTP context.
        /// </summary>
        [Obsolete()]
        protected MockHttpContext MockContext
        {
            get { return _mockContext ?? (_mockContext = new MockHttpContext()); }
            set { _mockContext = value; }
        }

        /// <summary>
        /// Gets or sets the mock repo.
        /// </summary>
        protected Mock<TContentRepo> MockRepo
        {
            get { return _mockRepo ?? (_mockRepo = new Mock<TContentRepo>()); }
            set { _mockRepo = value; }
        }

        /// <summary>
        /// The route data
        /// </summary>
        [Obsolete()]
        protected RouteData RouteData
        {
            get { return _routeData ?? (_routeData = new RouteData()); }
            set { _routeData = value; }
        }

        /// <summary>
        /// Sets up the object under test.
        /// </summary>
        [Obsolete()]
        protected virtual void SetUpControllerToTest()
        {   }
    }
}