using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GcmPracticum;
using Xunit;

namespace GcmPracticum.Tests
{
    public class DishRequestItemTests
    {

        public class StaticFactory
        {
            [Fact]
            public void CreatesBasicItem()
            {
                DishRequestItem.Create(1, 2).Item.Should().Be(1);
                DishRequestItem.Create(1, 2).Count.Should().Be(2);
                DishRequestItem.Create(1, 2).Offset.Should().Be(0);
            }


        }
    }
}
