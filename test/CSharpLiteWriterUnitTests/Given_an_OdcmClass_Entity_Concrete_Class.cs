﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vipr.Writer.CSharp.Lite;
using FluentAssertions;
using Microsoft.OData.Client;
using Microsoft.OData.ProxyExtensions.Lite;
using System.Linq;
using System.Reflection;
using Vipr.Core;
using Vipr.Core.CodeModel;
using Type = System.Type;
using Xunit;

namespace CSharpLiteWriterUnitTests
{
    public class Given_an_OdcmClass_Entity_Concrete : EntityTestBase
    {
        
        public Given_an_OdcmClass_Entity_Concrete()
        {
            base.Init();
        }

        [Fact]
        public void The_Concrete_class_is_Public()
        {
            ConcreteType.IsPublic
                .Should().BeTrue("Because it is used when adding new instances to the model.");
        }

        [Fact]
        public void The_concrete_proxy_class_derives_from_EntityBase()
        {
            ConcreteType.Should().BeDerivedFrom(
                typeof(EntityBase),
                "Because it gives access to entity operations like Update and Delete.");
        }

        [Fact]
        public void The_concrete_proxy_class_implements_a_Concrete_interface()
        {
            ConcreteType.Should().Implement(ConcreteInterface);
        }

        [Fact]
        public void The_concrete_proxy_class_does_not_implement_a_Fetcher_interface()
        {
            ConcreteType.Should().NotImplement(FetcherInterface);
        }

        [Fact]
        public void The_Concrete_class_is_attributed_with_MicrosoftOdataClientKey_Attribute()
        {
            ConcreteType.Should()
                .BeDecoratedWith<KeyAttribute>(
                    a => a.KeyNames.SequenceEqual(EntityKeyNames),
                    "Because this is used to tell ODataLib how to build a request Uri.");
        }

        private IEnumerable<string> EntityKeyNames
        {
            get { return Class.Key.Select(p => p.Name); }
        }
    }
}
