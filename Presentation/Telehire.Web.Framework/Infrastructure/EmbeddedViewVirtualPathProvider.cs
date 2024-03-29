﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Telehire.Web.Framework.Infrastructure
{
    public class EmbeddedViewVirtualPathProvider : VirtualPathProvider
    {
        private readonly EmbeddedViewTable _embeddedViews;

        public EmbeddedViewVirtualPathProvider(EmbeddedViewTable embeddedViews)
        {
            if (embeddedViews == null)
                throw new ArgumentNullException("embeddedViews");

            this._embeddedViews = embeddedViews;
        }

        private bool IsEmbeddedView(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
                return false;
            // virtualPath = virtualPath.Replace("/Areas", "");
            string virtualPathAppRelative = VirtualPathUtility.ToAppRelative(virtualPath);
            if (!virtualPathAppRelative.StartsWith("~/Views/", StringComparison.InvariantCultureIgnoreCase))
                return false;
            var fullyQualifiedViewName = virtualPathAppRelative.Substring(virtualPathAppRelative.LastIndexOf("/") + 1, virtualPathAppRelative.Length - 1 - virtualPathAppRelative.LastIndexOf("/"));

            bool isEmbedded = _embeddedViews.ContainsEmbeddedView(fullyQualifiedViewName);
            return isEmbedded;
        }

        public override bool FileExists(string virtualPath)
        {
            //string newVirtualPath = "";
            //if (virtualPath.Contains("/Areas"))
            //{
            //    newVirtualPath = virtualPath.Replace("/Areas", "");
            //    return (IsEmbeddedView(newVirtualPath) ||
            //        Previous.FileExists(newVirtualPath));
            //}

            return (IsEmbeddedView(virtualPath) ||
                    Previous.FileExists(virtualPath));
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsEmbeddedView(virtualPath))
            {
                string virtualPathAppRelative = VirtualPathUtility.ToAppRelative(virtualPath);
                var fullyQualifiedViewName = virtualPathAppRelative.Substring(virtualPathAppRelative.LastIndexOf("/") + 1, virtualPathAppRelative.Length - 1 - virtualPathAppRelative.LastIndexOf("/"));

                var embeddedViewMetadata = _embeddedViews.FindEmbeddedView(fullyQualifiedViewName);
                return new EmbeddedResourceVirtualFile(embeddedViewMetadata, virtualPath);
            }

            return Previous.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(
            string virtualPath,
            IEnumerable virtualPathDependencies,
            DateTime utcStart)
        {
            return IsEmbeddedView(virtualPath)
                ? null : Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }
    }
}